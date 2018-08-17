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
using EntityFramework.Extensions;

namespace Higgs.Mbale.DAL.Concrete
{
    public class OrderDataService : DataServiceBase, IOrderDataService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(OrderDataService));

        public OrderDataService(IUnitOfWork<MbaleEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

        public IEnumerable<Order> GetAllOrders()
        {
            return this.UnitOfWork.Get<Order>().AsQueryable()
                .Where(e => e.Deleted == false);
        }

        public IEnumerable<Order> GetAllOrdersForAParticularBranch(long branchId)
        {
            return this.UnitOfWork.Get<Order>().AsQueryable().Where(e => e.Deleted == false && e.BranchId == branchId);
        }


        public IEnumerable<Order> GetAllOrdersForAParticularCustomer(string customerId)
        {
            return this.UnitOfWork.Get<Order>().AsQueryable().Where(e => e.Deleted == false && e.CustomerId == customerId);
        }

        public Order GetOrder(long orderId)
        {
            return this.UnitOfWork.Get<Order>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.OrderId == orderId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new Order or updates an already existing Order.
        /// </summary>
        /// <param name="orderDTO">Order to be saved or updated.</param>
        /// <param name="userId">userId of the Order creating or updating</param>
        /// <returns>orderId</returns>
        public long SaveOrder(OrderDTO orderDTO, string userId)
        {
            long orderId = 0;

            if (orderDTO.OrderId == 0)
            {
                var order = new Order()
                {
                    Name = orderDTO.Name,
                    Amount = orderDTO.Amount,
                    ProductId = orderDTO.ProductId,
                    StatusId = orderDTO.StatusId,
                    BranchId = orderDTO.BranchId,                   
                    CustomerId = orderDTO.CustomerId,                  
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
                };

                this.UnitOfWork.Get<Order>().AddNew(order);
                this.UnitOfWork.SaveChanges();
                orderId = order.OrderId;
                

                if (orderDTO.Grades!= null)
                {
                    foreach (var id in orderDTO.Grades)
                    {
                        var orderGrade = new OrderGrade()
                        {
                            OrderId = orderId,
                            GradeId = id,
                            TimeStamp = DateTime.Now
                        };
                        this.UnitOfWork.Get<OrderGrade>().AddNew(orderGrade);
                        this.UnitOfWork.SaveChanges();
                    }
                }
                return orderId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Order>().AsQueryable()
                    .FirstOrDefault(e => e.OrderId == orderDTO.OrderId);
                if (result != null)
                {
                    result.Name = orderDTO.Name;
                    result.CustomerId = orderDTO.CustomerId;
                    result.Amount = orderDTO.Amount;
                    result.ProductId = orderDTO.ProductId;                    
                    result.BranchId = orderDTO.BranchId;
                    result.StatusId = orderDTO.StatusId;
                    result.UpdatedBy = userId;
                    result.TimeStamp = DateTime.Now;
                    this.UnitOfWork.Get<Order>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return orderDTO.OrderId;
            }
        }

        public void MarkAsDeleted(long OrderId, string userId)
        {
            using (var dbContext = new MbaleEntities())
            {
                //TODO: THROW NOT IMPLEMENTED EXCEPTION
            }

        }

        public void SaveOrderGrade(OrderGradeDTO orderGradeDTO)
        {
            var orderGrade = new OrderGrade()
            {
                OrderId = orderGradeDTO.OrderId,
                GradeId = orderGradeDTO.GradeId,
                TimeStamp = DateTime.Now
            };
            this.UnitOfWork.Get<OrderGrade>().AddNew(orderGrade);
            this.UnitOfWork.SaveChanges();
        }

        public void SaveOrderGradeSize(OrderGradeSizeDTO orderGradeSizeDTO)
        {
            var orderGradeSize = new OrderGradeSize()
            {
                OrderId = orderGradeSizeDTO.OrderId,
                GradeId = orderGradeSizeDTO.GradeId,
                SizeId = orderGradeSizeDTO.SizeId,
                Quantity = orderGradeSizeDTO.Quantity,
                TimeStamp = DateTime.Now
            };
            this.UnitOfWork.Get<OrderGradeSize>().AddNew(orderGradeSize);
            this.UnitOfWork.SaveChanges();
        }

        public void PurgeOrderGradeSize(long orderId)
        {
            this.UnitOfWork.Get<OrderGradeSize>().AsQueryable()
                .Where(m => m.OrderId == orderId)
                .Delete();
        }
    }
}
