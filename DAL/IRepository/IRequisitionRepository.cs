using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace DAL.IRepository
{
    public interface IRequisitionRepository
    {

        Task<IEnumerable<RequisitionMainModel>> GetAllWarehouseRequisitionData();
        Task<IEnumerable<RequisitionMainItemModel>> GetAllItemInfoDataBySaleInfoId(int requisitionId);
        Task<string> SaveAllRequisition(RequisitionMainModel objRequisitionMainModel);
        Task<string> SaveAllRequisitionItem(RequisitionMainItemModel obRequisitionMainItemModel);
        Task<string> GetMaxRequisition();
        Task<IEnumerable<DcProductSearchModel>> GetDcProductByStyleName(string styleName);
        Task<string> UpdateWarehouseRequisitionInfo(RequisitionMainModel objRequisitionMainModel);
    }
}