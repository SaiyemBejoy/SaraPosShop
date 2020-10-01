using System;
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
    public class EmployeeRepository : ApplicationDbContext, IEmployeeRepository
    {
        public async Task<string> UpdateEmployee(AuthModel objAuthModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_EMPLOYEE_ID", OracleDbType.Varchar2, objAuthModel.EmployeeId, ParameterDirection.Input),
                new OracleParameter(":P_ACTIVE_YN", OracleDbType.Varchar2, objAuthModel.ActiveYn, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_UPDATE_EMPLOYEE_INFO");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }
    }
}
