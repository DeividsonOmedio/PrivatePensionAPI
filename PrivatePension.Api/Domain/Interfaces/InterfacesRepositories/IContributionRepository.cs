using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfacesRepositories
{
    public interface IContributionRepository : IGenericRepository<Contribution>
    {
        Task<Contribution?> GetByPurchaseId(int purchaseId);
        Task<List<Contribution>> GetByUser(int userId);
        Task<List<Contribution>> GetByContributionDateByUser(DateTime contributionDate, int userId);
        Task<List<Contribution>> GetByContributionDate(DateTime contributionDate);


    }
}
