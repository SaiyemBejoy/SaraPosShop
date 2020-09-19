using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.IManager
{
    public interface IRequisitionManager
    {     
        Task<string> SaveAllRequisitionData(RequisitionMainModel objRequisitionMainModel);
        Task<IEnumerable<RequisitionMainModel>> GetAllWarehouseRequisitionData();
        Task<string> GetMaxRequisition();
        Task<IEnumerable<DcProductSearchModel>> GetDcProductByStyleName(string styleName);
    }
}
