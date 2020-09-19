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

namespace PosShop.Controllers
{
    public class ReportController : Controller
    {
        private readonly ReportDocument _objReportDocument = new ReportDocument();
        private ExportFormatType _objExportFormatType = ExportFormatType.NoFormat;
        private readonly IReportManager _manager;

        public ReportController()
        {
            _manager = new ReportManager();
        }

        public FileStreamResult ShowReport(string pReportType, string pFileDownloadName)
        {

            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Clear();
            //Response.Buffer = true;



            if (pReportType == "PDF")
            {
                _objExportFormatType = ExportFormatType.PortableDocFormat;

                Stream oStream = _objReportDocument.ExportToStream(_objExportFormatType);
                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ContentType = "application/pdf";

                pFileDownloadName += ".pdf";

                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                _objReportDocument.Close();
                _objReportDocument.Dispose();

                return File(oStream, Response.ContentType, pFileDownloadName);
            }
            else if (pReportType == "Excel")
            {
                _objExportFormatType = ExportFormatType.Excel;

                Stream oStream = _objReportDocument.ExportToStream(_objExportFormatType);
                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ContentType = "application/vnd.ms-excel";

                pFileDownloadName += ".xls";

                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                _objReportDocument.Close();
                _objReportDocument.Dispose();

                return File(oStream, Response.ContentType, pFileDownloadName);
            }
            else if (pReportType == "CSV")
            {
                _objExportFormatType = ExportFormatType.CharacterSeparatedValues;

                Stream oStream = _objReportDocument.ExportToStream(_objExportFormatType);
                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ContentType = "text/csv";

                pFileDownloadName += ".csv";

                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                _objReportDocument.Close();
                _objReportDocument.Dispose();

                return File(oStream, Response.ContentType, pFileDownloadName);
            }
            else if (pReportType == "TXT")
            {
                _objExportFormatType = ExportFormatType.RichText;

                Stream oStream = _objReportDocument.ExportToStream(_objExportFormatType);
                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ContentType = "text/plain";

                pFileDownloadName += ".txt";

                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                _objReportDocument.Close();
                _objReportDocument.Dispose();

                return File(oStream, Response.ContentType, pFileDownloadName);
            }

            return null;
        }

        //public void PrintBarcode()
        //{
        //    //var itemCode = Session["ItemCode"] as List<int>;
        //    //var itemCode = new List<int>();
        //    string strPath = Path.Combine(Server.MapPath("~/Reports/BarCodePrint.rpt"));
        //    _objReportDocument.Load(strPath);

        //    DataSet objDataSet = (_objReportDal.BarcodePrint());


        //    _objReportDocument.Load(strPath);
        //    _objReportDocument.SetDataSource(objDataSet);
        //    _objReportDocument.SetDatabaseLogon("POSWAREHOUSE", "POSWAREHOUSE");

        //    Guid guid = new Guid();

        //    ShowReport("PDF", guid.ToString());
        //}
    }
}