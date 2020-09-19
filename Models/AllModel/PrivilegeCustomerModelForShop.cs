using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class PrivilegeCustomerModelForShop
    {
        public int CustomerId { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Discount { get; set; }

        public string ActiveYN { get; set; }

        public string CustomerType { get; set; }

        public static PrivilegeCustomerModelForShop ConvertPrivilegeCustomerModelForShop(DataRow row)
        {
            return new PrivilegeCustomerModelForShop
            {
                CustomerCode = row.Table.Columns.Contains("CUSTOMER_CODE") ? Convert.ToString(row["CUSTOMER_CODE"]) : "",
                CustomerName = row.Table.Columns.Contains("CUSTOMER_NAME") ? Convert.ToString(row["CUSTOMER_NAME"]) : "",
                ContactNo = row.Table.Columns.Contains("CONTACT_NO") ? Convert.ToString(row["CONTACT_NO"]) : "",
                Email = row.Table.Columns.Contains("EMAIL") ? Convert.ToString(row["EMAIL"]) : "",
                Address = row.Table.Columns.Contains("ADDRESS") ? Convert.ToString(row["ADDRESS"]) : "",
                Discount = row.Table.Columns.Contains("DISCOUNT") ? Convert.ToString(row["DISCOUNT"]) : "",
                CustomerType = row.Table.Columns.Contains("CUSTOMER_TYPE") ? Convert.ToString(row["CUSTOMER_TYPE"]) : "",
                ActiveYN = row.Table.Columns.Contains("ACTIVE_YN") ? Convert.ToString(row["ACTIVE_YN"]) : ""
            };
        }
    }
}
