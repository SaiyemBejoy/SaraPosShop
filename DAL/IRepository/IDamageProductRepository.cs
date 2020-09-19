using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;

namespace DAL.IRepository
{
    public interface IDamageProductRepository
    {
        Task<string> GetMaxDamageChallanNo();
        Task<string> SaveAllDamageMainData(DamageMainModel objDamageMainModel);
        Task<string> SaveAllDamageMainItemData(DamageMainItemModel objDamageMainItemModel);
        Task<IEnumerable<DamageMainModel>> GetAllData();
        Task<IEnumerable<DamageMainItemModel>> GetAllDamageItemByChallanNo(string challanNo);
        Task<DamageMainModel> GetAllDamageInfoForTransfer(string challanNo);
    }
}