using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class CircularPriceChangeSub
    {
        public int CircularId { get; set; }
        public int CircularPriceSubId { get; set; }
        public string BarcodeNo { get; set; }
        public string PurchasePrice { get; set; }
        public string PreSalePrice { get; set; }
        public string NewSalePrice { get; set; }
        public string UpdateBy { get; set; }
        public string EffectiveDate { get; set; }
        public string WareHouseId { get; set; }
    }
}
