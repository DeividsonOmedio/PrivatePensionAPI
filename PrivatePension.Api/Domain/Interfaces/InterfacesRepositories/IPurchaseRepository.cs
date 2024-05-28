using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfacesRepositories
{
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        Task<Purchase> GetByUser(User user);
        Task<Purchase> GetByProduct(Product product);
        Task<Purchase> GetByDate(DateTime date);
        Task<Purchase> GetByStatus(bool status);
    }
}
