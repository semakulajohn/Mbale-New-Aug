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
    public class OrderService: IOrderService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(OrderService));
        private IOrderDataService _dataService;
        private IUserService _userService;
        private IGradeService _gradeService;
        

        public OrderService(IOrderDataService dataService,IUserService userService, IGradeService gradeService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._gradeService = gradeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Order GetOrder(long orderId)
        {
            var result = this._dataService.GetOrder(orderId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetAllOrders()
        {
            var results = this._dataService.GetAllOrders();
            return MapEFToModel(results);
        }

        public IEnumerable<Order> GetAllOrdersForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllOrdersForAParticularBranch(branchId);
            return MapEFToModel(results);
        }

        public IEnumerable<Order> GetAllOrdersForAParticularCustomer(string customerId)
        {
            var results = this._dataService.GetAllOrdersForAParticularCustomer(customerId);
            return MapEFToModel(results);
        } 

       
        public long SaveOrder(Order order, string userId)
        {
            var orderDTO = new DTO.OrderDTO()
            {
                Name = order.Name,
                Amount = order.Amount,
                ProductId = order.ProductId,
                StatusId = order.StatusId,
                BranchId = order.BranchId,
                CustomerId = order.CustomerId,  
                Deleted = order.Deleted,
                CreatedBy = order.CreatedBy,
                CreatedOn = order.CreatedOn
            };

           var orderId = this._dataService.SaveOrder(orderDTO, userId);
           if (order.Grades != null)
           {
               if (order.Grades.Any())
               {
                   List<OrderGradeSize> orderGradeSizeList = new List<OrderGradeSize>();

                   foreach (var grade in order.Grades)
                   {
                       var gradeId = grade.GradeId;
                       if (grade.Denominations != null)
                       {
                           if(grade.Denominations.Any())
                           {
                               foreach (var denomination in grade.Denominations)
                               {
                                   var orderGradeSize = new OrderGradeSize()
                                   {
                                       GradeId = gradeId,
                                       SizeId = denomination.DenominationId,
                                       OrderId = orderId,
                                       Quantity = denomination.Quantity,
                                       TimeStamp = DateTime.Now
                                   };
                                   orderGradeSizeList.Add(orderGradeSize);
                               }
                           }
                       }
                   }
                   this._dataService.PurgeOrderGradeSize(orderId);                  
                   this.SaveOrderGradeSizeList(orderGradeSizeList);
               }
           }
           return orderId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long orderId, string userId)
        {
            _dataService.MarkAsDeleted(orderId, userId);
        }

        void SaveOrderGradeSizeList(List<OrderGradeSize> orderGradeSizeList)
        {
            if (orderGradeSizeList != null)
            {
                if (orderGradeSizeList.Any())
                {
                    foreach (var orderGradeSize in orderGradeSizeList)
                    {
                        var orderGradeSizeDTO = new DTO.OrderGradeSizeDTO()
                        {
                            OrderId = orderGradeSize.OrderId,
                            GradeId = orderGradeSize.GradeId,
                            Quantity = orderGradeSize.Quantity,
                            SizeId = orderGradeSize.SizeId,
                            TimeStamp = orderGradeSize.TimeStamp
                        };
                        this.SaveOrderGradeSize(orderGradeSizeDTO);
                    }
                }
            }
        }
        void SaveOrderGradeSize(OrderGradeSizeDTO orderGradeSizeDTO)
        {
            _dataService.SaveOrderGradeSize(orderGradeSizeDTO);
        }
      
        #region Mapping Methods

        private IEnumerable<Order> MapEFToModel(IEnumerable<EF.Models.Order> data)
        {
            var list = new List<Order>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Order EF object to Order Model Object and
        /// returns the Order model object.
        /// </summary>
        /// <param name="result">EF Order object to be mapped.</param>
        /// <returns>Order Model Object.</returns>
        public Order MapEFToModel(EF.Models.Order data)
        {
          
            var order = new Order()
            {
                CustomerName = _userService.GetUserFullName(data.AspNetUsers1),
                Amount = data.Amount,
                Name = data.Name,
                ProductId = data.ProductId,
                ProductName = data.Product!=null?data.Product.Name:"",
                BranchName = data.Branch !=null? data.Branch.Name:"",               
                BranchId = data.BranchId,
                StatusId = data.StatusId,
                OrderId = data.OrderId,
                StatusName = data.Status!=null? data.Status.Name:"",
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUsers),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUsers2)            
            };

            if (data.OrderGradeSizes != null)
            {
                if (data.OrderGradeSizes.Any())
                {
                    List<Grade> grades = new List<Grade>();
                    var distinctGrades = data.OrderGradeSizes.GroupBy(g => g.GradeId).Select(o => o.First()).ToList();
                    foreach (var orderGradeSize in distinctGrades)
                    {
                        var grade = new Grade()
                        {
                            GradeId = orderGradeSize.Grade.GradeId,
                            Value = orderGradeSize.Grade.Value,
                            CreatedOn = orderGradeSize.Grade.CreatedOn,
                            TimeStamp = orderGradeSize.Grade.TimeStamp,
                            Deleted = orderGradeSize.Grade.Deleted,
                            CreatedBy = _userService.GetUserFullName(orderGradeSize.Grade.AspNetUser),
                            UpdatedBy = _userService.GetUserFullName(orderGradeSize.Grade.AspNetUser1),
                        };
                        List<Denomination> denominations = new List<Denomination>();
                           if (orderGradeSize.Grade.OrderGradeSizes != null)
                            {
                                if (orderGradeSize.Grade.OrderGradeSizes.Any())
                                {
                                    var distinctSizes = orderGradeSize.Grade.OrderGradeSizes.GroupBy(s => s.SizeId).Select(o => o.First()).ToList();
                                    foreach (var ogs in distinctSizes)
                                    {
                                        var denomination = new Denomination()
                                        {
                                            DenominationId = ogs.SizeId,
                                            Value = ogs.Size != null ? ogs.Size.Value : 0,
                                            Quantity = ogs.Quantity
                                        };
                                        order.TotalQuantity += (ogs.Quantity * ogs.Size.Value);
                                        denominations.Add(denomination);
                                    }
                                }
                               grade.Denominations = denominations;
                           }                          
                       grades.Add(grade);
                    }
                    order.Grades = grades;                    
                }
            }
            
            return order;
        }



       #endregion
    }
}
