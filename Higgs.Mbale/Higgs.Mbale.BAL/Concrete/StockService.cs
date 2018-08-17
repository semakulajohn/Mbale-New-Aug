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
 public   class StockService : IStockService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(StockService));
        private IStockDataService _dataService;
        private IUserService _userService;
        private IGradeService _gradeService;
        

        public StockService(IStockDataService dataService,IUserService userService, IGradeService gradeService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._gradeService = gradeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StockId"></param>
        /// <returns></returns>
        public Stock GetStock(long stockId)
        {
            var result = this._dataService.GetStock(stockId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Stock> GetAllStocks()
        {
            var results = this._dataService.GetAllStocks();
            return MapEFToModel(results);
        }

        public IEnumerable<Stock> GetAllStocksForAParticularBranch(long branchId)
        {
            var results = this._dataService.GetAllStocksForAParticularBranch(branchId);
            return MapEFToModel(results);
        }

        private double GetStockBalanceForLastStockTransaction(long storeId,long productId)
        {
            double balance = 0;
           
                var result = this._dataService.GetLatestStockForAParticularStore(storeId,productId);
                if (result.StoreStockId > 0)
                {
                    balance = result.StockBalance;
                }

                return balance;
           
        }

        public IEnumerable<StoreStock> GetStocksForAParticularStore(long storeId)
        {
            var results = this._dataService.GetStocksForAParticularStore(storeId);
            return MapEFToModel(results);
        }

        public void SaveStoreStock(StoreStock storeStock, bool inOrOut)
        {
         
            double startStock = 0;
            double OldStockBalance = 0;
            double NewStockBalance = 0;


                OldStockBalance = GetStockBalanceForLastStockTransaction(storeStock.StoreId, storeStock.ProductId);
                startStock = OldStockBalance;
           

            if (inOrOut == true)
            {
                NewStockBalance = OldStockBalance + storeStock.Quantity;
            }
            else
            {
                NewStockBalance = OldStockBalance - storeStock.Quantity;
            }

            var storeStockDTO = new DTO.StoreStockDTO()
            {
                StoreStockId = storeStock.StoreStockId,
                StoreId = storeStock.StoreId,
                StartStock = startStock,
                StockId = storeStock.StockId,
                ProductId = storeStock.ProductId,
                StockBalance = NewStockBalance,
                BranchId = storeStock.BranchId,
               Quantity = storeStock.Quantity,
                SectorId = storeStock.SectorId,
                TimeStamp = storeStock.TimeStamp,

            };

          this._dataService.SaveStockStore(storeStockDTO);
        }

        public long SaveStock(Stock stock, string userId)
        {
            //saves stock object into stock table
            var stockDTO = new DTO.StockDTO()
            {
                InOrOut = stock.InOrOut,
                ProductId = stock.ProductId,
                BatchId = stock.BatchId,
                BranchId = stock.BranchId,
                SectorId = stock.SectorId,  
                Deleted = stock.Deleted,
                StoreId = stock.StoreId,
                CreatedBy = stock.CreatedBy,
                CreatedOn = stock.CreatedOn,
                ProductOutPut = stock.ProductOutPut,
            };

           var stockId = this._dataService.SaveStock(stockDTO, userId);
          

           if (stock.StockGradeSize != null)
           {
               List<StockGradeSize> stockGradeSizeList = new List<StockGradeSize>();
              
               foreach (var stockGradeSize in stock.StockGradeSize)
               {
                   var stockGrade_Size = new StockGradeSize(){
                       StockId = stockId,
                     GradeId = stockGradeSize.GradeId,
                     SizeId = stockGradeSize.SizeId,
                    Quantity = stockGradeSize.Quantity,
                   };

                   stockGradeSizeList.Add(stockGrade_Size);  

        //Method that adds flour output into storeGradeSize table(store flour stock)
                   var storeGradeSize = new StoreGradeSizeDTO(){
                     StoreId = stock.StoreId,
                     GradeId = stockGradeSize.GradeId,
                     SizeId = stockGradeSize.SizeId,
                    Quantity = stockGradeSize.Quantity,
                   };

                   this._dataService.SaveStoreGradeSize(storeGradeSize);
               }

               this._dataService.PurgeStockGradeSize(stockId);
               this.SaveStockGradeSizeList(stockGradeSizeList);
           }
            //save stock and productId into stockproduct table
            var stockProductDTO = new StockProductDTO(){
                StockId = stockId,
                ProductId = stockDTO.ProductId,
                Quantity = stockDTO.ProductOutPut,
                
            };
           this._dataService.PurgeStockProduct(stockId);
           this._dataService.SaveStockProduct(stockProductDTO);

             var storeStock = new StoreStock()
            {
                
                StoreId = stock.StoreId,
                ProductId = stock.ProductId,
                BranchId = stock.BranchId,
                StockId= stockId,
               Quantity = stock.ProductOutPut,
                SectorId = stock.SectorId,
               

            };
             SaveStoreStock(storeStock, stock.InOrOut);
           return stockId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StockId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long stockId, string userId)
        {
            _dataService.MarkAsDeleted(stockId, userId);
        }

        void SaveStockGradeSizeList(List<StockGradeSize> stockGradeSizeList)
        {
            if (stockGradeSizeList != null)
            {
                if (stockGradeSizeList.Any())
                {
                    foreach (var stockGradeSize in stockGradeSizeList)
                    {
                        var stockGradeSizeDTO = new DTO.StockGradeSizeDTO()
                        {
                            StockId = stockGradeSize.StockId,
                            GradeId = stockGradeSize.GradeId,
                            Quantity = stockGradeSize.Quantity,
                            SizeId = stockGradeSize.SizeId,
                            TimeStamp = stockGradeSize.TimeStamp
                        };
                        this.SaveStockGradeSize(stockGradeSizeDTO);
                    }
                }
            }
        }
        void SaveStockGradeSize(StockGradeSizeDTO stockGradeSizeDTO)
        {
            _dataService.SaveStockGradeSize(stockGradeSizeDTO);
        }

        //public IEnumerable<StoreGradeSize> GetStoreFlourStock(long storeId)
        //{
        //    var result = this._dataService.GetStoreFlourStock(storeId);
        //    return MapEFToModel(result);
        //}

        public StoreGrade GetStoreFlourStock(long storeId)
        {
            var result = this._dataService.GetStoreFlourStock(storeId);
           var storeGrade = GetStoreFlourStock2(MapEFToModel(result));
           return storeGrade;
        }
        public StoreGrade GetStoreFlourStock2(IEnumerable<Models.StoreGradeSize> list)
        {
            var storeGrade = new StoreGrade()
            {
                StoreSizeGrades = list,
            };

            
            return storeGrade;
        }

        #region Mapping Methods

        private IEnumerable<Stock> MapEFToModel(IEnumerable<EF.Models.Stock> data)
        {
            var list = new List<Stock>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Stock EF object to Stock Model Object and
        /// returns the Stock model object.
        /// </summary>
        /// <param name="result">EF Stock object to be mapped.</param>
        /// <returns>Stock Model Object.</returns>
        public Stock MapEFToModel(EF.Models.Stock data)
        {
          
            var stock = new Stock()
            {
                BatchId = data.BatchId,
                SectorId = data.SectorId,
                SectorName = data.Sector !=null?data.Sector.Name:"",
                ProductId = data.ProductId,
                ProductName = data.Product!=null?data.Product.Name:"",
                BranchName = data.Branch !=null? data.Branch.Name:"",               
                BranchId = data.BranchId,
                BatchNumber = data.Batch != null? data.Batch.Name:"",
                StockId = data.StockId,
                InOrOut = data.InOrOut,
                StoreId = data.StoreId,
                StoreName = data.Store != null? data.Store.Name:"",
                StockInOrOut = (data.InOrOut == true)?"Stock In":"Stock Out",
                CreatedOn = data.CreatedOn,
                TimeStamp = data.TimeStamp,
                Deleted = data.Deleted,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1)            
            };

            if (data.StockGradeSizes != null)
            {
                if (data.StockGradeSizes.Any())
                {
                    List<Grade> grades = new List<Grade>();
                    var distinctGrades = data.StockGradeSizes.GroupBy(g => g.GradeId).Select(o => o.First()).ToList();
                    foreach (var stockGradeSize in distinctGrades)
                    {
                        var grade = new Grade()
                        {
                            GradeId = stockGradeSize.Grade.GradeId,
                            Value = stockGradeSize.Grade.Value,
                            CreatedOn = stockGradeSize.Grade.CreatedOn,
                            TimeStamp = stockGradeSize.Grade.TimeStamp,
                            Deleted = stockGradeSize.Grade.Deleted,
                            CreatedBy = _userService.GetUserFullName(stockGradeSize.Grade.AspNetUser),
                            UpdatedBy = _userService.GetUserFullName(stockGradeSize.Grade.AspNetUser1),
                        };
                        List<Denomination> denominations = new List<Denomination>();
                           if (stockGradeSize.Grade.StockGradeSizes != null)
                            {
                                if (stockGradeSize.Grade.StockGradeSizes.Any())
                                {
                                    var distinctSizes = stockGradeSize.Grade.StockGradeSizes.GroupBy(s => s.SizeId).Select(o => o.First()).ToList();
                                    foreach (var ogs in distinctSizes)
                                    {
                                        var denomination = new Denomination()
                                        {
                                            DenominationId = ogs.SizeId,
                                            Value = ogs.Size != null ? ogs.Size.Value : 0,
                                            Quantity = ogs.Quantity
                                        };
                                        denominations.Add(denomination);
                                    }
                                }
                               grade.Denominations = denominations;
                           }                          
                       grades.Add(grade);
                    }
                    stock.Grades = grades;                    
                }
            }

            var stockProduct =_dataService.GetStockProductForAStock(data.StockId);
           
           
            if (stockProduct != null)
            {
                    var stock_Product = new StockProduct()
                    {
                        StockId = stockProduct.StockId,
                       Quantity = stockProduct.Quantity,

                    };
                stock.ProductOutPut =stock_Product.Quantity;
             
            }

            
            return stock;
        }


        private IEnumerable<StoreStock> MapEFToModel(IEnumerable<EF.Models.StoreStock> data)
        {
            var list = new List<StoreStock>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps StoreStock EF object to StoreStock Model Object and
        /// returns the StoreStock model object.
        /// </summary>
        /// <param name="result">EF StoreStock object to be mapped.</param>
        /// <returns>StoreStock Model Object.</returns>
        public StoreStock MapEFToModel(EF.Models.StoreStock data)
        {
            
            var stock = GetStock(data.StockId);
           
            var storeStock = new StoreStock()
            {
                
                SectorId = data.SectorId,
                SectorName = data.Sector != null ? data.Sector.Name : "",
                ProductId = data.ProductId,
                ProductName = data.Product != null ? data.Product.Name : "",
                BranchName = data.Branch != null ? data.Branch.Name : "",
                BranchId = data.BranchId,
                StockId = data.StockId,
                Quantity = data.Quantity,
                StockBalance = data.StockBalance,
                StartStock = data.StartStock,
                StoreStockId = data.StoreStockId,
                 BatchNumber  = stock.BatchNumber,
                 StockInOrOut = stock.StockInOrOut,
              
                StoreId = data.StoreId,
                StoreName = data.Store != null ? data.Store.Name : "",
                 TimeStamp = data.TimeStamp,
               
            };

             return storeStock;
        }


        private IEnumerable<StoreGradeSize> MapEFToModel(IEnumerable<EF.Models.StoreGradeSize> data)
        {
            var list = new List<StoreGradeSize>();
            
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
           
            return list;
        }

        /// <summary>
        /// Maps StoreGradeSize EF object to StoreGradeSize Model Object and
        /// returns the StoreGradeSize model object.
        /// </summary>
        /// <param name="result">EF StoreGradeSize object to be mapped.</param>
        /// <returns>StoreGradeSize Model Object.</returns>
        public StoreGradeSize MapEFToModel(EF.Models.StoreGradeSize data)
        {

            var storeGradeSize = new StoreGradeSize()
            {

                GradeId = data.GradeId,
                Quantity = data.Quantity,
               SizeId = data.SizeId,
               SizeValue = data.Size.Value,
               GradeValue = data.Grade.Value,
                StoreId = data.StoreId,
                StoreName = data.Store != null ? data.Store.Name : "",
                TimeStamp = data.TimeStamp,

            };

            //List<Grade> grades = new List<Grade>();

            //var distinctGrade = this._dataService.GetGrade(storeGradeSize.GradeId);

            //var grade = new Grade()
            //{
            //    GradeId = distinctGrade.GradeId,
            //    Value = distinctGrade.Value,

            //};
            //var results = this._dataService.GetStoreGradeSizeForParticularGradeAtAStore(storeGradeSize.GradeId, storeGradeSize.StoreId);
            //foreach (var gradeItem in results)
            //{
            //    List<Denomination> denominations = new List<Denomination>();

            //    var distinctSize = this._dataService.GetSize(gradeItem.SizeId);

            //    var denomination = new Denomination()
            //    {
            //        DenominationId = distinctSize.SizeId,
            //        Value = distinctSize.Value,
            //        Quantity = data.Quantity,

            //    };

            //    denominations.Add(denomination);


            //    grade.Denominations = denominations;
            //}


            //grades.Add(grade);

            //storeGradeSize.Grades = grades;

     

            return storeGradeSize;
        }


       #endregion
    }
}
