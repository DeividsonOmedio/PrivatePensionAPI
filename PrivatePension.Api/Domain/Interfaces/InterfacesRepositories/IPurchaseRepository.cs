using Domain.Entities;

namespace Domain.Interfaces.InterfacesRepositories
{
    public interface IPurchaseRepository : IGenericRepository<Purchase>
    {
        Task<List<Purchase>> GetByUser(int user);
        Task<List<Purchase>> GetByProduct(int productId);
        Task<List<Purchase>> GetByDate(DateTime date);
        Task<List<Purchase>> GetByStatus(bool status);
    }
}