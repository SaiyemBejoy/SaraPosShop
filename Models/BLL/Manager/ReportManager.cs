using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;
using Models.AllModel.Report;

namespace BLL.Manager
{
    public class ReportManager : IReportManager
    {
        private readonly IReportRepository _repository;

        public ReportManager()
        {
            _repository = new ReportRepository();
        }

        public async Task<DataSet> CashierSaleSummary(SaleReportModel objSaleReportModel)
        {
            try
            {
                var data = await _repository.CashierSaleSummary(objSaleReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public async Task<DataSet> CurrentStockDetails(StockReportModel objStockReportModel)
        {
            try
            {
                var data = await _repository.CurrentStockDetails(objStockReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public async Task<DataSet> GenerateCashierSaleDetails(SaleReportModel objSaleReportModel)
        {
            try
            {
                var data =await _repository.GenerateCashierSaleDetails(objSaleReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public async Task<DataSet> GenerateSaleSummaryByCategoryAndSubCtgy(SaleReportModel objSaleReportModel)
        {
            try
            {
               // var dataPro = _repository.ExecuteProcedureForRptCtg(objSaleReportModel);
                var data = await _repository.GenerateSaleSummaryByCategoryAndSubCtgy(objSaleReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public async Task<DataSet> GenerateVoidInvoiceHistory(SaleReportModel objSaleReportModel)
        {
            try
            {
                var data = await _repository.GenerateVoidInvoiceHistory(objSaleReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public async Task<DataSet> GetallDamageProductForRpt(string challanNo)
        {
            try
            {
                var data = await _repository.GetallDamageProductForRpt(challanNo);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DataSet> GetallExchangeProductDetails(string newInvoiceNUmber)
        {
            try
            {
                var data = await _repository.GetallExchangeProductDetails(newInvoiceNUmber);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DataSet> GetallRequisitionItem(string requisitionNo)
        {
            try
            {
                var data = await _repository.GetallRequisitionItem(requisitionNo);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DataSet> GetallShopReceiveItem(string storeReceiveChallanNo)
        {
            try
            {
                var data = await _repository.GetallShopReceiveItem(storeReceiveChallanNo);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DataSet> GetallShopRequisitionItem(string shopRequisitionNo)
        {
            try
            {
                var data = await _repository.GetallShopRequisitionItem(shopRequisitionNo);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DataSet> GetallShopTransferItem(string transferChallanNo)
        {
            try
            {
                var data = await _repository.GetallShopTransferItem(transferChallanNo);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public  async Task<DataSet> GetallShopTransferItemForGvtFormate(string transferChallanNo)
        {
            try
            {
                var data = await _repository.GetallShopTransferItemForGvtFormate(transferChallanNo);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DataSet> GetallShopTransitItem(string challanNo)
        {
            try
            {
                var data = await _repository.GetallShopTransitItem(challanNo);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DataSet> GetallShopTransitReturnItem(string challanNo)
        {
            try
            {
                var data = await _repository.GetallShopTransitReturnItem(challanNo);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DataSet> InvoicePrint(string invoice)
        {
            try
            {
                var data = await _repository.InvoicePrint(invoice);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DataSet> InvoicePrintAsync(string invoice)
        {
            try
            {
                var data = await _repository.InvoicePrintAsync(invoice);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DataSet> LowStockDetails(StockReportModel objStockReportModel)
        {
            try
            {
                var data = await _repository.LowStockDetails(objStockReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public async Task<DataSet> SaleDetails(SaleReportModel objSaleReportModel)
        {
            try
            {
                var data =await _repository.SaleDetails(objSaleReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public async Task<DataSet> SalesManSaleSummary(SalesManReportModel objSalesManReportModel)
        {
            try
            {
                var data =await _repository.SalesManSaleSummary(objSalesManReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public async Task<DataSet> SaleSummary(SaleReportModel objSaleReportModel)
        {
            try
            {
                var data = await _repository.SaleSummary(objSaleReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public async Task<string> SaleSummarySave(SaleReportModel objSaleReportModel)
        {
            try
            {
                var data = await _repository.SaleSummarySave(objSaleReportModel);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<DataSet> StyleWiseCurrentStockDetails(StockReportModel objStockReportModel)
        {
            try
            {
                var data = await _repository.StyleWiseCurrentStockDetails(objStockReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public async Task<DataSet> StyleWiseProductHistory(StockReportModel objStockReportMod)
        {
            try
            {
                var data = await _repository.StyleWiseProductHistory(objStockReportMod);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public async Task<DataSet> StyleWiseStockSummary(StockReportModel objStockReportModel)
        {
            try
            {
                var data = await _repository.StyleWiseStockSummary(objStockReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public  async Task<DataSet> TimeSaleSummary(SaleReportModel objSaleReportModel)
        {
            try
            {
                var data = await _repository.TimeSaleSummary(objSaleReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        public async Task<DataSet> TransitStock(TransitReportModel objTransitReportModel)
        {
            try
            {
                var data = await _repository.TransitStock(objTransitReportModel);
                return data;
            }
            catch (Exception s)
            {

                return null;
            }
        }

        //DataSet IReportManager.InvoicePrint(string invoice)
        //{
        //    try
        //    {
        //        var data = _repository.InvoicePrint(invoice);
        //        return data;
        //    }
        //    catch (Exception)
        //    {

        //        return null;
        //    }
        //}
    }
}
