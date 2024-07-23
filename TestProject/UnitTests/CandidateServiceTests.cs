using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using TestProject.Models;
using TestProject.Repository;
using TestProject.Services;

namespace UnitTests
{
    [TestFixture]
    public class CandidateServiceTests
    {
        private Mock<ICandidateRepository> _mockCandidateRepository;
        private IMemoryCache _mockMemoryCache;
        private CandidateService _service;

        [SetUp]
        public void Setup()
        {
            _mockCandidateRepository = new Mock<ICandidateRepository>();
            _mockMemoryCache = CreateMemoryCache();
            _service = new CandidateService(_mockCandidateRepository.Object, _mockMemoryCache);
        }

        private static IMemoryCache CreateMemoryCache()
        {
            var cache = new MemoryCache(new MemoryCacheOptions());
            var mockMemoryCache = new Mock<IMemoryCache>();

            mockMemoryCache.Setup(m => m.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny))
                .Returns((object key, out object value) =>
                {
                    return cache.TryGetValue(key, out value);
                });

            mockMemoryCache.Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns<object>(key => cache.CreateEntry(key));

            return mockMemoryCache.Object;
        }

        [Test]
        public async Task UpsertCandidateAsync_ShouldAddNewCandidate_WhenCandidateDoesNotExist()
        {
            // Arrange
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Looking forward to the opportunity."
            };

            _mockCandidateRepository.Setup(repo => repo.GetByEmailAsync(candidate.Email))
                .ReturnsAsync((Candidate)null);

            // Act
            var result = await _service.UpsertCandidateAsync(candidate);

            // Assert
            Assert.That(result, Is.True);
            _mockCandidateRepository.Verify(repo => repo.AddAsync(candidate), Times.Once);
        }

        [Test]
        public async Task UpsertCandidateAsync_ShouldUpdateExistingCandidate_WhenCandidateExists()
        {
            // Arrange
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Comment = "Looking forward to the opportunity."
            };

            _mockCandidateRepository.Setup(repo => repo.GetByEmailAsync(candidate.Email))
                .ReturnsAsync(candidate);

            // Act
            var result = await _service.UpsertCandidateAsync(candidate);

            // Assert
            Assert.That(result, Is.False);
            _mockCandidateRepository.Verify(repo => repo.UpdateAsync(candidate), Times.Once);
        }

        [Test]
        public async Task GetCandidateByEmailAsync_ShouldReturnCandidate_WhenCandidateExists()
        {
            // Arrange
            var email = "john.doe@example.com";
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = email,
                Comment = "Looking forward to the opportunity."
            };

            _mockCandidateRepository.Setup(repo => repo.GetByEmailAsync(email))
                .ReturnsAsync(candidate);

            // Act
            var result = await _service.GetCandidateByEmailAsync(email);

            // Assert
            Assert.That(result, Is.EqualTo(candidate));
        }

        [Test]
        public async Task GetCandidateByEmailAsync_ShouldReturnNull_WhenCandidateDoesNotExist()
        {
            // Arrange
            var email = "nonexistent@example.com";
            _mockCandidateRepository.Setup(repo => repo.GetByEmailAsync(email))
                .ReturnsAsync((Candidate)null);

            // Act
            var result = await _service.GetCandidateByEmailAsync(email);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
