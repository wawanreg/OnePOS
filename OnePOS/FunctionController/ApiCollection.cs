using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using OnePOS.FunctionController.Json.Converter;
using OnePOS.Helpers;
using OnePOS.Models;
using OnePOS.Models.Dashboard.Items;
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
            List<VendorViewModels> mVendor = db.Vendor.OrderBy(x=> x.VendorId).Skip(take * (page - 1)).Take(take).ToList();
            
            var mListVendor = new List<ListVendorViewModels>();
            foreach (var vendorViewModels in mVendor)
            {
                mListVendor.Add(new ListVendorViewModels()
                {
                    VendorId = vendorViewModels.VendorId,
                    VendorAddress = vendorViewModels.VendorAddress,
                    VendorEmail = vendorViewModels.VendorEmail,
                    VendorPhone = vendorViewModels.VendorPhone,
                    VendorOwner = vendorViewModels.VendorOwner,
                    VendorName = vendorViewModels.VendorName,
                    PaginationNumber = mListVendor.Count
                });

            }
            var itemsCount = 0;
            if (mVendor.Count != 0) itemsCount = mVendor.Count;

            return Json(new { @datajson = mListVendor.ToJson(new VendorListJsonConverter()),itemsPerPage = itemsCount }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiCollection/GetItemList")]
        public JsonResult ItemListJson(int take, int page)
        {
            List<ItemViewModels> mItem = db.Item.OrderBy(x => x.ItemId).Skip(take * (page - 1)).Take(take).ToList();

            var mListItem = new List<ListItemViewModels>();
            foreach (var itemViewModels in mItem)
            {
                var tempVendorName = "";
                if(itemViewModels.Vendor != null)
                    tempVendorName = itemViewModels.Vendor.VendorName;

                mListItem.Add(new ListItemViewModels()
                {
                    ItemName = itemViewModels.ItemName,
                    ItemBuyPrice = itemViewModels.BuyPrice,
                    ItemSalePrice = itemViewModels.SalePrice,
                    ItemVendorName = tempVendorName,
                    ItemLocation = itemViewModels.ItemLocation,
                    ItemQuantitiy = itemViewModels.Stock,
                    ItemId = itemViewModels.ItemId,
                    PaginationNumber= mListItem.Count
                });

            }
            var itemsCount = 0;
            if (mItem.Count != 0) itemsCount = mItem.Count;

            return Json(new { @datajson = mListItem.ToJson(new ItemListJsonConverter()), itemsPerPage = itemsCount }, JsonRequestBehavior.AllowGet);
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