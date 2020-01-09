using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using OnePOS.Models;
using OnePOS.Models.Dashboard.Brand;
using OnePOS.Models.Dashboard.Items;
using OnePOS.Models.Dashboard.Storage;
using OnePOS.Models.Dashboard.Vendors;

namespace OnePOS.FunctionController
{
    public partial class ExcelFunction : System.Web.UI.Page  
    {
        public static byte[] DownloadStorageExcel(ApplicationDbContext db, string currentUserName)
        {
            byte[] fileData = null;
            
            var dt = db.Storage.Where(x => !x.Deleted).ToList();
            
            using (ExcelPackage p = new ExcelPackage())
            {
                ExcelWorksheet ws = p.Workbook.Worksheets.Add("Storage");
                
                ws.Cells["A1"].Value = "Id";
                ws.Cells["B1"].Value = "Name";
                ws.Cells["C1"].Value = "Description";
                
                
                var index = 2;

                foreach (var data in dt)
                {
                    ws.Cells[index, 1].Value = data.StorageId;
                    ws.Cells[index, 2].Value = data.StorageName;
                    ws.Cells[index, 3].Value = data.StorageDescription;

                    index++;
                }

                ws.Cells.AutoFitColumns();
                ws.Column(1).Hidden = true;

                fileData = p.GetAsByteArray();
                
            }

            return fileData;
        }

        public static byte[] DownloadBrandExcel(ApplicationDbContext db, string currentUserName)
        {
            byte[] fileData = null;

            var dt = db.Brand.Where(x => !x.Deleted).ToList();

            using (ExcelPackage p = new ExcelPackage())
            {
                ExcelWorksheet ws = p.Workbook.Worksheets.Add("Brand");

                ws.Cells["A1"].Value = "Id";
                ws.Cells["B1"].Value = "Name";
                ws.Cells["C1"].Value = "Description";


                var index = 2;

                foreach (var data in dt)
                {
                    ws.Cells[index, 1].Value = data.BrandId;
                    ws.Cells[index, 2].Value = data.BrandName;
                    ws.Cells[index, 3].Value = data.BrandDescription;

                    index++;
                }

                ws.Cells.AutoFitColumns();
                ws.Column(1).Hidden = true;

                fileData = p.GetAsByteArray();

            }

            return fileData;
        }

        public static byte[] DownloadVendorExcel(ApplicationDbContext db, string currentUserName)
        {
            byte[] fileData = null;

            var dt = db.Vendor.Where(x => !x.Deleted).ToList();

            using (ExcelPackage p = new ExcelPackage())
            {
                ExcelWorksheet ws = p.Workbook.Worksheets.Add("Vendor");

                ws.Cells["A1"].Value = "Id";
                ws.Cells["B1"].Value = "Name";
                ws.Cells["C1"].Value = "Phone";
                ws.Cells["D1"].Value = "Owner";
                ws.Cells["E1"].Value = "Address";
                ws.Cells["F1"].Value = "Email";

                var index = 2;

                foreach (var data in dt)
                {
                    ws.Cells[index, 1].Value = data.VendorId;
                    ws.Cells[index, 2].Value = data.VendorName;
                    ws.Cells[index, 3].Value = data.VendorPhone;
                    ws.Cells[index, 4].Value = data.VendorOwner;
                    ws.Cells[index, 5].Value = data.VendorAddress;
                    ws.Cells[index, 6].Value = data.VendorEmail;

                    index++;
                }

                ws.Cells.AutoFitColumns();
                ws.Column(1).Hidden = true;

                fileData = p.GetAsByteArray();

            }

            return fileData;
        }

        public static void SetDropdownExcelList(ApplicationDbContext db,ExcelWorksheet ddList, ExcelPackage p, string dbTarget, 
            ExcelWorksheet ws, int targetPosition, int sourcePosition)
        {
            
            const int index = 2;
            var maxRow = 0;
            bool fetchingDataSuccess = false;

            if (dbTarget == "storage")
            {
                var listData = db.Storage.Where(x => !x.Deleted).ToList();
                
                maxRow = listData.Count();
                for (int i = 1; i <= listData.Count; i++)
                {
                    ddList.Cells[i, sourcePosition].Value = listData[i - 1].StorageId;
                    ddList.Cells[i, sourcePosition+1].Value = listData[i - 1].StorageName;
                }
                fetchingDataSuccess = true;
            }else if (dbTarget == "brand")
            {
                var listData = db.Brand.Where(x => !x.Deleted).ToList();

                maxRow = listData.Count();
                for (int i = 1; i <= listData.Count; i++)
                {
                    ddList.Cells[i, sourcePosition].Value = listData[i - 1].BrandId;
                    ddList.Cells[i, sourcePosition+1].Value = listData[i - 1].BrandName;
                }
                fetchingDataSuccess = true;
            }else if (dbTarget == "vendor")
            {
                var listData = db.Vendor.Where(x => !x.Deleted).ToList();

                maxRow = listData.Count();
                for (int i = 1; i <= listData.Count; i++)
                {
                    ddList.Cells[i, sourcePosition].Value = listData[i - 1].VendorId;
                    ddList.Cells[i, sourcePosition+1].Value = listData[i - 1].VendorName;
                }
                fetchingDataSuccess = true;
            }
            else if (dbTarget == "brandCategory")
            {
                var listData = db.BrandCategory.ToList();

                maxRow = listData.Count();
                for (int i = 1; i <= listData.Count; i++)
                {
                    ddList.Cells[i, sourcePosition].Value = listData[i - 1].BrandCategoryId;
                    ddList.Cells[i, sourcePosition+1].Value = listData[i - 1].BrandCategoryName;
                }
                fetchingDataSuccess = true;
            }

            if (fetchingDataSuccess)
            {
                var targetSetting = ws.DataValidations.AddListValidation(ws.Cells[index, targetPosition, maxRow * 2, targetPosition].Address);


                var address = ddList.Cells[1, sourcePosition, maxRow, sourcePosition].Address.ToString();
                var arr = address.Split(':');
                var char1 = arr[0][0];
                var num1 = arr[0].Trim(char1);
                var char2 = arr[1][0];
                var num2 = arr[1].Trim(char2);

                targetSetting.Formula.ExcelFormula = string.Format("=DropDownList!${0}${1}:${2}${3}", char1, num1, char2, num2);
                targetSetting.ShowErrorMessage = true;
                targetSetting.Error = "Select from List of Values ...";

                ddList.Cells.AutoFitColumns();
            }
            

        }

        public static byte[] DownloadItemExcel(ApplicationDbContext db, string currentUserName)
        {
            byte[] fileData = null;

            var dt = db.Item.Where(x => !x.Deleted).ToList();

            using (ExcelPackage p = new ExcelPackage())
            {
                ExcelWorksheet ws = p.Workbook.Worksheets.Add("Vendor");

                ws.Cells["A1"].Value = "Id";
                ws.Cells["B1"].Value = "Name";
                ws.Cells["C1"].Value = "Sale Price";
                ws.Cells["D1"].Value = "Buy Price";
                ws.Cells["E1"].Value = "Stock";
                ws.Cells["F1"].Value = "Storage";
                ws.Cells["G1"].Value = "Brand";
                ws.Cells["H1"].Value = "Vendor";
                ws.Cells["I1"].Value = "Brand Category";
                
                var index = 2;

                
                //set dropdown list
                ExcelWorksheet ddList = p.Workbook.Worksheets.Add("DropDownList");

                SetDropdownExcelList(db, ddList, p, "storage", ws, 6, 1);
                SetDropdownExcelList(db, ddList, p, "brand", ws, 7, 3);
                SetDropdownExcelList(db, ddList, p, "vendor", ws, 8, 5);
                SetDropdownExcelList(db, ddList, p, "brandCategory", ws, 9, 7);

                ExcelWorksheet ddListValue = p.Workbook.Worksheets.Add("Current List Value");
                ddListValue.Cells["A1"].Value = "Storage Value";
                ddListValue.Cells["B1"].Value = "Brand Value";
                ddListValue.Cells["C1"].Value = "Vendor Value";
                ddListValue.Cells["D1"].Value = "Brand Category Value";

                foreach (var data in dt)
                {
                    ws.Cells[index, 1].Value = data.ItemId;
                    ws.Cells[index, 2].Value = data.ItemName;
                    ws.Cells[index, 3].Value = data.SalePrice;
                    ws.Cells[index, 4].Value = data.BuyPrice;
                    ws.Cells[index, 5].Value = data.Stock;
                    ws.Cells[index, 6].Value = data.Storage.StorageName;
                    ws.Cells[index, 7].Value = data.Brand.BrandName;
                    ws.Cells[index, 8].Value = data.Vendor.VendorName;
                    ws.Cells[index, 9].Value = data.BrandCategory.BrandCategoryName;

                    index++;
                }

                ws.Cells.AutoFitColumns();
                ws.Column(1).Hidden = true;

                fileData = p.GetAsByteArray();

            }

            return fileData;
        }
        public static UploadModel UploadBrandExcel(ApplicationDbContext db, string currentUserName, HttpPostedFileBase excelFile)
        {
            const bool isSuccess = true;
            var successCtr = 0;
            var failedCtr = 0;

            using (var excel = new ExcelPackage(excelFile.InputStream))
            {
                ExcelWorksheet workSheet = excel.Workbook.Worksheets[1];
                int totalRows = workSheet.Dimension.Rows;
                ActionBrandViewModels mActionBrand = new ActionBrandViewModels();
                

                for (var i = 2; i <= totalRows; i++)
                {

                    var id = workSheet.Cells[i, 1].Value ?? "0";

                    var brandId = Convert.ToInt32(id.ToString());

                    var brandName = workSheet.Cells[i, 2].Value;
                    var brandDescription = workSheet.Cells[i, 3].Value ?? "";

                    if (brandName == null)
                    {
                        failedCtr++;
                    }
                    else if (!db.Brand.Any(x => !x.Deleted && x.BrandId == brandId))
                    {
                        mActionBrand.BrandName = brandName.ToString();
                        mActionBrand.BrandDescription = brandDescription.ToString();

                        DashboardFunction.AddBrandsToDb(db, mActionBrand, currentUserName);

                        successCtr++;
                    }
                }
                
                
            }

            return new UploadModel
            {
                SuccessCtr = successCtr,
                FailedCtr = failedCtr,
                IsSuccess = isSuccess
            };
        }

        public static UploadModel UploadStorageExcel(ApplicationDbContext db, string currentUserName, HttpPostedFileBase excelFile)
        {
            const bool isSuccess = true;
            var successCtr = 0;
            var failedCtr = 0;

            using (var excel = new ExcelPackage(excelFile.InputStream))
            {
                ExcelWorksheet workSheet = excel.Workbook.Worksheets[1];
                int totalRows = workSheet.Dimension.Rows;
                ActionStorageViewModels mActionStorage = new ActionStorageViewModels();


                for (var i = 2; i <= totalRows; i++)
                {

                    var id = workSheet.Cells[i, 1].Value ?? "0";

                    var storageId = Convert.ToInt32(id.ToString());

                    var storageName = workSheet.Cells[i, 2].Value;
                    var storageDescription = workSheet.Cells[i, 3].Value ?? "";

                    if (storageName == null)
                    {
                        failedCtr++;
                    }
                    else if (!db.Storage.Any(x => !x.Deleted && x.StorageId == storageId))
                    {
                        mActionStorage.StorageName = storageName.ToString();
                        mActionStorage.StorageDescription = storageDescription.ToString();

                        DashboardFunction.AddStoragesToDb(db, mActionStorage, currentUserName);

                        successCtr++;
                    }
                }


            }

            return new UploadModel
            {
                SuccessCtr = successCtr,
                FailedCtr = failedCtr,
                IsSuccess = isSuccess
            };
        }
        public static UploadModel UploadVendorExcel(ApplicationDbContext db, string currentUserName, HttpPostedFileBase excelFile)
        {
            const bool isSuccess = true;
            var successCtr = 0;
            var failedCtr = 0;

            using (var excel = new ExcelPackage(excelFile.InputStream))
            {
                ExcelWorksheet workSheet = excel.Workbook.Worksheets[1];
                int totalRows = workSheet.Dimension.Rows;
                AddVendorViewModels mActionVendor = new AddVendorViewModels();


                for (var i = 2; i <= totalRows; i++)
                {

                    var id = workSheet.Cells[i, 1].Value ?? "0";

                    var vendorId = Convert.ToInt32(id.ToString());

                    var vendorName = workSheet.Cells[i, 2].Value;
                    var vendorPhone = workSheet.Cells[i, 3].Value ?? "";
                    var vendorOwner = workSheet.Cells[i, 4].Value ?? "";
                    var vendorAddress = workSheet.Cells[i, 5].Value ?? "";
                    var vendorEmail = workSheet.Cells[i, 6].Value ?? "";


                    if (vendorName == null)
                    {
                        failedCtr++;
                    }
                    else if (!db.Vendor.Any(x => !x.Deleted && x.VendorId == vendorId))
                    {
                        mActionVendor.VendorName = vendorName.ToString();
                        mActionVendor.VendorPhone = vendorPhone.ToString();
                        mActionVendor.VendorOwner = vendorOwner.ToString();
                        mActionVendor.VendorAddress = vendorAddress.ToString();
                        mActionVendor.VendorEmail = vendorEmail.ToString();

                        DashboardFunction.AddVendorsToDb(db, mActionVendor, currentUserName);

                        successCtr++;
                    }
                }


            }

            return new UploadModel
            {
                SuccessCtr = successCtr,
                FailedCtr = failedCtr,
                IsSuccess = isSuccess
            };
        }
        public static UploadModel UploadItemExcel(ApplicationDbContext db, string currentUserName, HttpPostedFileBase excelFile)
        {
            const bool isSuccess = true;
            var successCtr = 0;
            var failedCtr = 0;

            using (var excel = new ExcelPackage(excelFile.InputStream))
            {
                ExcelWorksheet workSheet = excel.Workbook.Worksheets[1];
                int totalRows = workSheet.Dimension.Rows;
                ActionItemViewModels mActionItem = new ActionItemViewModels();


                for (var i = 2; i <= totalRows; i++)
                {

                    var id = workSheet.Cells[i, 1].Value ?? "0";

                    var itemId = Convert.ToInt32(id.ToString());
                    var itemName = workSheet.Cells[i, 2].Value;
                    var itemSalePrice = workSheet.Cells[i, 3].Value;
                    var itemBuyPrice = workSheet.Cells[i, 4].Value;
                    var stock = workSheet.Cells[i, 5].Value;
                    var itemStorageName = workSheet.Cells[i, 6].Value;
                    var itemBrandName = workSheet.Cells[i, 7].Value;
                    var itemVendorName = workSheet.Cells[i, 8].Value;
                    var itemBrandCategoryName = workSheet.Cells[i, 9].Value;

                    if (itemName == null || itemSalePrice == null || itemBuyPrice == null || stock == null 
                        || itemBrandName == null || itemVendorName == null || itemBrandCategoryName == null)
                    {
                        failedCtr++;
                    }
                    else if (!db.Item.Any(x => !x.Deleted && x.ItemId == itemId))
                    {
                        var mStorage = db.Storage.Single(x => !x.Deleted && x.StorageName == (string)itemStorageName);
                        var mBrand = db.Brand.Single(x => !x.Deleted && x.BrandName == (string)itemBrandName);
                        var mVendor = db.Vendor.Single(x => !x.Deleted && x.VendorName == (string)itemVendorName);
                        var mBrandCategory = db.BrandCategory.Single(x => x.BrandCategoryName == (string)itemBrandCategoryName);

                        mActionItem.ItemName = itemName.ToString();
                        mActionItem.ItemSalePrice = itemSalePrice.ToString();
                        mActionItem.ItemBuyPrice = itemBuyPrice.ToString();
                        mActionItem.ItemStock = stock.ToString();
                        mActionItem.ItemStorage = mStorage.StorageId.ToString();
                        mActionItem.ItemBrandType = mBrand.BrandId.ToString();
                        mActionItem.ItemVendor = mVendor.VendorId.ToString();
                        mActionItem.ItemBrandCategory = mBrandCategory.BrandCategoryId.ToString();

                        DashboardFunction.AddItemsToDb(db, mActionItem, currentUserName);

                        successCtr++;
                    }
                    else {
                        var crnItem = db.Item.Single(x => !x.Deleted && x.ItemId == itemId);

                        var crnStorage = db.Storage.Single(x => !x.Deleted && x.StorageName == (string)itemStorageName);
                        //var mBrand = db.Brand.Single(x => !x.Deleted && x.BrandName == (string)itemBrandName);
                        //var mVendor = db.Vendor.Single(x => !x.Deleted && x.VendorName == (string)itemVendorName);
                        //var mBrandCategory = db.BrandCategory.Single(x => x.BrandCategoryName == (string)itemBrandCategoryName);
                       
                        if (!crnItem.ItemName.Equals(itemName))
                            crnItem.ItemName = itemName.ToString();

                        if (!crnItem.SalePrice.Equals(itemSalePrice))
                            crnItem.SalePrice = int.Parse(itemSalePrice.ToString());

                        if (!crnItem.BuyPrice.Equals(itemBuyPrice))
                            crnItem.BuyPrice = int.Parse(itemBuyPrice.ToString());

                        if (!crnItem.Stock.Equals(stock))
                            crnItem.Stock = int.Parse(stock.ToString());
                        var test = "lol";
                        if (!crnStorage.StorageId.Equals(crnStorage.StorageId))
                            test = "";//crnItem.SalePrice = itemSalePrice;

                    }
                }


            }

            return new UploadModel
            {
                SuccessCtr = successCtr,
                FailedCtr = failedCtr,
                IsSuccess = isSuccess
            };
        }
    }
}


////Set the output directory to the SampleApp folder where the app is running from. 
//Helpers.Utils.OutputDir = new DirectoryInfo(@"c:\MyDir");

////create a new Excel package from the file
//using (ExcelPackage excelPackage = new ExcelPackage())
//{
//    //create an instance of the the first sheet in the loaded file
//    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("OnePos-Brand");

//    //add some data
//    //Add the headers
//    worksheet.Cells[1, 1].Value = "ID";
//    worksheet.Cells[1, 2].Value = "Product";
//    worksheet.Cells[1, 3].Value = "Quantity";
//    worksheet.Cells[1, 4].Value = "Price";
//    worksheet.Cells[1, 5].Value = "Value";

//    //Add some items...
//    worksheet.Cells["A2"].Value = 12001;
//    worksheet.Cells["B2"].Value = "Nails";
//    worksheet.Cells["C2"].Value = 37;
//    worksheet.Cells["D2"].Value = 3.99;

//    worksheet.Cells["A3"].Value = 12002;
//    worksheet.Cells["B3"].Value = "Hammer";
//    worksheet.Cells["C3"].Value = 5;
//    worksheet.Cells["D3"].Value = 12.10;

//    worksheet.Cells["A4"].Value = 12003;
//    worksheet.Cells["B4"].Value = "Saw";
//    worksheet.Cells["C4"].Value = 12;
//    worksheet.Cells["D4"].Value = 15.37;

//    //Add a formula for the value-column
//    worksheet.Cells["E2:E4"].Formula = "C2*D2";

//    //Ok now format the values;
//    using (var range = worksheet.Cells[1, 1, 1, 5])
//    {
//        range.Style.Font.Bold = true;
//        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
//        range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
//        range.Style.Font.Color.SetColor(Color.White);
//    }

//    worksheet.Cells["A5:E5"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
//    worksheet.Cells["A5:E5"].Style.Font.Bold = true;

//    worksheet.Cells[5, 3, 5, 5].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(2, 3, 4, 3).Address);
//    worksheet.Cells["C2:C5"].Style.Numberformat.Format = "#,##0";
//    worksheet.Cells["D2:E5"].Style.Numberformat.Format = "#,##0.00";

//    //Create an autofilter for the range
//    worksheet.Cells["A1:E4"].AutoFilter = true;

//    worksheet.Cells["A2:A4"].Style.Numberformat.Format = "@";   //Format as text

//    //There is actually no need to calculate, Excel will do it for you, but in some cases it might be useful. 
//    //For example if you link to this workbook from another workbook or you will open the workbook in a program that hasn't a calculation engine or 
//    //you want to use the result of a formula in your program.
//    worksheet.Calculate();

//    worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

//    // lets set the header text 
//    worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Inventory";
//    // add the page number to the footer plus the total number of pages
//    worksheet.HeaderFooter.OddFooter.RightAlignedText =
//        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
//    // add the sheet name to the footer
//    worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
//    // add the file path to the footer
//    worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

//    worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
//    worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

//    // Change the sheet view to show it in page layout mode
//    worksheet.View.PageLayoutView = true;

//    // set some document properties
//    excelPackage.Workbook.Properties.Title = "Invertory";
//    excelPackage.Workbook.Properties.Author = "Jan Källman";
//    excelPackage.Workbook.Properties.Comments = "This sample demonstrates how to create an Excel 2007 workbook using EPPlus";

//    // set some extended property values
//    excelPackage.Workbook.Properties.Company = "AdventureWorks Inc.";

//    // set some custom property values
//    excelPackage.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
//    excelPackage.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");

//    var xlFile = Helpers.Utils.GetFileInfo("sample1.xlsx");
//    // save our new workbook in the output directory and we are done!
//    excelPackage.SaveAs(xlFile);

//    return xlFile.FullName;
//}