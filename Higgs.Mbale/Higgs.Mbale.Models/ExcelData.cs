using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Higgs.Mbale.Models
{
 public   class ExcelData
    {
        public Columns ColumnConfigurations { get; set; }
        public List<string> Headers { get; set; }
        public List<List<string>> DataRows { get; set; }
        public string SheetName { get; set; }

        public ExcelData()
        {

            Headers = new List<string>();
            DataRows = new List<List<string>>();
        }
    }
}
