using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService _contributionService;
        private readonly IMapper _mapper;

        public ContributionController(IContributionService contributionService, IMapper mapper)
        {
            _contributionService = contributionService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddContribution(ContributionDto contributionDto)
        {
            var contribution = _mapper.Map<Contribution>(contributionDto);
            var result = await _contributionService.AddContribution(contribution);
            if (!result.Status == true)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContribution(int id, ContributionDto contributionDto)
        {
            if (id != contributionDto.Id)
            {
                return BadRequest("Invalid contribution ID");
            }

            var contribution = _mapper.Map<Contribution>(contributionDto);
            var result = await _contributionService.UpdateContribution(contribution);
            if (!result.Status == true)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContribution(int id)
        {
            var result = await _contributionService.DeleteContribution(id);
            if (!result.Status == true)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContributionDto>>> GetAllContributions()
        {
            var contributions = await _contributionService.GetAllContributions();
            var contributionDtos = _mapper.Map<IEnumerable<ContributionDto>>(contributions);
            return Ok(contributionDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContributionDto>> GetContributionById(int id)
        {
            var contribution = await _contributionService.GetContributionById(id);
            if (contribution == null)
            {
                return NotFound("Contribution not found");
            }
            var contributionDto = _mapper.Map<ContributionDto>(contribution);

            return Ok(contributionDto);
        }

        [HttpGet("GetByContributionDate/{contributionDate}")]
        public async Task<ActionResult<IEnumerable<ContributionDto>>> GetByContributionDate(DateTime contributionDate)
        {
            var contributions = await _contributionService.GetByContributionDate(contributionDate);
            if (contributions == null)
                return NotFound("Contribution not found");

            var contributionDtos = _mapper.Map<IEnumerable<ContributionDto>>(contributions);
            return Ok(contributionDtos);
        }

        [HttpGet("GetByContributionDateByUser/{contributionDate}/{userId}")]
        public async Task<ActionResult<IEnumerable<ContributionDto>>> GetByContributionDateByUser(DateTime contributionDate, int userId)
        {
            var contributions = await _contributionService.GetByContributionDateByUser(contributionDate, userId);
            if (contributions == null)
                return NotFound("Contribution not found");

            var contributionDtos = _mapper.Map<IEnumerable<ContributionDto>>(contributions);
            return Ok(contributionDtos);
        }

        [HttpGet("GetByPurchaseId/{purchaseId}")]
        public async Task<ActionResult<IEnumerable<ContributionDto>>> GetByPurchaseId(int purchaseId)
        {
            var contribution = await _contributionService.GetByPurchaseId(purchaseId);
            if (contribution == null)
                return NotFound("Contribution not found");

            var contributionDto = _mapper.Map<IEnumerable<ContributionDto>>(contribution);
            return Ok(contributionDto);
        }

        [HttpGet("GetByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<ContributionDto>>> GetByUser(int userId)
        {
            var contributions = await _contributionService.GetByUser(userId);
            if (contributions == null)
                return NotFound("Contribution not found");

            var contributionDtos = _mapper.Map<IEnumerable<ContributionDto>>(contributions);
            return Ok(contributionDtos);
        }
    }
}