using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;
using Models.AllModel.Report;

namespace BLL.IManager
{
    public interface IReportManager
    {
        Task<DataSet> InvoicePrint(string invoice);

        Task<DataSet> InvoicePrintAsync(string invoice);

        Task<DataSet> SaleSummary(SaleReportModel objSaleReportModel);

        Task<DataSet> TimeSaleSummary(SaleReportModel objSaleReportModel);

        Task<DataSet> CashierSaleSummary(SaleReportModel objSaleReportModel);

        Task<DataSet> GenerateCashierSaleDetails(SaleReportModel objSaleReportModel);

        Task<DataSet> SalesManSaleSummary(SalesManReportModel objSalesManReportModel);

        Task<DataSet> GenerateSaleSummaryByCategoryAndSubCtgy(SaleReportModel objSaleReportModel);

        Task<DataSet> CurrentStockDetails(StockReportModel objStockReportModel);

        Task<DataSet> StyleWiseCurrentStockDetails(StockReportModel objStockReportModel);

        Task<DataSet> LowStockDetails(StockReportModel objStockReportModel);

        Task<DataSet> StyleWiseStockSummary(StockReportModel objStockReportMod);

        Task<DataSet> StyleWiseProductHistory(StockReportModel objStockReportMod);

        Task<DataSet> SaleDetails(SaleReportModel objSaleReportModel);

        Task<DataSet> GetallShopReceiveItem(string storeReceiveChallanNo);

        Task<DataSet> GetallShopTransferItem(string transferChallanNo);

        Task<DataSet> GetallShopTransferItemForGvtFormate(string transferChallanNo);

        Task<DataSet> GetallDamageProductForRpt(string challanNo);

        Task<DataSet> GetallShopTransitItem(string challanNo);

        Task<DataSet> GetallRequisitionItem(string requisitionNo);

        Task<DataSet> GetallExchangeProductDetails(string newInvoiceNUmber);

        Task<DataSet> GetallShopRequisitionItem(string shopRequisitionNo);

        Task<string> SaleSummarySave(SaleReportModel objSaleReportModel);

    }
}
