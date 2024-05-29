using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Notifications;

namespace Domain.Interfaces.Interfaceservices
{
    public interface IContributionService
    {
        Task<Notifies> AddContribution(Contribution contribution);
        Task<Notifies> UpdateContribution(Contribution contribution);
        Task<Notifies> DeleteContribution(int contributionId);
        Task<Contribution?> GetContributionById(int id);
        Task<List<Contribution>> GetAllContributions();
        Notifies ValidateContribution(Contribution contribution);
    }
}