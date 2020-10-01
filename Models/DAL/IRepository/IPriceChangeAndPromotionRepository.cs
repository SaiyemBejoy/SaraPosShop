using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ApiModel;

namespace DAL.IRepository
{
    public interface IPriceChangeAndPromotionRepository
    {
        Task<string> ChangeAllPriceByBarcode(CircularPriceChangeSub objCircularPriceChangeSub);
    }
}
