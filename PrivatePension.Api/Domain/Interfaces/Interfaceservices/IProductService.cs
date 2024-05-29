using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Notifications;

namespace Domain.Interfaces.Interfaceservices
{
    public interface IProductService
    {
        Task<Notifies> AddProduct(Product product);
        Task<Notifies> UpdateProduct(Product product);
        Task<Notifies> DeleteProduct(int productId);
        Task<Product?> GetProductById(int id);
        Task<Product?> GetByName(string name);
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetByAvailable(bool available);
        Notifies ValidateProduct(Product product);
    }
}