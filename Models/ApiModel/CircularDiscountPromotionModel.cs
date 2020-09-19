using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class CircularDiscountPromotionModel
    {
        public int DiscountCircularId { get; set; }
        public int DiscountCircularItemId { get; set; }
        public string DiscountCircularName { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string Barcode { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountAmount { get; set; }
        public string UpdateBy { get; set; }
        public string WareHouseId { get; set; }
        public int ShopId { get; set; }
    }
}
