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
using OnePOS.Models.Dashboard.Storage;
using OnePOS.Models.Dashboard.Vendors;
using ModelState = System.Web.WebPages.Html.ModelState;

namespace OnePOS.FunctionController
{
    public class DashboardFunction
    {
        public static void StarDashboardIndex()
        {
           
        }

        public static SelectList GetDropdownBrandCategory(ApplicationDbContext db, string selectedVal)
        {
            return new SelectList(db.BrandCategory.ToList(), "BrandCategoryId", "BrandCategoryName", selectedVal);
        }
        public static SelectList GetDropdownVendor(ApplicationDbContext db, string selectedVal)
        {
            return new SelectList(db.Vendor.Where(x => x.Active).ToList(), "VendorId", "VendorName", selectedVal);
        }
        public static SelectList GetDropdownBrand(ApplicationDbContext db, string selectedVal)
        {
            return new SelectList(db.Brand.Where(x => x.Active).ToList(), "BrandId", "BrandName", selectedVal);
        }
        public static SelectList GetDropdownStorage(ApplicationDbContext db, string selectedVal)
        {
            return new SelectList(db.Storage.Where(x => x.Active).ToList(), "StorageId", "StorageName", selectedVal);
        }

        public static ItemViewModels GetItemViewModels(ApplicationDbContext db, int? itemId)
        {
            ItemViewModels mItem = db.Item.Single(x => !x.Deleted && x.ItemId == itemId);

            return mItem;
        }
        public static ActionItemViewModels GetActionItemViewModels(ApplicationDbContext db, int? itemId)
        {
            ItemViewModels mItem = GetItemViewModels(db, itemId);

            ActionItemViewModels actionItemModels = new ActionItemViewModels();

            actionItemModels.ItemName = mItem.ItemName;
            actionItemModels.ItemBuyPrice = mItem.BuyPrice.ToString();
            actionItemModels.ItemSalePrice = mItem.SalePrice.ToString();
            actionItemModels.ItemQuantitiy = mItem.Stock.ToString();

            actionItemModels.BranchCategoryDropdownLists = GetDropdownBrandCategory(db,
                mItem.BrandCategory.BrandCategoryId.ToString());
            actionItemModels.BranchDropdownLists = GetDropdownBrand(db, mItem.Brand.BrandId.ToString());
            actionItemModels.StorageDropdownLists = GetDropdownStorage(db, mItem.Storage.StorageId.ToString());
            actionItemModels.VendorDropdownLists = GetDropdownVendor(db, mItem.Vendor.VendorId.ToString());

            actionItemModels.ItemStorage = mItem.Storage.StorageId.ToString();
            actionItemModels.ItemVendor = mItem.Vendor.VendorId.ToString();
            actionItemModels.ItemBrandType = mItem.Brand.BrandId.ToString();
            actionItemModels.ItemBrandCategory = mItem.BrandCategory.BrandCategoryId.ToString();


            return actionItemModels;
        }
        public static void AddItemsToDb(ApplicationDbContext db, ActionItemViewModels mItemsList, string userName)
        {
            var arrItemName = mItemsList.ItemName.Split('|');
            var arrItemSalePrice = mItemsList.ItemSalePrice.Split('|');
            var arrItemBuyPrice = mItemsList.ItemBuyPrice.Split('|');
            var arrItemStorage = mItemsList.ItemStorage.Split('|');
            var arrItemBrandType = mItemsList.ItemBrandType.Split('|');
            var arrItemQuantitiy = mItemsList.ItemQuantitiy.Split('|');
            var arrItemVendor = mItemsList.ItemVendor.Split('|');
            var arrItemBrandCategory = mItemsList.ItemBrandCategory.Split('|');

            

            for (var i = 0; i < arrItemName.Length; i++)
            {
                var tempItemVendor = int.Parse(arrItemVendor[i]);
                var tempBrandType = int.Parse(arrItemBrandType[i]);
                var tempBrandCategory = int.Parse(arrItemBrandCategory[i]);
                var tempStorage = int.Parse(arrItemStorage[i]);
                

                VendorViewModels mVendor = db.Vendor.SingleOrDefault(x => x.VendorId == tempItemVendor);
                BrandViewModels mBrand = db.Brand.SingleOrDefault(x => x.BrandId == tempBrandType);
                BrandCategoryModels mBrandCategory =
                    db.BrandCategory.SingleOrDefault(x => x.BrandCategoryId == tempBrandCategory);
                StorageViewModels mStorage = db.Storage.SingleOrDefault(x => x.StorageId == tempStorage);
                ItemViewModels mItemViewModels = new ItemViewModels
                {
                    ItemName = arrItemName[i],
                    SalePrice = int.Parse(arrItemSalePrice[i]),
                    BuyPrice = int.Parse(arrItemBuyPrice[i]),
                    Storage = mStorage,
                    Brand = mBrand,
                    Stock = int.Parse(arrItemQuantitiy[i]),
                    Vendor = mVendor,
                    BrandCategory = mBrandCategory,
                    CreatedBy = userName,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = userName,
                    UpdatedDate = DateTime.UtcNow,
                    Active = true
                    
                };
                db.Item.Add(mItemViewModels);
                db.SaveChanges();
            }

        }
        public static bool EditItemViewModels(ApplicationDbContext db, string currentUserName, ActionItemViewModels mEditItem, int idItem)
        {
            ItemViewModels mItem = db.Item.Find(idItem);

            int valVendorId = int.Parse(mEditItem.ItemVendor);
            int valBrandId = int.Parse(mEditItem.ItemBrandType);
            int valBrandCategoryId = int.Parse(mEditItem.ItemBrandCategory);
            int valStorageId = int.Parse(mEditItem.ItemStorage);

            VendorViewModels mVendor = db.Vendor.SingleOrDefault(x => x.VendorId == valVendorId);
            BrandViewModels mBrand = db.Brand.SingleOrDefault(x => x.BrandId == valBrandId);
            BrandCategoryModels mBrandCategory = db.BrandCategory.SingleOrDefault(x => x.BrandCategoryId == valBrandCategoryId);
            StorageViewModels mStorage = db.Storage.SingleOrDefault(x => x.StorageId == valStorageId);

            mItem.ItemName = mEditItem.ItemName;
            mItem.Stock = decimal.Parse(mEditItem.ItemQuantitiy);
            mItem.BuyPrice = decimal.Parse(mEditItem.ItemBuyPrice);
            mItem.SalePrice = decimal.Parse(mEditItem.ItemSalePrice);
            mItem.Vendor = mVendor;
            mItem.Brand = mBrand;
            mItem.BrandCategory = mBrandCategory;
            mItem.Storage = mStorage;
            mItem.UpdatedBy = currentUserName;
            mItem.UpdatedDate = DateTime.UtcNow;


            db.Entry(mItem).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }
        public static void DeleteItemViewModels(ApplicationDbContext db, int idItem, string currentUserName)
        {
            ItemViewModels mItem = db.Item.Find(idItem);
            mItem.Deleted = true;
            mItem.UpdatedBy = currentUserName;
            mItem.UpdatedDate = DateTime.UtcNow;

            db.Entry(mItem).State = EntityState.Modified;
            //db.Item.Remove(mItem);
            db.SaveChanges();
        }
        
        
        public static VendorViewModels GetVendorViewModels(ApplicationDbContext db, int? vendorId)
        {
            VendorViewModels mVendor = db.Vendor.Single(x=> !x.Deleted && x.VendorId == vendorId);

            return mVendor;
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
        public static void DeleteVendorViewModels(ApplicationDbContext db, int idVendor, string currentUserName)
        {
            VendorViewModels mVendor = db.Vendor.Find(idVendor);
            mVendor.Deleted = true;
            mVendor.UpdatedBy = currentUserName;
            mVendor.UpdatedDate = DateTime.UtcNow;

            db.Entry(mVendor).State = EntityState.Modified;

            //db.Vendor.Remove(mVendor);
            db.SaveChanges();
        }


        public static BrandViewModels GetBrandViewModels(ApplicationDbContext db, int? brandId)
        {
            BrandViewModels mBrand = db.Brand.Single(x=>!x.Deleted && x.BrandId == brandId);

            return mBrand;
        }
        public static void AddBrandsToDb(ApplicationDbContext db, ActionBrandViewModels mBrandList, string userName)
        {

            var arrBrandName = mBrandList.BrandName.Split('|');
            //var arrBrandCategory = mBrandList.BrandCategoryId.Split('|');
            var arrBrandDescription = mBrandList.BrandDescription.Split('|');

            for (var i = 0; i < arrBrandName.Length; i++)
            {
                //int brandCategoryId = int.Parse(arrBrandCategory[i]);
                BrandViewModels mBrandViewModels = new BrandViewModels
                {
                    BrandName = arrBrandName[i],
                    BrandDescription = arrBrandDescription[i],
                    //BrandCategory = db.BrandCategory.Find(brandCategoryId),
                    CreatedBy = userName,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = userName,
                    UpdatedDate = DateTime.UtcNow,
                    Active = true
                };

                db.Brand.Add(mBrandViewModels);
                db.SaveChanges();
            }

        }
        public static bool EditBrandViewModels(ApplicationDbContext db, string currentUserName, ActionBrandViewModels mEditBrand, int idBrand)
        {
            BrandViewModels mBrand = db.Brand.Find(idBrand);


            mBrand.BrandName = mEditBrand.BrandName;
            mBrand.BrandDescription = mEditBrand.BrandDescription;
            mBrand.UpdatedBy = currentUserName;
            mBrand.UpdatedDate = DateTime.UtcNow;


            db.Entry(mBrand).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }
        public static void DeleteBrandViewModels(ApplicationDbContext db, int idBrand, string currentUserName)
        {
            BrandViewModels mBrand = db.Brand.Find(idBrand);
            mBrand.Deleted = true;
            mBrand.UpdatedBy = currentUserName;
            mBrand.UpdatedDate = DateTime.UtcNow;

            db.Entry(mBrand).State = EntityState.Modified;
            //db.Brand.Remove(mBrand);
            db.SaveChanges();
        }


        public static StorageViewModels GetStorageViewModels(ApplicationDbContext db, int? storageId)
        {
            StorageViewModels mStorage = db.Storage.Single(x=> !x.Deleted && x.StorageId == storageId);

            return mStorage;
        }
        public static void AddStoragesToDb(ApplicationDbContext db, ActionStorageViewModels mStorage, string userName)
        {

            var arrStorageName = mStorage.StorageName.Split('|');
            var arrStorageDescription = mStorage.StorageDescription.Split('|');

            for (var i = 0; i < arrStorageName.Length; i++)
            {
                StorageViewModels mStorageViewModels = new StorageViewModels
                {
                    StorageName = arrStorageName[i],
                    StorageDescription = arrStorageDescription[i],
                    CreatedBy = userName,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = userName,
                    UpdatedDate = DateTime.UtcNow,
                    Active = true
                };

                db.Storage.Add(mStorageViewModels);
                db.SaveChanges();
            }
        }
        public static bool EditStorageViewModels(ApplicationDbContext db, string currentUserName, ActionStorageViewModels mEditStorage, int idStorage)
        {
            StorageViewModels mStorage = db.Storage.Find(idStorage);


            mStorage.StorageName = mEditStorage.StorageName;
            mStorage.StorageDescription = mEditStorage.StorageDescription;
            mStorage.UpdatedBy = currentUserName;
            mStorage.UpdatedDate = DateTime.UtcNow;

            db.Entry(mStorage).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }
        public static void DeleteStorageViewModels(ApplicationDbContext db, int idStorage, string currentUserName)
        {
            StorageViewModels mStorage = db.Storage.Find(idStorage);
            mStorage.Deleted = true;
            mStorage.UpdatedBy = currentUserName;
            mStorage.UpdatedDate = DateTime.UtcNow;
            db.Entry(mStorage).State = EntityState.Modified;

            //db.Storage.Remove(mStorage);
            db.SaveChanges();
        }
    
    }
}