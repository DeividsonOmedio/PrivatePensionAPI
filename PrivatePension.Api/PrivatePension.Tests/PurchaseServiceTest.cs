//using Bogus;
//using Domain.Entities;
//using Domain.Interfaces.Interfaceservices;
//using Domain.Interfaces.InterfacesRepositories;
//using Domain.Notifications;
//using Moq;
//using Services;

//namespace PrivatePension.Tests
//{
//    public class PurchaseServiceTest
//    {
//        private readonly Mock<IPurchaseRepository> _purchaseRepositoryMock;
//        private readonly Mock<IUserService> _userServiceMock;
//        private readonly Mock<IProductService> _productServiceMock;
//        private readonly PurchaseService _purchaseService;

//        public PurchaseServiceTest()
//        {
//            _purchaseRepositoryMock = new Mock<IPurchaseRepository>();
//            _userServiceMock = new Mock<IUserService>();
//            _productServiceMock = new Mock<IProductService>();
//            _purchaseService = new PurchaseService(_purchaseRepositoryMock.Object, _productServiceMock.Object, _userServiceMock.Object);
//        }

//        [Fact]
//        public async Task AddPurchase_ShouldBeOk()
//        {
//            var user = new User { Id = 1, WalletBalance = 2000 };
//            var product = new Product { Id = 1, Price = 1000 };
//            var purchase = new Purchase { ClientId = user.Id, ProductId = product.Id };

//            _userServiceMock.Setup(u => u.GetUserById(purchase.ClientId)).ReturnsAsync(user);
//            _productServiceMock.Setup(p => p.GetProductById(purchase.ProductId)).ReturnsAsync(product);
//            _purchaseRepositoryMock.Setup(p => p.Add(It.IsAny<Purchase>())).ReturnsAsync(Notifies.Success());

//            var result = await _purchaseService.AddPurchase(purchase);

//            Assert.True(result.Status);
//        }

//        [Fact]
//        public async Task AddPurchase_ShouldBeErrorByCustomerId()
//        {
//            var purchase = new Purchase { ClientId = 0, ProductId = 1 };

//            var result = await _purchaseService.AddPurchase(purchase);

//            Assert.False(result.Status);
//            Assert.Equal("Campo Client Id Obrigatório e maior do que 0", result.Message);
//        }

//        [Fact]
//        public async Task AddPurchase_ShouldBeErrorByProduct()
//        {
//            var user = new User { Id = 1, WalletBalance = 2000 };
//            var purchase = new Purchase { ClientId = user.Id, ProductId = 0 };

//            _userServiceMock.Setup(u => u.GetUserById(purchase.ClientId)).ReturnsAsync(user);
//            _productServiceMock.Setup(p => p.GetProductById(purchase.ProductId)).ReturnsAsync((Product)null);

//            var result = await _purchaseService.AddPurchase(purchase);

//            Assert.False(result.Status);
//            Assert.Equal("Campo Product Id Obrigatório e maior do que 0", result.Message);
//        }

//        [Fact]
//        public async Task AddPurchase_ShouldBeErrorByCustomer()
//        {
//            var purchase = new Purchase { ClientId = 1, ProductId = 1 };

//            _userServiceMock.Setup(u => u.GetUserById(purchase.ClientId)).ReturnsAsync((User)null);

//            var result = await _purchaseService.AddPurchase(purchase);

//            Assert.False(result.Status);
//            Assert.Equal("Product not found", result.Message);
//        }

//        [Fact]
//        public async Task AddPurchase_ShouldBeErrorByInsufficientBalance()
//        {
//            var user = new User { Id = 1, WalletBalance = 500 };
//            var product = new Product { Id = 1, Price = 1000 };
//            var purchase = new Purchase { ClientId = user.Id, ProductId = product.Id };

//            _userServiceMock.Setup(u => u.GetUserById(purchase.ClientId)).ReturnsAsync(user);
//            _productServiceMock.Setup(p => p.GetProductById(purchase.ProductId)).ReturnsAsync(product);

//            var result = await _purchaseService.AddPurchase(purchase);

//            Assert.False(result.Status);
//            Assert.Equal("Insufficient balance", result.Message);
//        }

//        [Fact]
//        public async Task UpdatePurchaseIsApproved_ShouldBeOk()
//        {
//            var purchase = new Faker<Purchase>()
//                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
//                .RuleFor(p => p.IsApproved, false)
//                .Generate();

//            _purchaseRepositoryMock.Setup(p => p.GetById(purchase.Id)).ReturnsAsync(purchase);
//            _purchaseRepositoryMock.Setup(p => p.Update(It.IsAny<Purchase>())).ReturnsAsync(Notifies.Success());

//            var result = await _purchaseService.UpdatePurchaseIsApproved(purchase.Id);

//            Assert.True(result.Status);
//        }

//        [Fact]
//        public async Task UpdatePurchaseIsApproved_ShouldBeErrorByPurchase()
//        {
//            var purchaseId = 1;

//            _purchaseRepositoryMock.Setup(p => p.GetById(purchaseId)).ReturnsAsync((Purchase)null);

//            var result = await _purchaseService.UpdatePurchaseIsApproved(purchaseId);

//            Assert.False(result.Status);
//            Assert.Equal("Purchase not found", result.Message);
//        }

//        [Fact]
//        public async Task DeletePurchase_ShouldBeOk()
//        {
//            var purchase = new Faker<Purchase>()
//                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
//                .RuleFor(p => p.IsApproved, false)
//                .Generate();

//            _purchaseRepositoryMock.Setup(p => p.GetById(purchase.Id)).ReturnsAsync(purchase);
//            _purchaseRepositoryMock.Setup(p => p.Delete(It.IsAny<Purchase>())).ReturnsAsync(Notifies.Success());

//            var result = await _purchaseService.DeletePurchase(purchase.Id);

//            Assert.True(result.Status);
//        }

//        [Fact]
//        public async Task DeletePurchase_ShouldBeErrorByPurchase()
//        {
//            var purchaseId = 1;

//            _purchaseRepositoryMock.Setup(p => p.GetById(purchaseId)).ReturnsAsync((Purchase)null);

//            var result = await _purchaseService.DeletePurchase(purchaseId);

//            Assert.False(result.Status);
//            Assert.Equal("Purchase not found", result.Message);
//        }

//        [Fact]
//        public async Task DeletePurchase_ShouldBeErrorByApprovedPurchase()
//        {
//            var purchase = new Faker<Purchase>()
//                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
//                .RuleFor(p => p.IsApproved, true)
//                .Generate();

//            _purchaseRepositoryMock.Setup(p => p.GetById(purchase.Id)).ReturnsAsync(purchase);

//            var result = await _purchaseService.DeletePurchase(purchase.Id);

//            Assert.False(result.Status);
//            Assert.Equal("Purchase already approved", result.Message);
//        }

//        [Fact]
//        public async Task GetPurchaseById_ShouldBeOk()
//        {
//            var purchase = new Faker<Purchase>()
//                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
//                .Generate();

//            _purchaseRepositoryMock.Setup(p => p.GetById(purchase.Id)).ReturnsAsync(purchase);

//            var result = await _purchaseService.GetPurchaseById(purchase.Id);

//            Assert.NotNull(result);
//        }

//        [Fact]
//        public async Task GetPurchaseById_ShouldBeErrorByInvalidId()
//        {
//            var purchaseId = 0;

//            var result = await _purchaseService.GetPurchaseById(purchaseId);

//            Assert.Null(result);
//        }

//        [Fact]
//        public async Task GetByUser_ShouldBeOk()
//        {
//            var user = new Faker<User>()
//                .RuleFor(u => u.Id, f => f.Random.Int(1, 1000))
//                .Generate();

//            var purchases = new Faker<Purchase>()
//                .RuleFor(p => p.ClientId, user.Id)
//                .Generate(5);

//            _userServiceMock.Setup(u => u.GetUserById(user.Id)).ReturnsAsync(user);
//            _purchaseRepositoryMock.Setup(p => p.GetByUser(user.Id)).ReturnsAsync(purchases);

//            var result = await _purchaseService.GetByUser(user.Id);

//            Assert.NotEmpty(result);
//        }

//        [Fact]
//        public async Task GetByUser_ShouldReturnEmptyListIfUserNotFound()
//        {
//            var userId = 1;

//            _userServiceMock.Setup(u => u.GetUserById(userId)).ReturnsAsync((User)null);

//            var result = await _purchaseService.GetByUser(userId);

//            Assert.Empty(result);
//        }

//        [Fact]
//        public async Task GetAllPurchases_ShouldBeOk()
//        {
//            var purchases = new Faker<Purchase>()
//                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
//                .Generate(5);

//            _purchaseRepositoryMock.Setup(p => p.GetAll()).ReturnsAsync(purchases);

//            var result = await _purchaseService.GetAllPurchases();

//            Assert.NotEmpty(result);
//        }
//    }
//}