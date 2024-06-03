using Domain.Entities;
using Domain.Notifications;

namespace Domain.Interfaces.Interfaceservices
{
    public interface IUserService
    {
        Task<Notifies> AddUser(User user);
        Task<Notifies> UpdateUser(User user);
        Task<Notifies> UpdateWalletBalance(int id, decimal walletBalance);
        Task<Notifies> DeleteUser(int userId);
        Task<User?> GetUserById(int id);
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserByEmail(string email);
        Task<User?> ValidateUserCredentials(string email, string password);
        Notifies ValidateUser(User user);
    }
}