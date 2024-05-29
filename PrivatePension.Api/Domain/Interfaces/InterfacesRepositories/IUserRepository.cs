using Domain.Entities;

namespace Domain.Interfaces.InterfacesRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmail(string email);
    }
}
