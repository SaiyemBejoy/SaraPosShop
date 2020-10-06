using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserPermissionRepository : ApplicationDbContext, IUserPermissionRepository
    {
        #region Menu
        public async Task<MenuMainModel> GetAMainMenu(int? menuId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":MENU_ID", OracleDbType.Varchar2, menuId, ParameterDirection.Input),
            };

            var query = "Select * from MENU_MAIN WHERE MENU_ID = :MENU_ID";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return MenuMainModel.ConvertMenuMainModel(dt.Rows[0]);
        }

        public async Task<IEnumerable<MenuMainModel>> GetMainMenuList()
        {
            var query = "Select * from MENU_MAIN ORDER BY MENU_ORDER DESC";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(MenuMainModel.ConvertMenuMainModel);
        }

        public async Task<string> GetMaxOrderNumberForMenu()
        {
            var query = "Select NVL(MAX(MENU_ORDER) + 1,0) MENU_ORDER from MENU_MAIN";
            var dt = await GetSingleStringAsync(query, null);
            return dt;
        }

        public async Task<string> SaveMenuData(MenuMainModel objMenuMainModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_MENU_ID", OracleDbType.Varchar2, objMenuMainModel.MenuMainId, ParameterDirection.Input),
                new OracleParameter(":P_MENU_NAME", OracleDbType.Varchar2, objMenuMainModel.MenuMainName, ParameterDirection.Input),
                new OracleParameter(":P_MENU_URL", OracleDbType.Varchar2, objMenuMainModel.MenuUrl, ParameterDirection.Input),
                new OracleParameter(":P_MENU_ICON", OracleDbType.Varchar2, objMenuMainModel.MenuIcon, ParameterDirection.Input),
                new OracleParameter(":P_MENU_ORDER", OracleDbType.Varchar2, objMenuMainModel.MenuOrder, ParameterDirection.Input),
                new OracleParameter(":P_UPDATE_BY", OracleDbType.Varchar2, objMenuMainModel.UpdateBy, ParameterDirection.Input),
                new OracleParameter(":P_CONTROLLER", OracleDbType.Varchar2, objMenuMainModel.Controller, ParameterDirection.Input),
                new OracleParameter(":P_ACTION", OracleDbType.Varchar2, objMenuMainModel.Action, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_MENU_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }
        #endregion

        #region SubMenu

        public async Task<IEnumerable<MenuSubModel>> GetSubMenuList()
        {
            var query = "Select * from MENU_SUB ORDER BY MENU_SUB_ID DESC";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(MenuSubModel.ConvertMenuSubModel);
        }

        public async Task<IEnumerable<MenuSubModel>> GetSubMenuListByMenu(string menuId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":MENU_ID", OracleDbType.Varchar2, menuId, ParameterDirection.Input),
            };
            var query = "Select * from MENU_SUB WHERE MENU_MAIN_ID = :MENU_ID ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return (dt.Rows).Cast<DataRow>().Select(MenuSubModel.ConvertMenuSubModel);
        }

        public async Task<string> SaveAllMenuPermisionData(MainMenuPermisionModel objMainMenuPermisionModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_MENU_ID", OracleDbType.Varchar2, objMainMenuPermisionModel.MainMenuId, ParameterDirection.Input),
                new OracleParameter(":P_USER_ROLE", OracleDbType.Varchar2, objMainMenuPermisionModel.UserRole, ParameterDirection.Input),
                new OracleParameter(":P_UPDATE_BY", OracleDbType.Varchar2, objMainMenuPermisionModel.UpdatedBy, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_MAIN_MENU_PER_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllSubMenuPermisionData(SubMenuPermisionModel objSubMenuPermisionModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SUB_MENU_ID", OracleDbType.Varchar2, objSubMenuPermisionModel.SubMenuId, ParameterDirection.Input),
                new OracleParameter(":P_MENU_ID", OracleDbType.Varchar2, objSubMenuPermisionModel.MainMenuId, ParameterDirection.Input),
                new OracleParameter(":P_USER_ROLE", OracleDbType.Varchar2, objSubMenuPermisionModel.UserRole, ParameterDirection.Input),
                new OracleParameter(":P_UPDATE_BY", OracleDbType.Varchar2, objSubMenuPermisionModel.UpdatedBy, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SUB_MENU_PER_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> DeleteAllMenuPermisionData(MainMenuPermisionModel objMainMenuPermisionModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
               
                new OracleParameter(":P_MENU_ID", OracleDbType.Varchar2, objMainMenuPermisionModel.MainMenuId, ParameterDirection.Input),
                new OracleParameter(":P_USER_ROLE", OracleDbType.Varchar2, objMainMenuPermisionModel.UserRole, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SUB_MENU_PER_DELETE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> VoidAndReturnPermisionData(UserMenuManageModel objUserMenuManageModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {

                new OracleParameter(":P_EMPLOYEE_ID", OracleDbType.Varchar2, objUserMenuManageModel.EmployeeId, ParameterDirection.Input),
                new OracleParameter(":P_VOID_PERMISION", OracleDbType.Varchar2, objUserMenuManageModel.VoidPermision, ParameterDirection.Input),
                new OracleParameter(":P_RETURN_PERMISION", OracleDbType.Varchar2, objUserMenuManageModel.ReturnPermision, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_VOID_RETURN_PERMISION");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> GetMaxOrderNumberForSubMenu(int menuId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":MENU_MAIN_ID", OracleDbType.Varchar2, menuId, ParameterDirection.Input),
            };
            var query = "SELECT " +
                      " NVL (MAX (MENU_ORDER) + 1, 0) " +
                         "FROM MENU_SUB WHERE MENU_MAIN_ID = :MENU_MAIN_ID ";
            var dt = await GetSingleStringAsync(query, param);
            return dt;
        }

        public async Task<string> SaveSubMenuData(MenuSubModel objMenuSubModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_MENU_SUB_ID", OracleDbType.Varchar2, objMenuSubModel.MenuSubId, ParameterDirection.Input),
                new OracleParameter(":P_MENU_MAIN_ID", OracleDbType.Varchar2, objMenuSubModel.MenuMainId, ParameterDirection.Input),
                new OracleParameter(":P_MENU_SUB_NAME", OracleDbType.Varchar2, objMenuSubModel.MenuSubName, ParameterDirection.Input),
                new OracleParameter(":P_MENU_SUB_URL", OracleDbType.Varchar2, objMenuSubModel.SubMenuUrl, ParameterDirection.Input),
                new OracleParameter(":P_MENU_SUB_ICON", OracleDbType.Varchar2, objMenuSubModel.SubMenuIcon, ParameterDirection.Input),
                new OracleParameter(":P_MENU_ORDER", OracleDbType.Varchar2, objMenuSubModel.SubMenuOrder, ParameterDirection.Input),
                new OracleParameter(":P_UPDATE_BY", OracleDbType.Varchar2, objMenuSubModel.UpdateBy, ParameterDirection.Input),
                new OracleParameter(":P_CONTROLLER", OracleDbType.Varchar2, objMenuSubModel.Controller, ParameterDirection.Input),
                new OracleParameter(":P_ACTION", OracleDbType.Varchar2, objMenuSubModel.Action, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_MENU_SUB_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<MenuSubModel> GetASubMenu(int subMenuId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":MENU_SUB_ID", OracleDbType.Varchar2, subMenuId, ParameterDirection.Input)
            };

            var query = "Select * from MENU_SUB WHERE MENU_SUB_ID = :MENU_SUB_ID";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return MenuSubModel.ConvertMenuSubModel(dt.Rows[0]);
        }

        #endregion

        #region OtherActionPermission

        public async Task<IEnumerable<OtherActionPermissionModel>> GetOtherPermissionList()
        {
            var query = "Select * from USER_ACTION_PERMISION ORDER BY AUTO_ID DESC";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(OtherActionPermissionModel.ConvertOtherPermissionModel);
        }

        public async Task<string> SaveOtherPermissionData(OtherActionPermissionModel objOtherPermissionModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_AUTO_ID", OracleDbType.Varchar2, objOtherPermissionModel.AutoId, ParameterDirection.Input),
                new OracleParameter(":P_ROLE_NAME", OracleDbType.Varchar2, objOtherPermissionModel.RoleName, ParameterDirection.Input),
                new OracleParameter(":P_CONTROLLER", OracleDbType.Varchar2, objOtherPermissionModel.Controller, ParameterDirection.Input),
                new OracleParameter(":P_ACTION_NAME", OracleDbType.Varchar2, objOtherPermissionModel.Action, ParameterDirection.Input),
                new OracleParameter(":P_CREATE_BY", OracleDbType.Varchar2, objOtherPermissionModel.CreateBy, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_OTR_ACTN_PRMSN_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<OtherActionPermissionModel> GetAOtherPermission(int autoId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":AUTO_ID", OracleDbType.Varchar2, autoId, ParameterDirection.Input)
            };

            var query = "Select * from USER_ACTION_PERMISION WHERE AUTO_ID = :AUTO_ID";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return OtherActionPermissionModel.ConvertOtherPermissionModel(dt.Rows[0]);
        }

        #endregion
    }
}
