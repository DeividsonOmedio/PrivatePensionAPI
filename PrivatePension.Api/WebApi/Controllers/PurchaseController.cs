using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetAllPurchases()
        {
            var purchases = await _purchaseService.GetAllPurchases();
            return Ok(purchases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetPurchaseById(int id)
        {
            var purchase = await _purchaseService.GetPurchaseById(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        [HttpGet("GetByApproved/isApproved")]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetByApproved()
        {
            var purchases = await _purchaseService.GetByStatus(true);
            return Ok(purchases);
        }

        [HttpGet("GetByApproved/notApproved")]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetNotApproved()
        {
            var purchases = await _purchaseService.GetByStatus(false);
            return Ok(purchases);
        }

        [HttpGet("GetByClient/{clientId}")]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetByClient(int clientId)
        {
            var purchases = await _purchaseService.GetByUser(clientId);
            return Ok(purchases);
        }

        [HttpGet("GetByDate/{date}")]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetByDate(DateTime date)
        {
            var purchases = await _purchaseService.GetByDate(date);
            return Ok(purchases);
        }

        [HttpGet("GetByProduct/{productId}")]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetByProduct(int productId)
        {
            var purchases = await _purchaseService.GetByProduct(productId);
            return Ok(purchases);
        }

        [HttpPost]
        public async Task<ActionResult<Purchase>> CreatePurchase(Purchase purchase)
        {
            var result = await _purchaseService.AddPurchase(purchase);
            if (!result.Status == true)
            {
                return BadRequest(result);
            }

            return CreatedAtAction(nameof(GetPurchaseById), new { id = purchase.Id }, purchase);
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApprovePurchase(int id)
        {
            var result = await _purchaseService.UpdatePurchaseIsApproved(id);
            if (!result.Status == true)
            {
                return NotFound(result);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var result = await _purchaseService.DeletePurchase(id);
            if (!result.Status == true)
            {
                return NotFound(result);
            }

            return NoContent();
        }
    }
}