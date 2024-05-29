using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ContributionRepository(DbContext context) : GenericRepository<Contribution>(context), IContributionRepository
    {
        public async Task<List<Contribution>> GetByContributionDate(DateTime contributionDate)
        {
            try
            {
                return await _dbSet.Where(contribution => contribution.ContributionDate == contributionDate).ToListAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<Contribution>> GetByContributionDateByUser(DateTime contributionDate)
        {
            try
            {
                return await _dbSet.Where(contribution => contribution.ContributionDate == contributionDate).ToListAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<Contribution?> GetByPurchaseId(int purchaseId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(contribution => contribution.PurchaseId == purchaseId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Contribution>> GetByUser(int clientId)
        {
            try
            {
                return await _dbSet.Where(contribution => contribution.Purchase.ClientId == clientId).ToListAsync();
            }
            catch (Exception)
            {
                return [];
            }
        }
    }
}