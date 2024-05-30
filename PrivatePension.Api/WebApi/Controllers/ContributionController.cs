using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService _contributionService;

        public ContributionController(IContributionService contributionService)
        {
            _contributionService = contributionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddContribution(Contribution contribution)
        {
            var result = await _contributionService.AddContribution(contribution);
            if (!result.Status == true)
            {
                return BadRequest(result.Message);
            }

            return Ok("Contribution added successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContribution(int id)
        {
            var result = await _contributionService.DeleteContribution(id);
            if (!result.Status == true)
            {
                return NotFound(result.Message);
            }

            return Ok("Contribution deleted successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContribution(int id, Contribution contribution)
        {
            if (id != contribution.Id)
            {
                return BadRequest("Invalid contribution ID");
            }

            var result = await _contributionService.UpdateContribution(contribution);
            if (!result.Status == true)
            {
                return BadRequest(result.Message);
            }

            return Ok("Contribution updated successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContributions()
        {
            var contributions = await _contributionService.GetAllContributions();
            return Ok(contributions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContributionById(int id)
        {
            var contribution = await _contributionService.GetContributionById(id);
            if (contribution == null)
            {
                return NotFound("Contribution not found");
            }

            return Ok(contribution);
        }

        [HttpGet("GetByContributionDate/{contributionDate}")]
        public async Task<IActionResult> GetByContributionDate(DateTime contributionDate)
        {
            var contributions = await _contributionService.GetByContributionDate(contributionDate);
            return Ok(contributions);
        }

        [HttpGet("GetByContributionDateByUser/{contributionDate}/{userId}")]
        public async Task<IActionResult> GetByContributionDateByUser(DateTime contributionDate, int userId)
        {
            var contributions = await _contributionService.GetByContributionDateByUser(contributionDate, userId);
            return Ok(contributions);
        }

        [HttpGet("GetByPurchaseId/{purchaseId}")]
        public async Task<IActionResult> GetByPurchaseId(int purchaseId)
        {
            var contribution = await _contributionService.GetByPurchaseId(purchaseId);
            if (contribution == null)
            {
                return NotFound("Contribution not found");
            }

            return Ok(contribution);
        }

        [HttpGet("GetByUser/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var contributions = await _contributionService.GetByUser(userId);
            return Ok(contributions);
        }
    }
}