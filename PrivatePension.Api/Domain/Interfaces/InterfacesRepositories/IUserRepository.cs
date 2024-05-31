using Domain.Entities;

namespace Domain.Interfaces.InterfacesRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmail(string email);
        Task<User?> ValidateUserCredentials(string email, string password);
    }
}
