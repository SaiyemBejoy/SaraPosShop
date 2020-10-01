using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.ApiModel;

namespace BLL.Manager
{
    public class PriceChangeAndPromotionManager : IPriceChangeAndPromotionManager
    {
        private readonly IPriceChangeAndPromotionRepository _repository;

        public PriceChangeAndPromotionManager()
        {
            _repository = new PriceChangeAndPromotionRepository();
        }


        public async Task<string> ChangeAllPriceByBarcode(CircularPriceChangeMain objCircularPriceChangeMain)
        {
            string returnMessage = "";
            try
            {
                if (objCircularPriceChangeMain.CircularPriceChangeSubList != null)
                {
                    foreach (var tableData in objCircularPriceChangeMain.CircularPriceChangeSubList)
                    {
                        tableData.EffectiveDate = objCircularPriceChangeMain.EffectiveDate;
                        returnMessage = await _repository.ChangeAllPriceByBarcode(tableData);
                    }
                }
                return returnMessage;
            }
            catch (Exception e)
            {
                return "something went wrong";
            }
        }
    }
}
