using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Notifications;

namespace Domain.Interfaces.Interfaceservices
{
    public interface IPurchaseService
    {
        Task<Notifies> AddPurchase(Purchase purchase);
        Task<Notifies> UpdatePurchase(Purchase purchase);
        Task<Notifies> DeletePurchase(int purchaseId);
        Task<Purchase?> GetPurchaseById(int id);
        Task<List<Purchase>> GetAllPurchases();
        Task<List<Purchase>> GetByProduct(int productId);
        Task<List<Purchase>> GetByDate(DateTime date);
        Task<List<Purchase>> GetByStatus(bool status);
        Task<Notifies> ValidatePurchase(Purchase purchase);
    }
}