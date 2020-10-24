using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.Manager
{
    public class RequisitionManager : IRequisitionManager
    {
        private readonly IRequisitionRepository _repository;

        public RequisitionManager()
        {
            _repository = new RequisitionRepository();
        }

        public async Task<IEnumerable<RequisitionMainModel>> GetAllWarehouseRequisitionData()
        {
            try
            {
                var data = await _repository.GetAllWarehouseRequisitionData();
                var requisitionMain = data.ToList();
                if (requisitionMain.Count > 0)
                {
                    foreach (var requisition in requisitionMain)
                    {
                        requisition.RequisitionMainItemList = await _repository.GetAllItemInfoDataBySaleInfoId(requisition.RequisitionId);
                    }

                }
                return requisitionMain;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<DcProductSearchModel>> GetDcProductByStyleName(string styleName)
        {
            try
            {
                var data = await _repository.GetDcProductByStyleName(styleName);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> GetMaxRequisition()
        {
            try
            {
                var data = await _repository.GetMaxRequisition();
                return data;
            }
            catch (Exception e)
            {
                return null ;
            }
        }

        public async Task<string> SaveAllRequisitionData(RequisitionMainModel objRequisitionMainModel)
        {
            string returnMessage = "";
            try
            {
                var data = await _repository.SaveAllRequisition(objRequisitionMainModel);
                if (data != null)
                {
                    foreach (var tableData in objRequisitionMainModel.RequisitionMainItemList)
                    {
                        tableData.RequisitionId = Convert.ToInt32(data);
                        returnMessage = await _repository.SaveAllRequisitionItem(tableData);
                    }
                }
                return returnMessage;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<string> UpdateWarehouseRequisitionInfo(RequisitionMainModel objRequisitionMainModel)
        {
            string returnMessage = "";
            try
            {
                var data = await _repository.UpdateWarehouseRequisitionInfo(objRequisitionMainModel);
                return data;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }
    }
}
