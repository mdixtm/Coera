using Microsoft.EntityFrameworkCore;
using TestProject.Models;

namespace TestProject.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Candidate> GetByEmailAsync(string email)
        {
            return await _context.Candidates
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();
        }
    }
}
