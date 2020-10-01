using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ApiModel;

namespace BLL.IManager
{
    public interface IPriceChangeAndPromotionManager
    {
        Task<string> ChangeAllPriceByBarcode(CircularPriceChangeMain objCircularPriceChangeMain);
    }
}
