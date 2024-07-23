using TestProject.Models;
using TestProject.Repository;

namespace TestProject.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<bool> UpsertCandidateAsync(Candidate candidate)
        {
            var existingCandidate = await _candidateRepository.GetByEmailAsync(candidate.Email);

            if (existingCandidate == null)
            {
                await _candidateRepository.AddAsync(candidate);
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
                return false;
            }
        }
    }
}
