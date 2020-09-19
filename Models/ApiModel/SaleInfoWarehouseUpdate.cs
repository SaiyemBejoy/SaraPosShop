using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class SaleInfoWarehouseUpdate
    {
        public int SaleInfoId { get; set; }
        public string InvoiceNumber { get; set; }
        public int ShopId { get; set; }
    }
}
