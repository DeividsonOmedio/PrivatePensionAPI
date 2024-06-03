using Bogus;
using Domain.Entities;
using Domain.Interfaces.InterfacesRepositories;
using Domain.Notifications;
using Microsoft.AspNetCore.Identity;
using Moq;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PrivatePension.Tests
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher<User>>();
            _userService = new UserService(_userRepositoryMock.Object, new PasswordHasherService(_passwordHasherMock.Object));
        }

        [Fact]
        public async Task AddUser_ShouldBeOk()
        {
            var user = new User { UserName = "JohnDoe", Email = "john@example.com", Password = "password" };

            _userRepositoryMock.Setup(u => u.GetByEmail(user.Email)).ReturnsAsync((User)null);
            _userRepositoryMock.Setup(u => u.Add(It.IsAny<User>())).ReturnsAsync(Notifies.Success());

            var result = await _userService.AddUser(user);

            Assert.True(result.Status);
        }

        [Fact]
        public async Task AddUser_ShouldBeErrorByExistingEmail()
        {
            var existingUser = new User { UserName = "ExistingUser", Email = "existing@example.com", Password = "password" };
            var newUser = new User { UserName = "NewUser", Email = "existing@example.com", Password = "password" };

            _userRepositoryMock.Setup(u => u.GetByEmail(existingUser.Email)).ReturnsAsync(existingUser);

            var result = await _userService.AddUser(newUser);

            Assert.False(result.Status);
            Assert.Equal("Email already registered", result.Message);
        }

        [Fact]
        public async Task UpdateUser_ShouldBeOk()
        {
            var user = new User { Id = 1, UserName = "JohnDoe", Email = "john@example.com", Password = "password" };

            _userRepositoryMock.Setup(u => u.GetById(user.Id)).ReturnsAsync(user);
            _userRepositoryMock.Setup(u => u.Update(It.IsAny<User>())).ReturnsAsync(Notifies.Success());

            var result = await _userService.UpdateUser(user);

            Assert.True(result.Status);
        }

        [Fact]
        public async Task UpdateWalletBalance_ShouldBeOk()
        {
            var user = new User { Id = 1, UserName = "JohnDoe", Email = "john@example.com", Password = "password", WalletBalance = 100 };

            _userRepositoryMock.Setup(u => u.GetById(user.Id)).ReturnsAsync(user);
            _userRepositoryMock.Setup(u => u.Update(It.IsAny<User>())).ReturnsAsync(Notifies.Success());

            var result = await _userService.UpdateWalletBalance(user.Id, 50);

            Assert.True(result.Status);
            Assert.Equal(150, user.WalletBalance);
        }

        [Fact]
        public async Task DeleteUser_ShouldBeOk()
        {
            var user = new User { Id = 1, UserName = "JohnDoe", Email = "john@example.com", Password = "password" };

            _userRepositoryMock.Setup(u => u.GetById(user.Id)).ReturnsAsync(user);
            _userRepositoryMock.Setup(u => u.Delete(It.IsAny<User>())).ReturnsAsync(Notifies.Success());

            var result = await _userService.DeleteUser(user.Id);

            Assert.True(result.Status);
        }

        [Fact]
        public async Task DeleteUser_ShouldBeErrorByUserNotFound()
        {
            var userId = 1;

            _userRepositoryMock.Setup(u => u.GetById(userId)).ReturnsAsync((User)null);

            var result = await _userService.DeleteUser(userId);

            Assert.False(result.Status);
            Assert.Equal("User not found", result.Message);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnListOfUsers()
        {
            var users = new Faker<User>()
                .RuleFor(u => u.Id, f => f.Random.Int(1, 1000))
                .Generate(5);

            _userRepositoryMock.Setup(u => u.GetAll()).ReturnsAsync(users);

            var result = await _userService.GetAllUsers();

            Assert.NotEmpty(result);
            Assert.Equal(users.Count, result.Count);
        }

        [Fact]
        public async Task UpdateUser_ShouldBeErrorByInvalidId()
        {
            var user = new User { Id = 0, UserName = "JohnDoe", Email = "john@example.com", Password = "password" };

            var result = await _userService.UpdateUser(user);

            Assert.False(result.Status);
            Assert.Equal("Invalid Id", result.Message);
        }

        [Fact]
        public async Task UpdateUser_ShouldBeErrorByUserNotFound()
        {
            var user = new User { Id = 1, UserName = "JohnDoe", Email = "john@example.com", Password = "password" };

            _userRepositoryMock.Setup(u => u.GetById(user.Id)).ReturnsAsync((User)null);

            var result = await _userService.UpdateUser(user);

            Assert.False(result.Status);
            Assert.Equal("User not found", result.Message);
        }

        [Fact]
        public async Task UpdateUser_ShouldBeErrorByInvalidPassword()
        {
            var user = new User { Id = 1, UserName = "JohnDoe", Email = "john@example.com", Password = "12345" };

            var result = await _userService.UpdateUser(user);

            Assert.False(result.Status);
            Assert.Equal("User not found", result.Message);
        }

        [Fact]
        public async Task UpdateUser_ShouldBeOkWithPasswordChange()
        {
            var user = new User { Id = 1, UserName = "JohnDoe", Email = "john@example.com", Password = "password" };
            var newPassword = "newpassword";

            _userRepositoryMock.Setup(u => u.GetById(user.Id)).ReturnsAsync(user);
            _userRepositoryMock.Setup(u => u.Update(It.IsAny<User>())).ReturnsAsync(Notifies.Success());
            _passwordHasherMock.Setup(p => p.HashPassword(user, newPassword)).Returns(newPassword);

            user.Password = newPassword;

            var result = await _userService.UpdateUser(user);

            Assert.True(result.Status);
        }

        [Fact]
        public async Task GetUserByEmail_ShouldReturnUserIfExists()
        {
            var email = "john@example.com";
            var user = new User { Id = 1, UserName = "JohnDoe", Email = email, Password = "password" };

            _userRepositoryMock.Setup(u => u.GetByEmail(email)).ReturnsAsync(user);

            var result = await _userService.GetUserByEmail(email);

            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUserByEmail_ShouldReturnNullIfNotExists()
        {
            var email = "nonexistent@example.com";

            _userRepositoryMock.Setup(u => u.GetByEmail(email)).ReturnsAsync((User)null);

            var result = await _userService.GetUserByEmail(email);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUserIfExists()
        {
            var userId = 1;
            var user = new User { Id = userId, UserName = "JohnDoe", Email = "john@example.com", Password = "password" };

            _userRepositoryMock.Setup(u => u.GetById(userId)).ReturnsAsync(user);

            var result = await _userService.GetUserById(userId);

            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNullIfNotExists()
        {
            var userId = 999;

            _userRepositoryMock.Setup(u => u.GetById(userId)).ReturnsAsync((User)null);

            var result = await _userService.GetUserById(userId);

            Assert.Null(result);
        }

        [Fact]
        public void ValidateUser_ShouldReturnSuccessIfValid()
        {
            var user = new User { UserName = "JohnDoe", Email = "john@example.com", Password = "password" };

            var result = _userService.ValidateUser(user);

            Assert.True(result.Status);
        }

        [Fact]
        public void ValidateUser_ShouldReturnErrorIfUserNameInvalid()
        {
            var user = new User { Email = "john@example.com", Password = "password" };

            var result = _userService.ValidateUser(user);

            Assert.False(result.Status);
            Assert.Equal("Campo Name Obrigatório", result.Message);
        }

        [Fact]
        public void ValidateUser_ShouldReturnErrorIfEmailInvalid()
        {
            var user = new User { UserName = "JohnDoe", Password = "password" };

            var result = _userService.ValidateUser(user);

            Assert.False(result.Status);
            Assert.Equal("Campo Email Obrigatório", result.Message);
        }

        [Fact]
        public void ValidPassword_ShouldReturnSuccessIfValid()
        {
            var password = "password";

            var result = _userService.ValidPassword(password);

            Assert.True(result.Status);
        }

        [Fact]
        public void ValidPassword_ShouldReturnErrorIfInvalid()
        {
            var password = "12345";

            var result = _userService.ValidPassword(password);

            Assert.False(result.Status);
            Assert.Equal("Password must be at least 6 characters long", result.Message);
        }
    }
}