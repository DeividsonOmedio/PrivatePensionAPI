using Domain.Entities;
using Domain.Interfaces.InterfacesRepositories;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class PurchaseRepository(ContextBase context) : GenericRepository<Purchase>(context), IPurchaseRepository
    {
        public async Task<List<Purchase>> GetByDate(DateTime date)
        {
            return await _dbSet.Where(purchase => purchase.PurchaseDate == date).ToListAsync();
        }

        public async Task<List<Purchase>> GetByProduct(int productId)
        {
            return await _dbSet.Where(purchase => purchase.ProductId == productId).ToListAsync();
        }

        public async Task<List<Purchase>> GetByStatus(bool status)
        {
            return await _dbSet.Where(purchase => purchase.IsApproved == status).ToListAsync();
        }

        public async Task<List<Purchase>> GetByUser(int user)
        {
            return await _dbSet.Where(purchase => purchase.ClientId == user).ToListAsync();
        }

        public async Task<Purchase?> GetByProductAndUser(int productId, int userId)
        {
            return await _dbSet.FirstOrDefaultAsync(purchase => purchase.ProductId == productId && purchase.ClientId == userId);
        }

    }
}