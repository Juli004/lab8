using Catalog.BLL.Services.Impl;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.EF;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using Catalog.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using OSBB.Security;
using OSBB.Security.Identity;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace BLL.Tests
{
    public class infoServiceTests
    {

        [Fact]
        public void Ctor_InputNull_ThrowArgumentNullException()
        {
            
            IUnitOfWork nullUnitOfWork = null;

            Assert.Throws<ArgumentNullException>(() => new InfoService(nullUnitOfWork));
        }

        [Fact]
        public void GetProducts_UserIsAdmin_ThrowMethodAccessException()
        {
            
            Client user = new SSi(1, "test", 1);
            SecurityContext.SetUser(user);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            IinfoService productService = new InfoService(mockUnitOfWork.Object);

            
        }

        [Fact]
        public void GetProducts_ProductFromDAL_CorrectMappingToProductDTO()
        {
            // Arrange
            Client user = new Admin(1, "test", 1);
            SecurityContext.SetUser(user);
            var productService = Getinfos();

            // Act
            var actualproductDto = productService.Getinfos(0).First();

            // Assert
            Assert.True(
                actualproductDto.infoID == 1
                && actualproductDto.Name == "testN"
                && actualproductDto.Description == "testD"
                );
        }

        IinfoService Getinfos()
        {
            var mockContext = new Mock<IUnitOfWork>();
            var expectedProduct = new info() { infoID = 1, Name = "testN", Description = "testD", CatalogID = 2};
            var mockDbSet = new Mock<infoRepository>();
            mockDbSet.Setup(z => 
                z.Find(
                    It.IsAny<Func<info, bool>>(), 
                    It.IsAny<int>(), 
                    It.IsAny<int>()))
                  .Returns(
                    new List<info>() { expectedProduct }
                    );
            mockContext
                .Setup(context =>
                    context.infos)
                .Returns(mockDbSet.Object);

            IinfoService productService = new InfoService(mockContext.Object);

            return productService;
        }
    }
}
