using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Domain.Notifications;

namespace Services
{
    public class ProductService(IProductService productService) : IProductService
    {
        private readonly IProductService _productService = productService;

        public async Task<Notifies> AddProduct(Product product)
        {
            var validateProduct = ValidateProduct(product);
            if (validateProduct.Status == false)
                return validateProduct;

            return await _productService.AddProduct(product);
        }

        public async Task<Notifies> DeleteProduct(int productId)
        {
            var entitie = await _productService.GetProductById(productId);

            if (entitie == null)
                return Notifies.Error("Product not found");

            return await _productService.DeleteProduct(productId);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productService.GetAllProducts();
        }

        public async Task<List<Product>> GetByAvailable(bool available)
        {
            return await _productService.GetByAvailable(available);
        }

        public async Task<Product?> GetByName(string name)
        {
            var validateName = Notifies.ValidatePropertyString(name, "Name");
            if (validateName.Status == false)
                return null;

            return await _productService.GetByName(name);
        }

        public async Task<Product?> GetProductById(int id)
        {
            var validateId = Notifies.ValidatePropertyInt(id, "Id");
            if (validateId.Status == false)
                return null;

            return await _productService.GetProductById(id);
        }

        public async Task<Notifies> UpdateProduct(Product product)
        {
            var validateProduct = ValidateProduct(product);
            if (validateProduct.Status == false)
                return validateProduct;

            return await _productService.UpdateProduct(product);
        }

        public Notifies ValidateProduct(Product product)
        {
            var validateName = Notifies.ValidatePropertyString(product.Name, "Name");
            if (validateName.Status == false)
                return validateName;

            var validateDescription = Notifies.ValidatePropertyString(product.Description, "Description");
            if (validateDescription.Status == false)
                return validateDescription;

            var validatePrice = Notifies.ValidatePropertyDecimal(product.Price, "Value");
            if (validatePrice.Status == false)
                return validatePrice;

            return Notifies.Success();
        }
    }
}