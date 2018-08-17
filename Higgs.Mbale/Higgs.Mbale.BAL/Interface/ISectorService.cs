using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
 public   interface ISectorService
    {
        IEnumerable<Sector> GetAllSectors();
        Sector GetSector(long sectorId);
        long SaveSector(Sector sector, string userId);
        void MarkAsDeleted(long sectorId, string userId);
    }
}
