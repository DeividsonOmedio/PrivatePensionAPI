using Domain.Entities;
using Domain.Interfaces.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class PurchaseRepository(DbContext context) : GenericRepository<Purchase>(context), IPurchaseRepository
    {
        public async Task<List<Purchase>> GetByDate(DateTime date)
        {
            try
            {
                return await _dbSet.Where(purchase => purchase.PurchaseDate == date).ToListAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<Purchase>> GetByProduct(int productId)
        {
            try
            {
                return await _dbSet.Where(purchase => purchase.ProductId == productId).ToListAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<Purchase>> GetByStatus(bool status)
        {
            try
            {
                return await _dbSet.Where(purchase => purchase.IsApproved == status).ToListAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<Purchase>> GetByUser(int user)
        {
            try
            {
                return await _dbSet.Where(purchase => purchase.ClientId == user).ToListAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }
    }
}