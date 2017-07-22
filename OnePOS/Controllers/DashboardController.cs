using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnePOS.FunctionController;
using OnePOS.Models;
using OnePOS.Models.Dashboard;
using OnePOS.Models.Dashboard.Items;
using OnePOS.Models.Dashboard.Vendors;

namespace OnePOS.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //private ApplicationRoleManager _roleManager;
        //public ApplicationRoleManager RoleManager
        //{
        //    get
        //    {
        //        return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
        //    }
        //    private set
        //    {
        //        _roleManager = value;
        //    }
        //}

        [Route("Dashboard")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardHome()
        {
            //DashboardFunction.StarDashboardIndex();

            return View();
        }

        [Route("Dashboard/ListItems")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardListItems()
        {
            //DashboardFunction.StarDashboardIndex();

            return View();
        }

        
        [Route("Dashboard/AddItems")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddItems()
        {
            //DashboardFunction.StarDashboardIndex();

            return View();
        }
        [HttpPost]
        [Route("Dashboard/AddItems")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddItems(AddItemViewModels itemsData)
        {
            DashboardFunction.AddItemsToDb(itemsData);

            return View();
        }

        [Route("Dashboard/ListVendors")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardListVendors()
        {
            List<VendorViewModels> mVendor = db.Vendor.ToList();
            List<ListVendorViewModels> mListVendor = new List<ListVendorViewModels>();
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
            return View(mListVendor);
            //return View(db.Vendor.ToList());
        }
        public JsonResult StarDashboardIndex()
        {
            return Json(new { @datajson = "" }, JsonRequestBehavior.AllowGet);
        }
        [Route("Dashboard/EditVendor/{idVendor}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardEditVendor(int idVendor)
        {

            return View(DashboardFunction.GetVendorViewModels(db, idVendor));
        }

        [Route("Dashboard/AddVendors")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddVendors()
        {
            //DashboardFunction.StarDashboardIndex();

            return View();
        }
        [HttpPost]
        [Route("Dashboard/AddVendors")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddVendors(AddVendorViewModels vendorsData)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(vendorsData);
            //}
            var currentUserName = User.Identity.GetUserName();

            var catchFlag = DashboardFunction.AddVendorsToDb(db, vendorsData, currentUserName);
            
            return View();
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