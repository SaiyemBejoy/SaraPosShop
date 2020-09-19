using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class TransitProductItem
    {
        public int TransitProductItemId { get; set; }

        public string TransitChallnNo { get; set; }

        public int ProductId { get; set; }

        public int ItemId { get; set; }

        public string Barcode { get; set; }

        public string ItemName { get; set; }

        public double SalePrice { get; set; }

        public double GrowingPercent { get; set; }

        public double GrowingPrice { get; set; }

        public double DollarRate { get; set; }

        public double USDollar { get; set; }

        public int Quantity { get; set; }
    }
}
