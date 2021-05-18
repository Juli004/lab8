using System;
using Xunit;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System.Linq;

namespace DAL.Tests
{
    public class infoRepositoryInMemoryDBTests
    {
        public CatalogContext Context => SqlLiteInMemoryContext();

        private CatalogContext SqlLiteInMemoryContext()
        {

            var options = new DbContextOptionsBuilder<CatalogContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new CatalogContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void Create_InputinfoWithId0_SetinfoId1()
        {
         
            int expectedListCount = 1;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.infoRepository repository = uow.infos;

            info info = new info()
            {
                CatalogID = 4,
                Name = "testN",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 4}
            };

            repository.Create(info);
            uow.Save();
            var factListCount = context.infos.Count();

          
            Assert.Equal(expectedListCount, factListCount);
        }

        [Fact]
        public void Delete_InputExistinfoId_Removed()
        {
          
            int expectedListCount = 0;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.infoRepository repository = uow.infos;
            info info = new info()
            {
                
                CatalogID = 4,
                Name = "testN",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 4 }
            };
            context.infos.Add(info);
            context.SaveChanges();

        
            repository.Delete(info.infoID);
            uow.Save();
            var factinfoCount = context.infos.Count();

          
            Assert.Equal(expectedListCount, factinfoCount);
        }

        [Fact]
        public void Get_InputExistinfoId_Returninfo()
        {
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.infoRepository repository = uow.infos;
            info expectedinfo = new info()
            {
                
                CatalogID = 4,
                Name = "testN",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 4 }
            };
            context.infos.Add(expectedinfo);
            context.SaveChanges();

            
            var factinfo = repository.Get(expectedinfo.infoID);

            
            Assert.Equal(expectedinfo, factinfo);
        }
    }
}
