using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class SearchHintsModel
    {
        public string SearchValue { get; set; }

        public static SearchHintsModel ConvertSearchHintsModel(DataRow row)
        {
            return new SearchHintsModel
            {
                SearchValue = row.Table.Columns.Contains("STYLE_NAME") ? Convert.ToString(row["STYLE_NAME"]) : ""
               
            };
        }
    }
}
