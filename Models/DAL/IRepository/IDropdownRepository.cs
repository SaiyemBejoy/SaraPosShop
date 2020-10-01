using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.AllModel;

namespace DAL.IRepository
{
    public interface IDropdownRepository
    {
        Task<IEnumerable<DropDown>> GetAllShopList();
        Task<IEnumerable<DropDown>> GetAllShopListForShopRequisition();
        Task<IEnumerable<DropDown>> GetAllStoreDeliveryNumber();
        Task<IEnumerable<DropDown>> GetAllEmployeeInfo();
        Task<IEnumerable<DropDown>> GetAllSubCategoryName();
        Task<IEnumerable<DropDown>> GetAllSubCategoryName(string categoryName);
        Task<IEnumerable<DropDown>> GetAllLineNo(string lineNo);
        Task<IEnumerable<DropDown>> GetAllCategoryName();
        Task<IEnumerable<DropDown>> GetAllLineNo();
        Task<IEnumerable<DropDown>> GetAllStyleName();
        Task<IEnumerable<DropDown>> GetOwnShopInfo();
        Task<IEnumerable<DropDown>> GetAllPaymentType();
        Task<IEnumerable<DropDown>> GetAllStyleNameForShopReq();
        Task<IEnumerable<DropDown>> GetAllMenuList();
        Task<IEnumerable<DropDown>> GetAllMenuListHaveSubMenu();
        Task<IEnumerable<DropDown>> GetAllSubMenuList(string menuId);
        Task<IEnumerable<DropDown>> GetAllUserRoleList();
        Task<IEnumerable<DropDown>> GetAllMarketPlaceList();

    }
}
