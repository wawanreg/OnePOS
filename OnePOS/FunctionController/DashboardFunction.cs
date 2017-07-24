using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using OnePOS.Models;
using OnePOS.Models.Dashboard;
using OnePOS.Models.Dashboard.Brand;
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
        public static void AddItemsToDb(ApplicationDbContext db, AddItemViewModels mItemsList, string userName)
        {
            var arrItemName = mItemsList.ItemName.Split('|');
            var arrItemSalePrice = mItemsList.ItemSalePrice.Split('|');
            var arrItemBuyPrice = mItemsList.ItemBuyPrice.Split('|');
            var arrItemLocation = mItemsList.ItemLocation.Split('|');
            var arrItemBrandType = mItemsList.ItemBrandType.Split('|');
            var arrItemQuantitiy = mItemsList.ItemQuantitiy.Split('|');
            var arrItemVendor = mItemsList.ItemVendor.Split('|');

            

            for (var i = 0; i < arrItemName.Length; i++)
            {
                var tempItemVendor = arrItemVendor[i];
                var tempBrandType = arrItemBrandType[i];
                VendorViewModels mVendor = db.Vendor.SingleOrDefault(x => x.VendorName == tempItemVendor);
                BrandViewModels mBrand = db.Brand.SingleOrDefault(x => x.BrandName == tempBrandType);

                ItemViewModels mItemViewModels = new ItemViewModels
                {
                    ItemName = arrItemName[i],
                    SalePrice = int.Parse(arrItemSalePrice[i]),
                    BuyPrice = int.Parse(arrItemBuyPrice[i]),
                    ItemLocation = arrItemLocation[i],
                    Brand = mBrand,
                    Stock = int.Parse(arrItemQuantitiy[i]),
                    Vendor = mVendor,
                    CreatedBy = userName,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = userName,
                    UpdatedDate = DateTime.UtcNow
                    
                };
                db.Item.Add(mItemViewModels);
                db.SaveChanges();
            }

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

        public static void AddBrandsToDb(ApplicationDbContext db, AddBrandViewModels mBrandList, string userName)
        {

            var arrBrandName = mBrandList.BrandName.Split('|');
            var arrBrandCategory = mBrandList.BrandCategoryId.Split('|');
            var arrBrandDescription = mBrandList.BrandDescription.Split('|');

            for (var i = 0; i < arrBrandName.Length; i++)
            {
                int brandCategoryId = int.Parse(arrBrandCategory[i]);
                BrandViewModels mBrandViewModels = new BrandViewModels
                {
                    BrandName = arrBrandName[i],
                    BrandDescription = arrBrandDescription[i],
                    BrandCategory = db.BrandCategory.Find(brandCategoryId),
                    CreatedBy = userName,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = userName,
                    UpdatedDate = DateTime.UtcNow
                };

                db.Brand.Add(mBrandViewModels);
                db.SaveChanges();
            }

        }
        public static VendorViewModels GetVendorViewModels(ApplicationDbContext db, int? vendorId)
        {
            VendorViewModels mVendor = db.Vendor.Find(vendorId);
            
            return mVendor;
        }

        public static bool EditVendorViewModels(ApplicationDbContext db, string currentUserName, VendorViewModels mEditVendor, int idVendor)
        {
            VendorViewModels mVendor = db.Vendor.Find(idVendor);

            mVendor.VendorName = mEditVendor.VendorName;
            mVendor.VendorAddress = mEditVendor.VendorAddress;
            mVendor.VendorEmail = mEditVendor.VendorEmail;
            mVendor.VendorOwner = mEditVendor.VendorOwner;
            mVendor.VendorPhone = mEditVendor.VendorPhone;

            mVendor.UpdatedBy = currentUserName;
            mVendor.UpdatedDate = DateTime.UtcNow;


            db.Entry(mVendor).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }

        public static void DeleteVendorViewModels(ApplicationDbContext db, VendorViewModels mDeleteVendor, int idVendor)
        {
            VendorViewModels mVendor = db.Vendor.Find(idVendor);
            db.Vendor.Remove(mVendor);
            db.SaveChanges();
        }
    }
}