using Catalog.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.BLL.Services.Interfaces
{
    public interface IinfoService
    {
        IEnumerable<infoDTO> Getinfos(int page);
    }
}
