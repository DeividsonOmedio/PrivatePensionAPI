using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Domain.Notifications;

namespace Services
{
    public class ContributionService(IContributionService contributionService) : IContributionService
    {
        private readonly IContributionService _contributionService = contributionService;

        public async Task<Notifies> AddContribution(Contribution contribution)
        {
            var validateContribution = ValidateContribution(contribution);
            if (validateContribution.Status == false)
                return validateContribution;

            return await _contributionService.AddContribution(contribution);
        }

        public async Task<Notifies> DeleteContribution(int contributionId)
        {
            var entitie = await _contributionService.GetContributionById(contributionId);
            if (entitie == null)
                return Notifies.Error("Contribution not found");

            return await _contributionService.DeleteContribution(contributionId);
        }

        public async Task<List<Contribution>> GetAllContributions()
        {
            return await _contributionService.GetAllContributions();
        }

        public async Task<Contribution?> GetContributionById(int id)
        {
            var validateId = Notifies.ValidatePropertyInt(id, "ContributionId");
            if (validateId.Status == false)
                return null;

            return await _contributionService.GetContributionById(id);
        }

        public async Task<Notifies> UpdateContribution(Contribution contribution)
        {
            var validateContribution = ValidateContribution(contribution);
            if (validateContribution.Status == false)
                return validateContribution;

            return await _contributionService.UpdateContribution(contribution);
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