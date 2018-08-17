using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
 public   class StoreGradeSize
    {
      
        public long GradeId { get; set; }
        public long StoreId { get; set; }
        public long SizeId { get; set; }
        public double Quantity { get; set; }
        public System.DateTime TimeStamp { get; set; }

        public int SizeValue { get; set; }
        public string GradeValue { get; set; }

        public List<Grade> Grades { get; set; }
        public string StoreName { get; set; }

       
    
    }
 
}
