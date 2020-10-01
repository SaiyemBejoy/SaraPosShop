using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.IManager;
using BLL.Manager;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Models.AllModel;
using PosShop.Utility;
using System.Drawing.Printing;
using System.Management;

namespace PosShop.Controllers
{
    public class PointOfSaleController : Controller
    {
        private readonly IDropdownManager _dropdownManager;
        private readonly IPointOfSaleManager _manager;
        private readonly IReportManager _reportManager;

        public PointOfSaleController()
        {
            _manager = new PointOfSaleManager();
            _dropdownManager = new DropdownManager();
            _reportManager = new ReportManager();
        }

        #region "Common"
        private string _employeeId;
        private string _employeeName;
        private string _role;

        public void LoadSession()
        {
            var auth = (AuthModel)Session["authentication"];

            if (auth != null)
            {
                _employeeId = auth.EmployeeId;
                _employeeName = auth.EmployeeName;
                _role = auth.EmployeeRole;
            }
            else
            {
                string url = Url.Action("Index", "Auth");
                if (url != null) Response.Redirect(url);
            }
        }

        #endregion
        [UserRoleFilter]
        public async Task<ActionResult> PointOfSale()
        {
            LoadSession();
            SaleInfoModel obj = new SaleInfoModel();
            var data = await _dropdownManager.GetAllEmployeeInfo();
            var marketPlaceData = await _dropdownManager.GetAllMarketPlaceList();
            var shopId = UtilityClass.ShopId;
            //var shopName = await _dropdownManager.GetAllShopListForExchange(shopId);
            ViewBag.employeelist = data;
            ViewBag.marketPlaceList = marketPlaceData;
            //ViewData["shopList"] = shopName;
            var maxInvoiceNumber = await _manager.GetMaxInvoiceNumber(UtilityClass.ShopId);
            ViewBag.maxInvoice = maxInvoiceNumber;
            return View();
        }

        public async Task<ActionResult> GetAllInfoByBarcode(string barcode)
        {
            var item = await _manager.GetAllInfoByBarcode(barcode);
            if (item == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllShopForEx()
        {
            var shopName = await _dropdownManager.GetAllShopListForExchange(UtilityClass.ShopId);
            if (shopName == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            return Json(shopName, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetAllPaymentType()
        {
            var paymentType = await _dropdownManager.GetAllPaymentType();   
            return Json(paymentType, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllInfoByCustomerCode(string customerCode)
        {
            var data = await _manager.GetAllInfoByCustomerCode(customerCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetAllInfoByCustomerPhone(string customerPhone)
        {
            var data = await _manager.GetAllInfoByCustomerPhone(customerPhone);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllInfoByGiftVoucherCode(string giftVoucherCode)
        {
            var data = await _manager.GetAllInfoByGiftVoucherCode(giftVoucherCode);
            if (data == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> GetAllItemInfoForDataTable(DataTableAjaxPostModel model)
        {
            model = await _manager.GetAllItemInfoForDataTable(model);
            List<ItemInfoModel> data = (List<ItemInfoModel>)model.ListOfData;

            return Json(new { model.draw, model.recordsTotal, model.recordsFiltered, data }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetSearchHintsList(string model)
       {
            var list = await _manager.GetProductGridsForHints(model);
            var asList = list.ToList();
            List<string> ListHint = new List<string>();
            foreach (var V in asList)
            {
                ListHint.Add(V.SearchValue);
            }
            return Json(ListHint, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> SaveData(CustomerSaleModel objCustomerSaleModel, SaleInfoModel objSaleInfoModel, string counterName)
        {
            LoadSession();

            if (string.IsNullOrWhiteSpace(_employeeId))
            {
                var returnDataOut = new
                {
                    isRedirect = false,
                };

                return Json(returnDataOut, JsonRequestBehavior.AllowGet);
            }

            var data = "";
            objSaleInfoModel.CreatedBy = _employeeId;
            objSaleInfoModel.ShopId = UtilityClass.ShopId;
            objSaleInfoModel.WareHouseId = UtilityClass.WareHouseId;

            data = await _manager.SaveData(objCustomerSaleModel, objSaleInfoModel);
            if (data == null)
            {
                var returnNullData = new
                {
                    m = "",
                    isRedirect = false,
                    redirectUrl = Url.Action("PointOfSale")
                };
                return Json(returnNullData, JsonRequestBehavior.AllowGet);
            }
            if (objSaleInfoModel.HoldInvoiceYN != "Y" && data != "something went wrong")
            {
                string invoiceNumber = data;
                await Task.Run(() => GenerateInvoiceAsync(invoiceNumber, counterName));
            }
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("PointOfSale")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllHoldInvoice()
        {
            var data = await _manager.GetAllHoldInvoice();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> DeleteAllHoldInvoice(string invoiceNumber)
        {
            var data = await _manager.DeleteAllHoldInvoice(invoiceNumber);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("PointOfSale")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllDataByInvoiceNumber(string invoiceNumber)
        {
            var data = await _manager.GetAllDataByInvoiceNumber(invoiceNumber);
            var customerData = await _manager.GetAllSaleCustomerByInvoiceNumber(data.CustomerId);
            
            var returnData = new
            {
                saleData = data,
                customerData = customerData,
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllDataByInvoiceNumberForExchange(string invoiceNumber, int shopId)
        {
            CustomerSaleModel customerData = null;
            int ownShopId = Convert.ToInt32(UtilityClass.ShopId);
            bool shopToShopExc = false;
            var saleInfoData = await _manager.GetAllDataByInvoiceNumberForExchange(invoiceNumber, shopId, ownShopId);
            if (saleInfoData != null)
            {
                if (Convert.ToInt32(UtilityClass.ShopId) != shopId)
                    shopToShopExc = true;

                if (saleInfoData.CustomerId > 0)
                {
                    customerData = new CustomerSaleModel();
                    customerData.CustomerId = saleInfoData.CustomerId;
                    customerData.CustomerName = saleInfoData.CustomerName;
                    customerData.ContactNo = saleInfoData.CustomerContactNO;
                }
                else
                {
                    int customerId = Convert.ToInt32(saleInfoData.CustomerId);
                    customerData = await _manager.GetAllSaleCustomerByInvoiceNumber(customerId);
                }
            }

            var result = new { data = saleInfoData, data2 = customerData, shopToShopExc };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllDataByInvoiceNumberForVoid(string invoiceNumber)
        {
            var saleInfoData = await _manager.GetAllDataByInvoiceNumberForVoid(invoiceNumber);
            if (saleInfoData == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(saleInfoData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> VoidInvoice(string invoiceNum)
        {
            string shopId = UtilityClass.ShopId;
            var voidInvoice = await _manager.VoidInvoice(invoiceNum, shopId);
            if (voidInvoice == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            return Json(voidInvoice, JsonRequestBehavior.AllowGet);
        }


        public async void GenerateInvoiceAsync(string invoice, string counterName)
        {
            var invoiceData = await _reportManager.InvoicePrintAsync(invoice);
            var strPath = Path.Combine(Server.MapPath("~/Reports/Invoice.rpt"));
            DataSet objDataSet = invoiceData;
            DataTable dataTable = objDataSet.Tables[0];
            try
            {
                using (ReportDocument report = new ReportDocument())
                {
                    report.Load(strPath);
                    report.Database.Tables["VEW_RPT_INVOICE_PRINT"].SetDataSource((DataTable)dataTable);
                    string printerName = null;

                    var query = new ObjectQuery("SELECT * FROM Win32_Printer");
                    var searcher = new ManagementObjectSearcher(query);

                    foreach (var o in searcher.Get())
                    {
                        var mo = (ManagementObject)o;
                        string fPrinterName = mo["Name"] as string;

                        if (fPrinterName != null && fPrinterName.ToLower().Contains(counterName))
                        {
                            printerName = fPrinterName;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(printerName))
                    {
                        report.PrintOptions.PrinterName = printerName;
                    }
                    else
                    {
                        PrinterSettings settings = new PrinterSettings();
                        report.PrintOptions.PrinterName = settings.PrinterName;
                    }

                    report.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch
            {
                // ignored
            }
        }

        public async void GenerateInvoice(string invoice, string counterName)
        {
            var invoiceData = await _reportManager.InvoicePrint(invoice);
            var strPath = Path.Combine(Server.MapPath("~/Reports/Invoice.rpt"));
            DataSet objDataSet = invoiceData;
            DataTable dataTable = objDataSet.Tables[0];

            try
            {
                using (ReportDocument report = new ReportDocument())
                {
                    report.Load(strPath);
                    report.Database.Tables["VEW_RPT_INVOICE_PRINT"].SetDataSource((DataTable)dataTable);
                    string printerName = null;

                    var query = new ObjectQuery("SELECT * FROM Win32_Printer");
                    var searcher = new ManagementObjectSearcher(query);

                    foreach (var o in searcher.Get())
                    {
                        var mo = (ManagementObject)o;
                        string fPrinterName = mo["Name"] as string;

                        if (fPrinterName != null && fPrinterName.ToLower().Contains(counterName))
                        {
                            printerName = fPrinterName;
                            break;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(printerName))
                    {
                        report.PrintOptions.PrinterName = printerName;
                    }
                    else
                    {
                        PrinterSettings settings = new PrinterSettings();
                        report.PrintOptions.PrinterName = settings.PrinterName;
                    }


                    report.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch
            {
                //
            }

        }

        public async Task<JsonResult> RePrintInvoice(string invoice, string counterName)
        {
            //GenerateInvoice(invoice, counterName);
            await Task.Run(() => GenerateInvoice(invoice, counterName));
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> ReturnInvoice(string invoiceNumber)
        {
            var data = await _manager.GetAllDataByInvoiceNumberForReturn(invoiceNumber);
            if (data ==null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveReturnData(ReturnSaleInfoModel objReturnSaleInfoModel)
        {
            LoadSession();
            var data = "";
            data = await _manager.SaveReturnData(objReturnSaleInfoModel);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("PointOfSale")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllShopList()
        {
            var shopId = UtilityClass.ShopId;
            var shopName = await _dropdownManager.GetAllShopListForExchange(shopId);
            if (shopName == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            return Json(shopName, JsonRequestBehavior.AllowGet);
        }


    }
}