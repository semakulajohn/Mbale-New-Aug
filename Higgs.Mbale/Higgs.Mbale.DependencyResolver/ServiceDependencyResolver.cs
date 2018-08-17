 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.BAL.Interface;
using Higgs.Mbale.BAL.Concrete;
using Higgs.Mbale.DAL.Concrete;
using Higgs.Mbale.DAL.Interface;
using Ninject.Modules;

namespace Higgs.Mbale.DependencyResolver
{
 public   class ServiceDependencyResolver : NinjectModule
    {
       
     public override void Load()
     {
         //BAL
         Bind(typeof(IUserService)).To(typeof(UserService));
         Bind(typeof(IBranchService)).To(typeof(BranchService));
         Bind(typeof(ISectorService)).To(typeof(SectorService));
         Bind(typeof(ICasualWorkerService)).To(typeof(CasualWorkerService));
         Bind(typeof(IBatchService)).To(typeof(BatchService));
         Bind(typeof(IProductService)).To(typeof(ProductService));
         Bind(typeof(IDeliveryService)).To(typeof(DeliveryService));
         Bind(typeof(IOrderService)).To(typeof(OrderService));
         Bind(typeof(IStatusService)).To(typeof(StatusService));
         Bind(typeof(IMachineRepairService)).To(typeof(MachineRepairService));
         Bind(typeof(ITransactionSubTypeService)).To(typeof(TransactionSubTypeService));
         Bind(typeof(IRequistionService)).To(typeof(RequistionService));
         Bind(typeof(ITransactionService)).To(typeof(TransactionService));
         Bind(typeof(ISupplyService)).To(typeof(SupplyService));
         Bind(typeof(IInventoryService)).To(typeof(InventoryService));
         Bind(typeof(IStoreService)).To(typeof(StoreService));
         Bind(typeof(IGradeService)).To(typeof(GradeService));
         Bind(typeof(IAccountTransactionActivityService)).To(typeof(AccountTransactionActivityService));
         Bind(typeof(IActivityService)).To(typeof(ActivityService));
         Bind(typeof(IReportService)).To(typeof(ReportService));
         Bind(typeof(ICreditorService)).To(typeof(CreditorService));
         Bind(typeof(IDebtorService)).To(typeof(DebtorService));
         Bind(typeof(IFactoryExpenseService)).To(typeof(FactoryExpenseService));
         Bind(typeof(ILabourCostService)).To(typeof(LabourCostService));
         Bind(typeof(IStockService)).To(typeof(StockService));
         Bind(typeof(ICasualActivityService)).To(typeof(CasualActivityService));
         Bind(typeof(IOtherExpenseService)).To(typeof(OtherExpenseService));
         Bind(typeof(IBatchOutPutService)).To(typeof(BatchOutPutService));



         //DAL
         Bind(typeof(IUserDataService)).To(typeof(UserDataService));
         Bind(typeof(IBranchDataService)).To(typeof(BranchDataService));
         Bind(typeof(ISectorDataService)).To(typeof(SectorDataService));
         Bind(typeof(ICasualWorkerDataService)).To(typeof(CasualWorkerDataService));
         Bind(typeof(ISupplyDataService)).To(typeof(SupplyDataService));
         Bind(typeof(IBatchDataService)).To(typeof(BatchDataService));
         Bind(typeof(IProductDataService)).To(typeof(ProductDataService));
         Bind(typeof(IDeliveryDataService)).To(typeof(DeliveryDataService));
         Bind(typeof(IOrderDataService)).To(typeof(OrderDataService));
         Bind(typeof(IStatusDataService)).To(typeof(StatusDataService));
         Bind(typeof(IMachineRepairDataService)).To(typeof(MachineRepairDataService));
         Bind(typeof(ITransactionDataService)).To(typeof(TransactionDataService));
         Bind(typeof(ITransactionSubTypeDataService)).To(typeof(TransactionSubTypeDataService));
         Bind(typeof(IRequistionDataService)).To(typeof(RequistionDataService));
         Bind(typeof(IInventoryDataService)).To(typeof(InventoryDataService));
         Bind(typeof(IStoreDataService)).To(typeof(StoreDataService));
         Bind(typeof(IGradeDataService)).To(typeof(GradeDataService));
         Bind(typeof(IAccountTransactionActivityDataService)).To(typeof(AccountTransactionActivityDataService));
         Bind(typeof(IActivityDataService)).To(typeof(ActivityDataService));
         Bind(typeof(IReportDataService)).To(typeof(ReportDataService));
         Bind(typeof(ICreditorDataService)).To(typeof(CreditorDataService));
         Bind(typeof(IDebtorDataService)).To(typeof(DebtorDataService));
         Bind(typeof(IFactoryExpenseDataService)).To(typeof(FactoryExpenseDataService));
         Bind(typeof(ILabourCostDataService)).To(typeof(LabourCostDataService));
         Bind(typeof(IStockDataService)).To(typeof(StockDataService));
         Bind(typeof(ICasualActivityDataService)).To(typeof(CasualActivityDataService));
         Bind(typeof(IOtherExpenseDataService)).To(typeof(OtherExpenseDataService));
         Bind(typeof(IBatchOutPutDataService)).To(typeof(BatchOutPutDataService));
     } 
        
          
    }
}
