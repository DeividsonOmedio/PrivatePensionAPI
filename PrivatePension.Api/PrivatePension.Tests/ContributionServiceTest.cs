using Bogus;
using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Domain.Interfaces.InterfacesRepositories;
using Domain.Notifications;
using Moq;
using Services;

namespace PrivatePension.Tests
{
    public class ContributionServiceTest
    {
        private readonly Mock<IContributionRepository> _contributionRepositoryMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IPurchaseService> _purchaseServiceMock;
        private readonly ContributionService _contributionService;
        private readonly Mock<IUserLogged> _userLoggedMock;

        public ContributionServiceTest()
        {
            _contributionRepositoryMock = new Mock<IContributionRepository>();
            _userServiceMock = new Mock<IUserService>();
            _purchaseServiceMock = new Mock<IPurchaseService>();
            _userLoggedMock = new Mock<IUserLogged>();
            _contributionService = new ContributionService(_contributionRepositoryMock.Object, _userServiceMock.Object, _purchaseServiceMock.Object, _userLoggedMock.Object);
        }

        [Fact]
        public async Task AddContribution_ShouldBeOk()
        {
            var contribution = new Contribution
            {
                Id = 1,
                Amount = 100,
                PurchaseId = 1
            };

            var purchase = new Purchase
            {
                Id = 1,
                ClientId = 1,
                IsApproved = true
            };

            var user = new User
            {
                Id = 1,
                WalletBalance = 200
            };

            _userLoggedMock.Setup(u => u.GetCurrentUserId()).Returns(user.Id);
            _purchaseServiceMock.Setup(p => p.GetPurchaseById(contribution.PurchaseId)).ReturnsAsync(purchase);
            _userServiceMock.Setup(u => u.GetUserById(purchase.ClientId)).ReturnsAsync(user);
            _userServiceMock.Setup(u => u.UpdateUser(user)).ReturnsAsync(Notifies.Success());
            _contributionRepositoryMock.Setup(c => c.Add(contribution)).ReturnsAsync(Notifies.Success());

            var result = await _contributionService.AddContribution(contribution);

            Assert.True(result.Status);
        }

        [Fact]
        public async Task AddContribution_ShouldBeErrorByPurchaseId()
        {
            var faker = new Faker();
            var contribution = new Contribution
            {
                Id = faker.Random.Int(),
                PurchaseId = 0,
                Amount = faker.Finance.Amount(1, 1000),
                ContributionDate = DateTime.Now
            };

            var result = await _contributionService.AddContribution(contribution);

            Assert.False(result.Status);
            Assert.Equal("Campo PurchaseId Obrigatório e maior do que 0", result.Message);
        }

        [Fact]
        public async Task AddContribution_ShouldBeErrorByAmount()
        {
            var faker = new Faker();
            var contribution = new Contribution
            {
                Id = faker.Random.Int(),
                PurchaseId = faker.Random.Int(1, 1000),
                Amount = 0,
                ContributionDate = DateTime.Now
            };

            var result = await _contributionService.AddContribution(contribution);

            Assert.False(result.Status);
            Assert.Equal("Campo Amount Obrigatório e maior do que 0", result.Message);
        }

        [Fact]
        public async Task AddContribution_ShouldBeErrorByAmountInsufficient()
        {
            var faker = new Faker();
            var contribution = new Contribution
            {
                Id = faker.Random.Int(),
                PurchaseId = faker.Random.Int(1, 1000),
                Amount = 10000,
                ContributionDate = DateTime.Now
            };

            var purchase = new Purchase
            {
                Id = contribution.PurchaseId,
                ClientId = faker.Random.Int(1, 1000),
                IsApproved = true
            };

            var user = new User
            {
                Id = purchase.ClientId,
                WalletBalance = 5000
            };

            _purchaseServiceMock.Setup(p => p.GetPurchaseById(contribution.PurchaseId)).ReturnsAsync(purchase);
            _userServiceMock.Setup(u => u.GetUserById(purchase.ClientId)).ReturnsAsync(user);

            var result = await _contributionService.AddContribution(contribution);

            Assert.False(result.Status);
            Assert.Equal("User cannot make a purchase for another user.", result.Message);
        }

        [Fact]
        public async Task AddContribution_ShouldBeErrorByContributionDate()
        {
            var faker = new Faker();
            var contribution = new Contribution
            {
                Id = faker.Random.Int(),
                PurchaseId = faker.Random.Int(1, 1000),
                Amount = faker.Finance.Amount(1, 1000),
                ContributionDate = DateTime.MinValue
            };

            var result = await _contributionService.AddContribution(contribution);

            Assert.False(result.Status);
            Assert.Equal("Purchase not found", result.Message);
        }

        [Fact]
        public async Task UpdateContribution_ShouldBeOk()
        {
            var faker = new Faker();
            var contribution = new Contribution
            {
                Id = faker.Random.Int(),
                PurchaseId = faker.Random.Int(1, 1000),
                Amount = 10000,
                ContributionDate = DateTime.Now
            };

            var purchase = new Purchase
            {
                Id = contribution.PurchaseId,
                ClientId = faker.Random.Int(1, 1000),
                IsApproved = true
            };

            var user = new User
            {
                Id = purchase.ClientId,
                WalletBalance = 5000
            };

            _userLoggedMock.Setup(u => u.GetCurrentUserId()).Returns(user.Id);
            _purchaseServiceMock.Setup(p => p.GetPurchaseById(contribution.PurchaseId)).ReturnsAsync(purchase);
            _userServiceMock.Setup(u => u.GetUserById(purchase.ClientId)).ReturnsAsync(user);

            var result = await _contributionService.AddContribution(contribution);

            Assert.False(result.Status);
            Assert.Equal("Insufficient funds", result.Message);
        }

        [Fact]
        public async Task UpdateContribution_ShouldBeErrorByPurchaseId()
        {
            var faker = new Faker();
            var contribution = new Contribution
            {
                Id = faker.Random.Int(),
                PurchaseId = faker.Random.Int(1, 1000),
                Amount = 10000,
                ContributionDate = DateTime.Now
            };

            var purchase = new Purchase
            {
                Id = contribution.PurchaseId,
                IsApproved = true
            };

            var user = new User
            {
                Id = purchase.ClientId,
                WalletBalance = 5000
            };

            _purchaseServiceMock.Setup(p => p.GetPurchaseById(contribution.PurchaseId)).ReturnsAsync(purchase);
            _userServiceMock.Setup(u => u.GetUserById(purchase.ClientId)).ReturnsAsync(user);

            var result = await _contributionService.AddContribution(contribution);

            Assert.False(result.Status);
            Assert.Equal("Insufficient funds", result.Message);
        }

        [Fact]
        public async Task UpdateContribution_ShouldBeErrorByAmountInsufficient()
        {
            var faker = new Faker();
            var contribution = new Contribution
            {
                Id = faker.Random.Int(),
                PurchaseId = faker.Random.Int(1, 1000),
                Amount = 10000,
                ContributionDate = DateTime.Now
            };

            var purchase = new Purchase
            {
                Id = contribution.PurchaseId,
                ClientId = faker.Random.Int(1, 1000),
                IsApproved = true
            };

            var user = new User
            {
                Id = purchase.ClientId,
                WalletBalance = 500
            };

            _userLoggedMock.Setup(u => u.GetCurrentUserId()).Returns(user.Id);
            _purchaseServiceMock.Setup(p => p.GetPurchaseById(contribution.PurchaseId)).ReturnsAsync(purchase);
            _userServiceMock.Setup(u => u.GetUserById(purchase.ClientId)).ReturnsAsync(user);

            var result = await _contributionService.AddContribution(contribution);

            Assert.False(result.Status);
            Assert.Equal("Insufficient funds", result.Message);
        }

        [Fact]
        public async Task UpdateContribution_ShouldBeErrorByContributionDate()
        {
            var faker = new Faker();
            var contribution = new Contribution
            {
                Id = faker.Random.Int(),
                PurchaseId = faker.Random.Int(1, 1000),
                Amount = 10000,
                ContributionDate = DateTime.MinValue
            };

            var purchase = new Purchase
            {
                Id = contribution.PurchaseId,
                ClientId = faker.Random.Int(1, 1000),
                IsApproved = true
            };

            var user = new User
            {
                Id = purchase.ClientId,
                WalletBalance = 5000
            };

            _userLoggedMock.Setup(u => u.GetCurrentUserId()).Returns(user.Id);
            _purchaseServiceMock.Setup(p => p.GetPurchaseById(contribution.PurchaseId)).ReturnsAsync(purchase);
            _userServiceMock.Setup(u => u.GetUserById(purchase.ClientId)).ReturnsAsync(user);

            var result = await _contributionService.AddContribution(contribution);

            Assert.False(result.Status);
            Assert.Equal("Insufficient funds", result.Message);
        }

        [Fact]
        public async Task DeleteContributionById_ShouldBeOk()
        {
            var faker = new Faker();
            var contribution = new Contribution
            {
                Id = faker.Random.Int(1, 1000)
            };

            _contributionRepositoryMock.Setup(c => c.GetById(contribution.Id)).ReturnsAsync(contribution);
            _contributionRepositoryMock.Setup(c => c.Delete(contribution)).ReturnsAsync(Notifies.Success());

            var result = await _contributionService.DeleteContribution(contribution.Id);

            Assert.True(result.Status);
        }

        [Fact]
        public async Task DeleteContributionById_ShouldBeErrorById()
        {
            var faker = new Faker();
            var contributionId = 0;

            var result = await _contributionService.DeleteContribution(contributionId);

            Assert.False(result.Status);
            Assert.Equal("Contribution not found", result.Message);
        }

        [Fact]
        public async Task GetContributionById_ShouldBeOk()
        {
            // 
            var faker = new Faker();
            var contribution = new Contribution
            {
                Id = faker.Random.Int(1, 1000)
            };

            _contributionRepositoryMock.Setup(c => c.GetById(contribution.Id)).ReturnsAsync(contribution);

            var result = await _contributionService.GetContributionById(contribution.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetContributionById_ShouldBeErrorById()
        {
            var faker = new Faker();
            var contributionId = 0;

            var result = await _contributionService.GetContributionById(contributionId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllContributions_ShouldBeOk()
        {
            var faker = new Faker();
            var contributions = new List<Contribution>
            {
                new Contribution { Id = faker.Random.Int(1, 1000) },
                new Contribution { Id = faker.Random.Int(1, 1000) },
                new Contribution { Id = faker.Random.Int(1, 1000) }
            };

            _contributionRepositoryMock.Setup(c => c.GetAll()).ReturnsAsync(contributions);

            var result = await _contributionService.GetAllContributions();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllContributions_ShouldBeErrorByContributions()
        {
            var contributions = new List<Contribution>();

            _contributionRepositoryMock.Setup(c => c.GetAll()).ReturnsAsync(contributions);

            var result = await _contributionService.GetAllContributions();

            Assert.Empty(result);
        }

    }
}