using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Domain.Interfaces.InterfacesRepositories;
using Domain.Notifications;

namespace Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Notifies> AddUser(User user)
        {
            var validate = ValidateUser(user);
            if (validate.Status == false)
                return validate;

            return await _userRepository.Add(user);
        }

        public async Task<Notifies> DeleteUser(int userId)
        {
            var entitie = await _userRepository.GetById(userId);

            if (entitie == null)
                return Notifies.Error("User not found");

            return await _userRepository.Delete(entitie);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }

        public async Task<User?> GetUserById(int id)
        {
            var validateId = Notifies.ValidatePropertyInt(id, "Id");
            if (validateId.Status == false)
                return null;

            return await _userRepository.GetById(id);
        }

        public async Task<Notifies> UpdateUser(User user)
        {
            var validate = ValidateUser(user);
            if (validate.Status == false)
                return validate;

            return await _userRepository.Update(user);
        }

        public Notifies ValidateUser(User user)
        {
            var validateId = Notifies.ValidatePropertyInt(user.Id, "Id");
            if (validateId.Status == false)
                return validateId;

            var validateName = Notifies.ValidatePropertyString(user.UserName, "Name");
            if (validateName.Status == false)
                return validateName;

            var validateEmail = Notifies.ValidatePropertyString(user.Email, "Email");
            if (validateEmail.Status == false)
                return validateEmail;

            var validatePassword = Notifies.ValidatePropertyString(user.Password, "Password");
            if (validatePassword.Status == false)
                return validatePassword;

            return Notifies.Success();
        }
    }
}