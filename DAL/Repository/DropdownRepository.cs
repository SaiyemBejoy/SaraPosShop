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
    public class DropdownRepository : ApplicationDbContext, IDropdownRepository
    {

        #pragma warning disable 1998
        public async Task<IEnumerable<DropDown>> GetAllShopList()
        #pragma warning restore 1998
        {
            var dropDowns = new List<DropDown>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WarehouseApi);
                    //HTTP GET
                    var responseTask = client.GetAsync("Shop");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<ShopModel>>();
                        readTask.Wait();

                        IEnumerable<ShopModel> shopList = readTask.Result;
                        foreach (var val in shopList)
                        {
                            DropDown dropDown = new DropDown { Value = val.ShopId.ToString(), Text = val.ShopName };
                            dropDowns.Add(dropDown);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return dropDowns;
        }


        public async Task<IEnumerable<DropDown>> GetAllShopListForShopRequisition()
        #pragma warning restore 1998
        {
            var dropDowns = new List<DropDown>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WarehouseApi);
                    //HTTP GET
                    var responseTask = client.GetAsync("ShopToShopRequisition");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<ShopModel>>();
                        readTask.Wait();

                        IEnumerable<ShopModel> shopList = readTask.Result;
                        foreach (var val in shopList)
                        {
                            DropDown dropDown = new DropDown { Value = val.ShopId.ToString(), Text = val.ShopName };
                            dropDowns.Add(dropDown);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return dropDowns;
        }

        public async Task<IEnumerable<DropDown>> GetAllStoreDeliveryNumber()
        {
            var query = " SELECT DISTINCT (STORE_DELIVERY_NUMBER) AS VALUE, STORE_DELIVERY_NUMBER AS Text FROM DELIVERED_PRODUCT " +
                        "WHERE STORE_DELIVERY_NUMBER NOT IN " +
                        "(SELECT STORE_RECEIVE_CHALLAN_NO FROM STORE_RECEIVE) " +
                        "ORDER BY STORE_DELIVERY_NUMBER ASC ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }
        public async Task<IEnumerable<DropDown>> GetAllEmployeeInfo()
        {
            var query = " SELECT  EMPLOYEE_ID AS VALUE, EMPLOYEE_NAME AS Text FROM EMPLOYEE_DISTRIBUTION Where ACTIVE_YN ='Y' AND SUPER_ADMIN_YN ='N' ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetAllSubCategoryName()
        {
            var query = " SELECT DISTINCT PRODUCT_SUB_CATEGORY AS VALUE, PRODUCT_SUB_CATEGORY AS Text" +
                        " FROM VEW_DELIVERED_PRODUCT";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetAllCategoryName()
        {
            var query = " SELECT DISTINCT PRODUCT_CATEGORY AS VALUE, PRODUCT_CATEGORY AS Text" +
                        " FROM VEW_DELIVERED_PRODUCT";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetAllSubCategoryName(string categoryName)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":PRODUCT_CATEGORY", OracleDbType.Varchar2, categoryName, ParameterDirection.Input)
            };

            var query = " SELECT DISTINCT PRODUCT_SUB_CATEGORY AS VALUE, PRODUCT_SUB_CATEGORY AS Text" +
                        " FROM VEW_DELIVERED_PRODUCT WHERE PRODUCT_CATEGORY = :PRODUCT_CATEGORY";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetAllStyleName()
        {
            var query = "SELECT DISTINCT PRODUCT_ID AS VALUE, STYLE_NAME AS TEXT FROM VEW_STORE_HOUSE_INFO ORDER BY PRODUCT_ID ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetOwnShopInfo()
        {
            var query = "SELECT DISTINCT SHOP_ID AS VALUE, SHOP_NAME AS TEXT FROM SHOP ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetAllStyleNameForShopReq()
        {
            var query = "SELECT DISTINCT PRODUCT_ID AS VALUE, STYLE_NAME AS TEXT FROM VEW_STORE_HOUSE_INFO ORDER BY PRODUCT_ID ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetAllPaymentType()
        {
            var query = "SELECT  PAYMENT_TYPE_NAME AS VALUE, PAYMENT_TYPE_NAME AS TEXT FROM PAYMENT_TYPE Where ACTIVE_YN= 'Y' ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetAllMenuList()
        {
            var query = "SELECT MENU_ID AS VALUE, MENU_NAME AS TEXT FROM MENU_MAIN ORDER BY MENU_ID ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetAllUserRoleList()
        {
            var query = "SELECT DISTINCT EMPLOYEE_ROLE AS VALUE, EMPLOYEE_ROLE AS TEXT FROM EMPLOYEE_DISTRIBUTION ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetAllSubMenuList(string menuId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":MENU_MAIN_ID", OracleDbType.Varchar2, menuId, ParameterDirection.Input)
            };
            var query = "SELECT MENU_SUB_ID AS VALUE, MENU_SUB_NAME AS TEXT FROM MENU_SUB WHERE MENU_MAIN_ID = :MENU_MAIN_ID ORDER BY MENU_SUB_ID ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetAllMenuListHaveSubMenu()
        {
            var query = "SELECT MENU_ID AS VALUE, MENU_NAME AS TEXT FROM MENU_MAIN Where MENU_URL='#' ORDER BY MENU_ID ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetAllLineNo()
        {
            var query = "  SELECT DISTINCT LINE_NO AS VALUE, LINE_NO AS TEXT FROM STORE_HOUSE_PRODUCT ORDER BY LINE_NO ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }
        public async Task<IEnumerable<DropDown>> GetAllLineNo(string lineNo)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":LINE_NO", OracleDbType.Varchar2, lineNo, ParameterDirection.Input)
            };
            var query = "  SELECT DISTINCT ROPE_NO AS VALUE, ROPE_NO AS TEXT FROM STORE_HOUSE_PRODUCT " +
                        " WHERE LINE_NO = :LINE_NO ORDER BY ROPE_NO ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }

        public async Task<IEnumerable<DropDown>> GetAllMarketPlaceList()
        {
            var query = "SELECT MARKETPLACE_ID AS VALUE, MARKETPLACE_NAME AS TEXT FROM L_MARKETPLACE ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DropDown.ConvertToModel);
        }
    }
}
