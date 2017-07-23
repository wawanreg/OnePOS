using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OnePOS.FunctionController.Json.Converter;
using OnePOS.Helpers;
using OnePOS.Models;
using OnePOS.Models.Dashboard.Vendors;


namespace OnePOS.FunctionController
{
    public class ApiVendorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("ApiVendor/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [Route("ApiVendor/GetVendorList")]
        public JsonResult StarDashboardIndex(int take, int page)
        {
            List<VendorViewModels> mVendor = db.Vendor.OrderBy(x=> x.VendorId).Skip(take * (page - 1)).Take(take).ToList();
            
            var mListVendor = new List<ListVendorViewModels>();
            foreach (var vendorViewModelse in mVendor)
            {
                mListVendor.Add(new ListVendorViewModels()
                {
                    VendorId = vendorViewModelse.VendorId,
                    VendorAddress = vendorViewModelse.VendorAddress,
                    VendorEmail = vendorViewModelse.VendorEmail,
                    VendorPhone = vendorViewModelse.VendorPhone,
                    VendorOwner = vendorViewModelse.VendorOwner,
                    VendorName = vendorViewModelse.VendorName,
                    PaginationNumber = mListVendor.Count
                });

            }
            var itemsCount = 0;
            if (mVendor.Count != 0) itemsCount = mVendor.Count;

            return Json(new { @datajson = mListVendor.ToJson(new VendorListJsonConverter()),itemsPerPage = itemsCount }, JsonRequestBehavior.AllowGet);
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