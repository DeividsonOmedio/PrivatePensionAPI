using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatePension.Tests
{
    public class PurchaseServiceTest
    {
        //add Purchase e dar certo
        [Fact]
        public void AddPurchase_ShouldBeOk()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.AddPurchase(purchase);

            //Assert
            Assert.True(result.status);
        }

        //add Purchase e dar erro por CustomerId inexistente
        [Fact]
        public void AddPurchase_ShouldBeErrorByCustomerId()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 0,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.AddPurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("CustomerId inválido", result.message);
        }

        //add Purchase e dar erro por Amount inválido
        [Fact]
        public void AddPurchase_ShouldBeErrorByAmount()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 0,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.AddPurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Amount inválido", result.message);
        }

        //add Purchase e dar erro por PurchaseDate inválida
        [Fact]
        public void AddPurchase_ShouldBeErrorByPurchaseDate()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.MinValue
            };

            //Act
            var result = _purchaseService.AddPurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("PurchaseDate inválida", result.message);
        }

        //add Purchase e dar erro por Amount insuficiente
        [Fact]
        public void AddPurchase_ShouldBeErrorByAmountInsufficient()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 100,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.AddPurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Amount insuficiente", result.message);
        }

        //add Purchase e dar erro por Cliente inexistente
        [Fact]
        public void AddPurchase_ShouldBeErrorByCustomer()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 0,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.AddPurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Cliente inválido", result.message);
        }

        //add Purchase e dar erro por Produto inexistente
        [Fact]
        public void AddPurchase_ShouldBeErrorByProduct()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.AddPurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Produto inválido", result.message);
        }

        //add Purchase e dar erro por Purchase já existente
        [Fact]
        public void AddPurchase_ShouldBeErrorByPurchaseAlreadyExists()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.AddPurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Compra já existente", result.message);
        }

        //update Purchase e dar certo
        [Fact]
        public void UpdatePurchase_ShouldBeOk()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.UpdatePurchase(purchase);

            //Assert
            Assert.True(result.status);
        }

        //update Purchase e dar erro por Purchase inexistente
        [Fact]
        public void UpdatePurchase_ShouldBeErrorByPurchase()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 0,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.UpdatePurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Compra inexistente", result.message);
        }

        //update Purchase e dar erro por CustomerId inexistente
        [Fact]
        public void UpdatePurchase_ShouldBeErrorByCustomerId()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 0,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.UpdatePurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("CustomerId inválido", result.message);
        }

        //update Purchase e dar erro por Amount inválido
        [Fact]
        public void UpdatePurchase_ShouldBeErrorByAmount()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 0,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.UpdatePurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Amount inválido", result.message);
        }

        //update Purchase e dar erro por PurchaseDate inválida
        [Fact]
        public void UpdatePurchase_ShouldBeErrorByPurchaseDate()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.MinValue
            };

            //Act
            var result = _purchaseService.UpdatePurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("PurchaseDate inválida", result.message);
        }

        //update Purchase e dar erro por Amount insuficiente
        [Fact]
        public void UpdatePurchase_ShouldBeErrorByAmountInsufficient()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 100,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.UpdatePurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Amount insuficiente", result.message);
        }

        //update Purchase e dar erro por Cliente inexistente
        [Fact]
        public void UpdatePurchase_ShouldBeErrorByCustomer()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 0,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.UpdatePurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Cliente inválido", result.message);
        }

        //update Purchase e dar erro por Produto inexistente
        [Fact]
        public void UpdatePurchase_ShouldBeErrorByProduct()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.UpdatePurchase(purchase);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Produto inválido", result.message);
        }

        //delete Purchase por Id e dar certo
        [Fact]
        public void DeletePurchaseById_ShouldBeOk()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.DeletePurchaseById(purchase.Id);

            //Assert
            Assert.True(result.status);
        }

        //delete Purchase por Id e dar erro por Purchase inexistente
        [Fact]
        public void DeletePurchaseById_ShouldBeErrorByPurchase()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 0,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.DeletePurchaseById(purchase.Id);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Compra inexistente", result.message);
        }

        //get Purchase por Id e dar certo
        [Fact]
        public void GetPurchaseById_ShouldBeOk()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.GetPurchaseById(purchase.Id);

            //Assert
            Assert.True(result.status);
        }

        //get Purchase por Id e dar erro por Purchase inexistente
        [Fact]
        public void GetPurchaseById_ShouldBeErrorByPurchase()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 0,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.GetPurchaseById(purchase.Id);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Compra inexistente", result.message);
        }

        //get Purchase por CustomerId e dar certo
        [Fact]
        public void GetPurchaseByCustomerId_ShouldBeOk()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.GetPurchaseByCustomerId(purchase.CustomerId);

            //Assert
            Assert.True(result.status);
        }

        //get Purchase por CustomerId e dar erro por Purchase inexistente
        [Fact]
        public void GetPurchaseByCustomerId_ShouldBeErrorByPurchase()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 0,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.GetPurchaseByCustomerId(purchase.CustomerId);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Compra inexistente", result.message);
        }

        //get Purchase por CustomerId e dar erro por Customer inexistente
        [Fact]
        public void GetPurchaseByCustomerId_ShouldBeErrorByCustomer()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 0,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.GetPurchaseByCustomerId(purchase.CustomerId);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Cliente inválido", result.message);
        }

        //get Purchase por ProductId e dar certo
        [Fact]
        public void GetPurchaseByProductId_ShouldBeOk()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.GetPurchaseByProductId(purchase.ProductId);

            //Assert
            Assert.True(result.status);
        }

        //get Purchase por ProductId e dar erro por Purchase inexistente
        [Fact]
        public void GetPurchaseByProductId_ShouldBeErrorByPurchase()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 0,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.GetPurchaseByProductId(purchase.ProductId);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Compra inexistente", result.message);
        }

        // get all Purchases e dar certo
        [Fact]
        public void GetAllPurchases_ShouldBeOk()
        {
            //Arrange
            var purchase = new Purchase
            {
                Id = 1,
                CustomerId = 1,
                Amount = 1000,
                PurchaseDate = DateTime.Now
            };

            //Act
            var result = _purchaseService.GetAllPurchases();

            //Assert
            Assert.True(result.status);
        }

        //get all Purchase e retornar lista vazia
        [Fact]
        public void GetAllPurchases_ShouldBeEmpty()
        {
            //Act
            var result = _purchaseService.GetAllPurchases();

            //Assert
            Assert.False(result.status);
            Assert.Empty(result.purchases);
        }

    }
}
