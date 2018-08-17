using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
  public  interface ISectorDataService
    {
        IEnumerable<Sector> GetAllSectors();
        Sector GetSector(long sectorId);
        long SaveSector(SectorDTO sector, string userId);
        void MarkAsDeleted(long sectorId, string userId);
    }
}
