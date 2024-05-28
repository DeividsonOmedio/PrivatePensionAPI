using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatePension.Tests
{
    public class ContributionServiceTest
    {

        //add Contribution e dar certo
        [Fact]
        public void AddContribution_ShouldBeOk()
        {
            //Arrange
            var contribution = new Contribution
            {
                Id = 1,
                PurchaseId = 1,
                Amount = 1000,
                ContributionDate = DateTime.Now
            };

            //Act
            var result = _contributionService.AddContribution(contribution);

            //Assert
            Assert.True(result.status);
        }

        //add Contribution e dar erro por PurchaseId inexistente
        [Fact]
        public void AddContribution_ShouldBeErrorByPurchaseId()
        {
            //Arrange
            var contribution = new Contribution
            {
                Id = 1,
                PurchaseId = 0,
                Amount = 1000,
                ContributionDate = DateTime.Now
            };

            //Act
            var result = _contributionService.AddContribution(contribution);

            //Assert
            Assert.False(result.status);
            Assert.Equal("PurchaseId inválido", result.message);
        }

        //add Contribution e dar erro por Amount inválido
        [Fact]
        public void AddContribution_ShouldBeErrorByAmount()
        {
            //Arrange
            var contribution = new Contribution
            {
                Id = 1,
                PurchaseId = 1,
                Amount = 0,
                ContributionDate = DateTime.Now
            };

            //Act
            var result = _contributionService.AddContribution(contribution);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Amount inválido", result.message);
        }

        //add Contribution e dar erro por Amount insuficiente
        [Fact]
        public void AddContribution_ShouldBeErrorByAmountInsufficient()
        {
            //Arrange
            var contribution = new Contribution
            {
                Id = 1,
                PurchaseId = 1,
                Amount = 10000,
                ContributionDate = DateTime.Now
            };

            //Act
            var result = _contributionService.AddContribution(contribution);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Amount insuficiente", result.message);
        }

        //add Contribution e dar erro por ContributionDate inválido
        [Fact]
        public void AddContribution_ShouldBeErrorByContributionDate()
        {
            //Arrange
            var contribution = new Contribution
            {
                Id = 1,
                PurchaseId = 1,
                Amount = 1000,
                ContributionDate = DateTime.MinValue
            };

            //Act
            var result = _contributionService.AddContribution(contribution);

            //Assert
            Assert.False(result.status);
            Assert.Equal("ContributionDate inválido", result.message);
        }

        //update Contribution e dar certo
        [Fact]
        public void UpdateContribution_ShouldBeOk()
        {
            //Arrange
            var contribution = new Contribution
            {
                Id = 1,
                PurchaseId = 1,
                Amount = 1000,
                ContributionDate = DateTime.Now
            };

            //Act
            var result = _contributionService.UpdateContribution(contribution);

            //Assert
            Assert.True(result.status);
        }

        //update Contribution e dar erro por PurchaseId inexistente
        [Fact]
        public void UpdateContribution_ShouldBeErrorByPurchaseId()
        {
            //Arrange
            var contribution = new Contribution
            {
                Id = 1,
                PurchaseId = 0,
                Amount = 1000,
                ContributionDate = DateTime.Now
            };

            //Act
            var result = _contributionService.UpdateContribution(contribution);

            //Assert
            Assert.False(result.status);
            Assert.Equal("PurchaseId inválido", result.message);
        }

        //update Contribution e dar erro por Amount insuficiente
        [Fact]
        public void UpdateContribution_ShouldBeErrorByAmountInsufficient()
        {
            //Arrange
            var contribution = new Contribution
            {
                Id = 1,
                PurchaseId = 1,
                Amount = 10000,
                ContributionDate = DateTime.Now
            };

            //Act
            var result = _contributionService.UpdateContribution(contribution);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Amount insuficiente", result.message);
        }

        //update Contribution e dar erro por ContributionDate inválido
        [Fact]
        public void UpdateContribution_ShouldBeErrorByContributionDate()
        {
            //Arrange
            var contribution = new Contribution
            {
                Id = 1,
                PurchaseId = 1,
                Amount = 1000,
                ContributionDate = DateTime.MinValue
            };

            //Act
            var result = _contributionService.UpdateContribution(contribution);

            //Assert
            Assert.False(result.status);
            Assert.Equal("ContributionDate inválido", result.message);
        }

        //delete Contribution por id e dar certo
        [Fact]
        public void DeleteContributionById_ShouldBeOk()
        {
            //Arrange
            var id = 1;

            //Act
            var result = _contributionService.DeleteContributionById(id);

            //Assert
            Assert.True(result.status);
        }

        //delete Contribution por id inexistente e dar erro por id inexistente
        [Fact]
        public void DeleteContributionById_ShouldBeErrorById()
        {
            //Arrange
            var id = 0;

            //Act
            var result = _contributionService.DeleteContributionById(id);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Id inválido", result.message);
        }

        //get Contribution por id e dar certo
        [Fact]
        public void GetContributionById_ShouldBeOk()
        {
            //Arrange
            var id = 1;

            //Act
            var result = _contributionService.GetContributionById(id);

            //Assert
            Assert.True(result.status);
        }

        //get Contribution por id inexistente e dar erro por id inexistente
        [Fact]
        public void GetContributionById_ShouldBeErrorById()
        {
            //Arrange
            var id = 0;

            //Act
            var result = _contributionService.GetContributionById(id);

            //Assert
            Assert.False(result.status);
            Assert.Equal("Id inválido", result.message);
        }

        //get all Contributions e dar certo
        [Fact]
        public void GetAllContributions_ShouldBeOk()
        {
            //Act
            var result = _contributionService.GetAllContributions();

            //Assert
            Assert.True(result.status);
        }

        //get all Contributions e dar erro por não ter Contributions
        [Fact]
        public void GetAllContributions_ShouldBeErrorByContributions()
        {
            //Arrange
            _contributionService.DeleteAllContributions();

            //Act
            var result = _contributionService.GetAllContributions();

            //Assert
            Assert.False(result.status);
            Assert.Equal("Não há Contributions", result.message);
        }

        //get Contributions por PurchaseId e dar certo
        [Fact]
        public void GetContributionsByPurchaseId_ShouldBeOk()
        {
            //Arrange
            var purchaseId = 1;

            //Act
            var result = _contributionService.GetContributionsByPurchaseId(purchaseId);

            //Assert
            Assert.True(result.status);
        }

       

    }
}
