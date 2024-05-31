using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IMapper _mapper;

        public PurchaseController(IPurchaseService purchaseService, IMapper mapper)
        {
            _purchaseService = purchaseService;
            _mapper = mapper;
        }

        [Authorize(Roles = "client")]
        [HttpPost]
        public async Task<ActionResult<Purchase>> CreatePurchase(PurchaseDTO purchaseDto)
        {
            purchaseDto.Id = null;
            var purchase = _mapper.Map<Purchase>(purchaseDto);
            var result = await _purchaseService.AddPurchase(purchase);
            if (!result.Status == true)
                return BadRequest(result);

            purchaseDto.IsApproved = false;
            return CreatedAtAction(nameof(GetPurchaseById), new { id = purchaseDto.Id}, purchaseDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("/approve/{id}")]
        public async Task<IActionResult> ApprovePurchase(int id)
        {
            var result = await _purchaseService.UpdatePurchaseIsApproved(id);
            if (!result.Status == true)
                return NotFound(result);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var result = await _purchaseService.DeletePurchase(id);
            if (!result.Status == true)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDTO>>> GetAllPurchases()
        {
            var purchases = await _purchaseService.GetAllPurchases();
            var purchaseDtos = _mapper.Map<List<PurchaseDTO>>(purchases);
            return Ok(purchaseDtos);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDTO>> GetPurchaseById(int id)
        {
            var purchase = await _purchaseService.GetPurchaseById(id);
            if (purchase == null)
                return NotFound();

            var purchaseDto = _mapper.Map<PurchaseDTO>(purchase);
            return Ok(purchaseDto);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetByApproved/isApproved")]
        public async Task<ActionResult<IEnumerable<PurchaseDTO>>> GetByApproved()
        {
            var purchases = await _purchaseService.GetByStatus(true);
            if (purchases == null)
                return NotFound();

            var purchaseDtos = _mapper.Map<List<PurchaseDTO>>(purchases);
            return Ok(purchaseDtos);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetByApproved/notApproved")]
        public async Task<ActionResult<IEnumerable<PurchaseDTO>>> GetNotApproved()
        {
            var purchases = await _purchaseService.GetByStatus(false);
            if (purchases == null)
                return NotFound();

            var purchaseDtos = _mapper.Map<List<PurchaseDTO>>(purchases);
            return Ok(purchaseDtos);
        }

        [Authorize]
        [HttpGet("GetByClient/{clientId}")]
        public async Task<ActionResult<IEnumerable<PurchaseDTO>>> GetByClient(int clientId)
        {
            var purchases = await _purchaseService.GetByUser(clientId);
            if (purchases == null)
                return NotFound();

            var purchaseDtos = _mapper.Map<List<PurchaseDTO>>(purchases);
            return Ok(purchaseDtos);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetByDate/{date}")]
        public async Task<ActionResult<IEnumerable<PurchaseDTO>>> GetByDate(DateTime date)
        {
            var purchases = await _purchaseService.GetByDate(date);
            if (purchases == null)
                return NotFound();

            var purchaseDtos = _mapper.Map<List<PurchaseDTO>>(purchases);
            return Ok(purchaseDtos);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetByProduct/{productId}")]
        public async Task<ActionResult<IEnumerable<PurchaseDTO>>> GetByProduct(int productId)
        {
            var purchases = await _purchaseService.GetByProduct(productId);
            if (purchases == null)
                return NotFound();

            var purchaseDtos = _mapper.Map<List<PurchaseDTO>>(purchases);
            return Ok(purchaseDtos);
        }
    }
}