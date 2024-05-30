using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
    
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> AddProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var result = await _productService.AddProduct(product);
            if (!result.Status == true)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            if (id != productDto.Id)
                return BadRequest(Notifies.Error("Id passado na requisição é diferente do Id do produto"));

            var product = _mapper.Map<Product>(productDto);
            var result = await _productService.UpdateProduct(product);
            if (!result.Status == true)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (!result.Status == true)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            var productsDto = _mapper.Map<IEnumerable<Product>>(products);
            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();

            var productDto = _mapper.Map<Product>(product);
            return Ok(productDto);
        }

        [HttpGet("GetProductsPurchasedByUser")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsPurchasedByUser(int userId)
        {
            var products = await _productService.GetProductsPurchasedByUser(userId);
            if (products == null)
                return NotFound();
            var productsDto = _mapper.Map<IEnumerable<Product>>(products);
            return Ok(productsDto);
        }

        [HttpGet("GetProductsNotPurchasedByUser")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsNotPurchasedByUser(int userId)
        {
            var products = await _productService.GetProductsNotPurchasedByUser(userId);
            if (products == null)
                return NotFound();
            var productsDto = _mapper.Map<IEnumerable<Product>>(products);
            return Ok(productsDto);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<ProductDto>> GetProductByName(string name)
        {
            var product = await _productService.GetByName(name);
            if (product == null)
                return NotFound();

            var productDto = _mapper.Map<Product>(product);
            return Ok(productDto);
        }

        [HttpGet("GetByAvailable/true")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAvailableProducts()
        {
            var products = await _productService.GetByAvailable(true);

            var productsDto = _mapper.Map<IEnumerable<Product>>(products);
            return Ok(productsDto);
        }

        [HttpGet("GetByAvailable/false")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetUnavailableProducts()
        {
            var products = await _productService.GetByAvailable(false);
            var productsDto = _mapper.Map<IEnumerable<Product>>(products);
            return Ok(productsDto);
        }
    }
}