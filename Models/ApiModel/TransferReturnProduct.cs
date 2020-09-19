using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class TransferReturnProduct
    {
        public string StockTranferChallanNo { get; set; }

        public string StockTransferId { get; set; }

        public int TransferShopIdTo { get; set; }

        public string TransferShopToName { get; set; }

        public int TransferShopIdFrom { get; set; }

        public string TransferShopIdFromName { get; set; }

        public string TransferDate { get; set; }

        public string RequisitionNo { get; set; }

        public int StocktransferItemId { get; set; }

        public int ItemId { get; set; }

        public int ProductId { get; set; }

        public string ItemName { get; set; }

        public string BarCode { get; set; }

        public int TransferQuantity { get; set; }

        public double PurchasePrice { get; set; }

        public double SalePrice { get; set; }

        public double Vat { get; set; }

        public string Umo { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string Brand { get; set; }

        public int WarehouseId { get; set; }

        public string TransferBy { get; set; }
    }
}
