using Models.AllModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IManager
{
    public interface ITransitProductManager
    {
        Task<string> SaveTransitData(TransitProductModel objTransitProductModel);
        Task<IEnumerable<TransitProductModel>> ViewAllData();
    }
}
