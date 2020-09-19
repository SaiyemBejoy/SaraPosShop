using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel.Report
{
    public class SaleReportModel
    {
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        [DisplayName("Sub Category")]
        public string SubCategoryName { get; set; }
        [DisplayName("Category")]
        public string CategoryName { get; set; }

        public DateTime FromDateAndTime { get; set; }

        public DateTime ToDateAndTime { get; set; }

        public string RadioFor { get; set; }

        public string ReportType { get; set; }
    }

    public class StockReportModel
    {
        [Required]
        [DisplayName("Sub Category")]
        public string SubCategoryName { get; set; }
        [Required]
        [DisplayName("Category")]
        public string CategoryName { get; set; }

        public string ProductStyle { get; set; }

        public string RadioFor { get; set; }

        public string ReportType { get; set; }
    }
}
