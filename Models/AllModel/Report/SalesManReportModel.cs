using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel.Report
{
    public class SalesManReportModel
    {
        [Required]
        public string FromDate { get; set; }
        [Required]
        public string ToDate { get; set; }
        [DisplayName("Sales Man")]
        public string SalesMan{ get; set; }

        public string RadioFor { get; set; }

        public string ReportType { get; set; }
    }
}
