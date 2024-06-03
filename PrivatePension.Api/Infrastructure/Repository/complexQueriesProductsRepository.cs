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
        private readonly ContextBase _context;

        public complexQueriesProductsRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsPurchasedByUser(int userId)
        {
            var purchasedProductIds = await _context.Purchases
                .Where(purchase => purchase.ClientId == userId)
                .Select(purchase => purchase.ProductId)
                .ToListAsync();

            var productsPurchased = await _context.Products
                .Where(product => purchasedProductIds.Contains(product.Id))
                .ToListAsync();

            return productsPurchased;
        }

        public async Task<List<Product>> GetProductsNotPurchasedByUser(int userId)
        {
            var purchasedProductIds = await _context.Purchases
                .Where(purchase => purchase.ClientId == userId)
                .Select(purchase => purchase.ProductId)
                .ToListAsync();

            var productsNotPurchased = await _context.Products
                .Where(product => !purchasedProductIds.Contains(product.Id) && product.Available)
                .ToListAsync();

            return productsNotPurchased;
        }
    }
}