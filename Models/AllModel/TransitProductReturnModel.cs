using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class TransitProductReturnModel
    {
        public int TransitProductReturnId { get; set; }

        public string TransitReturnChallnNo { get; set; }

        public int MarketPalceId { get; set; }

        public string MarketPlaceName { get; set; }

        public string ShopId { get; set; }

        public string ShopName { get; set; }

        public string UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }

        public string UpdatedDate { get; set; }

        public string CreateddBy { get; set; }
        public string CreateddByName { get; set; }

        public string CreatedDate { get; set; }

        public string WareHouseId { get; set; }

        public List<TransitProductReturnItem> TransitProductReturnItemList { get; set; }

        public static TransitProductReturnModel ConvertTransitProductReturnModel(DataRow row)
        {
            return new TransitProductReturnModel
            {
                TransitProductReturnId = row.Table.Columns.Contains("TRANSIT_PRODUCT_RETURN_ID") ? Convert.ToInt32(row["TRANSIT_PRODUCT_RETURN_ID"]) : 0,
                TransitReturnChallnNo = row.Table.Columns.Contains("TRANSIT_RETURN_CHALLAN_NUM") ? Convert.ToString(row["TRANSIT_RETURN_CHALLAN_NUM"]) : "",
                MarketPlaceName = row.Table.Columns.Contains("MARKETPLACE_NAME") ? Convert.ToString(row["MARKETPLACE_NAME"]) : "",
                MarketPalceId = row.Table.Columns.Contains("MARKET_PLACE_ID") ? Convert.ToInt32(row["MARKET_PLACE_ID"]) : 0,
                ShopId = row.Table.Columns.Contains("SHOP_ID") ? Convert.ToString(row["SHOP_ID"]) : "",
                ShopName = row.Table.Columns.Contains("FROM_SHOP_NAME") ? Convert.ToString(row["FROM_SHOP_NAME"]) : "",
                UpdatedBy = row.Table.Columns.Contains("UPDATED_BY") ? Convert.ToString(row["UPDATED_BY"]) : "",
                UpdatedByName = row.Table.Columns.Contains("UPDATED_BY_NAME") ? Convert.ToString(row["UPDATED_BY_NAME"]) : "",
                UpdatedDate = row.Table.Columns.Contains("UPDATED_DATE") ? Convert.ToString(row["UPDATED_DATE"]) : "",
                CreateddBy = row.Table.Columns.Contains("CREATED_BY") ? Convert.ToString(row["CREATED_BY"]) : "",
                CreateddByName = row.Table.Columns.Contains("CREATED_BY_NAME") ? Convert.ToString(row["CREATED_BY_NAME"]) : "",
                CreatedDate = row.Table.Columns.Contains("CREATED_DATE") ? Convert.ToString(row["CREATED_DATE"]) : "",

            };
        }
    }
}
