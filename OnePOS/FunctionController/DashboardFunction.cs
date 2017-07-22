using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using OnePOS.Models;
using OnePOS.Models.Dashboard;
using OnePOS.Models.Dashboard.Items;
using OnePOS.Models.Dashboard.Vendors;
using ModelState = System.Web.WebPages.Html.ModelState;

namespace OnePOS.FunctionController
{
    public class DashboardFunction
    {
        public static void StarDashboardIndex()
        {
           
        }
        public static void AddItemsToDb(AddItemViewModels mItemsList)
        {
            var arrBranchName = mItemsList.ItemName.Split('|');
            var arrBranchAddress = mItemsList.ItemSalePrice.Split('|');
            var arrBranchCity = mItemsList.ItemBuyPrice.Split('|');
            var arrBranchProvince = mItemsList.ItemLocation.Split('|');
            var arrBranchState = mItemsList.ItemBrandType.Split('|');
            var arrBranchPostalcode = mItemsList.ItemQuantitiy.Split('|');
            var arrBranchPhone = mItemsList.ItemVendor.Split('|');
        }

        public static bool AddVendorsToDb(ApplicationDbContext db, AddVendorViewModels mVendorsList, string userName)
        {
            var checkerFlag = false;
            
            var arrVendorName = mVendorsList.VendorName.Split('|');
            var arrVendorAddress = mVendorsList.VendorAddress.Split('|');
            var arrVendorPhone = mVendorsList.VendorPhone.Split('|');
            var arrVendorEmail = mVendorsList.VendorEmail.Split('|');
            var arrVendorOwner = mVendorsList.VendorOwner.Split('|');
            
            for (var i = 0; i < arrVendorName.Length; i++)
            {
                VendorViewModels mVendorViewModels = new VendorViewModels
                {
                    VendorName = arrVendorName[i],
                    VendorAddress = arrVendorAddress[i],
                    VendorPhone = arrVendorPhone[i],
                    VendorEmail = arrVendorEmail[i],
                    VendorOwner = arrVendorOwner[i],
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = userName,
                    UpdatedDate = DateTime.UtcNow,
                    UpdatedBy = userName,
                    Active = true
                };

                db.Vendor.Add(mVendorViewModels);
                db.SaveChanges();
                checkerFlag = true;
            }

            return checkerFlag;
        }

        public static VendorViewModels GetVendorViewModels(ApplicationDbContext db, int? vendorId)
        {
            VendorViewModels mVendor = db.Vendor.Find(vendorId);
            
            return mVendor;
        }
    }
}