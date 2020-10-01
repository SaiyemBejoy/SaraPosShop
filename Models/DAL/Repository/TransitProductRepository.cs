using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Models.ApiModel;
using Oracle.ManagedDataAccess.Client;

namespace DAL.Repository
{
    public class TransitProductRepository : ApplicationDbContext, ITransitProductRepository
    {
        public async Task<string> DeleteAllTransitProductData(string TransitChallanNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_TRANSIT_CHALLAN_NUM", OracleDbType.Varchar2, TransitChallanNumber, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_TRANSIT_PRODUCT_DELETE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<IEnumerable<TransitProductModel>> GetAllData()
        {
            var query = "Select * from VEW_TRANSIT_PRODUCT_MAIN ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(TransitProductModel.ConvertTransitProductModel);
        }

        public async Task<string> SaveAllTransitProductData(TransitProductModel objTransitProductModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_TRANSIT_PRODUCT_ID", OracleDbType.Varchar2, objTransitProductModel.TransitProductId, ParameterDirection.Input),
                new OracleParameter(":P_TRANSIT_CHALLAN_NUM", OracleDbType.Varchar2, objTransitProductModel.TransitChallnNo, ParameterDirection.Input),
                new OracleParameter(":P_MARKET_PLACE_ID", OracleDbType.Varchar2, objTransitProductModel.MarketPalceId, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, objTransitProductModel.ShopId, ParameterDirection.Input),
                new OracleParameter(":P_CREATED_BY", OracleDbType.Varchar2, objTransitProductModel.CreateddBy, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_TRANSIT_PRODUCT_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllTransitProductItem(TransitProductItem objTransitProductItem)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_TRANSIT_PRODUCT_ITEM_ID", OracleDbType.Varchar2, objTransitProductItem.TransitProductItemId, ParameterDirection.Input),
                new OracleParameter(":P_TRANSIT_CHALLAN_NUM", OracleDbType.Varchar2, objTransitProductItem.TransitChallnNo, ParameterDirection.Input),
                new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, objTransitProductItem.ProductId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_ID", OracleDbType.Varchar2, objTransitProductItem.ItemId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_NAME", OracleDbType.Varchar2, objTransitProductItem.ItemName, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objTransitProductItem.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_QUANTITY", OracleDbType.Varchar2, objTransitProductItem.Quantity, ParameterDirection.Input),
                new OracleParameter(":P_SALE_PRICE", OracleDbType.Varchar2, objTransitProductItem.SalePrice, ParameterDirection.Input),
                new OracleParameter(":P_GROWING_PRICE", OracleDbType.Varchar2, objTransitProductItem.GrowingPrice, ParameterDirection.Input),
                new OracleParameter(":P_DOLLAR_RATE", OracleDbType.Varchar2, objTransitProductItem.DollarRate, ParameterDirection.Input),
                new OracleParameter(":P_GROWING_PERCENT", OracleDbType.Varchar2, objTransitProductItem.GrowingPercent, ParameterDirection.Input),
                new OracleParameter(":P_US_DOLLAR", OracleDbType.Varchar2, objTransitProductItem.USDollar, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_TRANSIT_PRODUCT_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        #region "Transit Return"
        public async Task<string> SaveAllTransitProductReturnData(TransitProductReturnModel objTransitProductReturnModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_TRANSIT_PRODUCT_RETURN_ID", OracleDbType.Varchar2, objTransitProductReturnModel.TransitProductReturnId, ParameterDirection.Input),
                new OracleParameter(":P_TRANSIT_RETURN_CHALLAN_NUM", OracleDbType.Varchar2, objTransitProductReturnModel.TransitReturnChallnNo, ParameterDirection.Input),
                new OracleParameter(":P_MARKET_PLACE_ID", OracleDbType.Varchar2, objTransitProductReturnModel.MarketPalceId, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, objTransitProductReturnModel.ShopId, ParameterDirection.Input),
                new OracleParameter(":P_CREATED_BY", OracleDbType.Varchar2, objTransitProductReturnModel.CreateddBy, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_TRANSIT_PRODUCT_RTN_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllTransitProductReturnItem(TransitProductReturnItem objTransitProductReturnItem)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_TRANSIT_PROD_RTN_ITEM_ID", OracleDbType.Varchar2, objTransitProductReturnItem.TransitProductReurnItemId, ParameterDirection.Input),
                new OracleParameter(":P_TRANSIT_RETURN_CHALLAN_NUM", OracleDbType.Varchar2, objTransitProductReturnItem.TransitReturnChallnNo, ParameterDirection.Input),
                new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, objTransitProductReturnItem.ProductId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_ID", OracleDbType.Varchar2, objTransitProductReturnItem.ItemId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_NAME", OracleDbType.Varchar2, objTransitProductReturnItem.ItemName, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objTransitProductReturnItem.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_QUANTITY", OracleDbType.Varchar2, objTransitProductReturnItem.Quantity, ParameterDirection.Input),
                new OracleParameter(":P_SALE_PRICE", OracleDbType.Varchar2, objTransitProductReturnItem.SalePrice, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_TRANSIT_PROD_RTN_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> DeleteAllTransitReturnProductData(string ChallanNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_TRANSIT_RTN_CHALLAN_NUM", OracleDbType.Varchar2, ChallanNumber, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_TRANSIT_PRODUCT_RTN_DELETE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<IEnumerable<TransitProductReturnModel>> GetAllReturnData()
        {
            var query = "Select * from VEW_TRANSIT_RTN_PRODUCT_MAIN ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(TransitProductReturnModel.ConvertTransitProductReturnModel);
        }
        #endregion
    }
}
