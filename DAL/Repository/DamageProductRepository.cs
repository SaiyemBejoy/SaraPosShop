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
    public class DamageProductRepository : ApplicationDbContext, IDamageProductRepository
    {
        public async Task<DamageMainModel> GetAllDamageInfoForTransfer(string challanNo)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":DAMAGE_CHALLAN_NO", OracleDbType.Varchar2, challanNo, ParameterDirection.Input)
            };

            var query = "Select * from VEW_DAMAGE_MAIN  Where DAMAGE_CHALLAN_NO = : DAMAGE_CHALLAN_NO ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return DamageMainModel.ConvertDamageMainModel(dt.Rows[0]);
        }

        public async Task<IEnumerable<DamageMainItemModel>> GetAllDamageItemByChallanNo(string challanNo)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":DAMAGE_CHALLAN_NO", OracleDbType.Varchar2, challanNo, ParameterDirection.Input)
            };

            var query = "Select * from VEW_DAMAGE_MAIN_ITEM Where DAMAGE_CHALLAN_NO = : DAMAGE_CHALLAN_NO ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return (dt.Rows).Cast<DataRow>().Select(DamageMainItemModel.ConvertDamageMainItemModel);
        }

        public async Task<IEnumerable<DamageMainModel>> GetAllData()
        {
            var query = "Select * from VEW_DAMAGE_MAIN Where TRANSFER_YN ='N' ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DamageMainModel.ConvertDamageMainModel);
        }

        public async Task<string> GetMaxDamageChallanNo()
        {
            string query = " SELECT  'DM' || LPAD ( (SELECT SHOP_ID FROM Shop), 2, 0) || " +
                           " (SELECT LPAD ( NVL (MAX (SUBSTR ( (DAMAGE_CHALLAN_NO), 9, 8)),0)+ 1, 8,0) FROM DAMAGE_MAIN) DAMAGE_CHALLAN_NO FROM DUAL ";
            var maxDamageChallanNo = await GetSingleStringAsync(query, null);
            return maxDamageChallanNo;
        }

        public async Task<string> SaveAllDamageMainData(DamageMainModel objDamageMainModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_DAMAGE_ID", OracleDbType.Varchar2, objDamageMainModel.DamageId, ParameterDirection.Input),
                new OracleParameter(":P_DAMAGE_CHALLAN_NO", OracleDbType.Varchar2, objDamageMainModel.DamageChallanNo, ParameterDirection.Input),
                new OracleParameter(":P_CREATED_DATE", OracleDbType.Varchar2, objDamageMainModel.CreatedDate, ParameterDirection.Input),
                new OracleParameter(":P_CREATED_BY", OracleDbType.Varchar2, objDamageMainModel.CreatedBy, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, objDamageMainModel.ShopId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_DAMAGE_MAIN_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllDamageMainItemData(DamageMainItemModel objDamageMainItemModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_DAMAGE_MAIN_ITEM_ID", OracleDbType.Varchar2, objDamageMainItemModel.DamageMainItemId, ParameterDirection.Input),
                new OracleParameter(":P_DAMAGE_CHALLAN_NO", OracleDbType.Varchar2, objDamageMainItemModel.DamageChallanNo, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objDamageMainItemModel.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_NAME", OracleDbType.Varchar2, objDamageMainItemModel.ItemName, ParameterDirection.Input),
                new OracleParameter(":P_PRICE", OracleDbType.Varchar2, objDamageMainItemModel.Price, ParameterDirection.Input),
                new OracleParameter(":P_QUANTITY", OracleDbType.Varchar2, objDamageMainItemModel.Quantity, ParameterDirection.Input),
                new OracleParameter(":P_REMARKS", OracleDbType.Varchar2, objDamageMainItemModel.Remarks, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_ID", OracleDbType.Varchar2, objDamageMainItemModel.ItemId, ParameterDirection.Input),
                new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, objDamageMainItemModel.ProductId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_DAMAGE_MAIN_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }
    }
}
