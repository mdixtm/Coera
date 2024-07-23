using Microsoft.AspNetCore.Mvc;
using TestProject.Models;
using TestProject.Repository;
using TestProject.Services;

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert([FromBody] Candidate candidate)
        {
            if (candidate == null)
            {
                return BadRequest("Candidate data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _candidateService.UpsertCandidateAsync(candidate);

            if (result)
            {
                return CreatedAtAction(nameof(Upsert), new { id = candidate.Email }, candidate);
            }
            else
            {
                return Ok(candidate);
            }
        }
    }
}
