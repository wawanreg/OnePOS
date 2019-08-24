using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using OnePOS.FunctionController.Json.Converter;
using OnePOS.Helpers;
using OnePOS.Models;
using OnePOS.Models.Dashboard.Brand;
using OnePOS.Models.Dashboard.Items;
using OnePOS.Models.Dashboard.ShoppingBasket;
using OnePOS.Models.Dashboard.Storage;
using OnePOS.Models.Dashboard.Vendors;
using OnePOS.Models.History;
using OnePOS.Models.Invoice;


namespace OnePOS.FunctionController
{
    public class ApiCollectionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("ApiCollection/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/GetVendorList")]
        public JsonResult VendorListJson(int take, int page)
        {
            List<ListVendorViewModels> mVendor = db.Vendor.Where(x => !x.Deleted).OrderBy(x => x.VendorId).Select(x => new ListVendorViewModels
            {
                VendorId = x.VendorId,
                VendorAddress = x.VendorAddress,
                VendorEmail = x.VendorEmail,
                VendorPhone = x.VendorPhone,
                VendorOwner = x.VendorOwner,
                VendorName = x.VendorName
            }).Skip(take * (page - 1)).Take(take).ToList();


            return Json(new { @datajson = JsonConvert.SerializeObject(mVendor), itemsPerPage = db.Vendor.Count(x => !x.Deleted) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/GetItemList")]
        public JsonResult ItemListJson(int take, int page)
        {

            List<ListItemViewModels> mItem = db.Item.Where(x => !x.Deleted).OrderBy(x => x.ItemId).Select(x => new ListItemViewModels
            {
                ItemName = x.ItemName, 
                ItemBuyPrice = x.BuyPrice,
                ItemSalePrice = x.SalePrice,
                ItemVendorName = x.Vendor.VendorName,
                ItemQuantitiy = x.Stock,
                ItemLocation = x.Storage.StorageName,
                ItemId = x.ItemId
            }).Skip(take * (page - 1)).Take(take).ToList();

            return Json(new { @datajson = JsonConvert.SerializeObject(mItem), itemsPerPage = db.Item.Count(x => !x.Deleted) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/GetBrandList")]
        public JsonResult BrandListJson(int take, int page)
        {
            List<ListBrandViewModels> mBrand = db.Brand.Where(x => !x.Deleted).OrderBy(x => x.BrandId).Select(x => new ListBrandViewModels
            {
                BrandName = x.BrandName,
                BrandDescription = x.BrandDescription,
                BrandId = x.BrandId
            }).Skip(take * (page - 1)).Take(take).ToList();

            return Json(new { @datajson = JsonConvert.SerializeObject(mBrand), itemsPerPage = db.Brand.Count(x => !x.Deleted) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/GetStorageList")]
        public JsonResult StorageListJson(int take, int page)
        {
            List<ListStorageViewModels> mStorage = db.Storage.Where(x => !x.Deleted).OrderBy(x => x.StorageId).Select(x => new ListStorageViewModels
            {
                StorageName = x.StorageName,
                StorageDescription = x.StorageDescription,
                StorageId = x.StorageId
            }).Skip(take * (page - 1)).Take(take).ToList();

            return Json(new { @datajson = JsonConvert.SerializeObject(mStorage), itemsPerPage = db.Storage.Count(x => !x.Deleted) }, JsonRequestBehavior.AllowGet);
        }
        //shopping basket
        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/GetItemCollection")]
        public JsonResult ItemCollectionJson()
        {
            List<ShoppingBasketCollection> mItemCollections = db.Item.Where(x => !x.Deleted && x.Stock !=0).OrderBy(x => x.ItemId).Select(x => new ShoppingBasketCollection
            {
                ItemName = x.ItemName,
                ItemPrice = x.SalePrice,
                TotalStock = x.Stock,
                ItemId = x.ItemId
            }).ToList();

            return Json(new { @datajson = JsonConvert.SerializeObject(mItemCollections), itemsPerPage = 0 }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/GetInvoiceList")]
        public JsonResult InvoiceListJson(int take, int page)
        {
            List<ListInvoiceModel> mInvoice = db.BillingHeader.Where(x => !x.Deleted).OrderBy(x => x.NoBillingHeader).Select(x => new ListInvoiceModel
            {
                InvoiceDate = x.InvoiceDate,
                BillingHeaderId = x.NoBillingHeader,
                BillingStatus = x.BillingStatus.BillingName,
                NoInvoice = x.NoInvoice,
                TotalPayment = x.TotalPayment
            }).Skip(take * (page - 1)).Take(take).ToList();

            return Json(new { @datajson = JsonConvert.SerializeObject(mInvoice), itemsPerPage = db.BillingHeader.Count(x => !x.Deleted) }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/HistoryTransaction")]
        public JsonResult HistoryTransaction()
        {
            var currentDate = DateTime.UtcNow;
            var crnYear = currentDate.Year;
            var crnMonth = currentDate.Month;

            var lastDateOfWeek = currentDate.AddDays(-6);
            

            List<WeeklyHistory> listWeekly = new List<WeeklyHistory>();

            for (var i = lastDateOfWeek.Day; i < currentDate.Day; i++)
            {
                var totalPayment = 0;
                var listData = db.BillingHeader.Where(x => !x.Deleted && x.CreatedDate.Year == crnYear
                                                               && x.CreatedDate.Month == crnMonth &&
                                                               x.CreatedDate.Day == lastDateOfWeek.Day).ToList();
                if (listData.Count > 0)
                {
                    totalPayment = (int) listData.Sum(x => x.TotalPayment);
                }

                listWeekly.Add(new WeeklyHistory
                {
                    Date = new DateTime(crnYear,crnMonth,i),
                    TotalPayment = totalPayment
                });
                lastDateOfWeek = lastDateOfWeek.AddDays(1);
            }

            //var history = db.BillingHeader.Where(x => !x.Deleted &&
            //    x.CreatedDate >= lastDateOfWeek
            //    && x.CreatedDate < addDay).OrderBy(x => x.NoBillingHeader).Select(x => new ListTransactionHistoryModel
            //{
            //    InvoiceDate = x.InvoiceDate,
            //    BillingHeaderId = x.NoBillingHeader,
            //    BillingStatus = x.BillingStatus.BillingName,
            //    NoInvoice = x.NoInvoice,
            //    TotalPayment = x.TotalPayment
            //}).ToList().GroupBy(x => x.DayOfWeek).Select(x => new
            //{
            //    DayOfWeek = x.Key,
            //    TotalPayment = x.Sum(t => t.TotalPayment),
            //    InvoiceDate = x.First().InvoiceDate,
            //    Day = x.First().InvoiceDate.Day,
            //    Month = x.First().InvoiceDate.Month,
            //    Year = x.First().InvoiceDate.Year
            //});

            
            //foreach (var weekly in listWeekly)
            //{
            //    WeeklyHistory weekly1 = weekly;
            //    var day = weekly1.Date.Day;
            //    var month = weekly1.Date.Month;
            //    var year = weekly1.Date.Year;

            //    foreach (var mHistory in history.Where(mHistory => day == mHistory.Day && month == mHistory.Month))
            //    {
            //        weekly1.TotalPayment += mHistory.TotalPayment;
            //    }
            //}

            return Json(new { @datajson = JsonConvert.SerializeObject(listWeekly), itemsPerPage = 0 }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/MonthHistoryTransaction")]
        public JsonResult MonthHistoryTransaction()
        {
            var currentDate = DateTime.UtcNow;
            var crnYear = currentDate.Year;

            List<MonthlyHistory> listMonthly = new List<MonthlyHistory>();

            for (var i = 1; i <+12; i++)
            {
                var ctrMonth = i;
                var totalPayment = 0;
                var listData =
                    db.BillingHeader.Where(
                        x => !x.Deleted && x.CreatedDate.Year == crnYear && x.CreatedDate.Month == ctrMonth).ToList();

                if (listData.Count > 0)
                {
                    totalPayment = (int)listData.Sum(x => x.TotalPayment);
                }
                listMonthly.Add(new MonthlyHistory
                {
                    Date = new DateTime(crnYear, ctrMonth, 1),
                    TotalPayment = totalPayment
                });
            }

            //var history = db.BillingHeader.Where(x => !x.Deleted &&
            //    x.CreatedDate.Year == crnYear).OrderBy(x => x.NoBillingHeader).Select(x => new ListTransactionHistoryModel
            //    {
            //        InvoiceDate = x.InvoiceDate,
            //        BillingHeaderId = x.NoBillingHeader,
            //        BillingStatus = x.BillingStatus.BillingName,
            //        NoInvoice = x.NoInvoice,
            //        TotalPayment = x.TotalPayment
            //    }).ToList().GroupBy(x => x.InvoiceDate.Month).Select(x => new
            //    {
            //        Key = x.Key,
            //        TotalPayment = x.Sum(t => t.TotalPayment),
            //        InvoiceDate = x.First().InvoiceDate,
            //        Day = x.First().InvoiceDate.Day,
            //        Month = x.First().InvoiceDate.Month,
            //        Year = x.First().InvoiceDate.Year
            //    });


            //foreach (var monthly in listMonthly)
            //{
            //    MonthlyHistory monthly1 = monthly;
                
            //    var month = monthly1.Date.Month;
            //    var year = monthly1.Date.Year;

            //    foreach (var mHistory in history.Where(mHistory => month == mHistory.Month && year == mHistory.Year))
            //    {
            //        monthly1.TotalPayment += mHistory.TotalPayment;
            //    }
            //}

            return Json(new { @datajson = JsonConvert.SerializeObject(listMonthly), itemsPerPage = 0 }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/ItemHistoryTransaction")]
        public JsonResult ItemHistoryTransaction()
        {
            var currentDate = DateTime.UtcNow;
            var crnYear = currentDate.Year;

            

            var history = db.BillingDetail.Where(x => !x.Deleted &&
                x.CreatedDate.Year == crnYear).OrderBy(x => x.NoBillingDetail).Select(x => new ListTransactionDetailHistoryModel()
                {
                    ItemId = x.Item.ItemId,
                    ItemName = x.Item.ItemName,
                    CreateDate = x.CreatedDate,
                    NoBillingDetail = x.NoBillingDetail,
                    TotalItem = x.Quantity
                }).ToList().OrderBy(x=> x.ItemId).GroupBy(x => new {x.ItemName, x.CreateDate.Month}).Select(x => new
            {
                
                ItemId = x.First().ItemId,
                ItemName = x.First().ItemName,
                TotalItem = x.Sum(t => t.TotalItem),
                InvoiceDate = x.First().CreateDate,
                Day = x.First().CreateDate.Day,
                Month = x.First().CreateDate.Month,
                Year = x.First().CreateDate.Year
            });

            return Json(new { @datajson = JsonConvert.SerializeObject(history), itemsPerPage = 0 }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/TotalAssets")]
        public JsonResult TotalAssets()
        {

            //var currentDate = DateTime.UtcNow;
            var totalBuyPrice = db.Item.Where(x => !x.Deleted).Sum(x => x.BuyPrice * x.Stock);
            var totalSalePrice = db.Item.Where(x => !x.Deleted).Sum(x => x.SalePrice * x.Stock);

            var totalAssets = new List<TotalAssets>();
            totalAssets.Add(new TotalAssets
            {
                TotalAsset = totalBuyPrice
            });
            totalAssets.Add(new TotalAssets
            {
                TotalAsset = totalSalePrice
            });

            return Json(new { @datajson = JsonConvert.SerializeObject(totalAssets), itemsPerPage = 0 }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}