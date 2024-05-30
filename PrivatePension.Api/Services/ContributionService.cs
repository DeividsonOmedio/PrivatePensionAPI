using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Domain.Interfaces.InterfacesRepositories;
using Domain.Notifications;

namespace Services
{
    public class ContributionService : IContributionService
    {
        private readonly IContributionRepository _contributionRepository;
        private readonly IUserService _userService;
        private readonly IPurchaseService _purchaseService;

        public ContributionService(IContributionRepository contributionRepository, IUserService userService, IPurchaseService purchaseService)
        {
            _contributionRepository = contributionRepository;
            _userService = userService;
            _purchaseService = purchaseService;
        }

        public async Task<Notifies> AddContribution(Contribution contribution)
        {
            var validateContribution = ValidateContribution(contribution);
            if (validateContribution.Status == false)
                return validateContribution;

            var purchase = await _purchaseService.GetPurchaseById(contribution.PurchaseId);
            if (purchase == null)
                return Notifies.Error("Purchase not found");

            var user = await _userService.GetUserById(purchase.ClientId);
            if (user == null)
                return Notifies.Error("User not found");


            if (!purchase.IsApproved)
                return Notifies.Error("Purchase not approved");

            if (user.WalletBalance < contribution.Amount)
                return Notifies.Error("Insufficient funds");

            return await _contributionRepository.Add(contribution);
        }

        public async Task<Notifies> DeleteContribution(int contributionId)
        {
            var entitie = await GetContributionById(contributionId);
            if (entitie == null)
                return Notifies.Error("Contribution not found");

            return await _contributionRepository.Delete(entitie);
        }

        public async Task<List<Contribution>> GetAllContributions()
        {
            return await _contributionRepository.GetAll();
        }

        public async Task<List<Contribution>> GetByContributionDate(DateTime contributionDate)
        {
            return await _contributionRepository.GetByContributionDate(contributionDate);
        }

        public async Task<List<Contribution>> GetByContributionDateByUser(DateTime contributionDate, int userId)
        {
            return await _contributionRepository.GetByContributionDateByUser(contributionDate, userId);
        }

        public async Task<Contribution?> GetByPurchaseId(int purchaseId)
        {
            return await _contributionRepository.GetByPurchaseId(purchaseId);
        }

        public async Task<List<Contribution>> GetByUser(int clientId)
        {
            return await _contributionRepository.GetByUser(clientId);
        }

        public async Task<Contribution?> GetContributionById(int id)
        {
            var validateId = Notifies.ValidatePropertyInt(id, "ContributionId");
            if (validateId.Status == false)
                return null;

            return await _contributionRepository.GetById(id);
        }

        public async Task<Notifies> UpdateContribution(Contribution contribution)
        {
            var validateContribution = ValidateContribution(contribution);
            if (validateContribution.Status == false)
                return validateContribution;

            var purchase = await _purchaseService.GetPurchaseById(contribution.PurchaseId);
            if (purchase == null)
                return Notifies.Error("Purchase not found");

            var user = await _userService.GetUserById(purchase.ClientId);
            if (user.WalletBalance < contribution.Amount)
                return Notifies.Error("Insufficient funds");

            return await _contributionRepository.Update(contribution);
        }

        public Notifies ValidateContribution(Contribution contribution)
        {
            var PurchaseId = Notifies.ValidatePropertyInt(contribution.PurchaseId, "PurchaseId");
            if (PurchaseId.Status == false)
                return PurchaseId;

            var Amount = Notifies.ValidatePropertyDecimal(contribution.Amount, "Amount");
            if (Amount.Status == false)
                return Amount;

            return Notifies.Success();
        }
    }
}