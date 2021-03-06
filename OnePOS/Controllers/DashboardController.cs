﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OfficeOpenXml;
using OnePOS.FunctionController;
using OnePOS.Models;
using OnePOS.Models.Dashboard;
using OnePOS.Models.Dashboard.Brand;
using OnePOS.Models.Dashboard.Items;
using OnePOS.Models.Dashboard.Storage;
using OnePOS.Models.Dashboard.Vendors;
using OnePOS.Models.Invoice;
using System.Web.UI.WebControls;

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
        public void GetUploadStatus()
        {
            ViewBag.SuccessCtr = TempData["SuccessMessage"];
            ViewBag.FailedCtr = TempData["FailedMessage"];
        }

        private void SetUploadStatus(UploadModel uploadStatus)
        {
            if (uploadStatus.IsSuccess)
            {
                if (uploadStatus.SuccessCtr > 0)
                    TempData["SuccessMessage"] = uploadStatus.SuccessCtr + " file successfully upload";
                else
                    TempData["SuccessMessage"] = uploadStatus.SuccessCtr + " there's no new files";

                if (uploadStatus.FailedCtr > 0)
                    TempData["FailedMessage"] = uploadStatus.FailedCtr + " file failed to upload";
            }
        }

        public string SendCurrentUser()
        {
            var currentUserName = User.Identity.GetUserName();

            return currentUserName;
        }

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
            GetUploadStatus();
            ActionItemViewModels addItemModels = new ActionItemViewModels
            {
                VendorDropdownLists = DashboardFunction.GetDropdownVendor(db, ""),
                BrandCategoryDropdownLists = DashboardFunction.GetDropdownBrandCategory(db, ""),
                StorageDropdownLists = DashboardFunction.GetDropdownStorage(db, ""),
                BrandDropdownLists = DashboardFunction.GetDropdownBrand(db, "")
            };
            

            return View(addItemModels);
        }
        
        [HttpPost]
        [Route("Dashboard/AddItems")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddItems(ActionItemViewModels itemsData)
        {
            
            DashboardFunction.AddItemsToDb(db,itemsData, SendCurrentUser());

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
            if (ModelState.IsValid)
            {
                DashboardFunction.EditItemViewModels(db, SendCurrentUser(), mItem, idItem);

                return RedirectToAction("ListItems");    
            }
            
            mItem.BrandDropdownLists = DashboardFunction.GetDropdownBrand(db, mItem.ItemBrandType);
            mItem.BrandCategoryDropdownLists = DashboardFunction.GetDropdownBrandCategory(db, mItem.ItemBrandCategory);
            mItem.StorageDropdownLists = DashboardFunction.GetDropdownStorage(db, mItem.ItemStorage);
            mItem.VendorDropdownLists = DashboardFunction.GetDropdownVendor(db, mItem.ItemVendor);

            return View(mItem);
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
            DashboardFunction.DeleteItemViewModels(db, idItem, SendCurrentUser());
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
            if (ModelState.IsValid)
            {
                DashboardFunction.EditVendorViewModels(db, SendCurrentUser(), mVendor, idVendor);

                return RedirectToAction("ListVendors");    
            }

            return View(mVendor);
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
            
            DashboardFunction.DeleteVendorViewModels(db, idVendor, SendCurrentUser());
            return RedirectToAction("ListVendors");
        }

        [Route("Dashboard/AddVendors")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddVendors()
        {
            //DashboardFunction.StarDashboardIndex();
            GetUploadStatus();
            return View();
        }

        [HttpPost]
        [Route("Dashboard/AddVendors")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddVendors(AddVendorViewModels vendorsData)
        {
            var catchFlag = DashboardFunction.AddVendorsToDb(db, vendorsData, SendCurrentUser());

            return RedirectToAction("AddVendors");
        }

        [Route("Dashboard/AddBrands")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddBrands()
        {
            GetUploadStatus();
            return View();
        }
        
        [HttpPost]
        [Route("Dashboard/AddBrands")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddBrands(ActionBrandViewModels brandData)
        {
            DashboardFunction.AddBrandsToDb(db, brandData, SendCurrentUser());
            

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
        public ActionResult DashboardEditBrand(int idBrand, BrandViewModels mBrand)
        {
            
            if (ModelState.IsValid)
            {
                DashboardFunction.EditBrandViewModels(db, SendCurrentUser(), mBrand, idBrand);
                return RedirectToAction("ListBrands");
            }

            return View(mBrand); //RedirectToAction("DashboardEditBrand"); //RedirectToAction("ListBrands");
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

            DashboardFunction.DeleteBrandViewModels(db, idBrand, SendCurrentUser());
            return RedirectToAction("ListBrands");
        }

        [Route("Dashboard/AddStorages")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddStorages()
        {
            GetUploadStatus();
            return View();
        }

        [HttpPost]
        [Route("Dashboard/AddStorages")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardAddStorages(ActionStorageViewModels storageData)
        {
            DashboardFunction.AddStoragesToDb(db, storageData, SendCurrentUser());

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
            if (ModelState.IsValid)
            {
                DashboardFunction.EditStorageViewModels(db, SendCurrentUser(), mStorage, idStorage);

                return RedirectToAction("ListStorages");    
            }

            return View(mStorage);
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

            DashboardFunction.DeleteStorageViewModels(db, idStorage, SendCurrentUser());
            return RedirectToAction("ListStorages");
        }

        [Route("Dashboard/ShoppingBasket")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardShoppingBasket()
        {
            
            return View();
        }

        [HttpPost]
        [Route("Dashboard/ShoppingBasket")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardShoppingBasket(TransactionViewModels mTranscationModels)
        {
            var billindNo = InvoiceControllerServices.GenerateInvoice(db, mTranscationModels, SendCurrentUser());

            return RedirectToAction("InvoiceDetail", new { id = billindNo });
        }

        [Route("Dashboard/ListInvoice")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardListInvoice()
        {
            return View();
        }

        [Route("Dashboard/InvoiceDetail/{billingId}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardInvoiceDetail(int billingId)
        {
            return View(db.BillingHeader.Single(x => x.NoBillingHeader == billingId));
        }

        [Route("Dashboard/EditInvoice/{billingId}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardEditInvoice(int billingId)
        {
            return View(InvoiceControllerServices.SetBillingCollection(db, billingId));
        }

        [HttpPost]
        [Route("Dashboard/EditInvoice/{billingId}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardEditInvoice(int billingId, BillingCollectionModel mBillingCollectionModel)
        {
            InvoiceControllerServices.EditInvoice(db,SendCurrentUser(), billingId, mBillingCollectionModel);
            
            return RedirectToAction("ListInvoice");
        }

        [Route("Dashboard/DeleteInvoice/{billingId}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardDeleteInvoice(int billingId)
        {
            return View(InvoiceControllerServices.GetBillingHeader(db,billingId));
        }

        [HttpPost]
        [Route("Dashboard/DeleteInvoice/{billingId}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult DashboardDeleteInvoice(int billingId, BillingHeaderModel mBillingHeaderModel)
        {
            InvoiceControllerServices.DeleteInvoice(db, SendCurrentUser(), billingId, mBillingHeaderModel);

            return RedirectToAction("ListInvoice");
        }

        [Route("Dashboard/Excel/{targetedDb}")]
        [Authorize(Roles = "Super Admin,Admin")]
        public void DashboardDownloadExcel(string targetedDb)
        {
            byte[] fileData;
            string fileName;

            switch (targetedDb)
            {
                case "brand": 
                    fileData = ExcelFunction.DownloadBrandExcel(db,SendCurrentUser());
                    fileName = "OnePos-Brand.xlsx";
                    MimeMapping.GetMimeMapping(fileName);
                    Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", fileName));
                    Response.BinaryWrite(fileData);
                    Response.End();

                    break;

                case "storage": 
                    fileData = ExcelFunction.DownloadStorageExcel(db, SendCurrentUser());
                    fileName = "OnePos-Storage.xlsx";
                    MimeMapping.GetMimeMapping(fileName);
                    Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", fileName));
                    Response.BinaryWrite(fileData);
                    Response.End();

                    break;

                case "vendor":
                    fileData = ExcelFunction.DownloadVendorExcel(db, SendCurrentUser());
                    fileName = "OnePos-Vendor.xlsx";
                    MimeMapping.GetMimeMapping(fileName);
                    Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", fileName));
                    Response.BinaryWrite(fileData);
                    Response.End();

                    break;

                case "item":
                    fileData = ExcelFunction.DownloadItemExcel(db, SendCurrentUser());
                    fileName = "OnePos-Item.xlsx";
                    MimeMapping.GetMimeMapping(fileName);
                    Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", fileName));
                    Response.BinaryWrite(fileData);
                    Response.End();

                    break;
            }
        }

        
        [HttpPost]
        [Route("Dashboard/UploadExcel")]
        [Authorize(Roles = "Super Admin,Admin")]
        public ActionResult UploadExcel(string targetedDb, HttpPostedFileBase excelFile)
        {
            

            var position = "";
            var userName = SendCurrentUser();

            switch (targetedDb)
            {
                case "brand":

                    if (excelFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" && excelFile.ContentType != "application/vnd.ms-excel")
                    {
                        return View("DashboardAddBrands");
                    }        
                    
                    SetUploadStatus(ExcelFunction.UploadBrandExcel(db,userName,excelFile));
                    
                    position = "DashboardAddBrands";

                    break;

                case "storage":

                    if (excelFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" && excelFile.ContentType != "application/vnd.ms-excel")
                    {
                        return View("DashboardAddStorages");
                    }

                    SetUploadStatus(ExcelFunction.UploadStorageExcel(db, userName, excelFile));

                    position = "DashboardAddStorages";

                    break;

                case "vendor":

                    if (excelFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" && excelFile.ContentType != "application/vnd.ms-excel")
                    {
                        return View("DashboardAddVendors");
                    }

                    SetUploadStatus(ExcelFunction.UploadVendorExcel(db, userName, excelFile));

                    position = "DashboardAddVendors";

                    break;

                case "item":

                    if (excelFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" && excelFile.ContentType != "application/vnd.ms-excel")
                    {
                        return View("DashboardAddItems");
                    }

                    SetUploadStatus(ExcelFunction.UploadItemExcel(db, userName, excelFile));

                    position = "DashboardAddItems";

                    break;
            }
            
            return RedirectToAction(position);
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