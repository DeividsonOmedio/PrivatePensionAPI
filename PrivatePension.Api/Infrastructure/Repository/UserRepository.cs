using Domain.Entities;
using Domain.Interfaces.InterfacesRepositories;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository(ContextBase context) : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(user => user.Email == email);
        }
    }
}