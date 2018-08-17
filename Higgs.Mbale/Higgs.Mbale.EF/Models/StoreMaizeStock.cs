//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Higgs.Mbale.EF.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreMaizeStock
    {
        public long StoreMaizeStockId { get; set; }
        public long SupplyId { get; set; }
        public double StartStock { get; set; }
        public double Quantity { get; set; }
        public double StockBalance { get; set; }
        public long StoreId { get; set; }
        public long BranchId { get; set; }
        public long SectorId { get; set; }
        public System.DateTime TimeStamp { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual Store Store { get; set; }
        public virtual Supply Supply { get; set; }
    }
}