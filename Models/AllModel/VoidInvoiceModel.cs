using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class VoidInvoiceModel
    {
        public int SaleInfoId { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public int SalesmanId { get; set; }
        public string SalesmanName { get; set; }
        public int TotalItem { get; set; }
        public double TotalAmount { get; set; }
        public double Vat { get; set; }
        public int DiscountPercentage { get; set; }
        public double DiscountAmount { get; set; }
        public double SubTotal { get; set; }
        public int CustomerId { get; set; }
        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public double NetAmount { get; set; }

        public List<VoidInvoiceItem> VoidInvoiceItemList { get; set; }

        public static VoidInvoiceModel ConvertVoidInvoiceModelForDataTable(DataRow row)
        {
            return new VoidInvoiceModel
            {
                SaleInfoId = row.Table.Columns.Contains("SALE_INFO_ID") ? Convert.ToInt32(row["SALE_INFO_ID"]) : 0,
                InvoiceNumber = row.Table.Columns.Contains("INVOICE_NUMBER") ? Convert.ToString(row["INVOICE_NUMBER"]) : "",
                InvoiceDate = row.Table.Columns.Contains("INVOICE_DATE") ? Convert.ToString(row["INVOICE_DATE"]) : "",
                SalesmanId = row.Table.Columns.Contains("SALESMAN_ID") ? Convert.ToInt32(row["SALESMAN_ID"]) : 0,
                TotalItem = row.Table.Columns.Contains("TOTAL_ITEM") ? Convert.ToInt32(row["TOTAL_ITEM"]) : 0,
                TotalAmount = row.Table.Columns.Contains("TOTAL_AMOUNT") ? Convert.ToDouble(row["TOTAL_AMOUNT"]) : 0.0,
                Vat = row.Table.Columns.Contains("VAT") ? Convert.ToDouble(row["VAT"]) : 0.0,
                DiscountPercentage = row.Table.Columns.Contains("DISCOUNT_P") ? Convert.ToInt32(row["DISCOUNT_P"]) : 0,
                DiscountAmount = row.Table.Columns.Contains("DISCOUNT_A") ? Convert.ToDouble(row["DISCOUNT_A"]) : 0.0,
                SubTotal = row.Table.Columns.Contains("SUB_TOTAL") ? Convert.ToDouble(row["SUB_TOTAL"]) : 0.0,
                CustomerId = row.Table.Columns.Contains("CUSTOMER_ID") ? Convert.ToInt32(row["CUSTOMER_ID"]) : 0,
                CreatedById = row.Table.Columns.Contains("CREATED_BY") ? Convert.ToString(row["CREATED_BY"]) : "",
                CreatedByName = row.Table.Columns.Contains("CREATED_NAME") ? Convert.ToString(row["CREATED_ NAME"]) : "",
                NetAmount = row.Table.Columns.Contains("NET_AMOUNT") ? Convert.ToDouble(row["NET_AMOUNT"]) : 0.0
            };
        }


    }
}
