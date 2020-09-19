using System;
using System.Data;

namespace Models.AllModel
{
    public class TestModel
    {
        public string TestId { get; set; }
        public string TestName { get; set; }
        public string TestAddress { get; set; }
        public string TestPhone { get; set; }
        public string TestDate { get; set; }

        public static TestModel ConvertTestModel(DataRow row)
        {
            return new TestModel
            {
                TestId = row.Table.Columns.Contains("TEST_ID") ? Convert.ToString(row["TEST_ID"]) : "",
                TestName = row.Table.Columns.Contains("TEST_NAME") ? Convert.ToString(row["TEST_NAME"]) : "",
                TestAddress = row.Table.Columns.Contains("TEST_ADDRESS") ? Convert.ToString(row["TEST_ADDRESS"]) : "",
                TestPhone = row.Table.Columns.Contains("TEST_PHONE") ? Convert.ToString(row["TEST_PHONE"]) : "",
                TestDate = row.Table.Columns.Contains("TEST_DATE") ? Convert.ToDateTime(row["TEST_DATE"]).ToString("dd/MMM/yyyy") : ""

            };
        }
    }
}
