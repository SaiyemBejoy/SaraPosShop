using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.IManager
{
    public interface IAuthManager
    {
        Task<AuthModel> Login(string employeeId, string employeePassword);

        Task<string> SaveEmployee(AuthModel employee);

        Task<string> RoleWiseActionPermision(string controller,string action, string userRole);

        Task<string> LoginHistory(AuthModel authModel);

        Task<string> ChangePassword(ChangePasswordModel objChangePasswordModel);

        Task<IEnumerable<AuthModel>> GetEmployeeFromWareHouse(string shopId);

       IEnumerable<DeliveredProduct> GetDeliveryProductChallanNo();

      IEnumerable<MenuMainModel> GetMainMenuList(string roleName);

      Task<IEnumerable<OtherCompanyOfferModel>> GetAllOtherCompanyOffer();

      Task<string> SaveMenuPermisionData(List<MainMenuPermisionModel> obMainMenuPermisionModels, List<SubMenuPermisionModel> obSubMenuPermisionModels);

    }
}
