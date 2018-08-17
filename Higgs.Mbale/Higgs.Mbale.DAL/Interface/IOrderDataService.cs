using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.DTO;
using Higgs.Mbale.EF.Models;

namespace Higgs.Mbale.DAL.Interface
{
    public interface IOrderDataService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrder(long orderId);
        long SaveOrder(OrderDTO order, string userId);
        void MarkAsDeleted(long orderId, string userId);  
        IEnumerable<Order> GetAllOrdersForAParticularBranch(long branchId);
        IEnumerable<Order> GetAllOrdersForAParticularCustomer(string customerId);
        void SaveOrderGrade(OrderGradeDTO orderGradeDTO);
        void SaveOrderGradeSize(OrderGradeSizeDTO orderGradeSizeDTO);
        void PurgeOrderGradeSize(long orderId);
    }
}
