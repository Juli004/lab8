using Catalog.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.Entities;
using Catalog.BLL.DTO;
using Catalog.DAL.Repositories.Interfaces;
using AutoMapper;
using Catalog.DAL.UnitOfWork;
using OSBB.Security;
using OSBB.Security.Identity;

namespace Catalog.BLL.Services.Impl
{
    public class InfoService
        : IinfoService
    {
        private readonly IUnitOfWork _database;
        private int pageSize = 10;

        public InfoService( 
            IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(
                    nameof(unitOfWork));
            }
            _database = unitOfWork;
        }

        /// <exception cref="MethodAccessException"></exception>
        public IEnumerable<infoDTO> Getinfos(int pageNumber)
        {
            var user = SecurityContext.GetUser();
            //var userType = user.GetType();
            var catalogID = user.CatalogID;
            var streetsEntities = 
                _database
                    .infos
                    .Find(z => z.infoID == catalogID, pageNumber, pageSize);
            var mapper = 
                new MapperConfiguration(
                    cfg => cfg.CreateMap<info, infoDTO>()
                    ).CreateMapper();
            var streetsDto = 
                mapper
                    .Map<IEnumerable<info>, List<infoDTO>>(
                        streetsEntities);
            return streetsDto;
        }

        public void AddStreet(infoDTO street)
        {
          
            if (street == null)
            {
                throw new ArgumentNullException(nameof(street));
            }

            validate(street);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<infoDTO, info>()).CreateMapper();
            var streetEntity = mapper.Map<infoDTO, info>(street);
            _database.infos.Create(streetEntity);
        }

        private void validate(infoDTO street)
        {
            if (string.IsNullOrEmpty(street.Name))
            {
                throw new ArgumentException("Name повинне містити значення!");
            }
        }
    }
}
