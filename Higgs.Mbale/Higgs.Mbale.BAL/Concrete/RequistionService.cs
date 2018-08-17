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
using System.Web;
using System.Configuration;
using System.IO;

namespace Higgs.Mbale.BAL.Concrete
{
 public   class RequistionService : IRequistionService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(RequistionService));
        private IRequistionDataService _dataService;
        private IUserService _userService;
        

        public RequistionService(IRequistionDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RequistionId"></param>
        /// <returns></returns>
        public Requistion GetRequistion(long requistionId)
        {
            var result = this._dataService.GetRequistion(requistionId);
            return MapEFToModel(result);
        }

        public IEnumerable<Requistion> GetAllRequistionsForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllRequistionsForAParticularBranch(branchId);
            return MapEFToModel(results);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Requistion> GetAllRequistions()
        {
            var results = this._dataService.GetAllRequistions();
            return MapEFToModel(results);
        }

        public IEnumerable<Requistion> GetAllRequistionsForAParticularStatus(long statusId)
        {
            var results = this._dataService.GetAllRequistionsForAParticularStatus(statusId);
            return MapEFToModel(results);
        }
        public long SaveRequistion(Requistion requistion, string userId)
        {
            var requistionDTO = new DTO.RequistionDTO()
            {
                RequistionId = requistion.RequistionId,
                Response = requistion.Response,
                StatusId = requistion.StatusId,
                RequistionNumber = requistion.RequistionNumber,
                ApprovedById = requistion.ApprovedById,
                BranchId = requistion.BranchId,
                Description = requistion.Description,
                Deleted = requistion.Deleted,
                CreatedBy = requistion.CreatedBy,
                CreatedOn = requistion.CreatedOn,

            };

           var requistionId = this._dataService.SaveRequistion(requistionDTO, userId);
           //SendEmail(requistionDTO);
           return requistionId;
                      
        }

        //public void SendEmail(RequistionDTO requistion)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string strNewPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["RequistionEmail"]);
        //    using (StreamReader sr = new StreamReader(strNewPath))
        //    {
        //        while (!sr.EndOfStream)
        //        {
        //            sb.Append(sr.ReadLine());
        //        }
        //    }



        //    string body = sb.ToString().Replace("#REQUISTIONNUMBER#", requistion.RequistionNumber);
        //    body = body.Replace("#DESCRIPTION#", requistion.Description);

        //    Helpers.Email email = new Helpers.Email();
        //    email.MailBodyHtml = body;
        //    email.MailToAddress = ConfigurationManager.AppSettings["administrator-email"]; 
        //    email.MailFromAddress = ConfigurationManager.AppSettings["no-reply-email"];
        //    email.Subject = ConfigurationManager.AppSettings["requistion_email_subject"];
        //    email.SendMail();
        //    logger.Debug("Email sent");

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RequistionId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long requistionId, string userId)
        {
            _dataService.MarkAsDeleted(requistionId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Requistion> MapEFToModel(IEnumerable<EF.Models.Requistion> data)
        {
            var list = new List<Requistion>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Requistion EF object to Requistion Model Object and
        /// returns the Requistion model object.
        /// </summary>
        /// <param name="result">EF Requistion object to be mapped.</param>
        /// <returns>Requistion Model Object.</returns>
        public Requistion MapEFToModel(EF.Models.Requistion data)
        {
          
            var requistion = new Requistion()
            {
                RequistionId = data.RequistionId,
                ApprovedById = data.ApprovedById,
                StatusId = data.StatusId,
                Response = data.Response,
                BranchId = data.BranchId,
                BranchName = data.Branch != null ? data.Branch.Name : "",
                StatusName = data.Status != null ? data.Status.Name : "",
                ApprovedByName = _userService.GetUserFullName(data.AspNetUser),
                RequistionNumber = data.RequistionNumber,
                Description = data.Description,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser1),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser2),
               

            };
            return requistion;
        }



       #endregion
    }
}
