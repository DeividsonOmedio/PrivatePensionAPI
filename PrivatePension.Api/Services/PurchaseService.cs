using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Domain.Interfaces.InterfacesRepositories;
using Domain.Notifications;

namespace Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public PurchaseService(IPurchaseRepository purchaseRepository, IProductService productService, IUserService userService)
        {
            _purchaseRepository = purchaseRepository;
            _productService = productService;
            _userService = userService;
        }

        public async Task<Notifies> AddPurchase(Purchase purchase)
        {

            var validatePurchase = ValidatePurchase(purchase);
            if (validatePurchase.Status == false)
                return validatePurchase;

            var product = await _productService.GetProductById(purchase.ProductId);
            if (product == null)
                return Notifies.Error("Product not found");

            var user = await _userService.GetUserById(purchase.ClientId);
            if (user == null)
                return Notifies.Error("User not found");

            if(user.WalletBalance < product.Price)
                return Notifies.Error("Insufficient balance");

            purchase.PurchaseDate = DateTime.Now;
            purchase.IsApproved = false;

            return await _purchaseRepository.Add(purchase);
        }

        public async Task<Notifies> UpdatePurchaseIsApproved(int purchaseId)
        {
            var purchase = await _purchaseRepository.GetById(purchaseId);
            if (purchase == null)
                return Notifies.Error("Purchase not found");

            purchase.IsApproved = true;

            return await _purchaseRepository.Update(purchase);
        }

        public async Task<Notifies> DeletePurchase(int purchaseId)
        {
            var purchase = await _purchaseRepository.GetById(purchaseId);
            if (purchase == null)
                return Notifies.Error("Purchase not found");

            if (purchase.IsApproved)
                return Notifies.Error("Purchase already approved");

            return await _purchaseRepository.Delete(purchase);
        }

        public async Task<List<Purchase>> GetAllPurchases()
        {
            return await _purchaseRepository.GetAll();
        }

        public async Task<Purchase?> GetPurchaseById(int id)
        {
            var validateId = Notifies.ValidatePropertyInt(id, "PurchaseId");
            if (validateId.Status == false)
                return null;

            return await _purchaseRepository.GetById(id);
        }

        public async Task<List<Purchase>> GetByUser(int userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
                return new List<Purchase>();

            return await _purchaseRepository.GetByUser(userId);
        }

        public async Task<List<Purchase>> GetByDate(DateTime date)
        {
            return await _purchaseRepository.GetByDate(date);
        }

        public async Task<List<Purchase>> GetByProduct(int productId)
        {
            var validateProductId = Notifies.ValidatePropertyInt(productId, "ProductId");
            if (validateProductId.Status == false)
                return new List<Purchase>();

            return await _purchaseRepository.GetByProduct(productId);
        }

        public async Task<List<Purchase>> GetByStatus(bool status)
        {
            return await _purchaseRepository.GetByStatus(status);
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