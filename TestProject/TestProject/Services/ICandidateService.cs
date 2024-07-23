using TestProject.Models;

namespace TestProject.Services
{
    public interface ICandidateService
    {
        public Task<bool> UpsertCandidateAsync(Candidate candidate);
    }
}
