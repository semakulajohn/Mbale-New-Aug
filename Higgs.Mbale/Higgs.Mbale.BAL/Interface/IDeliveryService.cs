using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;

namespace Higgs.Mbale.BAL.Interface
{
    public interface IDeliveryService
    {
        IEnumerable<Delivery> GetAllDeliveries();
        Delivery GetDelivery(long deliveryId);
        long SaveDelivery(Delivery delivery, string userId);
        void MarkAsDeleted(long deliveryId, string userId);
        IEnumerable<Delivery> GetAllDeliveriesForAParticularBranch(long branchId);
        IEnumerable<Delivery> GetAllDeliveriesForAParticularOrder(long orderId);
    }
}
