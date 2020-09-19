using System;
using System.Data;

namespace Models
{
    public class DropDown
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public static DropDown ConvertToModel(DataRow row)
        {
            return new DropDown
            {
                Value = row.Table.Columns.Contains("Value") ? Convert.ToString(row["Value"]) : "",
                Text = row.Table.Columns.Contains("Text") ? Convert.ToString(row["Text"]) : ""

            };
        }
    }
}