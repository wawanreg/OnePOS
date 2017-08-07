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
using OnePOS.Models.Dashboard.Storage;
using OnePOS.Models.Dashboard.Vendors;
using OnePOS.Models.Invoice;

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
            ActionItemViewModels addItemModels = new ActionItemViewModels();
            addItemModels.VendorDropdownLists = DashboardFunction.GetDropdownVendor(db,"");
            addItemModels.BranchCategoryDropdownLists = DashboardFunction.GetDropdownBrandCategory(db, "");
            addItemModels.StorageDropdownLists = DashboardFunction.GetDropdownStorage(db, "");
            addItemModels.BranchDropdownLists = DashboardFunction.GetDropdownBrand(db, "");

            return View(addItemModels);
        }
        [HttpPost]
        [Route("Dashboard/AddItems")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddItems(ActionItemViewModels itemsData)
        {
            var currentUserName = User.Identity.GetUserName();
            DashboardFunction.AddItemsToDb(db,itemsData, currentUserName);

            return RedirectToAction("AddItems");
        }

        [Route("Dashboard/EditItem/{idItem}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardEditItem(int idItem)
        {
            return View(DashboardFunction.GetActionItemViewModels(db, idItem));
        }

        [HttpPost]
        [Route("Dashboard/EditItem/{idItem}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardEditItem(int idItem, ActionItemViewModels mItem)
        {
            var currentUserName = User.Identity.GetUserName();
            DashboardFunction.EditItemViewModels(db, currentUserName, mItem, idItem);

            return RedirectToAction("ListItems");
        }

        [Route("Dashboard/DeleteItem/{idItem}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardDeleteItem(int idItem)
        {
            return View(DashboardFunction.GetItemViewModels(db, idItem));
        }

        [HttpPost]
        [Route("Dashboard/DeleteItem/{idItem}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardDeleteItem(int idItem, ItemViewModels mItem)
        {
            DashboardFunction.DeleteItemViewModels(db, mItem, idItem);
            return RedirectToAction("ListItems");
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
            return View();
        }
        
        [HttpPost]
        [Route("Dashboard/AddBrands")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddBrands(ActionBrandViewModels brandData)
        {

            var currentUserName = User.Identity.GetUserName();

            DashboardFunction.AddBrandsToDb(db, brandData, currentUserName);

            return RedirectToAction("AddBrands");
        }

        [Route("Dashboard/ListBrands")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardListBrands()
        {
            return View();
        }

        [Route("Dashboard/EditBrand/{idBrand}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardEditBrand(int idBrand)
        {
            return View(DashboardFunction.GetBrandViewModels(db, idBrand));
        }

        [HttpPost]
        [Route("Dashboard/EditBrand/{idBrand}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardEditBrand(int idBrand, ActionBrandViewModels mBrand)
        {
            var currentUserName = User.Identity.GetUserName();
            DashboardFunction.EditBrandViewModels(db, currentUserName, mBrand, idBrand);

            return RedirectToAction("ListBrands");
        }

        [Route("Dashboard/DeleteBrand/{idBrand}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardDeleteBrand(int idBrand)
        {
            return View(DashboardFunction.GetBrandViewModels(db, idBrand));
        }

        [HttpPost]
        [Route("Dashboard/DeleteBrand/{idBrand}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardDeleteBrand(int idBrand, BrandViewModels mBrand)
        {
            DashboardFunction.DeleteBrandViewModels(db, mBrand, idBrand);
            return RedirectToAction("ListBrands");
        }

        [Route("Dashboard/AddStorages")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddStorages()
        {
            return View();
        }
        [HttpPost]
        [Route("Dashboard/AddStorages")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddStorages(ActionStorageViewModels storageData)
        {

            var currentUserName = User.Identity.GetUserName();

            DashboardFunction.AddStoragesToDb(db, storageData, currentUserName);

            return RedirectToAction("AddStorages");
        }

        [Route("Dashboard/ListStorages")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardListStorages()
        {
            return View();
        }

        [Route("Dashboard/EditStorage/{idStorage}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardEditStorage(int idStorage)
        {
            return View(DashboardFunction.GetStorageViewModels(db, idStorage));
        }

        [HttpPost]
        [Route("Dashboard/EditStorage/{idStorage}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardEditStorage(int idStorage, ActionStorageViewModels mStorage)
        {
            var currentUserName = User.Identity.GetUserName();
            DashboardFunction.EditStorageViewModels(db, currentUserName, mStorage, idStorage);

            return RedirectToAction("ListStorages");
        }

        [Route("Dashboard/DeleteStorage/{idStorage}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardDeleteStorage(int idStorage)
        {
            return View(DashboardFunction.GetStorageViewModels(db, idStorage));
        }

        [HttpPost]
        [Route("Dashboard/DeleteStorage/{idStorage}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardDeleteStorage(int idStorage, StorageViewModels mStorage)
        {
            DashboardFunction.DeleteStorageViewModels(db, mStorage, idStorage);
            return RedirectToAction("ListStorages");
        }

        [Route("Dashboard/ShoppingBasket")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardShoppingBasket()
        {
            //DashboardFunction.StarDashboardIndex();

            return View();
        }

        [HttpPost]
        [Route("Dashboard/ShoppingBasket")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardShoppingBasket(TransactionViewModels mTranscationModels)
        {
            //DashboardFunction.StarDashboardIndex();
            var currentUserName = User.Identity.GetUserName();
            
            InvoiceControllerServices.GenerateInvoice(db, mTranscationModels, currentUserName);

            return View();
        }

        [Route("Dashboard/ListInvoice")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardListInvoice()
        {
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