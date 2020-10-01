using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DAL.DBManager
{
    public class ApplicationDbContext : DatabaseConfiguration
    {
        private readonly OracleConnection _con;
        OracleTransaction _trans;

        public ApplicationDbContext()
        {
            _con = GetConnection();
        }

        private OracleConnection GetConnection()
        {
            string connection = ConnectionString;
            OracleConnection con = new OracleConnection(connection);
            return con;
        }

        private void ConnectionOpen()
        {
            if (_con.State == ConnectionState.Closed)
                _con.Open();
        }

        private void ConnectionClose()
        {
            if (_con.State == ConnectionState.Open)
                _con.Close();
        }

        private async Task ConnectionOpenAsync()
        {
            if (_con.State == ConnectionState.Closed)
                await _con.OpenAsync();
        }


        //Get and Set Data To Database
        public DataTable GetDataThroughDataTable(string sqlQuery, List<OracleParameter> param)
        {
            DataTable dt = new DataTable();
            try
            {
                ConnectionOpen();

                using (OracleCommand cmd = new OracleCommand(sqlQuery, _con))
                {
                    cmd.CommandType = CommandType.Text;

                    if (param != null)
                        cmd.Parameters.AddRange(param.ToArray());

                    using (OracleDataAdapter oda = new OracleDataAdapter(cmd))
                    {
                        using (dt)
                        {
                            oda.Fill(dt);
                        }
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                ConnectionClose();
            }

            return dt;
        }

        public DataSet GetDataThroughDataSet(string sqlQuery, List<OracleParameter> param)
        {
            DataSet ds = null;
            try
            {
                ConnectionOpen();
                using (OracleCommand cmd = new OracleCommand(sqlQuery, _con))
                {
                    cmd.CommandType = CommandType.Text;

                    if (param != null)
                        cmd.Parameters.AddRange(param.ToArray());

                    using (OracleDataAdapter oda = new OracleDataAdapter(cmd))
                    {
                        ds = new System.Data.DataSet();
                        using (ds)
                        {
                            oda.Fill(ds);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                ConnectionClose();
            }
            return ds;
        }

        public DataSet GetDataThroughDataSetRpt(string sqlQuery, List<OracleParameter> param,string viewName)
        {
            DataSet ds = null;
            try
            {
                ConnectionOpen();
                using (OracleCommand cmd = new OracleCommand(sqlQuery, _con))
                {
                    cmd.CommandType = CommandType.Text;

                    if (param != null)
                        cmd.Parameters.AddRange(param.ToArray());

                    using (OracleDataAdapter oda = new OracleDataAdapter(cmd))
                    {
                        ds = new System.Data.DataSet();
                        using (ds)
                        {
                            oda.Fill(ds,viewName);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                ConnectionClose();
            }
            return ds;
        }


        public string GetSingleString(String query, List<OracleParameter> param)
        {
            var dt = GetDataThroughDataTable(query, param);
            return dt.Rows.Count > 0 ? Convert.ToString(dt.Rows[0][0]) : "";
        }

        public Int32 GetSingleInt(String query, List<OracleParameter> param)
        {
            var dt = GetDataThroughDataTable(query, param);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
        }

        public string ExecuteNonQuery(string query, List<OracleParameter> param)
        {
            string returnMessage;

            try
            {
                ConnectionOpen();

                using (OracleCommand cmd = new OracleCommand(query, _con))
                {
                    cmd.CommandType = CommandType.Text;

                    if (param != null)
                        cmd.Parameters.AddRange(param.ToArray());

                    _trans = _con.BeginTransaction();
                    cmd.ExecuteNonQuery();
                    _trans.Commit();
                    returnMessage = cmd.Parameters["P_MESSAGE"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                _trans.Rollback();
                return ex.ToString();
            }
            finally
            {
                ConnectionClose();
            }

            return returnMessage;
        }


        //Get and Set Data To Database with Async
        public async Task<DataTable> GetDataThroughDataTableAsync(string sqlQuery, List<OracleParameter> param)
        {
            DataTable dt = new DataTable();
            try
            {
                await ConnectionOpenAsync();
                using (OracleCommand cmd = new OracleCommand(sqlQuery, _con))
                {
                    cmd.CommandType = CommandType.Text;

                    if (param != null)
                        cmd.Parameters.AddRange(param.ToArray());
                    
                    using (OracleDataAdapter oda = new OracleDataAdapter(cmd))
                    {
                        using (dt)
                        {
                            await Task.Run(() => oda.Fill(dt));
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                ConnectionClose();
            }
            return dt;
        }

        public async Task<DataSet> GetDataThroughDataSetRptAsync(string sqlQuery, List<OracleParameter> param, string viewName)
        {
            DataSet ds = null;
            try
            {
                await ConnectionOpenAsync();
                using (OracleCommand cmd = new OracleCommand(sqlQuery, _con))
                {
                    cmd.CommandType = CommandType.Text;

                    if (param != null)
                        cmd.Parameters.AddRange(param.ToArray());

                    using (OracleDataAdapter oda = new OracleDataAdapter(cmd))
                    {
                        ds = new System.Data.DataSet();
                        using (ds)
                        {
                            await Task.Run(() => oda.Fill(ds, viewName));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                ConnectionClose();
            }
            return ds;
        }

        public async Task<DataSet> GetDataThroughDataSetAsync(string sqlQuery, List<OracleParameter> param)
        {
            DataSet ds = null;
            try
            {
                await ConnectionOpenAsync();
                using (OracleCommand cmd = new OracleCommand(sqlQuery, _con))
                {
                    cmd.CommandType = CommandType.Text;

                    if (param != null)
                        cmd.Parameters.AddRange(param.ToArray());

                    using (OracleDataAdapter oda = new OracleDataAdapter(cmd))
                    {
                        ds = new System.Data.DataSet();
                        using (ds)
                        {
                            await Task.Run(() => oda.Fill(ds));
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                ConnectionClose();
            }
            return ds;
        }

        public async Task<string> GetSingleStringAsync(String query, List<OracleParameter> param)
        {
            var dt = await GetDataThroughDataTableAsync(query, param);
            return dt.Rows.Count > 0 ? Convert.ToString(dt.Rows[0][0]) : "";
        }

        public async Task<Int32> GetSingleIntAsync(String query, List<OracleParameter> param)
        {
            var dt = await GetDataThroughDataTableAsync(query, param);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
        }

        public async Task<string> ExecuteNonQueryAsync(string query, List<OracleParameter> param)
        {
            string returnMessage;

            try
            {
                await ConnectionOpenAsync();
                using (OracleCommand cmd = new OracleCommand(query))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;

                    if (param != null)
                        cmd.Parameters.AddRange(param.ToArray());

                    _trans = _con.BeginTransaction();
                    await cmd.ExecuteNonQueryAsync();
                    _trans.Commit();
                    ConnectionClose();

                    returnMessage = cmd.Parameters["P_MESSAGE"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                _trans.Rollback();
                return ex.ToString();
            }
            finally
            {
                ConnectionClose();
            }

            return returnMessage;
        }

        public async Task<string> ExecuteListNonQueryAsync(string query, List<List<OracleParameter>> param)
        {
            string returnMessage;

            try
            {
                await ConnectionOpenAsync();
                using (OracleCommand cmd = new OracleCommand(query))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    _trans = _con.BeginTransaction();
                    foreach (var paramitter in param)
                    {
                        if (paramitter != null)
                            cmd.Parameters.AddRange(paramitter.ToArray());
                        await cmd.ExecuteNonQueryAsync();
                       
                    }
                    _trans.Commit();
                    ConnectionClose();

                    returnMessage = cmd.Parameters["P_MESSAGE"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                _trans.Rollback();
                return ex.ToString();
            }
            finally
            {
                ConnectionClose();
            }

            return returnMessage;
        }
    }
}