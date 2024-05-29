using Bogus;
using Domain.Entities;
using Domain.Interfaces.Interfaceservices;
using Domain.Interfaces.InterfacesRepositories;
using Domain.Notifications;
using Moq;
using Services;

namespace PrivatePension.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IPurchaseService> _purchaseServiceMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _userServiceMock = new Mock<IUserService>();
            _purchaseServiceMock = new Mock<IPurchaseService>();
            _productService = new ProductService(_productRepositoryMock.Object, _userServiceMock.Object, _purchaseServiceMock.Object);
        }

        [Fact]
        public async Task AddProduct_ShouldAddProduct()
        {
            var newProduct = new Faker<Product>()
                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                .RuleFor(p => p.Available, f => f.Random.Bool())
                .Generate();

            _productRepositoryMock.Setup(r => r.Add(It.IsAny<Product>())).ReturnsAsync(Notifies.Success());

            var result = await _productService.AddProduct(newProduct);

            _productRepositoryMock.Verify(r => r.Add(newProduct), Times.Once);
            Assert.True(result.Status);
        }

        [Fact]
        public async Task AddProduct_ShouldNotAddProductWithEmptyName()
        {
            var newProduct = new Faker<Product>()
                           .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                           .RuleFor(p => p.Name, f => string.Empty)
                           .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                           .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                           .RuleFor(p => p.Available, f => f.Random.Bool())
                           .Generate();
            var result = await _productService.AddProduct(newProduct);

            _productRepositoryMock.Verify(r => r.Add(It.IsAny<Product>()), Times.Never);
            Assert.False(result.Status);
        }

        [Fact]
        public async Task AddProduct_ShouldNotAddProductWithNegativePrice()
        {
            var newProduct = new Faker<Product>()
                           .RuleFor(p => p.Id, f => f.Random.Int(1, 100))
                           .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                           .RuleFor(p => p.Price, f => f.Random.Decimal(-1000, 0))
                           .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                           .RuleFor(p => p.Available, f => f.Random.Bool())
                           .Generate();
            var result = await _productService.AddProduct(newProduct);

            _productRepositoryMock.Verify(r => r.Add(It.IsAny<Product>()), Times.Never);
            Assert.False(result.Status);
        }

        [Fact]
        public async Task AddProduct_ShouldNotAddProductWithZeroPrice()
        {
            var newProduct = new Faker<Product>()
                           .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                           .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                           .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                           .RuleFor(p => p.Available, f => f.Random.Bool())
                           .Generate();
            newProduct.Price = 0;
            var result = await _productService.AddProduct(newProduct);

            _productRepositoryMock.Verify(r => r.Add(It.IsAny<Product>()), Times.Never);
            Assert.False(result.Status);
        }

        [Fact]
        public async Task AddProduct_ShouldNotAddProductWithEmptyDescription()
        {
            var newProduct = new Faker<Product>()
                           .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                           .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                           .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                           .RuleFor(p => p.Available, f => f.Random.Bool())
                           .Generate();
            var result = await _productService.AddProduct(newProduct);

            _productRepositoryMock.Verify(r => r.Add(It.IsAny<Product>()), Times.Never);
            Assert.False(result.Status);
        }

        [Fact]
        public async Task UpdateProduct_ShouldUpdateProduct()
        {
            var existingProduct = new Faker<Product>()
                           .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                           .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                           .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                           .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                           .RuleFor(p => p.Available, f => f.Random.Bool())
                           .Generate();
            _productRepositoryMock.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(Notifies.Success());

            var result = await _productService.UpdateProduct(existingProduct);

            _productRepositoryMock.Verify(r => r.Update(existingProduct), Times.Once);
            Assert.True(result.Status);
        }

        [Fact]
        public async Task UpdateProduct_ShouldNotUpdateProductWithEmptyName()
        {
            var existingProduct = new Faker<Product>()
                                      .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                                      .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                                      .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                                      .RuleFor(p => p.Available, f => f.Random.Bool())
                                      .Generate();
            _productRepositoryMock.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(Notifies.Success());

            var result = await _productService.UpdateProduct(existingProduct);

            _productRepositoryMock.Verify(r => r.Update(It.IsAny<Product>()), Times.Never);
            Assert.False(result.Status);
        }

        [Fact]
        public async Task UpdateProduct_ShouldNotUpdateProductWithNegativePrice()
        {
            var existingProduct = new Faker<Product>()
                                      .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                                      .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                                      .RuleFor(p => p.Price, f => f.Random.Decimal(-1000, -1))
                                      .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                                      .RuleFor(p => p.Available, f => f.Random.Bool())
                                      .Generate();
            _productRepositoryMock.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(Notifies.Success());

            var result = await _productService.UpdateProduct(existingProduct);

            _productRepositoryMock.Verify(r => r.Update(It.IsAny<Product>()), Times.Never);
            Assert.False(result.Status);
        }

        [Fact]
        public async Task UpdateProduct_ShouldNotUpdateProductWithZeroPrice()
        {
            var existingProduct = new Faker<Product>()
                                      .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                                      .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                                      .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                                      .RuleFor(p => p.Available, f => f.Random.Bool())
                                      .Generate();
            existingProduct.Price = 0;
            _productRepositoryMock.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(Notifies.Success());

            var result = await _productService.UpdateProduct(existingProduct);

            _productRepositoryMock.Verify(r => r.Update(It.IsAny<Product>()), Times.Never);
            Assert.False(result.Status);
        }

        [Fact]
        public async Task UpdateProduct_ShouldNotUpdateProductWithEmptyDescription()
        {
            var existingProduct = new Faker<Product>()
                                      .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                                      .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                                      .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                                      .RuleFor(p => p.Available, f => f.Random.Bool())
                                      .Generate();
            _productRepositoryMock.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(Notifies.Success());

            var result = await _productService.UpdateProduct(existingProduct);

            _productRepositoryMock.Verify(r => r.Update(It.IsAny<Product>()), Times.Never);
            Assert.False(result.Status);
        }

        [Fact]
        public async Task DeleteProduct_ShouldDeleteProduct()
        {
            var existingProduct = new Faker<Product>()
                                      .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                                      .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                                      .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                                      .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                                      .RuleFor(p => p.Available, f => f.Random.Bool())
                                      .Generate();
            _productRepositoryMock.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(Notifies.Success());

            _productRepositoryMock.Setup(r => r.GetById(existingProduct.Id)).ReturnsAsync(existingProduct);
            _productRepositoryMock.Setup(r => r.Delete(It.IsAny<Product>())).ReturnsAsync(Notifies.Success());

            var result = await _productService.DeleteProduct(existingProduct.Id);

            _productRepositoryMock.Verify(r => r.Delete(existingProduct), Times.Once);
            Assert.True(result.Status);
        }

        [Fact]
        public async Task DeleteProductById_ShouldDeleteProduct()
        {
            var productId = 1;
            var existingProduct = new Faker<Product>()
                                      .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                                      .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                                      .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                                      .RuleFor(p => p.Available, f => f.Random.Bool())
                                      .Generate();
            existingProduct.Id = productId;
            _productRepositoryMock.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(Notifies.Success());

            _productRepositoryMock.Setup(r => r.GetById(productId)).ReturnsAsync(existingProduct);
            _productRepositoryMock.Setup(r => r.Delete(It.IsAny<Product>())).ReturnsAsync(Notifies.Success());

            var result = await _productService.DeleteProduct(productId);

            _productRepositoryMock.Verify(r => r.Delete(existingProduct), Times.Once);
            Assert.True(result.Status);
        }

        [Fact]
        public async Task DeleteProductById_ShouldNotDeleteProductWithNonExistentId()
        {
            var productId = 1;

            _productRepositoryMock.Setup(r => r.GetById(productId)).ReturnsAsync((Product)null);

            var result = await _productService.DeleteProduct(productId);

            _productRepositoryMock.Verify(r => r.Delete(It.IsAny<Product>()), Times.Never);
            Assert.False(result.Status);
        }

        [Fact]
        public async Task GetProductById_ShouldGetProduct()
        {
            var productId = 1;
            var existingProduct = new Faker<Product>()
                                                  .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                                                  .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                                                  .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                                                  .RuleFor(p => p.Available, f => f.Random.Bool())
                                                  .Generate();
            existingProduct.Id = productId;
            _productRepositoryMock.Setup(r => r.GetById(productId)).ReturnsAsync(existingProduct);

            var result = await _productService.GetProductById(productId);

            _productRepositoryMock.Verify(r => r.GetById(productId), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetProductById_ShouldNotGetProductWithNonExistentId()
        {
            var productId = 1;

            _productRepositoryMock.Setup(r => r.GetById(productId)).ReturnsAsync((Product)null);

            var result = await _productService.GetProductById(productId);

            _productRepositoryMock.Verify(r => r.GetById(productId), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllProducts_ShouldGetAllProducts()
        {
            var products = new Faker<Product>()
                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                .RuleFor(p => p.Available, f => f.Random.Bool())
                .Generate(5);

            _productRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(products);

            var result = await _productService.GetAllProducts();

            _productRepositoryMock.Verify(r => r.GetAll(), Times.Once);
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnEmptyListIfNoProducts()
        {
            _productRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<Product>());

            var result = await _productService.GetAllProducts();

            _productRepositoryMock.Verify(r => r.GetAll(), Times.Once);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByAvailable_ShouldGetAvailableProducts()
        {
            var products = new Faker<Product>()
                .RuleFor(p => p.Id, f => f.Random.Int(1, 1000))
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
                .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                .RuleFor(p => p.Available, true)
                .Generate(5);

            _productRepositoryMock.Setup(r => r.GetByAvailable(true)).ReturnsAsync(products);

            var result = await _productService.GetByAvailable(true);

            _productRepositoryMock.Verify(r => r.GetByAvailable(true), Times.Once);
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task GetByAvailable_ShouldReturnEmptyListIfNoProductsAvailable()
        {
            _productRepositoryMock.Setup(r => r.GetByAvailable(true)).ReturnsAsync(new List<Product>());

            var result = await _productService.GetByAvailable(true);

            _productRepositoryMock.Verify(r => r.GetByAvailable(true), Times.Once);
            Assert.Empty(result);
        }
    }
}
