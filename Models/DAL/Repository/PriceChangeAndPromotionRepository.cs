using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models.ApiModel;
using Oracle.ManagedDataAccess.Client;

namespace DAL.Repository
{
    public class PriceChangeAndPromotionRepository : ApplicationDbContext, IPriceChangeAndPromotionRepository
    {
        public async Task<string> ChangeAllPriceByBarcode(CircularPriceChangeSub objCircularPriceChangeSub)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objCircularPriceChangeSub.BarcodeNo, ParameterDirection.Input),
                new OracleParameter(":P_NEW_PRICE", OracleDbType.Varchar2, objCircularPriceChangeSub.NewSalePrice, ParameterDirection.Input),
                new OracleParameter(":P_OLD_PRICE", OracleDbType.Varchar2, objCircularPriceChangeSub.PreSalePrice, ParameterDirection.Input),
                new OracleParameter(":P_EFFECTIVE_DATE", OracleDbType.Varchar2, objCircularPriceChangeSub.EffectiveDate, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_CIR_PRICE_CHANGE_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }
    }
}
