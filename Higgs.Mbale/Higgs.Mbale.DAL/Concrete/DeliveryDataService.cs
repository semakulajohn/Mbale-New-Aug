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
    public class DeliveryDataService: DataServiceBase,IDeliveryDataService
    {
       ILog logger = log4net.LogManager.GetLogger(typeof(DeliveryDataService));

       public DeliveryDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }
              
        public IEnumerable<Delivery> GetAllDeliveries()
        {
            return this.UnitOfWork.Get<Delivery>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public Delivery GetDelivery(long deliveryId)
        {
            return this.UnitOfWork.Get<Delivery>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.DeliveryId == deliveryId &&
                    c.Deleted == false
                );
        }
        public IEnumerable<Delivery> GetAllDeliveriesForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<Delivery>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }

        public IEnumerable<Delivery> GetAllDeliveriesForAParticularOrder(long orderId)
        {
            return this.UnitOfWork.Get<Delivery>().AsQueryable().Where(e => e.Deleted == false && e.OrderId == orderId);
        }
        /// <summary>
        /// Saves a new Delivery or updates an already existing Delivery.
        /// </summary>
        /// <param name="Delivery">Delivery to be saved or updated.</param>
        /// <param name="DeliveryId">DeliveryId of the Delivery creating or updating</param>
        /// <returns>DeliveryId</returns>
        public long SaveDelivery(DeliveryDTO deliveryDTO, string userId)
        {
            long deliveryId = 0;
            
            if (deliveryDTO.DeliveryId == 0)
            {

                var delivery = new Delivery()
                {
                    CustomerName = deliveryDTO.CustomerName,
                    DeliveryCost = deliveryDTO.DeliveryCost,
                    OrderId =  deliveryDTO.OrderId,
                    VehicleNumber = deliveryDTO.VehicleNumber,
                    BranchId = deliveryDTO.BranchId,
                    Location = deliveryDTO.Location,
                    SectorId = deliveryDTO.SectorId,
                    MediaId = deliveryDTO.MediaId,
                    DriverNIN = deliveryDTO.DriverNIN,
                    DriverName = deliveryDTO.DriverName,
                    TransactionSubTypeId = deliveryDTO.TransactionSubTypeId,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false, 
                };

                this.UnitOfWork.Get<Delivery>().AddNew(delivery);
                this.UnitOfWork.SaveChanges();
                deliveryId = delivery.DeliveryId;
                return deliveryId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Delivery>().AsQueryable()
                    .FirstOrDefault(e => e.DeliveryId == deliveryDTO.DeliveryId);
                if (result != null)
                {
                    result.DeliveryCost = deliveryDTO.DeliveryCost;
                    result.CustomerName = deliveryDTO.CustomerName;
                    result.OrderId =  deliveryDTO.OrderId;
                    result.TransactionSubTypeId = deliveryDTO.TransactionSubTypeId;
                    result.VehicleNumber = deliveryDTO.VehicleNumber;
                    result.BranchId = deliveryDTO.BranchId;
                    result.Location = deliveryDTO.Location;
                    result.SectorId = deliveryDTO.SectorId;
                    result.MediaId = deliveryDTO.MediaId;
                    result.DriverName = deliveryDTO.DriverName;
                    result.DriverNIN = deliveryDTO.DriverNIN;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    result.Deleted = deliveryDTO.Deleted;
                    result.DeletedBy = deliveryDTO.DeletedBy;
                    result.DeletedOn = deliveryDTO.DeletedOn;

                    this.UnitOfWork.Get<Delivery>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return deliveryDTO.DeliveryId;
            }            
        }

        public void MarkAsDeleted(long deliveryId, string userId)
        {


            using (var dbContext = new MbaleEntities())
            {
              //TODO: THROW NOT IMPLEMENTED EXCEPTION
            }

        }
    
    }
}
