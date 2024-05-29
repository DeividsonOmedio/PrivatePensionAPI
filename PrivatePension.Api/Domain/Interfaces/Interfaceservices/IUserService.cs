using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Notifications;

namespace Domain.Interfaces.Interfaceservices
{
    public interface IUserService
    {
        Task<Notifies> AddUser(User user);
        Task<Notifies> UpdateUser(User user);
        Task<Notifies> DeleteUser(int userId);
        Task<User> GetUserById(int id);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        Task<Notifies> ValidateUser(Product product);
    }
}