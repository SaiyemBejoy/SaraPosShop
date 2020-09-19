using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;

namespace BLL.IManager
{
    public interface IDamageProductManager
    {
        Task<string> GetMaxDamageChallanNo();
        Task<string> SaveAllDamageData(DamageMainModel objDamageMainModel);
        Task<IEnumerable<DamageMainModel>> ViewAllData();
        Task<IEnumerable<DamageMainItemModel>> GetAllDamageItemByChallanNo(string challanNo);
        Task<DamageMainModel> GetAllDamageInfoForTransfer(string challanNo);
    }
}