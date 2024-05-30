using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
    
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<Product>> GetProductByName(string name)
        {
            var product = await _productService.GetByName(name);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("GetByAvailable/true")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAvailableProducts()
        {
            var products = await _productService.GetByAvailable(true);
            return Ok(products);
        }

        [HttpGet("GetByAvailable/false")]
        public async Task<ActionResult<IEnumerable<Product>>> GetUnavailableProducts()
        {
            var products = await _productService.GetByAvailable(false);
            return Ok(products);
        }


        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            var result = await _productService.AddProduct(product);
            if (!result.Status == true)
            {
                return BadRequest(result);
            }

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var result = await _productService.UpdateProduct(product);
            if (!result.Status == true)
            {
                return BadRequest(result);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (!result.Status == true)
            {
                return NotFound(result);
            }

            return NoContent();
        }
    }
}