using Microsoft.Extensions.Caching.Memory;
using TestProject.Models;
using TestProject.Repository;

namespace TestProject.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMemoryCache _cache;

        public CandidateService(ICandidateRepository candidateRepository, IMemoryCache cache)
        {
            _candidateRepository = candidateRepository;
            _cache = cache;
        }

        public async Task<bool> UpsertCandidateAsync(Candidate candidate)
        {
            var cacheKey = $"Candidate_{candidate.Email}";
            var existingCandidate = await _candidateRepository.GetByEmailAsync(candidate.Email);

            if (existingCandidate == null)
            {
                await _candidateRepository.AddAsync(candidate);
                _cache.Set(cacheKey, candidate, TimeSpan.FromMinutes(30)); // Cache the new candidate
                return true;
            }
            else
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.Comment = candidate.Comment;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.LinkedinUrl = candidate.LinkedinUrl;
                existingCandidate.GitHubUrl = candidate.GitHubUrl;
                existingCandidate.CallTimeStart = candidate.CallTimeStart;
                existingCandidate.CallTimeEnd = candidate.CallTimeEnd;

                await _candidateRepository.UpdateAsync(existingCandidate);
                _cache.Set(cacheKey, existingCandidate, TimeSpan.FromMinutes(30)); // Update cache
                return false;
            }
        }

        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            var cacheKey = $"Candidate_{email}";

            if (!_cache.TryGetValue(cacheKey, out Candidate candidate))
            {
                candidate = await _candidateRepository.GetByEmailAsync(email);

                if (candidate != null)
                {
                    _cache.Set(cacheKey, candidate, TimeSpan.FromMinutes(30));
                }
            }

            return candidate;
        }
    }
}
