using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Domain.Notifications;

namespace Services
{
    public class PurchaseService(IPurchaseService purchaseService) : IPurchaseService
    {
        private readonly IPurchaseService _purchaseService = purchaseService;

        public async Task<Notifies> AddPurchase(Purchase purchase)
        {
            var validatePurchase = ValidatePurchase(purchase);
            if (validatePurchase.Status == false)
                return validatePurchase;

            return await _purchaseService.AddPurchase(purchase);
        }

        public async Task<Notifies> DeletePurchase(int purchaseId)
        {
            var entitie = await _purchaseService.GetPurchaseById(purchaseId);
            if (entitie == null)
                return Notifies.Error("Purchase not found");

            return await _purchaseService.DeletePurchase(purchaseId);
        }

        public async Task<List<Purchase>> GetAllPurchases()
        {
            return await _purchaseService.GetAllPurchases();
        }

        public async Task<List<Purchase>> GetByDate(DateTime date)
        {
            return await _purchaseService.GetByDate(date);
        }

        public async Task<List<Purchase>> GetByProduct(int productId)
        {
            var validateProductId = Notifies.ValidatePropertyInt(productId, "ProductId");
            if (validateProductId.Status == false)
                return new List<Purchase>();

            return await _purchaseService.GetByProduct(productId);
        }

        public async Task<List<Purchase>> GetByStatus(bool status)
        {
            return await _purchaseService.GetByStatus(status);
        }

        public async Task<Purchase?> GetPurchaseById(int id)
        {
            var validateId = Notifies.ValidatePropertyInt(id, "PurchaseId");
            if (validateId.Status == false)
                return null;

            return await _purchaseService.GetPurchaseById(id);
        }

        public async Task<Notifies> UpdatePurchase(Purchase purchase)
        {
            var validatePurchase = ValidatePurchase(purchase);
            if (validatePurchase.Status == false)
                return validatePurchase;

            return await _purchaseService.UpdatePurchase(purchase);
        }

        public Notifies ValidatePurchase(Purchase purchase)
        {
            var validateClientId = Notifies.ValidatePropertyInt(purchase.ClientId, "Client Id");
            if (validateClientId.Status == false)
                return validateClientId;

            var validateProductId = Notifies.ValidatePropertyInt(purchase.ProductId, "Product Id");
            if (validateProductId.Status == false)
                return validateProductId;

            return Notifies.Success();
        }
    }
}