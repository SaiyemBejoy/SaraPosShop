using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Models.AllModel;
using System.Web.Mvc;

namespace BLL.IManager
{
    public interface IDropdownManager
    {
        Task<IEnumerable<SelectListItem>> GetAllShopList();
        //Task<IEnumerable<SelectListItem>> GetAllShopListForShopRequisition(string shopId);
        Task<IEnumerable<SelectListItem>> GetAllShopListForShopRequisition(string shopId);
        Task<IEnumerable<SelectListItem>> GetAllShopListForExchange(string shopId);
        Task<IEnumerable<SelectListItem>> GetAllShopListForTransfer(string shopId);
        Task<IEnumerable<SelectListItem>> GetAllStoreDeliveryNumber();
        Task<IEnumerable<SelectListItem>> GetAllEmployeeInfo();
        Task<IEnumerable<SelectListItem>> GetAllSubCategoryName();
        Task<IEnumerable<SelectListItem>> GetAllSubCategoryName(string categoryname);
        Task<IEnumerable<SelectListItem>> GetAllRopeNo(string lineNo);
        Task<IEnumerable<SelectListItem>> GetAllCategoryName();
        Task<IEnumerable<SelectListItem>> GetAllLineNo();
        Task<IEnumerable<SelectListItem>> GetAllStyleName();
        Task<IEnumerable<SelectListItem>> GetAllPaymentType();
        Task<IEnumerable<SelectListItem>> GetAllStyleNameForShopReq();
        Task<IEnumerable<SelectListItem>> GetAllMenuList();
        Task<IEnumerable<SelectListItem>> GetAllMenuListHaveSubMenu();
        Task<IEnumerable<SelectListItem>> GetAllSubMenuList(string menuId);
        Task<IEnumerable<SelectListItem>> GetAllUserRoleList();
        Task<IEnumerable<SelectListItem>> GetAllMarketPlaceList();
    }
}