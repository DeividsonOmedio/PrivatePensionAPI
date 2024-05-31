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
        private readonly IUserLogged _userLogged;

        public PurchaseService(IPurchaseRepository purchaseRepository, IProductService productService, IUserService userService, IUserLogged userLogged)
        {
            _purchaseRepository = purchaseRepository;
            _productService = productService;
            _userService = userService;
            _userLogged = userLogged;
        }

        public async Task<Notifies> AddPurchase(Purchase purchase)
        {
            int currentUserId = _userLogged.GetCurrentUserId();

            if (purchase.ClientId != currentUserId)
            {
                return Notifies.Error("User cannot make a purchase for another user.");
            }

            var validatePurchase = ValidatePurchase(purchase);
            if (validatePurchase.Status == false)
                return validatePurchase;

            var product = await _productService.GetProductById(purchase.ProductId);
            if (product == null)
                return Notifies.Error("Product not found");
            if(!product.Available)
                return Notifies.Error("Product not available");
            purchase.Product = product;

            var purchaseExists = await GetByProductAndUser(purchase.ProductId, purchase.ClientId);
            if (purchaseExists != null)
                return Notifies.Error("Product already purchased");

            var user = await _userService.GetUserById(purchase.ClientId);
            if (user == null)
                return Notifies.Error("User not found");
            if (user.Role == Domain.Enums.UserRolesEnum.admin)
                return Notifies.Error("Admins can't make purchases");
            

            if(user.WalletBalance < product.Price)
                return Notifies.Error("Insufficient balance");
            user.WalletBalance = user.WalletBalance - product.Price;
            var UpdateUserWallet = await _userService.UpdateUser(user);
            if (UpdateUserWallet.Status == false)
                return Notifies.Error("Erro no servidor");
            purchase.Client = user;

            purchase.PurchaseDate = DateTime.Now;
            purchase.IsApproved = false;

            var result = await _purchaseRepository.Add(purchase);
            return result;
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

        public async Task<Purchase?> GetByProductAndUser(int productId, int userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
                return null;

            return await _purchaseRepository.GetByProductAndUser(productId ,userId);
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