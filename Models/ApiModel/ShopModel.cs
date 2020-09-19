using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class ShopModel
    {
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public int WareHouseId { get; set; }

        public string WareHouseName { get; set; }

        public int ShopId { get; set; }

        public string ShopName { get; set; }

        public string ContactNo { get; set; }

        public string PostalCode { get; set; }

        public string DateOfEnrollment { get; set; }

        public string VatNo { get; set; }

        public string TINNo { get; set; }

        public string BINNo { get; set; }

        public string FAXNo { get; set; }

        public string EmailAddress { get; set; }

        public string ShopAddress { get; set; }

        public string ShopUrl { get; set; }

        public string UpdateBy { get; set; }


        public static ShopModel ConvertShopModel(DataRow row)
        {
            return new ShopModel
            {
                ShopId = row.Table.Columns.Contains("SHOP_ID") ? Convert.ToInt32(row["SHOP_ID"]) : 0,
                ShopName = row.Table.Columns.Contains("SHOP_NAME") ? Convert.ToString(row["SHOP_NAME"]) : "",
                CountryId = row.Table.Columns.Contains("COUNTRY_ID") ? Convert.ToInt32(row["COUNTRY_ID"]) : 0,
                CountryName = row.Table.Columns.Contains("COUNTRY_NAME") ? Convert.ToString(row["COUNTRY_NAME"]) : "",
                WareHouseId = row.Table.Columns.Contains("WAREHOUSE_ID") ? Convert.ToInt32(row["WAREHOUSE_ID"]) : 0,
                WareHouseName = row.Table.Columns.Contains("WAREHOUSE_NAME") ? Convert.ToString(row["WAREHOUSE_NAME"]) : "",
                ContactNo = row.Table.Columns.Contains("CONTACT_NUMBER") ? Convert.ToString(row["CONTACT_NUMBER"]) : "",
                PostalCode = row.Table.Columns.Contains("POSTAL_CODE") ? Convert.ToString(row["POSTAL_CODE"]) : "",
                DateOfEnrollment = row.Table.Columns.Contains("DATE_OF_ENROLLMENT") ? Convert.ToString(row["DATE_OF_ENROLLMENT"]) : "",
                VatNo = row.Table.Columns.Contains("VAT_NO") ? Convert.ToString(row["VAT_NO"]) : "",
                TINNo = row.Table.Columns.Contains("TIN_NO") ? Convert.ToString(row["TIN_NO"]) : "",
                BINNo = row.Table.Columns.Contains("BIN_NO") ? Convert.ToString(row["BIN_NO"]) : "",
                FAXNo = row.Table.Columns.Contains("FAX_NO") ? Convert.ToString(row["FAX_NO"]) : "",
                EmailAddress = row.Table.Columns.Contains("EMAIL_ADDRESS") ? Convert.ToString(row["EMAIL_ADDRESS"]) : "",
                ShopAddress = row.Table.Columns.Contains("SHOP_ADDRESS") ? Convert.ToString(row["SHOP_ADDRESS"]) : "",
                ShopUrl = row.Table.Columns.Contains("SHOP_URL") ? Convert.ToString(row["SHOP_URL"]) : ""
            };
        }
    }
}
