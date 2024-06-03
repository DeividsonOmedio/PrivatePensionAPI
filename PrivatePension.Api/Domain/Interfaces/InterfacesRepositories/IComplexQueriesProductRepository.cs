using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfacesRepositories
{
    public interface IComplexQueriesProductRepository
    {
        Task<List<Product>> GetProductsPurchasedByUser(int userId);
        Task<List<Product>> GetProductsNotPurchasedByUser(int userId);
    }
}
