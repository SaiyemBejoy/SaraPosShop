using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class DashboardModel
    {
        public double TotalSale { get; set; }
        public double ToDaysSale { get; set; }
        public double CurrentMonthSale { get; set; }
        public double LastSevenDaysSale { get; set; }

        public static DashboardModel ConvertDashboardModel(DataRow row)
        {
            return new DashboardModel
            {
                TotalSale = row.Table.Columns.Contains("TOTAL_SALE") ? Convert.ToDouble(row["TOTAL_SALE"]) : 0.0,
                ToDaysSale = row.Table.Columns.Contains("TODAYS_SALE") ? Convert.ToDouble(row["TODAYS_SALE"]) : 0.0,
                CurrentMonthSale = row.Table.Columns.Contains("CURRENT_MONTH_SALE") ? Convert.ToDouble(row["CURRENT_MONTH_SALE"]) : 0.0,
                LastSevenDaysSale = row.Table.Columns.Contains("LAST_SAVEN_DAYS_SALE") ? Convert.ToDouble(row["LAST_SAVEN_DAYS_SALE"]) :0.0,
               

            };
        }
    }
}
