using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.Manager
{
    public class ShopManager : IShopManager
    {
        
        private readonly IShopRepository _repository;

        public ShopManager()
        {
            _repository = new ShopRepository();
        }

        public async Task<ShopModel> GetShopInfo()
        {
            try
            {
                var data = await _repository.GetShopInfo();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> SaveShopInfo(ShopModel objShopModel)
        {
            try
            {             
                var data = await _repository.SaveShopInfo(objShopModel);

                objShopModel.UpdateBy = objShopModel.ShopName;
                var message = await _repository.UpdateWareHouse(objShopModel);

                return data;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }
    }
}