using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models;
using Models.AllModel;
using Models.ApiModel;
using Oracle.ManagedDataAccess.Client;

namespace DAL.Repository
{
    public class RequisitionRepository : ApplicationDbContext, IRequisitionRepository
    {
        public async Task<IEnumerable<RequisitionMainItemModel>> GetAllItemInfoDataBySaleInfoId(int requisitionId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":REQUISITION_ID", OracleDbType.Varchar2, requisitionId, ParameterDirection.Input)
            };
            var query = "Select * from VEW_REQUISITION_MAIN_ITEM WHERE REQUISITION_ID = :REQUISITION_ID ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return ((dt.Rows).Cast<DataRow>().Select(RequisitionMainItemModel.ConvertRequisitionMainItemModel));
        }

        public async Task<IEnumerable<RequisitionMainModel>> GetAllWarehouseRequisitionData()
        {
            var query = "Select * from VEW_REQUISITION_MAIN ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(RequisitionMainModel.ConvertRequisitionMainModel);
        }

        public async Task<IEnumerable<DcProductSearchModel>> GetDcProductByStyleName(string styleName)
        {
            IEnumerable<DcProductSearchModel> model = new List<DcProductSearchModel>();
            if (!string.IsNullOrWhiteSpace(styleName))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(DatabaseConfiguration.WarehouseApi);

                    var responseTask = client.GetAsync("WarehouseStock?styleName=" + styleName);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<DcProductSearchModel>>();
                        readTask.Wait();
                        model = readTask.Result;
                    }
                }
            }
            return model;
        }

        public async Task<string> GetMaxRequisition()
        {
            string query = " SELECT LPAD ( (SELECT SHOP_ID FROM Shop), 2, 0) || (SELECT LPAD (NVL (MAX (SUBSTR ( (REQUISITION_NO), 9, 8)), 0) + 1,8,0) FROM REQUISITION_MAIN) REQUISITION_NO FROM DUAL ";
            var maxRequisition = await GetSingleStringAsync(query, null);
            return maxRequisition;
        }

        public async Task<string> SaveAllRequisition(RequisitionMainModel objRequisitionMainModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_REQUISITION_ID", OracleDbType.Varchar2, objRequisitionMainModel.RequisitionId, ParameterDirection.Input),
                new OracleParameter(":P_REQUISITION_NO", OracleDbType.Varchar2, objRequisitionMainModel.RequisitionNo, ParameterDirection.Input),
                new OracleParameter(":P_REQUISITION_DATE", OracleDbType.Varchar2, objRequisitionMainModel.RequisitionDate, ParameterDirection.Input),
                new OracleParameter(":P_CREATED_BY", OracleDbType.Varchar2, objRequisitionMainModel.CreatedBy, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, objRequisitionMainModel.ShopId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_REQUISITION_MAIN_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }
        public async Task<string> SaveAllRequisitionItem(RequisitionMainItemModel objRequisitionMainItemModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_REQUISITION_MAIN_ITEM_ID", OracleDbType.Varchar2, objRequisitionMainItemModel.RequisitionMainItemId, ParameterDirection.Input),
                new OracleParameter(":P_REQUISITION_ID", OracleDbType.Varchar2, objRequisitionMainItemModel.RequisitionId, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objRequisitionMainItemModel.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_NAME", OracleDbType.Varchar2, objRequisitionMainItemModel.ItemName, ParameterDirection.Input),
                new OracleParameter(":P_PRICE", OracleDbType.Varchar2, objRequisitionMainItemModel.Price, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_REQUISITION_MAIN_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }
    }
}
