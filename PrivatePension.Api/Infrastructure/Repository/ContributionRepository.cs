using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.InterfacesRepositories;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ContributionRepository(ContextBase context) : GenericRepository<Contribution>(context), IContributionRepository
    {
        public async Task<List<Contribution>> GetByContributionDate(DateTime contributionDate)
        {
            return await _dbSet.Where(contribution => contribution.ContributionDate == contributionDate).ToListAsync();
        }

        public async Task<List<Contribution>> GetByContributionDateByUser(DateTime contributionDate, int userId)
        {
            return await _dbSet.Where(contribution => contribution.ContributionDate == contributionDate && contribution.Purchase.ClientId == userId).ToListAsync();
        }

        public async Task<Contribution?> GetByPurchaseId(int purchaseId)
        {
            return await _dbSet.FirstOrDefaultAsync(contribution => contribution.PurchaseId == purchaseId);
        }

        public async Task<List<Contribution>> GetByUser(int clientId)
        {
            return await _dbSet.Where(contribution => contribution.Purchase.ClientId == clientId).ToListAsync();
        }
    }
}