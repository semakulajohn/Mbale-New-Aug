using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.EF.Models;
using Higgs.Mbale.DAL.Concrete;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.EF.UnitOfWork;
using Higgs.Mbale.DTO;
using log4net;

namespace Higgs.Mbale.DAL.Concrete
{
  public  class SectorDataService : DataServiceBase,ISectorDataService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(SectorDataService));

       public SectorDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Sector> GetAllSectors()
        {
            return this.UnitOfWork.Get<Sector>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public Sector GetSector(long sectorId)
        {
            return this.UnitOfWork.Get<Sector>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.SectorId == sectorId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new Sector or updates an already existing Sector.
        /// </summary>
        /// <param name="Sector">Sector to be saved or updated.</param>
        /// <param name="SectorId">SectorId of the Sector creating or updating</param>
        /// <returns>SectorId</returns>
        public long SaveSector(SectorDTO sectorDTO, string userId)
        {
            long sectorId = 0;
            
            if (sectorDTO.SectorId == 0)
            {
           
                var sector = new Sector()
                {
                    Name = sectorDTO.Name,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
 

                };

                this.UnitOfWork.Get<Sector>().AddNew(sector);
                this.UnitOfWork.SaveChanges();
                sectorId = sector.SectorId;
                return sectorId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Sector>().AsQueryable()
                    .FirstOrDefault(e => e.SectorId == sectorDTO.SectorId);
                if (result != null)
                {
                    result.Name = sectorDTO.Name;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = sectorDTO.Deleted;
                    result.DeletedBy = sectorDTO.DeletedBy;
                    result.DeletedOn = sectorDTO.DeletedOn;

                    this.UnitOfWork.Get<Sector>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return sectorDTO.SectorId;
            }
            return sectorId;
        }

        public void MarkAsDeleted(long sectorId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
               // dbContext.Mark_Estate_And_RelatedData_AsDeleted(SectorId, userId);
            }

        }
    }
}
