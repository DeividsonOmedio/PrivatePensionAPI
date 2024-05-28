using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatePension.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public void CreateProduct_ShouldAddProduct()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var newProduct = new Product { Id = 1, Name = "Produto A", Price = 100m , Description = "teste", Available = true};

            // Act
            productService.CreateProductAsync(newProduct);

            // Assert
            productRepositoryMock.Verify(r => r.AddAsync(newProduct), Times.Once);
        }

        //criar produto mas com nome vazio e nao adicionar
        [Fact]
        public void CreateProduct_ShouldNotAddProductWithEmptyName()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var newProduct = new Product { Id = 1, Name = "", Price = 100m, Description = "teste", Available = true };

            productService.CreateProductAsync(newProduct);

            productRepositoryMock.Verify(r => r.AddAsync(newProduct), Times.Never);
        }

        //criar produto mas com preco negativo e nao adicionar
        [Fact]
        public void CreateProduct_ShouldNotAddProductWithNegativePrice()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var newProduct = new Product { Id = 1, Name = "Produto A", Price = -100m, Description = "teste", Available = true };

            productService.CreateProductAsync(newProduct);

            productRepositoryMock.Verify(r => r.AddAsync(newProduct), Times.Never);
        }

        //criar produto mas com preco zero e nao adicionar
        [Fact]
        public void CreateProduct_ShouldNotAddProductWithZeroPrice()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var newProduct = new Product { Id = 1, Name = "Produto A", Price = 0, Description = "teste", Available = true };

            productService.CreateProductAsync(newProduct);

            productRepositoryMock.Verify(r => r.AddAsync(newProduct), Times.Never);
        }

        //criar produto mas com descricao vazia e nao adicionar
        [Fact]
        public void CreateProduct_ShouldNotAddProductWithEmptyDescription()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var newProduct = new Product { Id = 1, Name = "Produto A", Price = 100m, Description = "", Available = true };

            productService.CreateProductAsync(newProduct);

            productRepositoryMock.Verify(r => r.AddAsync(newProduct), Times.Never);
        }

        //update produto e adicionar
        [Fact]
        public void UpdateProduct_ShouldUpdateProduct()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var newProduct = new Product { Id = 1, Name = "Produto A", Price = 100m, Description = "teste", Available = true };

            productService.UpdateProductAsync(newProduct);

            productRepositoryMock.Verify(r => r.UpdateAsync(newProduct), Times.Once);
        }

        //update produto mas com nome vazio e nao adicionar
        [Fact]
        public void UpdateProduct_ShouldNotUpdateProductWithEmptyName()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var newProduct = new Product { Id = 1, Name = "", Price = 100m, Description = "teste", Available = true };

            productService.UpdateProductAsync(newProduct);

            productRepositoryMock.Verify(r => r.UpdateAsync(newProduct), Times.Never);
        }

        //update produto mas com preco negativo e nao adicionar
        [Fact]
        public void UpdateProduct_ShouldNotUpdateProductWithNegativePrice()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var newProduct = new Product { Id = 1, Name = "Produto A", Price = -100m, Description = "teste", Available = true };

            productService.UpdateProductAsync(newProduct);

            productRepositoryMock.Verify(r => r.UpdateAsync(newProduct), Times.Never);
        }

        //update produto mas com preco zero e nao adicionar
        [Fact]
        public void UpdateProduct_ShouldNotUpdateProductWithZeroPrice()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var newProduct = new Product { Id = 1, Name = "Produto A", Price = 0, Description = "teste", Available = true };

            productService.UpdateProductAsync(newProduct);

            productRepositoryMock.Verify(r => r.UpdateAsync(newProduct), Times.Never);
        }

        //update produto mas com descricao vazia e nao adicionar
        [Fact]
        public void UpdateProduct_ShouldNotUpdateProductWithEmptyDescription()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var newProduct = new Product { Id = 1, Name = "Produto A", Price = 100m, Description = "", Available = true };

            productService.UpdateProductAsync(newProduct);

            productRepositoryMock.Verify(r => r.UpdateAsync(newProduct), Times.Never);
        }

        //delete produto e adicionar
        [Fact]
        public void DeleteProduct_ShouldDeleteProduct()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var newProduct = new Product { Id = 1, Name = "Produto A", Price = 100m, Description = "teste", Available = true };

            productService.DeleteProductAsync(newProduct);

            productRepositoryMock.Verify(r => r.DeleteAsync(newProduct), Times.Once);
        }

        //delete produto by id e dar certo
        [Fact]
        public void DeleteProductById_ShouldDeleteProduct()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var id = 1;

            productService.DeleteProductByIdAsync(id);

            productRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        //delete produto by id e id não existente
        [Fact]
        public void DeleteProductById_ShouldNotDeleteProductWithNonExistentId()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var id = 1;

            productService.DeleteProductByIdAsync(id);

            productRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Never);
        }

        //get produto by id e dar certo
        [Fact]
        public void GetProductById_ShouldGetProduct()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var id = 1;

            productService.GetProductByIdAsync(id);

            productRepositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        //get produto by id e id não existente
        [Fact]
        public void GetProductById_ShouldNotGetProductWithNonExistentId()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);
            var id = 1;

            productService.GetProductByIdAsync(id);

            productRepositoryMock.Verify(r => r.GetByIdAsync(id), Times.Never);
        }

        //get todos os produtos e dar certo
        [Fact]
        public void GetAllProducts_ShouldGetAllProducts()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);

            productService.GetAllProductsAsync();

            productRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        //get todos os produtos e não existir nenhum
        [Fact]
        public void GetAllProducts_ShouldNotGetAllProductsIfThereAreNone()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);

            productService.GetAllProductsAsync();

            productRepositoryMock.Verify(r => r.GetAllAsync(), Times.Never);
        }

        //get todos os produtos disponiveis e dar certo
        [Fact]
        public void GetAvailableProducts_ShouldGetAvailableProducts()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);

            productService.GetAvailableProductsAsync();

            productRepositoryMock.Verify(r => r.GetAvailableProductsAsync(), Times.Once);
        }

        //get todos os produtos disponiveis e não existir nenhum
        [Fact]
        public void GetAvailableProducts_ShouldNotGetAvailableProductsIfThereAreNone()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var productService = new ProductService(productRepositoryMock.Object);

            productService.GetAvailableProductsAsync();

            productRepositoryMock.Verify(r => r.GetAvailableProductsAsync(), Times.Never);
        }

    }
}
