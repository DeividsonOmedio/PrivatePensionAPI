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
            try
            {
                return await _dbSet.FirstOrDefaultAsync(user => user.Email == email);
            }
            catch
            {
                return null;
            }
        }

        public async Task<User?> ValidateUserCredentials(string email, string password)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(user => user.Email == email && user.Password == password);
            }
            catch
            {
                return null;
            }
        }
    }
}