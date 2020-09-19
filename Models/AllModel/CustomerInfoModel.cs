using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ApiModel;

namespace Models.AllModel
{
    public class CustomerInfoModel:CustomerModel
    {

        //public int CustomerId { get; set; }

        //public string CustomerCode { get; set; }

        //public string CustomerName { get; set; }

        //public string ContactNo { get; set; }

        //public string Email { get; set; }

        //public string Address { get; set; }

        //public string DiscountPercent { get; set; }
        //public string CustomerTypeName { get; set; }
        //public string Active_YN { get; set; }

        public static CustomerInfoModel ConvertCustomerInfoModel(DataRow row)
        {
            return new CustomerInfoModel
            {
                CustomerId = row.Table.Columns.Contains("CUSTOMER_ID") ? Convert.ToInt32(row["CUSTOMER_ID"]) : 0,
                CustomerCode = row.Table.Columns.Contains("CUSTOMER_CODE") ? Convert.ToString(row["CUSTOMER_CODE"]) : "",
                CustomerFirstName = row.Table.Columns.Contains("CUSTOMER_NAME") ? Convert.ToString(row["CUSTOMER_NAME"]) : "",
                ContactNo = row.Table.Columns.Contains("CONTACT_NO") ? Convert.ToString(row["CONTACT_NO"]) : "",
                Email = row.Table.Columns.Contains("EMAIL") ? Convert.ToString(row["EMAIL"]) : "",
                Address = row.Table.Columns.Contains("ADDRESS") ? Convert.ToString(row["ADDRESS"]) : "",
                DiscountPercent = row.Table.Columns.Contains("DISCOUNT") ? Convert.ToString(row["DISCOUNT"]) : "",
                CustomerTypeName = row.Table.Columns.Contains("CUSTOMER_TYPE") ? Convert.ToString(row["CUSTOMER_TYPE"]) : "",
                Active_YN = row.Table.Columns.Contains("ACTIVE_YN") ? Convert.ToString(row["ACTIVE_YN"]): ""

            };
        }
    }
}
