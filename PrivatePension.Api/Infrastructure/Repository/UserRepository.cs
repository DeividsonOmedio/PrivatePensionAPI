using Domain.Entities;
using Domain.Interfaces.InterfacesRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository(DbContext context) : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(user => user.Email == email);
        }
    }
}