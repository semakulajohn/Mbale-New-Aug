using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.DAL.Interface;
using Higgs.Mbale.Models;
using Higgs.Mbale.Helpers;
using log4net;

namespace Higgs.Mbale.BAL.Concrete
{
  public  class GradeService : IGradeService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(GradeService));
        private IGradeDataService _dataService;
        private IUserService _userService;
        

        public GradeService(IGradeDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Grade> GetAllGrades()
        {
            var results = this._dataService.GetAllGrades();
            return MapEFToModel(results);
        }

        public IEnumerable<Size> GetAllSizes()
        {
            var results = this._dataService.GetAllSizes();
            return MapSizeEFToModel(results);
        }

        #region Mapping Methods

        public IEnumerable<Grade> MapEFToModel(IEnumerable<EF.Models.Grade> data)
        {
            var list = new List<Grade>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Grade EF object to Grade Model Object and
        /// returns the Grade model object.
        /// </summary>
        /// <param name="result">EF Grade object to be mapped.</param>
        /// <returns>Grade Model Object.</returns>
        public Grade MapEFToModel(EF.Models.Grade data)
        {

            var grade = new Grade()
            {
                GradeId = data.GradeId,
                Value = data.Value,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
            };
            List<Denomination> denominations = new List<Denomination>();
           
                var sizes = GetAllSizes();
                foreach (var size in sizes)
                {
                    var denomination = new Denomination()
                    {
                        DenominationId = size.SizeId,
                        Value = size.Value,
                        Quantity = 0
                    };
                    denominations.Add(denomination);
                }
                  
            grade.Denominations = denominations;
            return grade;
        }

        private IEnumerable<Size> MapSizeEFToModel(IEnumerable<EF.Models.Size> data)
        {
            var list = new List<Size>();
            foreach (var result in data)
            {
                list.Add(MapSizeEFToModel(result));
            }
            return list;
        }

        public Size MapSizeEFToModel(EF.Models.Size data)
        {
            var size = new Size()
            {
                SizeId = data.SizeId,
                Value = data.Value,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),

            };
            return size;
        }

        #endregion

    }
}
