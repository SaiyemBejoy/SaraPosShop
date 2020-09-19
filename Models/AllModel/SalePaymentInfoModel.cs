using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class SalePaymentInfoModel
    {
        public int PaymentTypeId { get; set; }
        public int SaleInfoId { get; set; }
        public double DiscountPercent  { get; set; }
        public double DiscountAmount  { get; set; }
        public string PaymentType  { get; set; }
        public double Amount  { get; set; }
        public double SubTotal  { get; set; }
        public double PaidAmount { get; set; }
        public double ReturnAmount  { get; set; }

        public static SalePaymentInfoModel ConvertSalePaymentInfoModel(DataRow row)
        {
            return new SalePaymentInfoModel
            {
                PaymentTypeId = row.Table.Columns.Contains("PAYMENT_TYPE_ID") ? Convert.ToInt32(row["PAYMENT_TYPE_ID"]) : 0,
                SaleInfoId = row.Table.Columns.Contains("SALE_INFO_ID") ? Convert.ToInt32(row["SALE_INFO_ID"]) : 0,
                DiscountPercent = row.Table.Columns.Contains("DISCOUNT_P") ? Convert.ToDouble(row["DISCOUNT_P"]) : 0.0,
                DiscountAmount = row.Table.Columns.Contains("DISCOUNT_A") ? Convert.ToDouble(row["DISCOUNT_A"]) : 0.0,
                PaymentType = row.Table.Columns.Contains("PAYMENT_TYPE") ? Convert.ToString(row["PAYMENT_TYPE"]) : "",
                Amount = row.Table.Columns.Contains("AMOUNT") ? Convert.ToDouble(row["AMOUNT"]) : 0.0,             
                SubTotal = row.Table.Columns.Contains("SUB_TOTAL") ? Convert.ToDouble(row["SUB_TOTAL"]) : 0.0,
                PaidAmount = row.Table.Columns.Contains("PAID_AMOUNT") ? Convert.ToDouble(row["PAID_AMOUNT"]) : 0.0,
                ReturnAmount = row.Table.Columns.Contains("RETURN_AMOUNT") ? Convert.ToDouble(row["RETURN_AMOUNT"]) : 0.0
            };
        }
    }
}
