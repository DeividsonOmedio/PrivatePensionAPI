using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // HashPassword deve retornar um hash
        [Fact]
        public void HashPassword_ShouldReturnHash()
        {
            // Arrange
            var user = new User { Id = 1, Username = "TestUser" };
            var password = "TestPassword";
            var hashedPassword = "hashedPassword";

            _passwordHasherMock.Setup(hasher => hasher.HashPassword(user, password))
                               .Returns(hashedPassword);

            // Act
            var result = _passwordHasherService.HashPassword(user, password);

            // Assert
            Assert.Equal(hashedPassword, result);
        }

        // VerifyPassword deve retornar verdadeiro para senha correta
        [Fact]
        public void VerifyPassword_ShouldReturnTrueForCorrectPassword()
        {
            // Arrange
            var user = new User { Id = 1, Username = "TestUser", Password = "hashedPassword" };
            var password = "TestPassword";

            _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(user, user.Password, password))
                               .Returns(PasswordVerificationResult.Success);

            // Act
            var result = _passwordHasherService.VerifyPassword(user, password);

            // Assert
            Assert.True(result);
        }

        // VerifyPassword deve retornar falso para senha incorreta
        [Fact]
        public void VerifyPassword_ShouldReturnFalseForIncorrectPassword()
        {
            // Arrange
            var user = new User { Id = 1, Username = "TestUser", Password = "hashedPassword" };
            var password = "WrongPassword";

            _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(user, user.Password, password))
                               .Returns(PasswordVerificationResult.Failed);

            // Act
            var result = _passwordHasherService.VerifyPassword(user, password);

            // Assert
            Assert.False(result);
        }

        // VerifyPassword deve retornar falso para senha atualizada falhada
        [Fact]
        public void VerifyPassword_ShouldReturnFalseForUpdatedPassword()
        {
            // Arrange
            var user = new User { Id = 1, Username = "TestUser", Password = "hashedPassword" };
            var password = "TestPassword";

            _passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(user, user.Password, password))
                               .Returns(PasswordVerificationResult.SuccessRehashNeeded);

            // Act
            var result = _passwordHasherService.VerifyPassword(user, password);

            // Assert
            Assert.True(result); // True porque SuccessRehashNeeded significa que a senha está correta, mas precisa de rehash
        }
    }
}
