using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class SalesManWiseSaleReportForDc
    {
        public string SalesManId { get; set; }
        public string InvoiceDate { get; set; }
        public int SaleQuantity { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double NetAmount { get; set; }
        public double VatAmount { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }

        public static SalesManWiseSaleReportForDc ConvertSalesManWiseSaleReportForDc(DataRow row)
        {
            return new SalesManWiseSaleReportForDc
            {
                SalesManId = row.Table.Columns.Contains("SALESMAN_ID") ? Convert.ToString(row["SALESMAN_ID"]) : "",
                InvoiceDate = row.Table.Columns.Contains("INVOICE_DATE") ? Convert.ToString(row["INVOICE_DATE"]) : "",
                Category = row.Table.Columns.Contains("PRODUCT_CATEGORY") ? Convert.ToString(row["PRODUCT_CATEGORY"]) : "",
                SubCategory = row.Table.Columns.Contains("PRODUCT_SUB_CATEGORY") ? Convert.ToString(row["PRODUCT_SUB_CATEGORY"]) : "",
                SaleQuantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0,
                TotalAmount = row.Table.Columns.Contains("TOTAL") ? Convert.ToDouble(row["TOTAL"]) : 0,
                DiscountAmount = row.Table.Columns.Contains("DISCOUNT_AMOUNT") ? Convert.ToDouble(row["DISCOUNT_AMOUNT"]) : 0,
                NetAmount = row.Table.Columns.Contains("NET_AMOUNT") ? Convert.ToDouble(row["NET_AMOUNT"]) : 0,
                VatAmount = row.Table.Columns.Contains("VAT_AMOUNT") ? Convert.ToDouble(row["VAT_AMOUNT"]) : 0
            };
        }
    }
}
