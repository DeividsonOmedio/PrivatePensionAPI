using Domain.Entities;
using Domain.Interfaces.InterfacesRepositories;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class complexQueriesProductsRepository : IComplexQueriesProductRepository
    {
        private readonly DbContextOptions<ContextBase> _context;

        public complexQueriesProductsRepository()
        {
            _context = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Product>> GetProductsPurchasedByUser(int userId)
        {
            using (var banco = new ContextBase(_context))
            {
                var purchasedProductIds = await banco.Purchases
                    .Where(purchase => purchase.ClientId == userId)
                    .Select(purchase => purchase.ProductId)
                    .ToListAsync();

                var productsPurchased = await banco.Products
                    .Where(product => purchasedProductIds.Contains(product.Id))
                    .ToListAsync();

                return productsPurchased;
            }
        }

        public async Task<List<Product>> GetProductsNotPurchasedByUser(int userId)
        {
            using (var banco = new ContextBase(_context))
            {
                var purchasedProductIds = await banco.Purchases
                    .Where(purchase => purchase.ClientId == userId)
                    .Select(purchase => purchase.ProductId)
                    .ToListAsync();

                var productsNotPurchased = await banco.Products
                    .Where(product => !purchasedProductIds.Contains(product.Id))
                    .ToListAsync();

                return productsNotPurchased;
            }
        }
    }
}