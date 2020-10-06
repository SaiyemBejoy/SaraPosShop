using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Manager
{
    public class UserPermissionManager : IUserPermissionManager
    {
        #region Menu
        private readonly IUserPermissionRepository _repository;

        public UserPermissionManager()
        {
            _repository = new UserPermissionRepository();
        }

        public async Task<MenuMainModel> GetAMainMenu(int? menuId)
        {
            try
            {
                var data = await _repository.GetAMainMenu(menuId);
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<MenuMainModel>> GetMainMenuList()
        {
            try
            {
                var data = await _repository.GetMainMenuList();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> GetMaxOrderNumberForMenu()
        {
            try
            {
                var data = await _repository.GetMaxOrderNumberForMenu();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> SaveMenuData(MenuMainModel objMenuMainModel)
        {
            string returnMessage = "";
            try
            {
                returnMessage = await _repository.SaveMenuData(objMenuMainModel);
                return returnMessage;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }
        #endregion


        #region "Permission and SubMenu"

        public async Task<IEnumerable<MenuSubModel>> GetSubMenuList()
        {
            try
            {
                var data = await _repository.GetSubMenuList();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<MenuSubModel>> GetSubMenuListByMenu(string menuId)
        {
            try
            {
                var data = await _repository.GetSubMenuListByMenu(menuId);
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> SaveAllMenuPermisionData(MainMenuPermisionModel objMainMenuPermisionModel)
        {
            string returnMessage = "";
            try
            {
                returnMessage = await _repository.SaveAllMenuPermisionData(objMainMenuPermisionModel);
                if (objMainMenuPermisionModel.SubMenuPermisionList != null && returnMessage != null)
                {
                    foreach (var subMenuPermisionModel in objMainMenuPermisionModel.SubMenuPermisionList)
                    {
                        subMenuPermisionModel.UpdatedBy = objMainMenuPermisionModel.UpdatedBy;
                        subMenuPermisionModel.MainMenuId = objMainMenuPermisionModel.MainMenuId;
                        subMenuPermisionModel.UserRole = objMainMenuPermisionModel.UserRole;
                        var data = _repository.SaveAllSubMenuPermisionData(subMenuPermisionModel);
                    }
                  
                }
                return returnMessage;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<string> DeleteAllMenuPermisionData(MainMenuPermisionModel objMainMenuPermisionModel)
        {
            string returnMessage = "";
            try
            {
                returnMessage = await _repository.DeleteAllMenuPermisionData(objMainMenuPermisionModel);
                return returnMessage;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> VoidAndReturnPermisionData(UserMenuManageModel objUserMenuManageModel)
        {
            try
            {
                var data = await _repository.VoidAndReturnPermisionData(objUserMenuManageModel);
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> GetMaxOrderNumberForSubMenu(int menuId)
        {
            try
            {
                var data = await _repository.GetMaxOrderNumberForSubMenu(menuId);
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> SaveSubMenuData(MenuSubModel objMenuSubModel)
        {
            string returnMessage = "";
            try
            {
                returnMessage = await _repository.SaveSubMenuData(objMenuSubModel);
                return returnMessage;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<MenuSubModel> GetASubMenu(int subMenuId)
        {
            try
            {
                var data = await _repository.GetASubMenu(subMenuId);
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        #endregion

        #region OtherActionPermission

        public async Task<IEnumerable<OtherActionPermissionModel>> GetOtherPermissionList()
        {
            try
            {
                var data = await _repository.GetOtherPermissionList();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> SaveOtherPermissionData(OtherActionPermissionModel objOtherPermissionModel)
        {
            string returnMessage = "";
            try
            {
                returnMessage = await _repository.SaveOtherPermissionData(objOtherPermissionModel);
                return returnMessage;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<OtherActionPermissionModel> GetAOtherPermission(int autoId)
        {
            try
            {
                var data = await _repository.GetAOtherPermission(autoId);
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion
    }
}
