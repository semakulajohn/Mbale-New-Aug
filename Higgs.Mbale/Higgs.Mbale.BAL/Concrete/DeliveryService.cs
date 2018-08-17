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
    public class DeliveryService: IDeliveryService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(DeliveryService));
        private IDeliveryDataService _dataService;
        private IUserService _userService;
        private ITransactionDataService _transactionDataService;
        private ITransactionSubTypeService _transactionSubTypeService;
        

        public DeliveryService(IDeliveryDataService dataService,IUserService userService,ITransactionDataService transactionDataService,ITransactionSubTypeService transactionSubTypeService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._transactionDataService = transactionDataService;
            this._transactionSubTypeService = transactionSubTypeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeliveryId"></param>
        /// <returns></returns>
        public Delivery GetDelivery(long deliveryId)
        {
            var result = this._dataService.GetDelivery(deliveryId);
            return MapEFToModel(result);
        }

        public IEnumerable<Delivery> GetAllDeliveriesForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllDeliveriesForAParticularBranch(branchId);
            return MapEFToModel(results);

        }
        public IEnumerable<Delivery> GetAllDeliveriesForAParticularOrder(long orderId)
        {
            var results = this._dataService.GetAllDeliveriesForAParticularOrder(orderId);
            return MapEFToModel(results);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Delivery> GetAllDeliveries()
        {
            var results = this._dataService.GetAllDeliveries();
            return MapEFToModel(results);
        } 

       
        public long SaveDelivery(Delivery delivery, string userId)
        {
            var customerName = string.Empty;
            var customer = _userService.GetAspNetUser(delivery.CustomerId);
            if (customer != null)
            {
                 customerName = customer.FirstName + ' ' + customer.LastName;
            }

            var deliveryDTO = new DTO.DeliveryDTO()
            {
                CustomerName = customerName,
                DeliveryCost = delivery.DeliveryCost,
                OrderId = delivery.OrderId,
                VehicleNumber = delivery.VehicleNumber,
                BranchId = delivery.BranchId,
                SectorId = delivery.SectorId,
                Location = delivery.Location,
                TransactionSubTypeId = delivery.TransactionSubTypeId,
                MediaId = delivery.MediaId,
                DeliveryId = delivery.DeliveryId,
                DriverName = delivery.DriverName,
                DriverNIN = delivery.DriverNIN,
                Deleted = delivery.Deleted,
                CreatedBy = delivery.CreatedBy,
                CreatedOn = delivery.CreatedOn,
                

            };

           var deliveryId = this._dataService.SaveDelivery(deliveryDTO, userId);
          
            long  transactionTypeId = 0;
           var transactionSubtype = _transactionSubTypeService.GetTransactionSubType(deliveryDTO.TransactionSubTypeId);
           if (transactionSubtype != null)
           {
               transactionTypeId = transactionSubtype.TransactionTypeId;
           }

            var transaction = new TransactionDTO()
            {
                BranchId = deliveryDTO.BranchId,
                SectorId = deliveryDTO.SectorId,
                Amount = deliveryDTO.DeliveryCost,
                TransactionSubTypeId = deliveryDTO.TransactionSubTypeId,
                TransactionTypeId = transactionTypeId,
                CreatedOn = DateTime.Now,
                TimeStamp = DateTime.Now,
                CreatedBy = userId,
                Deleted = false,

            };
       var transactionId =  _transactionDataService.SaveTransaction(transaction,userId);
           return deliveryId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long deliveryId, string userId)
        {
            _dataService.MarkAsDeleted(deliveryId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Delivery> MapEFToModel(IEnumerable<EF.Models.Delivery> data)
        {
            var list = new List<Delivery>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Delivery EF object to Delivery Model Object and
        /// returns the Delivery model object.
        /// </summary>
        /// <param name="result">EF Delivery object to be mapped.</param>
        /// <returns>Delivery Model Object.</returns>
        public Delivery MapEFToModel(EF.Models.Delivery data)
        {
          
            var delivery = new Delivery()
            {
                CustomerName = data.CustomerName,
                DeliveryCost = data.DeliveryCost,
                OrderId = data.OrderId,
                BranchName = data.Branch !=null? data.Branch.Name:"",
                SectorName = data.Sector != null ? data.Sector.Name : "",
                TransactionSubTypeId = data.TransactionSubTypeId,
                TransactionSubTypeName = data.TransactionSubType !=null?data.TransactionSubType.Name:"",
                VehicleNumber = data.VehicleNumber,
                BranchId = data.BranchId,
                Location = data.Location,
                SectorId = data.SectorId,
                MediaId = data.MediaId,
                DeliveryId = data.DeliveryId,
                DriverName = data.DriverName,
                DriverNIN = data.DriverNIN,
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),               

            };
            return delivery;
        }



       #endregion
    }
}
