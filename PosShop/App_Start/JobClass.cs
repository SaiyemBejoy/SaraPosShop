using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BLL.IManager;
using BLL.Manager;
using Models.ApiModel;
using Quartz;
using DAL.DBManager;
using Models.AllModel;
using PosShop.Utility;

namespace PosShop
{
    public class JobClass : IJob
    {
        private readonly IDataExchangeManager _manager;
        private readonly IShopRequisitionManager _shopRequisitionManager;

        public JobClass()
        {
            _manager = new DataExchangeManager();
            _shopRequisitionManager = new ShopRequisitionManager();
        }
        public Task Execute(IJobExecutionContext context)
        {
            var value1 = WareHouseDeliveryProductAsync();
            var value2 = GetCustomerListAsync();
            var shopRequisitionData = GetShopRequisitionDataForNotificationAsync();
            var giftvoucher = GiftVoucherSaleDataUpdate();
            //var value3 = GetEmployeeDistributionListAsync();
            //DataBaseBackup();

            return null;
        }

        private async Task WareHouseDeliveryProductAsync()
        {
            if (!string.IsNullOrWhiteSpace(UtilityClass.ShopId))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(DatabaseConfiguration.WarehouseApi);

                    var responseTask = client.GetAsync("DeliveredProduct?shopId=" + UtilityClass.ShopId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<DeliveredProduct>>();
                        readTask.Wait();

                        IEnumerable<DeliveredProduct> product = readTask.Result;
                        foreach (var val in product)
                        {
                            await _manager.SaveData(val);
                        }

                    }
                }
            }
        }

        private async Task GetCustomerListAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(DatabaseConfiguration.WarehouseApi);
                //HTTP GET
                var responseTask = client.GetAsync("CustomerInfo");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CustomerModel>>();
                    readTask.Wait();

                    IEnumerable<CustomerModel> customer = readTask.Result;
                    foreach (var val in customer)
                    {
                        await _manager.SaveCustomerInfoData(val);
                    }

                }
            }
        }
        private async Task GetShopRequisitionDataForNotificationAsync()
        {
            var requisitiondata = await _shopRequisitionManager.GetAllFromRequisitionData(UtilityClass.ShopId);
            NotificationClass.RequisitionNotification = requisitiondata.Count();
        }
        //private void DataBaseBackup()
        //{
        //    System.Diagnostics.Process proc = new System.Diagnostics.Process();
        //    proc.StartInfo.FileName = "E:\\BACKUPSCRIPT\\db_backup.bat";
        //    proc.StartInfo.WorkingDirectory = "E:\\BACKUPSCRIPT\\";
        //    proc.Start();
        //}

        private async Task GiftVoucherSaleDataUpdate()
        {
            var giftVoucherSaleValue = await _manager.GetAllGiftVoucherSaleDataFromShop();
            if (giftVoucherSaleValue.Count() > 0)
            {
                if (!string.IsNullOrWhiteSpace(UtilityClass.ShopId))
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(DatabaseConfiguration.WarehouseApi);
                        foreach (var objGiftVoucherModel in giftVoucherSaleValue)
                        {
                            HttpResponseMessage response = await client.PostAsJsonAsync("GiftVoucherDataUpdate", objGiftVoucherModel);
                            response.EnsureSuccessStatusCode();
                            if (response.IsSuccessStatusCode)
                            {
                                var shopUpdate = await _manager.UpdateGiftVoucherSaleData(objGiftVoucherModel);
                            }
                        }
                    }
                }
            }
            
        }

    }
}