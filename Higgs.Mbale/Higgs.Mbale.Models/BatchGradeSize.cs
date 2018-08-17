using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
    public class BatchGradeSize
    {
        public long BatchOutPutId { get; set; }
        public long GradeId { get; set; }
        public long SizeId { get; set; }
        public double Quantity { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public  Batch Batch { get; set; }
        public  Grade Grade { get; set; }
        public  Size Size { get; set; }
    }
}
