using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestProject.Controllers;
using TestProject.Models;
using TestProject.Services;

namespace UnitTests
{
    [TestFixture]
    public class CandidatesControllerTests
    {
        private Mock<ICandidateService> _mockCandidateService;
        private CandidatesController _controller;

        [SetUp]
        public void Setup()
        {
            _mockCandidateService = new Mock<ICandidateService>();
            _controller = new CandidatesController(_mockCandidateService.Object);
        }

        [Test]
        public async Task Upsert_ShouldReturnBadRequest_WhenCandidateIsNull()
        {
            // Act
            var result = await _controller.Upsert(null);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.EqualTo("Candidate data is null."));
        }

        [Test]
        public async Task Upsert_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var candidate = new Candidate { Email = "invalid-email" };
            _controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var result = await _controller.Upsert(candidate);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.Value, Is.InstanceOf<SerializableError>());
        }
    }
}
