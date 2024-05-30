using Domain.Entities;
using Domain.Interfaces.InterfacesRepositories;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository(ContextBase context) : GenericRepository<Product>(context), IProductRepository
    {
        public async Task<List<Product>> GetByAvailable(bool available)
        {
            return await _dbSet.Where(product => product.Available == available).ToListAsync();
        }

        public async Task<Product?> GetByName(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(product => product.Name == name);
        }
    }
}