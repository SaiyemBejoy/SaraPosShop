using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class TransitProductReturnItem
    {
        public int TransitProductReurnItemId { get; set; }

        public string TransitReturnChallnNo { get; set; }

        public int ProductId { get; set; }

        public int ItemId { get; set; }

        public string Barcode { get; set; }

        public string ItemName { get; set; }

        public double SalePrice { get; set; }

        public int Quantity { get; set; }
    }
}
