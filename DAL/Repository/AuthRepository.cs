using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Models.ApiModel;
using Oracle.ManagedDataAccess.Client;

namespace DAL.Repository
{
    public class AuthRepository : ApplicationDbContext, IAuthRepository
    {
        public async Task<AuthModel> Login(string employeeId, string employeePassword)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":EMPLOYEE_ID", OracleDbType.Varchar2, employeeId, ParameterDirection.Input),
                new OracleParameter(":EMPLOYEE_PASSWORD", OracleDbType.Varchar2, employeePassword, ParameterDirection.Input)
            };

            var query = "SELECT * FROM EMPLOYEE_DISTRIBUTION WHERE EMPLOYEE_ID = :EMPLOYEE_ID AND EMPLOYEE_PASSWORD = :EMPLOYEE_PASSWORD AND ACTIVE_YN = 'Y'";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return AuthModel.ConvertAuthModel(dt.Rows[0]);
        }

        public async Task<string> SaveEmployee(AuthModel employee)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
                {
                    new OracleParameter(":P_EMPLOYEE_DIS_ID", OracleDbType.Varchar2, employee.EmployeeDisId, ParameterDirection.Input),
                    new OracleParameter(":P_EMPLOYEE_ID", OracleDbType.Varchar2, employee.EmployeeId, ParameterDirection.Input),
                    new OracleParameter(":P_EMPLOYEE_NAME", OracleDbType.Varchar2, employee.EmployeeName, ParameterDirection.Input),
                    new OracleParameter(":P_DESIGNATION", OracleDbType.Varchar2, employee.Designation, ParameterDirection.Input),
                    new OracleParameter(":P_CONTACT_NO", OracleDbType.Varchar2, employee.ContactNo, ParameterDirection.Input),
                    new OracleParameter(":P_EMAIL", OracleDbType.Varchar2, employee.Email, ParameterDirection.Input),
                    new OracleParameter(":P_SHOP_NAME", OracleDbType.Varchar2, employee.ShopName, ParameterDirection.Input),
                    new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, employee.ShopId, ParameterDirection.Input),
                    new OracleParameter(":P_EMPLOYEE_ROLE", OracleDbType.Varchar2, employee.EmployeeRole, ParameterDirection.Input),
                    new OracleParameter(":P_EMPLOYEE_PASSWORD", OracleDbType.Varchar2, employee.Password, ParameterDirection.Input),
                    new OracleParameter(":P_ACTIVE_YN", OracleDbType.Varchar2, employee.ActiveYn, ParameterDirection.Input),
                    new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
                };

            query.Append("PRO_EMPLOYEE_DISTRIBUTION_SAVE");
            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<IEnumerable<AuthModel>> GetEmployeeFromWareHouse(string shopId)
        {
            IEnumerable<AuthModel> employee = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WarehouseApi);
                    //HTTP GET
                    var responseTask = client.GetAsync("EmployeeDistribution?shopId=" + shopId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<AuthModel>>();
                        readTask.Wait();
                        await Task.Run(() => employee = readTask.Result);

                        return employee;
                    }
                    return null;
                }
            }
            catch (Exception )
            {
                return null;
            }
            
        }

        public IEnumerable<DeliveredProduct> GetDeliveryProductChallanNo()
        {
            var query = " SELECT DISTINCT (STORE_DELIVERY_NUMBER)  FROM VEW_DELIVERED_PRODUCT " +
                        "WHERE STORE_DELIVERY_NUMBER NOT IN " +
                        "(SELECT STORE_RECEIVE_CHALLAN_NO FROM STORE_RECEIVE) " +
                        "ORDER BY STORE_DELIVERY_NUMBER ASC ";
            var dt = GetDataThroughDataTable(query, null);
            return (dt.Rows).Cast<DataRow>().Select(DeliveredProduct.ConvertDeliveredProduct);
        }

        public async Task<string> ChangePassword(ChangePasswordModel oChangePasswordModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WarehouseApi);

                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "ChangePassword", oChangePasswordModel);
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

        public async Task<string> LoginHistory(AuthModel authModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_EMPLOYEE_ID", OracleDbType.Varchar2, authModel.EmployeeId, ParameterDirection.Input),
                new OracleParameter(":P_EMPLOYEE_PASSWORD", OracleDbType.Varchar2, authModel.Password, ParameterDirection.Input),       
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_Login_History_SAVE");
            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public IEnumerable<MenuMainModel> GetMainMenuList(string roleName)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":ROLE_NAME", OracleDbType.Varchar2, roleName, ParameterDirection.Input)
            };
            var query = "Select * from MENU_MAIN_PERMISION WHERE LOWER (ROLE_NAME) =LOWER (:ROLE_NAME) AND ACTIVE_YN = 'Y' ";
            var dt =  GetDataThroughDataTable(query, param);
            return (dt.Rows).Cast<DataRow>().Select(MenuMainModel.ConvertMenuMainModel);
        }

        public IEnumerable<MenuSubModel> GetSubMenuList(int menuId, string roleName)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":MENU_MAIN_ID", OracleDbType.Varchar2, menuId, ParameterDirection.Input),
                new OracleParameter(":ROLE_NAME", OracleDbType.Varchar2, roleName, ParameterDirection.Input)
            };
            var query = "SELECT * FROM MENU_SUB_PERMISION WHERE MENU_MAIN_ID = :MENU_MAIN_ID AND LOWER(ROLE_NAME) = LOWER(:ROLE_NAME) AND ACTIVE_YN = 'Y' ";
            var dt =  GetDataThroughDataTable(query, param);
            return (dt.Rows).Cast<DataRow>().Select(MenuSubModel.ConvertMenuSubModel);
        }

        public async Task<IEnumerable<OtherCompanyOfferModel>> GetAllOtherCompanyOffer()
        {
            var query = "Select * from OTHER_COMPANY_OFFER ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(OtherCompanyOfferModel.ConvertOtherCompanyOfferModel);
        }

        public async Task<string> SaveOtherCompanyOfferFromWarehouse(OtherCompanyOfferModel objOtherCompanyOfferModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_COMPANY_OFFER_ID", OracleDbType.Varchar2, objOtherCompanyOfferModel.OtherCompanyOfferId, ParameterDirection.Input),
                new OracleParameter(":P_COMPANY_NAME", OracleDbType.Varchar2, objOtherCompanyOfferModel.CompanyName, ParameterDirection.Input),
                new OracleParameter(":P_OFFER", OracleDbType.Varchar2, objOtherCompanyOfferModel.Offer, ParameterDirection.Input),
                new OracleParameter(":P_OFFER_VALIDITY", OracleDbType.Varchar2, objOtherCompanyOfferModel.OfferValidity, ParameterDirection.Input),
                new OracleParameter(":P_ELIGIBLE_FOR_OFFER", OracleDbType.Varchar2, objOtherCompanyOfferModel.EligibleForOffer, ParameterDirection.Input),
                new OracleParameter(":P_CREATE_BY", OracleDbType.Varchar2, objOtherCompanyOfferModel.CreatedBy, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };

            query.Append("PRO_OTHER_COMPANY_OFFER_SAVE");
            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<IEnumerable<OtherCompanyOfferModel>> GetAllOtherCompanyOfferFromWarehouse()
        {
            IEnumerable<OtherCompanyOfferModel> model = new List<OtherCompanyOfferModel>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(DatabaseConfiguration.WarehouseApi);
                    var responseTask = await client.GetAsync("OtherCompanyOffer");
                    var result = responseTask;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = await result.Content.ReadAsAsync<IList<OtherCompanyOfferModel>>();
                        model = readTask;
                    }
                }
            }
            catch (Exception e)
            {
               return null;
            }
            return model;
        }

        public async Task<string> DeleteOtherCompanyOfferFromWarehouse(string id)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_COMPANY_OFFER_ID", OracleDbType.Varchar2, id, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_OTHER_COMPANY_OFFER_DELETE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> RoleWiseActionPermision(string controller, string action, string userRole)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_CONTROLLER", OracleDbType.Varchar2, controller, ParameterDirection.Input),
                new OracleParameter(":P_ACTION", OracleDbType.Varchar2, action, ParameterDirection.Input),
                new OracleParameter(":P_USER_ROLE", OracleDbType.Varchar2, userRole, ParameterDirection.Input),   
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_ROLEWISE_PERMISION_CHECK");
            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public  async Task<string> SaveMainMenuPermision(MainMenuPermisionModel obMainMenuPermisionModels)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_MAIN_MENU_ID", OracleDbType.Varchar2, obMainMenuPermisionModels.MainMenuId, ParameterDirection.Input),
                new OracleParameter(":P_USER_ROLE", OracleDbType.Varchar2, obMainMenuPermisionModels.UserRole, ParameterDirection.Input),
                new OracleParameter(":P_CONTROLLER_NAME", OracleDbType.Varchar2, obMainMenuPermisionModels.ControllerName, ParameterDirection.Input),
                new OracleParameter(":P_ACTION_NAME", OracleDbType.Varchar2, obMainMenuPermisionModels.ActionName, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };

            query.Append("PRO_MAIN_MENU_PER_SAVE");
            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveSubMenuPermision(SubMenuPermisionModel objSubMenuPermisionModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SUB_MENU_ID", OracleDbType.Varchar2, objSubMenuPermisionModel.SubMenuId, ParameterDirection.Input),
                new OracleParameter(":P_MAIN_MENU_ID", OracleDbType.Varchar2, objSubMenuPermisionModel.MainMenuId, ParameterDirection.Input),
                new OracleParameter(":P_USER_ROLE", OracleDbType.Varchar2, objSubMenuPermisionModel.UserRole, ParameterDirection.Input),
                new OracleParameter(":P_CONTROLLER_NAME", OracleDbType.Varchar2, objSubMenuPermisionModel.ControllerName, ParameterDirection.Input),
                new OracleParameter(":P_ACTION_NAME", OracleDbType.Varchar2, objSubMenuPermisionModel.ActionName, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };

            query.Append("PRO_SUB_MENU_PER_SAVE");
            return await ExecuteNonQueryAsync(query.ToString(), param);
        }
    }
}
