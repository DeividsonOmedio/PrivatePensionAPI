using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfacesRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetByName(string name);
        Task<List<Product>> GetByAvailable(bool available);
    }
}
