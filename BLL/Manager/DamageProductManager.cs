using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;

namespace BLL.Manager
{
    public class DamageProductManager : IDamageProductManager
    {
        private readonly IDamageProductRepository _repository;

        public DamageProductManager()
        {
            _repository = new DamageProductRepository();
        }

        public async Task<DamageMainModel> GetAllDamageInfoForTransfer(string challanNo)
        {
            try
            {
                var data = await _repository.GetAllDamageInfoForTransfer(challanNo);
                if (data != null)
                {
                    data.DamageMainItemList = await GetAllDamageItemByChallanNo(challanNo);
                }
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<DamageMainItemModel>> GetAllDamageItemByChallanNo(string challanNo)
        {
            try
            {
                var data = await _repository.GetAllDamageItemByChallanNo(challanNo);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> GetMaxDamageChallanNo()
        {
            try
            {
                var data = await _repository.GetMaxDamageChallanNo();
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> SaveAllDamageData(DamageMainModel objDamageMainModel)
        {
            string returnMessage = "";
            try
            {
                var data = await _repository.SaveAllDamageMainData(objDamageMainModel);
                if (data != null)
                {
                    foreach (var tableData in objDamageMainModel.DamageMainItemList)
                    {
                        tableData.DamageChallanNo = Convert.ToString(data);
                        returnMessage = await _repository.SaveAllDamageMainItemData(tableData);
                    }
                }
                return returnMessage;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }
       
        public async Task<IEnumerable<DamageMainModel>> ViewAllData()
        {
            try
            {
                var data = await _repository.GetAllData();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
