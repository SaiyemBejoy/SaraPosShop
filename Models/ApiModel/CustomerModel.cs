using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        
        public string CustomerCode { get; set; }
            
        public string CustomerFirstName { get; set; }
       
        public string CustomerLastName { get; set; }
          
        public string ContactNo { get; set; }

        public string Email { get; set; }
       
        public string Address { get; set; }
        
        public string DiscountPercent { get; set; }
        public string CustomerTypeName { get; set; }
        public string Active_YN { get; set; }


    }

    public class PrivilegeCustomerModel
    {
        public int CustomerId { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public string DateOfBirth { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }
        public string WareHouseId { get; set; }
        public string ShopId { get; set; }

        public string UpdateBy { get; set; }
    }
}
