//using Bogus;
//using Domain.Entities;
//using Domain.Enums;
//using Domain.Interfaces.InterfacesRepositories;
//using Moq;
//using Services;

//namespace PrivatePension.Tests
//{
//    public class UserServiceTest
//    {
//        [Fact]
//        public async void UserService_Add_ShouldAddUser()
//        {
//            var client = new Faker<User>()
//                .RuleFor(Random => Random.Id, f => f.Random.Int(1, 100))
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => f.Person.Email)
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .RuleFor(c => c.WalletBalance, f => f.Random.Decimal(100, 1000))
//                .Generate();

//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);

//            await userService.AddUser(client);

//            userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);

//        }

//        [Fact]
//        public async Task UserService_Add_ShouldNotAddUserWithoutUsername()
//        {
//            var user = new Faker<User>()
//                .RuleFor(Random => Random.Id, f => f.Random.Int(1, 100))
//                .RuleFor(c => c.Email, f => f.Person.Email)
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .RuleFor(c => c.WalletBalance, f => f.Random.Decimal(100, 1000))
//                .Generate();
           
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);

//            var result = await userService.AddUser(user);

//            userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
//            Assert.False(result.Status);
//        }

//        [Fact]
//        public async Task UserService_Add_ShouldNotAddUserWithoutEmail()
//        {
//            var user = new Faker<User>()
//               .RuleFor(Random => Random.Id, f => f.Random.Int(1, 100))
//               .RuleFor(c => c.UserName, f => f.Person.FirstName)
//               .RuleFor(c => c.Password, f => f.Internet.Password())
//               .RuleFor(c => c.Role, f => UserRolesEnum.client)
//               .RuleFor(c => c.WalletBalance, f => f.Random.Decimal(100, 1000))
//               .Generate();

//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);

//            var result = await userService.AddUser(user);

//            userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
//            Assert.False(result.Status);
//        }

//        [Fact]
//        public async Task UserService_Add_ShouldNotAddUserWithoutPassword()
//        {
//            var user = new Faker<User>()
//                .RuleFor(Random => Random.Id, f => f.Random.Int(1, 100))
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => f.Person.Email)
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .RuleFor(c => c.WalletBalance, f => f.Random.Decimal(100, 1000))
//                .Generate();

//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
            
//            var result = await userService.AddUser(user);

//            userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
//            Assert.False(result.Status);
//        }

//        [Fact]
//        public async Task UserService_Add_ShouldAddUserAdminWithoutWalletBalance()
//        {
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            var user = new Faker<User>()
//                .RuleFor(Random => Random.Id, f => f.Random.Int(1, 100))
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => f.Person.Email)
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .Generate();

//            var result = await userService.AddUser(user);

//            userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
//        }

//        [Fact]
//        public async Task UserService_Add_ShouldNotAddUserWithExistingEmail()
//        {
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            var existingUser = new Faker<User>()
//                .RuleFor(c => c.Id, f => f.Random.Int(1, 100))
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => f.Person.Email)
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .RuleFor(c => c.WalletBalance, f => 100)
//                .Generate();

//            userRepositoryMock.Setup(x => x.GetByEmail(existingUser.Email)).ReturnsAsync(existingUser);

//            var user = new Faker<User>()
//                .RuleFor(c => c.Id, f => f.Random.Int(1, 100))
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => existingUser.Email)
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .RuleFor(c => c.WalletBalance, f => 100)
//                .Generate();

//            var result = await userService.AddUser(user);

//            userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
//            Assert.False(result.Status);
//        }

//        [Fact]
//        public async Task UserService_Update_ShouldUpdateUser()
//        {
//            var user = new Faker<User>()
//                .RuleFor(c => c.Id, f => f.Random.Int(1, 100))
//                .RuleFor(c => c.Id, f => f.Random.Int(1, 100))
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => f.Person.Email)
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .RuleFor(c => c.WalletBalance, f => 100)
//                .Generate();
            
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);


//            var result = await userService.UpdateUser(user);

//            userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
//        }

//        [Fact]
//        public async Task UserService_Update_ShouldNotUpdateUserWithoutUsername()
//        {
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            var user = new Faker<User>()
//                .RuleFor(c => c.Id, f => f.Random.Int(1, 100))
//                .RuleFor(c => c.UserName, f => "")
//                .RuleFor(c => c.Email, f => f.Person.Email)
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .RuleFor(c => c.WalletBalance, f => 100)
//                .Generate();

//            var result = await userService.UpdateUser(user);

//            userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
//            Assert.False(result.Status);
//        }

//        [Fact]
//        public async Task UserService_Update_ShouldNotUpdateUserWithoutEmail()
//        {
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            var user = new Faker<User>()
//                .RuleFor(c => c.Id, f => f.Random.Int(1, 100))
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => "")
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .RuleFor(c => c.WalletBalance, f => 100)
//                .Generate();

//            var result = await userService.UpdateUser(user);

//            userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
//            Assert.False(result.Status);
           
//        }

//        [Fact]
//        public async Task UserService_Update_ShouldNotUpdateUserWithoutPassword()
//        {
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            var user = new Faker<User>()
//                .RuleFor(c => c.Id, f => f.Random.Int(1, 100))
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => f.Person.Email)
//                .RuleFor(c => c.Password, f => "")
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .RuleFor(c => c.WalletBalance, f => 100)
//                .Generate();

//            var result = await userService.UpdateUser(user);

//            userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
//            Assert.False(result.Status);
//        }

//        [Fact]
//        public async Task UserService_Update_ShouldNotUpdateUserWithoutRole()
//        {
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            var user = new Faker<User>()
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => f.Person.Email)
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .RuleFor(c => c.WalletBalance, f => 100)
//                .Generate();

//            var result = await userService.UpdateUser(user);

//            userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
//            Assert.False(result.Status);
//        }

//        [Fact]
//        public async Task UserService_Update_ShouldNotUpdateUserClientWithoutWalletBalance()
//        {
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            var user = new Faker<User>()
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => f.Person.Email)
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .Generate();

//            var result = await userService.UpdateUser(user);

//            userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
//            Assert.False(result.Status);
//        }


//        [Fact]
//        public async Task UserService_Update_ShouldUpdateUserAdminWithoutWalletBalance()
//        {
//            var user = new Faker<User>()
//                .RuleFor(c => c.Id, f => f.Random.Int(1, 100))
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => f.Person.Email)
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.admin)
//                .Generate();

//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            userRepositoryMock.Setup(x => x.GetById(user.Id)).ReturnsAsync(user);

//            var result = await userService.UpdateUser(user);

//            userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
//        }

//        [Fact]
//        public async Task UserService_Update_ShouldNotUpdateUserAdminWithWalletBalance()
//        {
//            // Arrange
//            var user = new Faker<User>()
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => f.Person.Email)
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.admin)
//                .RuleFor(c => c.WalletBalance, f => 100)
//                .Generate();

//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            userRepositoryMock.Setup(x => x.GetById(user.Id)).ReturnsAsync(user);

//            var result = await userService.UpdateUser(user);

//            userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
//            Assert.False(result.Status);
//        }

//        [Fact]
//        public async Task UserService_Update_ShouldNotUpdateUserWithInvalidEmail()
//        {
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            var user = new Faker<User>()
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => "invalidemail")
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .RuleFor(c => c.WalletBalance, f => 100)
//                .Generate();

//            userRepositoryMock.Setup(x => x.GetById(user.Id)).ReturnsAsync(user);

//            var result = await userService.UpdateUser(user);

//            userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
//            Assert.False(result.Status);
//        }

//        [Fact]
//        public async Task UserService_Update_ShouldNotUpdateUserWithExistingEmail()
//        {
//            var user = new Faker<User>()
//                .RuleFor(c => c.UserName, f => f.Person.FirstName)
//                .RuleFor(c => c.Email, f => "")
//                .RuleFor(c => c.Password, f => f.Internet.Password())
//                .RuleFor(c => c.Role, f => UserRolesEnum.client)
//                .RuleFor(c => c.WalletBalance, f => 100)
//                .Generate();

//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            userRepositoryMock.Setup(x => x.GetByEmail(user.Email)).ReturnsAsync(user);

//            var result = await userService.UpdateUser(user);

//            userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
//            Assert.False(result.Status);
//        }


//        [Fact]
//        public async Task UserService_GetById_ShouldReturnUser()
//        {
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            var id = new Faker().Random.Int();
//            var user = new Faker<User>()
//                .RuleFor(c => c.Id, f => f.Random.Int(1 ,100))
//                .Generate();

//            userRepositoryMock.Setup(x => x.GetById(user.Id)).ReturnsAsync(user);

//            var result = await userService.GetUserById(user.Id);

//            userRepositoryMock.Verify(x => x.GetById(user.Id), Times.Once);
//            Assert.Equal(user, result);
//        }

//        [Fact]
//        public async Task UserService_GetById_ShouldReturnNullForNonExistingUser()
//        {
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            var userId = 1;

//            userRepositoryMock.Setup(x => x.GetById(userId)).ReturnsAsync((User)null);

//            var result = await userService.GetUserById(userId);

//            userRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
//        }
            
//        [Fact]
//        public async void UserService_GetAll_ShouldReturnAllUsers()
//        {
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);

//            await userService.GetAllUsers();

//            userRepositoryMock.Verify(x => x.GetAll(), Times.Once);

//        }

//        [Fact]
//        public async void UserService_GetByEmail_ShouldReturnUserByEmail()
//        {
//            var userRepositoryMock = new Mock<IUserRepository>();
//            var userService = new UserService(userRepositoryMock.Object);
//            var email = "teste@teste.com";

//            await userService.GetUserByEmail(email);

//            userRepositoryMock.Verify(x => x.GetByEmail(It.IsAny<string>()), Times.Once);
//        }
//    }
//}
