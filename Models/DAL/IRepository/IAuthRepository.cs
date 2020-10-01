using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace DAL.IRepository
{
    public interface IAuthRepository
    {
        Task<AuthModel> Login(string employeeId, string employeePassword);

        Task<string> SaveEmployee(AuthModel employee);

        Task<string> SaveMainMenuPermision(MainMenuPermisionModel obMainMenuPermisionModels);

        Task<string> SaveSubMenuPermision(SubMenuPermisionModel objSubMenuPermisionModel);

        Task<string> LoginHistory(AuthModel authModel);

        Task<string> RoleWiseActionPermision(string controller, string action, string userRole);

        Task<string> ChangePassword(ChangePasswordModel oChangePasswordModel);

        Task<IEnumerable<AuthModel>> GetEmployeeFromWareHouse(string shopId);

        IEnumerable<DeliveredProduct> GetDeliveryProductChallanNo();

        IEnumerable<MenuMainModel> GetMainMenuList(string roleName);

        IEnumerable<MenuSubModel> GetSubMenuList(int menuId,string roleName);

        Task<IEnumerable<OtherCompanyOfferModel>> GetAllOtherCompanyOffer();

        Task<IEnumerable<OtherCompanyOfferModel>> GetAllOtherCompanyOfferFromWarehouse();

        Task<string> SaveOtherCompanyOfferFromWarehouse(OtherCompanyOfferModel objOtherCompanyOfferModel);
        Task<string> DeleteOtherCompanyOfferFromWarehouse(string id);
    }
}