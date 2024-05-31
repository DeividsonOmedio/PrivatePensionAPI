using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Domain.Interfaces.InterfacesRepositories;
using Domain.Notifications;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasherService _passwordHasherService;

        public UserService(IUserRepository userRepository, PasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<Notifies> AddUser(User user)
        {
            var validate = ValidateUser(user);
            if (validate.Status == false)
                return validate;

            var validatePassword = ValidPassword(user.Password);
            if (validatePassword.Status == false)
                return validatePassword;

            user.Password = _passwordHasherService.HashPassword(user, user.Password);

            var userEmail = await _userRepository.GetByEmail(user.Email);
            if (userEmail != null)
                return Notifies.Error("Email already registered");

            if (user.Role == Domain.Enums.UserRolesEnum.admin)
                user.WalletBalance = null;

            return await _userRepository.Add(user);
        }

        public async Task<Notifies> UpdateUser(User user)
        {
            if (user.Id <= 0)
                return Notifies.Error("Invalid Id");

            var validate = ValidateUser(user);
            if (validate.Status == false)
                return validate;

            var searchUser = await _userRepository.GetById(user.Id);
            if (searchUser == null)
                return Notifies.Error("User not found");
            searchUser.UserName = user.UserName;
            searchUser.Email = user.Email;
            if (user.Password != "" && user.Password != null)
            {
                var validatePassword = ValidPassword(user.Password);
                if (validatePassword.Status == false)
                    return validatePassword;

                var passwordHasher = new PasswordHasher<User>();
                user.Password = passwordHasher.HashPassword(user, user.Password);
                searchUser.Password = user.Password;
            }

            return await _userRepository.Update(searchUser);
        }


        public async Task<Notifies> UpdateWalletBalance(int id, decimal walletBalance)
        {
            if (id <= 0)
                return Notifies.Error("Invalid Id");

            var user = await _userRepository.GetById(id);

            if (user == null)
                return Notifies.Error("User not found");
            
            if (user.Role == Domain.Enums.UserRolesEnum.admin)
                return Notifies.Error("Admin cannot have a wallet balance");

            user.WalletBalance += walletBalance;

            return await _userRepository.Update(user);
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

        public async Task<User?> ValidateUserCredentials(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
                return null;

            var result = _passwordHasherService.VerifyPassword(user, password);
            return result ? user : null;
        }

        public Notifies ValidateUser(User user)
        {
            var validateName = Notifies.ValidatePropertyString(user.UserName, "Name");
            if (validateName.Status == false)
                return validateName;

            var validateEmail = Notifies.ValidatePropertyString(user.Email, "Email");
            if (validateEmail.Status == false)
                return validateEmail;

            return Notifies.Success();
        }

        public Notifies ValidPassword(string password)
        {
            var validatePassword = Notifies.ValidatePropertyString(password, "Password");
            if (validatePassword.Status == false)
                return validatePassword;

            if (password.Length < 6)
                return Notifies.Error("Password must be at least 6 characters long");

            return Notifies.Success();
        }
    }
}