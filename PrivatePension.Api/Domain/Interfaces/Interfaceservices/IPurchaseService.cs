using Domain.Entities;
using Domain.Notifications;

namespace Domain.Interfaces.Interfaceservices
{
    public interface IPurchaseService
    {
        Task<Notifies> AddPurchase(Purchase purchase);
        Task<Notifies> UpdatePurchaseIsApproved(int purchaseId);
        Task<Notifies> DeletePurchase(int purchaseId);
        Task<Purchase?> GetPurchaseById(int id);
        Task<List<Purchase>> GetByUser(int userId);
        Task<List<Purchase>> GetAllPurchases();
        Task<List<Purchase>> GetByProduct(int productId);
        Task<List<Purchase>> GetByDate(DateTime date);
        Task<List<Purchase>> GetByStatus(bool status);
        Notifies ValidatePurchase(Purchase purchase);
    }
}