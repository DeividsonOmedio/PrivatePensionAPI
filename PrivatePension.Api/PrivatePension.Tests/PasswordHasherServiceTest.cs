using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using Services;

namespace PrivatePension.Tests
{
    public class PasswordHasherServiceTest
    {
        private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;
        private readonly PasswordHasherService _passwordHasherService;

        public PasswordHasherServiceTest()
        {
            _passwordHasherMock = new Mock<IPasswordHasher<User>>();
            _passwordHasherService = new PasswordHasherService(_passwordHasherMock.Object);
        }

        [Fact]
        public void HashPassword_ShouldReturnHash()
        {
            var user = new User { Id = 1, UserName = "TestUser" };
            var password = "TestPassword";
            var hashedPassword = "hashedPassword";

            _passwordHasherMock.Setup(hasher => hasher.HashPassword(user, password))
                               .Returns(hashedPassword);

            var result = _passwordHasherService.HashPassword(user, password);

            Assert.Equal(hashedPassword, result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrueForCorrectPassword()
        {
            var user = new User { Id = 1, UserName = "TestUser", Password = "hashedPassword" };
            var password = "TestPassword";

            _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(user, user.Password, password))
                               .Returns(PasswordVerificationResult.Success);

            var result = _passwordHasherService.VerifyPassword(user, password);

            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalseForIncorrectPassword()
        {
           
            var user = new User { Id = 1, UserName = "TestUser", Password = "hashedPassword" };
            var password = "WrongPassword";

            _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(user, user.Password, password))
                               .Returns(PasswordVerificationResult.Failed);

            var result = _passwordHasherService.VerifyPassword(user, password);

            Assert.False(result);
        }

    }
}
