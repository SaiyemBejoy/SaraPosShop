using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.Manager
{
    public class AuthManager : IAuthManager
    {
        private readonly IAuthRepository _repository;

        public AuthManager()
        {
            _repository = new AuthRepository();
        }
        public async Task<AuthModel> Login(string employeeId, string employeePassword)
        {
            try
            {
                var data = await _repository.Login(employeeId, employeePassword);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> SaveEmployee(AuthModel employee)
        {
            try
            {

                var data = await _repository.SaveEmployee(employee);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<AuthModel>> GetEmployeeFromWareHouse(string shopId)
        {
            try
            {
                var data = await _repository.GetEmployeeFromWareHouse(shopId);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<DeliveredProduct> GetDeliveryProductChallanNo()
        {
            try
            {
                var data =  _repository.GetDeliveryProductChallanNo();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> ChangePassword(ChangePasswordModel objChangePasswordModel)
        {
            try
            {
                var userCheck = await _repository.Login(objChangePasswordModel.EmployeeId, objChangePasswordModel.OldPassword);
                if (userCheck != null)
                {
                    var data = await _repository.ChangePassword(objChangePasswordModel);
                    return data;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> LoginHistory(AuthModel authModel)
        {
            try
            {
                var data = await _repository.LoginHistory(authModel);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<MenuMainModel> GetMainMenuList(string roleName)
        {
            try
            {
                var data =   _repository.GetMainMenuList(roleName);
                var menuMainModeLlList = data.OrderBy(c=>c.MenuOrder).ToList();
                foreach (var menuMainModel in menuMainModeLlList)
                {
                    menuMainModel.MenuSubModels =   _repository.GetSubMenuList(menuMainModel.MenuMainId, roleName).OrderBy(d=>d.SubMenuOrder).ToList();
                }
                return menuMainModeLlList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<OtherCompanyOfferModel>> GetAllOtherCompanyOffer()
        {
            try
            {
                
                var data = await _repository.GetAllOtherCompanyOffer();
                await GetDataFromDc();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private async Task<bool> GetDataFromDc()
        {
            try
            {
                 var result = await _repository.GetAllOtherCompanyOfferFromWarehouse();
                if (result != null)
                {
                    var deleteData = await _repository.DeleteOtherCompanyOfferFromWarehouse("");
                    foreach (var otherCompanyOfferModel in result)
                    {
                        otherCompanyOfferModel.OfferValidity = otherCompanyOfferModel.OfferValidity.Split(' ')[0];
                        var saveOtherCompanyData = await _repository.SaveOtherCompanyOfferFromWarehouse(otherCompanyOfferModel);
                    }
                    
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<string> RoleWiseActionPermision(string controller, string action, string userRole)
        {
            try
            {
                var data = await _repository.RoleWiseActionPermision(controller, action, userRole);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public  async Task<string> SaveMenuPermisionData(List<MainMenuPermisionModel> obMainMenuPermisionModels, List<SubMenuPermisionModel> obSubMenuPermisionModels)
        {
            try
            {
                string[] separator = { "Controller" };
                var menuData = "";
                var subMenuData = "";
                if (obMainMenuPermisionModels != null)
                {
                    foreach (var mainMenu in obMainMenuPermisionModels)
                    {
                        string controller= mainMenu.ControllerName.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0];
                        mainMenu.ControllerName = controller;
                        menuData = await _repository.SaveMainMenuPermision(mainMenu);
                    }
                }

                if (obSubMenuPermisionModels != null)
                {
                    foreach (var subMenu in obSubMenuPermisionModels)
                    {
                        string controller = subMenu.ControllerName.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0];
                        subMenu.ControllerName = controller;
                        subMenuData = await _repository.SaveSubMenuPermision(subMenu);
                    }
                }
               
               
                return menuData;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
