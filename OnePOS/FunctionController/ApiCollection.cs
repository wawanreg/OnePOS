using System.Collections.Generic;
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
            List<ListVendorViewModels> mVendor = db.Vendor.OrderBy(x=> x.VendorId).Select(x=> new ListVendorViewModels
            {
                VendorId = x.VendorId,
                VendorAddress = x.VendorAddress,
                VendorEmail = x.VendorEmail,
                VendorPhone = x.VendorPhone,
                VendorOwner = x.VendorOwner,
                VendorName = x.VendorName
            }).Skip(take * (page - 1)).Take(take).ToList();


            return Json(new { @datajson = JsonConvert.SerializeObject(mVendor), itemsPerPage = db.Vendor.Count() }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/GetItemList")]
        public JsonResult ItemListJson(int take, int page)
        {
            
            List<ListItemViewModels> mItem = db.Item.OrderBy(x => x.ItemId).Select(x=> new ListItemViewModels
            {
                ItemName = x.ItemName, 
                ItemBuyPrice = x.BuyPrice,
                ItemSalePrice = x.SalePrice,
                ItemVendorName = x.Vendor.VendorName,
                ItemQuantitiy = x.Stock,
                ItemLocation = x.Storage.StorageName,
                ItemId = x.ItemId
            }).Skip(take * (page - 1)).Take(take).ToList();

            return Json(new { @datajson = JsonConvert.SerializeObject(mItem), itemsPerPage = db.Item.Count() }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/GetBrandList")]
        public JsonResult BrandListJson(int take, int page)
        {
            List<ListBrandViewModels> mBrand = db.Brand.OrderBy(x => x.BrandId).Select(x => new ListBrandViewModels
            {
                BrandName = x.BrandName,
                BrandDescription = x.BrandDescription,
                BrandId = x.BrandId
            }).Skip(take * (page - 1)).Take(take).ToList();

            return Json(new { @datajson = JsonConvert.SerializeObject(mBrand), itemsPerPage = db.Brand.Count() }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/GetStorageList")]
        public JsonResult StorageListJson(int take, int page)
        {
            List<ListStorageViewModels> mStorage = db.Storage.OrderBy(x => x.StorageId).Select(x => new ListStorageViewModels
            {
                StorageName = x.StorageName,
                StorageDescription = x.StorageDescription,
                StorageId = x.StorageId
            }).Skip(take * (page - 1)).Take(take).ToList();

            return Json(new { @datajson = JsonConvert.SerializeObject(mStorage), itemsPerPage = db.Storage.Count() }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/GetItemCollection")]
        public JsonResult ItemCollectionJson()
        {
            List<ShoppingBasketCollection> mItemCollections = db.Item.OrderBy(x => x.ItemId).Select(x => new ShoppingBasketCollection
            {
                ItemName = x.ItemName,
                ItemPrice = x.SalePrice,
                TotalStock = x.Stock,
                ItemId = x.ItemId
            }).ToList();

            return Json(new { @datajson = JsonConvert.SerializeObject(mItemCollections), itemsPerPage = 0 }, JsonRequestBehavior.AllowGet);
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