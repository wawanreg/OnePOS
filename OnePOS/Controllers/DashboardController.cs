using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnePOS.FunctionController;
using OnePOS.Models;
using OnePOS.Models.Dashboard;
using OnePOS.Models.Dashboard.Brand;
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
            AddItemViewModels test = new AddItemViewModels();
            test.VendorDropdownLists = new SelectList(db.Vendor.Where(x => x.Active).ToList(), "VendorId", "VendorName");

            //ViewBag.RoleId = new SelectList(db.Vendor.Where(x=> x.Active).ToList(), "VendorId", "VendorName");

            return View(test);
        }
        [HttpPost]
        [Route("Dashboard/AddItems")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddItems(AddItemViewModels itemsData)
        {
            var currentUserName = User.Identity.GetUserName();
            DashboardFunction.AddItemsToDb(db,itemsData, currentUserName);

            return RedirectToAction("AddItems");
        }

        [Route("Dashboard/ListVendors")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardListVendors()
        {
            return View();
        }
        
        [Route("Dashboard/EditVendor/{idVendor}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardEditVendor(int idVendor)
        {
            return View(DashboardFunction.GetVendorViewModels(db, idVendor));
        }
        [HttpPost]
        [Route("Dashboard/EditVendor/{idVendor}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardEditVendor(int idVendor, VendorViewModels mVendor)
        {
            var currentUserName = User.Identity.GetUserName();
            DashboardFunction.EditVendorViewModels(db, currentUserName, mVendor, idVendor);

            return RedirectToAction("ListVendors");
        }

        
        [Route("Dashboard/DeleteVendor/{idVendor}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardDeleteVendor(int idVendor)
        {
            return View(DashboardFunction.GetVendorViewModels(db, idVendor));
        }

        [HttpPost]
        [Route("Dashboard/DeleteVendor/{idVendor}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardDeleteVendor(int idVendor, VendorViewModels mVendor)
        {
            DashboardFunction.DeleteVendorViewModels(db,mVendor, idVendor);
            return RedirectToAction("ListVendors");
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
            
            var currentUserName = User.Identity.GetUserName();

            var catchFlag = DashboardFunction.AddVendorsToDb(db, vendorsData, currentUserName);
            
            return View();
        }

        [Route("Dashboard/AddBrands")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddBrands()
        {
            //DashboardFunction.StarDashboardIndex();
            AddBrandViewModels test = new AddBrandViewModels();
            test.BrandCategoryDropdownLists = new SelectList(db.BrandCategory.ToList(), "BrandCategoryId", "BrandCategoryName");

            return View(test);
        }
        [HttpPost]
        [Route("Dashboard/AddBrands")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddBrands(AddBrandViewModels brandData)
        {

            var currentUserName = User.Identity.GetUserName();

            DashboardFunction.AddBrandsToDb(db, brandData, currentUserName);

            return RedirectToAction("AddBrands");
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