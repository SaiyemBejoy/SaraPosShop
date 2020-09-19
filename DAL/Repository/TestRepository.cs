using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Oracle.ManagedDataAccess.Client;

namespace DAL.Repository
{
    public class TestRepository : ApplicationDbContext, ITestRepository
    {
        public async Task<IEnumerable<TestModel>> GetAllData()
        {
            var query = "Select * from TEST_TABLE ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(TestModel.ConvertTestModel);
        }

        public async Task<TestModel> GetSingleData(int id)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":TEST_ID", OracleDbType.Varchar2, id, ParameterDirection.Input)
            };

            var query = "Select * from TEST_TABLE WHERE TEST_ID = :TEST_ID ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return TestModel.ConvertTestModel(dt.Rows[0]);
        }

        public async Task<TestModel> GetSingleData(string name)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":TEST_NAME", OracleDbType.Varchar2, name, ParameterDirection.Input)
            };

            var query = "Select * from TEST_TABLE WHERE TEST_NAME = :TEST_NAME ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return TestModel.ConvertTestModel(dt.Rows[0]);
        }

        public async Task<int> GetMaxId()
        {
            string query = "Select Max(TEST_ID) from TEST_TABLE ";
            var maxId = await GetSingleIntAsync(query, null);
            return maxId;
        }

        public async Task<bool> CheckIsExist(int id)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":TEST_ID", OracleDbType.Varchar2, id, ParameterDirection.Input)
            };

            query.Append("Select count(TEST_ID) from TEST_TABLE WHERE TEST_ID = :TEST_ID");
            var result = await GetSingleIntAsync(query.ToString(), param);

            return result > 0;
        }

        public async Task<string> SaveOrUpdate(TestModel objTestModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":TEST_ID", OracleDbType.Varchar2, objTestModel.TestId, ParameterDirection.Input),
                new OracleParameter(":TEST_NAME", OracleDbType.Varchar2, objTestModel.TestName, ParameterDirection.Input),
                new OracleParameter(":TEST_ADDRESS", OracleDbType.Varchar2, objTestModel.TestAddress, ParameterDirection.Input),
                new OracleParameter(":TEST_PHONE", OracleDbType.Varchar2, objTestModel.TestPhone, ParameterDirection.Input),
                new OracleParameter(":TEST_DATE", OracleDbType.Varchar2, objTestModel.TestDate, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_TEST_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> Delete(int id)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":TEST_ID", OracleDbType.Varchar2, id, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_TEST_DELETE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }
    }
}
