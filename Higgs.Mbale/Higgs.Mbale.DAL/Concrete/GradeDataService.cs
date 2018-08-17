using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DAL.Interface;
using log4net;
using Higgs.Mbale.EF.Models;
using Higgs.Mbale.EF.UnitOfWork;
using Higgs.Mbale.DTO;

namespace Higgs.Mbale.DAL.Concrete
{
 public   class GradeDataService : DataServiceBase,IGradeDataService
    {
      ILog logger = log4net.LogManager.GetLogger(typeof(GradeDataService));

       public GradeDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }
              
        public IEnumerable<Grade> GetAllGrades()
        {
            return this.UnitOfWork.Get<Grade>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public IEnumerable<Size> GetAllSizes()
        {
            return this.UnitOfWork.Get<Size>().AsQueryable().Where(e => e.Deleted == false);
        }
    }
}
