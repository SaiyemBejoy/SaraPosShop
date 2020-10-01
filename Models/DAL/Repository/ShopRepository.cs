using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
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
    public class ShopRepository : ApplicationDbContext, IShopRepository
    {
        public async Task<ShopModel> GetShopInfo()
        {
            var query = "Select * from VEW_SHOP_INFO ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return ShopModel.ConvertShopModel(dt.Rows[0]);
        }

        public async Task<string> SaveShopInfo(ShopModel objShopModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, objShopModel.ShopId, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_NAME", OracleDbType.Varchar2, objShopModel.ShopName, ParameterDirection.Input),
                new OracleParameter(":P_COUNTRY_ID", OracleDbType.Varchar2, objShopModel.CountryId, ParameterDirection.Input),
                new OracleParameter(":P_COUNTRY_NAME", OracleDbType.Varchar2, objShopModel.CountryName, ParameterDirection.Input),
                new OracleParameter(":P_WAREHOUSE_ID", OracleDbType.Varchar2, objShopModel.WareHouseId, ParameterDirection.Input),
                new OracleParameter(":P_WAREHOUSE_NAME", OracleDbType.Varchar2, objShopModel.WareHouseName, ParameterDirection.Input),
                new OracleParameter(":P_CONTACT_NUMBER", OracleDbType.Varchar2, objShopModel.ContactNo, ParameterDirection.Input),
                new OracleParameter(":P_POSTAL_CODE", OracleDbType.Varchar2, objShopModel.PostalCode, ParameterDirection.Input),
                new OracleParameter(":P_DATE_OF_ENROLLMENT", OracleDbType.Varchar2, objShopModel.DateOfEnrollment, ParameterDirection.Input),
                new OracleParameter(":P_VAT_NO", OracleDbType.Varchar2, objShopModel.VatNo, ParameterDirection.Input),
                new OracleParameter(":P_TIN_NO", OracleDbType.Varchar2, objShopModel.TINNo, ParameterDirection.Input),
                new OracleParameter(":P_BIN_NO", OracleDbType.Varchar2, objShopModel.BINNo, ParameterDirection.Input),
                new OracleParameter(":P_FAX_NO", OracleDbType.Varchar2, objShopModel.FAXNo, ParameterDirection.Input),
                new OracleParameter(":P_EMAIL_ADDRESS", OracleDbType.Varchar2, objShopModel.EmailAddress, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ADDRESS", OracleDbType.Varchar2, objShopModel.ShopAddress, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_URL", OracleDbType.Varchar2, objShopModel.ShopUrl, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SHOP_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> UpdateWareHouse(ShopModel objShopModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WarehouseApi);

                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "Shop", objShopModel);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
