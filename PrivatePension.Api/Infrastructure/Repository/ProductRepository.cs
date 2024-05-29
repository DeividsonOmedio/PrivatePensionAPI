using Domain.Entities;
using Domain.Interfaces.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository(DbContext context) : GenericRepository<Product>(context), IProductRepository
    {
        public async Task<List<Product>> GetByAvailable(bool available)
        {
            try
            {
                return await _dbSet.Where(product => product.Available == available).ToListAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<Product?> GetByName(string name)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(product => product.Name == name);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}