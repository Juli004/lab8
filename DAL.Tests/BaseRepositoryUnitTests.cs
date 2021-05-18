using System;
using Xunit;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System.Linq;
using Moq;

namespace DAL.Tests
{
    class TestinfoRepository
        : BaseRepository<info>
    {
        public TestinfoRepository(DbContext context) 
            : base(context)
        {
        }
    }

    public class BaseRepositoryUnitTests
    {

        [Fact]
        public void Create_InputinfoInstance_CalledAddMethodOfDBSetWithinfoInstance()
        {
            
            DbContextOptions opt = new DbContextOptionsBuilder<CatalogContext>()
                .Options;
            var mockContext = new Mock<CatalogContext>(opt);
            var mockDbSet = new Mock<DbSet<info>>();
            mockContext
                .Setup(context => 
                    context.Set<info>(
                        ))
                .Returns(mockDbSet.Object);
            
            var repository = new TestinfoRepository(mockContext.Object);

            info expectedinfo = new Mock<info>().Object;

          
            repository.Create(expectedinfo);

            
            mockDbSet.Verify(
                dbSet => dbSet.Add(
                    expectedinfo
                    ), Times.Once());
        }

        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            
            DbContextOptions opt = new DbContextOptionsBuilder<CatalogContext>()
                .Options;
            var mockContext = new Mock<CatalogContext>(opt);
            var mockDbSet = new Mock<DbSet<info>>();
            mockContext
                .Setup(context =>
                    context.Set<info>(
                        ))
                .Returns(mockDbSet.Object);
          
            var repository = new TestinfoRepository(mockContext.Object);

            info expectedinfo = new info() { infoID = 1};
            mockDbSet.Setup(mock => mock.Find(expectedinfo.infoID)).Returns(expectedinfo);

           
            repository.Delete(expectedinfo.infoID);

            
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedinfo.infoID
                    ), Times.Once());
            mockDbSet.Verify(
                dbSet => dbSet.Remove(
                    expectedinfo
                    ), Times.Once());
        }

        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
           
            DbContextOptions opt = new DbContextOptionsBuilder<CatalogContext>()
                .Options;
            var mockContext = new Mock<CatalogContext>(opt);
            var mockDbSet = new Mock<DbSet<info>>();
            mockContext
                .Setup(context =>
                    context.Set<info>(
                        ))
                .Returns(mockDbSet.Object);

            info expectedinfo = new info() { infoID = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedinfo.infoID))
                    .Returns(expectedinfo);
            var repository = new TestinfoRepository(mockContext.Object);

          
            var actualinfo = repository.Get(expectedinfo.infoID);

            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedinfo.infoID
                    ), Times.Once());
            Assert.Equal(expectedinfo, actualinfo);
        }

      
    }
}
