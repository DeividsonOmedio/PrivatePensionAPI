using Domain.Entities;
using Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatePension.Tests
{
    public class UserServiceTest
    {
        //criar user e adicionar
        [Fact]
        public void UserService_Add_ShouldAddUser()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "teste@teste.com",
                Password = "teste123",
                Role = UserRolesEnum.client,
                WalletBalance = 100
            };

            // Act
            userService.Add(user);

            // Assert
            productRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);

        }

        //criar user sem username e nao adicionar
        [Fact]
        public void UserService_Add_ShouldNotAddUserWithoutUsername()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "",
                Email = teste@teste.com"",
                Password = "teste123",
                Role = UserRolesEnum.client,
                WalletBalance = 100
            };

            // Act
            userService.Add(user);

            // Assert
            productRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
        }

        //criar user sem email e nao adicionar
        [Fact]
        public void UserService_Add_ShouldNotAddUserWithoutEmail()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "",
                Password = "teste123",
                Role = UserRolesEnum.client,
                WalletBalance = 100
            };

            // Act
            userService.Add(user);

            // Assert
            productRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
        }

        //criar user sem password e nao adicionar
        [Fact]
        public void UserService_Add_ShouldNotAddUserWithoutPassword()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "teste@teste.com",
                Password = "",
                Role = UserRolesEnum.client,
                WalletBalance = 100
            };

            // Act
            userService.Add(user);

            // Assert
            productRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
        }

        //criar user sem role e nao adicionar
        [Fact]
        public void UserService_Add_ShouldNotAddUserWithoutRole()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "teste@teste.com",
                Password = "teste123",
                WalletBalance = 100
            };

            // Act
            userService.Add(user);

            // Assert
            productRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
        }

        //criar user role=client sem walletbalance e nao adicionar
        [Fact]
        public void UserService_Add_ShouldNotAddUserClientWithoutWalletBalance()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "teste@teste.com",
                Password = "teste123",
                Role = UserRolesEnum.client
            };

            // Act
            userService.Add(user);

            // Assert
            productRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
        }

        //criar user role=admin sem walletbalance e adicionar
        [Fact]
        public void UserService_Add_ShouldAddUserAdminWithoutWalletBalance()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "teste@teste.com",
                Password = "teste123",
                Role = UserRolesEnum.admin
            }

            // Act
            userService.Add(user);

            // Assert
            productRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
        }

        //criar user role=admin com walletbalance e nao adicionar
        [Fact]
        public void UserService_Add_ShouldNotAddUserAdminWithWalletBalance()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "teste@teste.com",
                Password = "teste123",
                Role = UserRolesEnum.admin,
                WalletBalance = 100
            };

            // Act
            userService.Add(user);

            // Assert
            productRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
        }

        //criar user com email invalido e nao adicionar
        [Fact]
        public void UserService_Add_ShouldNotAddUserWithInvalidEmail()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "teste.com",
                Password = "teste123",
                Role = UserRolesEnum.client,
                WalletBalance = 100
            };

            // Act
            userService.Add(user);

            // Assert
            productRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
        }

        //criar user com email ja existente e nao adicionar
        [Fact]
        public void UserService_Add_ShouldNotAddUserWithExistingEmail()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "teste@teste.com",
                Password = "teste123",
                Role = UserRolesEnum.client,
                WalletBalance = 100
            };

            // Act
            userService.Add(user);

            // Assert
            productRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
        }

        //criar user com username ja existente e nao adicionar
        [Fact]
        public void UserService_Add_ShouldNotAddUserWithExistingUsername()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "teste@teste.com",
                Password = "teste123",
                Role = UserRolesEnum.client,
                WalletBalance = 100
            };

            // Act
            userService.Add(user);

            // Assert
            productRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
        }

        //update user e atualizar
        [Fact]
        public void UserService_Update_ShouldUpdateUser()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "teste@teste.com",
                Password = "teste123",
                Role = UserRolesEnum.client,
                WalletBalance = 100
            };

            // Act
            userService.Update(user);

            // Assert
            productRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }

        //update user sem username e nao atualizar
        [Fact]
        public void UserService_Update_ShouldNotUpdateUserWithoutUsername()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "",
                Email = "teste@teste.com",
                Password = "teste123",
                Role = UserRolesEnum.client,
                WalletBalance = 100
            };

            // Act
            userService.Update(user);

            // Assert
            productRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }

        //update user sem email e nao atualizar
        [Fact]
        public void UserService_Update_ShouldNotUpdateUserWithoutEmail()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "",
                Password = "teste123",
                Role = UserRolesEnum.client,
                WalletBalance = 100
            };

            // Act
            userService.Update(user);

            // Assert
            productRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }

        //update user sem password e nao atualizar
        [Fact]
        public void UserService_Update_ShouldNotUpdateUserWithoutPassword()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var user = new User
            {
                Id = 1,
                Username = "User 1",
                Email = "teste@teste.com",
                Password = "",
                Role = UserRolesEnum.client,
                WalletBalance = 100
            };

            // Act
            userService.Update(user);

            // Assert
            productRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }

        //delete user by id e remover
        [Fact]
        public void UserService_Delete_ShouldDeleteUserById()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var userId = 1;

            // Act
            userService.Delete(userId);

            // Assert
            productRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        //delete user by id inexistente e nao remover
        [Fact]
        public void UserService_Delete_ShouldNotDeleteUserByNonexistentId()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var userId = 0;

            // Act
            userService.Delete(userId);

            // Assert
            productRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
        }

        //get user by id e retornar
        [Fact]
        public void UserService_GetById_ShouldReturnUserById()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var userId = 1;

            // Act
            userService.GetById(userId);

            // Assert
            productRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        //get user by id inexistente e nao retornar
        [Fact]
        public void UserService_GetById_ShouldNotReturnUserByNonexistentId()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var userId = 0;

            // Act
            userService.GetById(userId);

            // Assert
            productRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Never);
        }

        //get all users e retornar
        [Fact]
        public void UserService_GetAll_ShouldReturnAllUsers()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);

            // Act
            userService.GetAll();

            // Assert
            productRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        //get all users sem usuarios e nao retornar
        [Fact]
        public void UserService_GetAll_ShouldNotReturnAllUsersWithoutUsers()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);

            // Act
            userService.GetAll();

            // Assert
            productRepositoryMock.Verify(x => x.GetAll(), Times.Never);
        }

        //get user by email e retornar
        [Fact]
        public void UserService_GetByEmail_ShouldReturnUserByEmail()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var email = "teste@teste.com";

            // Act
            userService.GetByEmail(email);

            // Assert
            productRepositoryMock.Verify(x => x.GetByEmail(It.IsAny<string>()), Times.Once);

        }

        //get user by email inexistente e nao retornar
        [Fact]
        public void UserService_GetByEmail_ShouldNotReturnUserByNonexistentEmail()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var userService = new UserService(productRepositoryMock.Object);
            var email = "";

            // Act
            userService.GetByEmail(email);

            // Assert
            productRepositoryMock.Verify(x => x.GetByEmail(It.IsAny<string>()), Times.Never);
        }


    }
}
