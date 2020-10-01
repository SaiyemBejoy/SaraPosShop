using Models.AllModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel.Report;

namespace DAL.IRepository
{
    public interface IReportRepository
    {
        Task<DataSet> InvoicePrintAsync(string invoice);

        Task<DataSet> GetallShopReceiveItem(string storeReceiveChallanNo);

        Task<DataSet> GetallShopTransferItem(string transferChallanNo);

        Task<DataSet> GetallShopTransferItemForGvtFormate(string transferChallanNo);

        Task<DataSet> GetallDamageProductForRpt(string challanNo);

        Task<DataSet> GetallRequisitionItem(string requisitionNo);

        Task<DataSet> GetallShopRequisitionItem(string shopRequisitionNo);

        Task<DataSet> GetallShopTransitItem(string challanNo);

        Task<DataSet> GetallShopTransitReturnItem(string challanNo);

        Task<DataSet> GetallExchangeProductDetails(string newInvoiceNumber);

        Task<DataSet> SaleSummary(SaleReportModel objSaleReportModel);

        Task<DataSet> TimeSaleSummary(SaleReportModel objSaleReportModel);

        Task<DataSet> CashierSaleSummary(SaleReportModel objSaleReportModel);

        Task<DataSet> GenerateCashierSaleDetails(SaleReportModel objSaleReportModel);

        Task<DataSet> SalesManSaleSummary(SalesManReportModel objSalesManReportModel);

        Task<DataSet> GenerateSaleSummaryByCategoryAndSubCtgy(SaleReportModel objSaleReportModel);

        Task<DataSet> CurrentStockDetails(StockReportModel objStockReportModel);

        Task<DataSet> StyleWiseCurrentStockDetails(StockReportModel objStockReportModel);

        Task<DataSet> LowStockDetails(StockReportModel objStockReportModel);

        Task<DataSet> StyleWiseStockSummary(StockReportModel objStockReportModel);

        Task<DataSet> SaleDetails(SaleReportModel objSaleReportModel);

        Task<DataSet> InvoicePrint(string invoice);

        Task<string> SaleSummarySave(SaleReportModel objSaleReportModel);

        Task<DataSet> StyleWiseProductHistory(StockReportModel objStockReportMod);

        Task<DataSet> GenerateVoidInvoiceHistory(SaleReportModel objSaleReportModel);

        #region "Transit Report"
        Task<DataSet> TransitStock(TransitReportModel objTransitReportModel);
        #endregion
    }
}
