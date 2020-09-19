using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class DcProductSearchModel
    {
        public int ProductId { get; set; }
        public int ItemId { get; set; }
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string SalePrice { get; set; }
        public int Quantity { get; set; }

    }
}
