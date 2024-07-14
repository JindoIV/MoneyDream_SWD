using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class InStock
    {
        public Product? Product { get; set; }
        public int Id { get; set; }
        public int SumImportList { get; set; }
        public int SumExportList { get; set; }
        public int Count { get; set; }
    }
}
