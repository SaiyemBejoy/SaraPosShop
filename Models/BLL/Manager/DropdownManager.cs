using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.Manager
{
    public class DropdownManager : IDropdownManager
    {
        private readonly IDropdownRepository _repository;

        public DropdownManager()
        {
            _repository = new DropdownRepository();
        }

       

        public async Task<IEnumerable<SelectListItem>> GetAllEmployeeInfo()
        {
            try
            {
                var data = await _repository.GetAllEmployeeInfo();
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllShopList()
        {
            try
            {
                var data = await _repository.GetAllShopList();

                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return new List<SelectListItem>();
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllShopListForExchange(string shopId)
        {
            try
            {
                var data = await _repository.GetAllShopList();
                //var result = data.TakeWhile(a => a.Value == shopId).ToList();
                //var result = data.Where(c => c.Value == shopId).ToList();
                if (data == null)
                {
                    data = await _repository.GetOwnShopInfo();
                }
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return new List<SelectListItem>();
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllShopListForShopRequisition(string shopId)
        {
            try
            {
                var data = await _repository.GetAllShopListForShopRequisition();
                //var result = data.SkipWhile(a=>a.Value ==shopId);
                var result = data.Where(c => c.Value != shopId).ToList();
                var list = result.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();



                return list;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllShopListForTransfer(string shopId)
        {
            try
            {
                var dropdown = new DropDown
                {
                    Value = "331",
                    Text = "Warehouse"
                };

                var data = await _repository.GetAllShopListForShopRequisition();
                var result = data.SkipWhile(a => a.Value == shopId).ToList();
                result.Add(dropdown);
                var list = result.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();



                return list;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllStoreDeliveryNumber()
        {
            try
            {
                var data = await _repository.GetAllStoreDeliveryNumber();
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllSubCategoryName()
        {
            try
            {
                var data = await _repository.GetAllSubCategoryName();
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return null;
            }
        }
        public async Task<IEnumerable<SelectListItem>> GetAllCategoryName()
        {
            try
            {
                var data = await _repository.GetAllCategoryName();
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllSubCategoryName(string categoryname)
        {
            try
            {
                var data = await _repository.GetAllSubCategoryName(categoryname);
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllStyleName()
        {
            try
            {
                var data = await _repository.GetAllStyleName();
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllStyleNameForShopReq()
        {
            try
            {
                var data = await _repository.GetAllStyleNameForShopReq();
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllPaymentType()
        {
            try
            {
                var data = await _repository.GetAllPaymentType();
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return new List<SelectListItem>();
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllMenuList()
        {
            try
            {
                var data = await _repository.GetAllMenuList();
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return new List<SelectListItem>();
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllUserRoleList()
        {
            try
            {
                var data = await _repository.GetAllUserRoleList();
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return new List<SelectListItem>();
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllSubMenuList(string menuId)
        {
            try
            {
                var data = await _repository.GetAllSubMenuList(menuId);
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return new List<SelectListItem>();
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllMenuListHaveSubMenu()
        {
            try
            {
                var data = await _repository.GetAllMenuListHaveSubMenu();
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return new List<SelectListItem>();
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllLineNo()
        {
            try
            {
                var data = await _repository.GetAllLineNo();
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return new List<SelectListItem>();
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllRopeNo(string lineNo)
        {
            try
            {
                var data = await _repository.GetAllLineNo(lineNo);
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return new List<SelectListItem>();
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetAllMarketPlaceList()
        {
            try
            {
                var data = await _repository.GetAllMarketPlaceList();
                var list = data.Select(o => new SelectListItem
                {
                    Value = o.Value,
                    Text = o.Text
                }).ToList();

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
