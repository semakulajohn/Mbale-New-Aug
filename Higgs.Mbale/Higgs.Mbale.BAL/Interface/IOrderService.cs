using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;


namespace Higgs.Mbale.BAL.Interface
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrder(long orderId);
        long SaveOrder(Order order, string userId);
        void MarkAsDeleted(long orderId, string userId);
        IEnumerable<Order> GetAllOrdersForAParticularBranch(long branchId);
        IEnumerable<Order> GetAllOrdersForAParticularCustomer(string customerId);
        
    }
}
