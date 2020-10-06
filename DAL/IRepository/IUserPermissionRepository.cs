using Models.AllModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepository
{
    public interface IUserPermissionRepository
    {
        #region Menu
        Task<IEnumerable<MenuMainModel>> GetMainMenuList();
        Task<MenuMainModel> GetAMainMenu(int? menuId);
        Task<string> GetMaxOrderNumberForMenu();
        Task<string> SaveMenuData(MenuMainModel objMenuMainModel);
        #endregion

        #region SubMenu
        Task<IEnumerable<MenuSubModel>> GetSubMenuList();
        Task<IEnumerable<MenuSubModel>> GetSubMenuListByMenu(string menuId);
        Task<string> SaveAllMenuPermisionData(MainMenuPermisionModel objMainMenuPermisionModel);
        Task<string> SaveAllSubMenuPermisionData(SubMenuPermisionModel objSubMenuPermisionModel);
        Task<string> DeleteAllMenuPermisionData(MainMenuPermisionModel objMainMenuPermisionModel);
        Task<string> VoidAndReturnPermisionData(UserMenuManageModel objUserMenuManageModel);

        Task<string> GetMaxOrderNumberForSubMenu(int menuId);
        Task<MenuSubModel> GetASubMenu(int subMenuId);
        Task<string> SaveSubMenuData(MenuSubModel objMenuSubModel);
        #endregion

        #region OtherActionPermission
        Task<IEnumerable<OtherActionPermissionModel>> GetOtherPermissionList();
        Task<OtherActionPermissionModel> GetAOtherPermission(int autoId);
        Task<string> SaveOtherPermissionData(OtherActionPermissionModel objOtherPermissionModel);
        #endregion
    }
}
