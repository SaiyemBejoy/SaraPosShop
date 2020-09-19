using System;
using System.Collections.Generic;
using System.Data;
using Models.ApiModel;

namespace Models.AllModel
{
    public class ShopToShopReceiveMainModel
    {
        public int ShopToShopReceiveId { get; set; }

        public string ShopToShopReceiveNumber { get; set; }

        public string RequisitionNumber { get; set; }

        public string DeliveryNumber { get; set; }

        public string ReceivedBy { get; set; }

        public string ReceivedYN { get; set; }

        public string ReceivedDate { get; set; }

        public string ReceiveShopId { get; set; }


        public IEnumerable<ShopToShopReceiveItemModel> ShopToShopShopToShopReceiveItemList { get; set; }

        public static ShopToShopReceiveMainModel ConvertShopToShopReceiveMainModel(DataRow row)
        {
            return new ShopToShopReceiveMainModel
            {
                ShopToShopReceiveId = row.Table.Columns.Contains("SHOP_RECEIVE_ID") ? Convert.ToInt32(row["SHOP_RECEIVE_ID"]) : 0,
                ShopToShopReceiveNumber = row.Table.Columns.Contains("SHOP_RECEIVE_NUMBER") ? Convert.ToString(row["SHOP_RECEIVE_NUMBER"]) : "",
                RequisitionNumber = row.Table.Columns.Contains("REQUISITION_NUMBER") ? Convert.ToString(row[""]) : "REQUISITION_NUMBER",
                DeliveryNumber = row.Table.Columns.Contains("DELIVERY_NUMBER") ? Convert.ToString(row["DELIVERY_NUMBER"]) : "",
                ReceivedBy = row.Table.Columns.Contains("RECEIVED_BY") ? Convert.ToString(row["RECEIVED_BY"]) : "",
                ReceivedDate = row.Table.Columns.Contains("RECEIVED_DATE") ? Convert.ToString(row["RECEIVED_DATE"]) : "",
                ReceiveShopId = row.Table.Columns.Contains("RECEIVE_SHOP_ID") ? Convert.ToString(row["RECEIVE_SHOP_ID"]) : "",
               

            };
        }
    }
}