using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Domain.Interfaces.InterfacesRepositories;
using Domain.Notifications;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserService _userService;
        private readonly IPurchaseService _purchaseService;

        public ProductService(IProductRepository productRepository, IUserService userService, IPurchaseService purchaseService)
        {
            _productRepository = productRepository;
            _userService = userService;
            _purchaseService = purchaseService;
        }

        public async Task<Notifies> AddProduct(Product product)
        {
            var validateProduct = ValidateProduct(product);
            if (validateProduct.Status == false)
                return validateProduct;

            return await _productRepository.Add(product);
        }

        public async Task<Notifies> UpdateProduct(Product product)
        {
            var validateProduct = ValidateProduct(product);
            if (validateProduct.Status == false)
                return validateProduct;

            return await _productRepository.Update(product);
        }

        public async Task<Notifies> DeleteProduct(int productId)
        {
            var entitie = await _productRepository.GetById(productId);

            if (entitie == null)
                return Notifies.Error("Product not found");

            return await _productRepository.Delete(entitie);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAll();
        }

        public async Task<List<Product>> GetByAvailable(bool available)
        {
            return await _productRepository.GetByAvailable(available);
        }

        public async Task<Product?> GetByName(string name)
        {
            var validateName = Notifies.ValidatePropertyString(name, "Name");
            if (validateName.Status == false)
                return null;

            return await _productRepository.GetByName(name);
        }

        public async Task<Product?> GetProductById(int id)
        {
            var validateId = Notifies.ValidatePropertyInt(id, "Id");
            if (validateId.Status == false)
                return null;

            return await _productRepository.GetById(id);
        }

        public async Task<List<Product>> GetProductsByUserNotPurchase(int userId)
        {
           var user = await _userService.GetUserById(userId);
           if (user == null)
                return null;

            var purchases = await _purchaseService.GetByUser(userId);
            var purchasedProductIds = purchases.Select(p => p.ProductId).ToList();

            var allProducts = await GetAllProducts();
            var productsNotPurchased = allProducts.Where(p => !purchasedProductIds.Contains(p.Id)).ToList();

            return productsNotPurchased;
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