using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Models
{
   public class OrderGrade
    {
        public long OrderId { get; set; }
        public long GradeId { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public List<long> Grades { get; set; }
    }
}
