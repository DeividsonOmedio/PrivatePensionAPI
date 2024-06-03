using Domain.Entities;

namespace Domain.Interfaces.InterfacesRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetByName(string name);
        Task<List<Product>> GetByAvailable(bool available);
    }
}
