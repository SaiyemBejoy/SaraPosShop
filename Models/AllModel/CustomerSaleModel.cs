using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class CustomerSaleModel
    {
        public int CustomerId { get; set; }
        public int CustomerInfoId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CustomerMedium { get; set; }
        public string Discount { get; set; }
        public string EnrollmentDate { get; set; }

        public SaleInfoModel SaleInfoModels { get; set; }

        public static CustomerSaleModel ConvertCustomerSaleModel(DataRow row)
        {
            return new CustomerSaleModel
            {
                CustomerId = row.Table.Columns.Contains("CUSTOMER_ID") ? Convert.ToInt32(row["CUSTOMER_ID"]) : 0,
                CustomerInfoId = row.Table.Columns.Contains("CUSTOMER_INFO_ID") ? Convert.ToInt32(row["CUSTOMER_INFO_ID"]) : 0,
                CustomerCode = row.Table.Columns.Contains("CUSTOMER_CODE") ? Convert.ToString(row["CUSTOMER_CODE"]) : "",
                CustomerName = row.Table.Columns.Contains("CUSTOMER_NAME") ? Convert.ToString(row["CUSTOMER_NAME"]) :"",
                ContactNo = row.Table.Columns.Contains("CONTACT_NO") ? Convert.ToString(row["CONTACT_NO"]) :"",
                Email = row.Table.Columns.Contains("EMAIL") ? Convert.ToString(row["EMAIL"]) : "",
                Address = row.Table.Columns.Contains("ADDRESS") ? Convert.ToString(row["ADDRESS"]) : "",
                CustomerMedium = row.Table.Columns.Contains("CUSTOMER_MEDIUM") ? Convert.ToString(row["CUSTOMER_MEDIUM"]) : "",
                Discount = row.Table.Columns.Contains("DISCOUNT") ? Convert.ToString(row["DISCOUNT"]) :"",
                EnrollmentDate = row.Table.Columns.Contains("ENROLMENT_DATE") ? Convert.ToString(row["ENROLMENT_DATE"]) : ""
            };
        }
    }
}
