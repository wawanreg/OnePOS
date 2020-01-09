using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using OnePOS.Models;
using OnePOS.Models.Dashboard.Items;
using OnePOS.Models.Invoice;

namespace OnePOS.FunctionController
{
    public class InvoiceControllerServices
    {
        public static List<string> romanNumerals = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
        public static List<int> numerals = new List<int>() { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

        public static string ToRomanNumeral(int number)
        {
            var romanNumeral = String.Empty;
            while (number > 0)
            {
                // find biggest numeral that is less than equal to number
                var index = numerals.FindIndex(x => x <= number);
                // subtract it's value from your number
                number -= numerals[index];
                // tack it onto the end of your roman numeral
                romanNumeral += romanNumerals[index];
            }
            return romanNumeral;
        }
        public static BillingHeaderModel GetBillingHeader(ApplicationDbContext db, int billingId)
        {
            return db.BillingHeader.Single(x=> !x.Deleted && x.NoBillingHeader == billingId);
        }
        public static int GenerateInvoice(ApplicationDbContext db, TransactionViewModels mTransaction, string userName)
        {

            var arrTransactionItemId = mTransaction.ItemId.Split('|');
            var arrTransactionItemQty = mTransaction.Quantity.Split('|');
            var arrDiscountPerItem = mTransaction.DiscountPerItems.Split('|');

            BillingHeaderModel mBillingHeader = new BillingHeaderModel
            {
                InvoiceDate = DateTime.UtcNow,
                TotalItem = mTransaction.TotalTransaction,
                TotalAfterTax = mTransaction.TotalPayment,// not clear enough
                DiscontTransaction = mTransaction.DiscountTotalTransaction,//not clear enough
                TotalBeforeDiscount = mTransaction.TotalPayment,//not clear enough
                //TotalTransaction = mTransaction.TotalTransaction,
                TotalPayment = mTransaction.TotalPayment,
                ReduceInvoiceValue = mTransaction.ReduceInvoiceValue,
                Active = true,
                CreatedBy = userName,
                UpdatedBy = userName,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Tax = 0,
                BillingStatus = db.BillingStatus.Single(x => x.BillingName == "Paid")
            };
            db.BillingHeader.Add(mBillingHeader);
            db.SaveChanges();


            var billingData = db.BillingHeader.FirstOrDefault(x => x.CreatedDate.Day == DateTime.UtcNow.Day
                && x.CreatedDate.Month == DateTime.UtcNow.Month
                && x.CreatedDate.Year == DateTime.UtcNow.Year
                && x.NoInvoice == null);

            if (billingData != null && billingData.NoInvoice == null)
            {
                billingData.NoInvoice = billingData.NoBillingHeader + "/OP/" + ToRomanNumeral(billingData.InvoiceDate.Day) + "/" + ToRomanNumeral(billingData.InvoiceDate.Month) + "/" + ToRomanNumeral(Int32.Parse(billingData.InvoiceDate.ToString("yy")));
                db.Entry(billingData).State = EntityState.Modified;
                db.SaveChanges();
            }

            for (var i = 0; i < arrTransactionItemId.Length; i++)
            {
                var tempId = int.Parse(arrTransactionItemId[i]);
                var itemViewModel = DashboardFunction.GetItemViewModels(db, tempId);

                var mBillingDetail = new BillingDetailModel()
                {
                    Item = itemViewModel,
                    BillingHeader = billingData,
                    CreatedBy = userName,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = userName,
                    UpdatedDate = DateTime.UtcNow,
                    Quantity = int.Parse(arrTransactionItemQty[i]),
                    DiscontPerItems = int.Parse(arrDiscountPerItem[i])
                };
                db.BillingDetail.Add(mBillingDetail);
                db.SaveChanges();

                //change stock data
                ActionItemViewModels actionItemView = new ActionItemViewModels
                {
                    ItemVendor = itemViewModel.Vendor.VendorId.ToString(),
                    ItemBrandType = itemViewModel.Brand.BrandId.ToString(),
                    ItemBrandCategory = itemViewModel.BrandCategory.BrandCategoryId.ToString(),
                    ItemStorage = itemViewModel.Storage.StorageId.ToString(),
                    ItemName = itemViewModel.ItemName,
                    ItemStock = (itemViewModel.Stock - mBillingDetail.Quantity).ToString(),
                    ItemBuyPrice = itemViewModel.BuyPrice.ToString(),
                    ItemSalePrice = itemViewModel.SalePrice.ToString()
                };
                DashboardFunction.EditItemViewModels(db, userName, actionItemView, tempId);

            }

            if (billingData != null) return billingData.NoBillingHeader;
            return 0;
        }
        public static BillingCollectionModel SetBillingCollection(ApplicationDbContext db, int billingId)
        {
            BillingHeaderModel mBillingHeader = GetBillingHeader(db, billingId);
            //var billingDetails = mBillingHeader.BillingDetails.Where(x => !x.Deleted);
            List<BillingDetailCollectionModel> mBillingDetailCollectionModels =
                
                mBillingHeader.BillingDetails.Where(x=> !x.Deleted).Select(x => new BillingDetailCollectionModel
                {
                    ItemId = x.Item.ItemId,
                    ItemName = x.Item.ItemName,
                    BuyQuantity = x.Quantity,
                    OriginalQuantity = x.Quantity,
                    PricePerItem = x.Item.SalePrice,
                    DiscountPerItem = x.DiscontPerItems,
                    BillingDetailId = x.NoBillingDetail
                }).ToList();

            BillingCollectionModel mBillCollectionModel = new BillingCollectionModel
            {
                DetailCollections = mBillingDetailCollectionModels,
                MaxStorageItm = mBillingHeader.BillingDetails.Where(x => !x.Deleted).Select(x => x.Item).Select(x=> x.Stock).ToList(),
                NoInvoice = mBillingHeader.NoInvoice,
                NoBillingHeader = mBillingHeader.NoBillingHeader,
                BillingStatus = mBillingHeader.BillingStatus,
                InvoiceDate = mBillingHeader.InvoiceDate,
                TotalItem = mBillingHeader.TotalItem,
                BillStatusDropdownLists = new SelectList(db.BillingStatus.ToList(), "BillingStatusId", "BillingName", mBillingHeader.BillingStatus.BillingStatusId),
                IsDeleted = mBillingHeader.Deleted,
                DiscontTransaction = mBillingHeader.DiscontTransaction,
                PaymentTax = mBillingHeader.PaymentTax,
                ReduceInvoiceValue = mBillingHeader.ReduceInvoiceValue
            };
            return mBillCollectionModel;
        }
        public static void EditInvoice(ApplicationDbContext db, string currentUserName, int billingId, BillingCollectionModel mBillingCollectionModel)
        {
            BillingHeaderModel mBillingHeader =  GetBillingHeader(db, billingId);
            
            if (mBillingCollectionModel.BillingStatus != null)
            {
                var mBillingStatus = db.BillingStatus.Find(mBillingCollectionModel.BillingStatus.BillingStatusId);
                mBillingHeader.BillingStatus = mBillingStatus;
            }

            mBillingHeader.TotalPayment = mBillingCollectionModel.TotalPayment;
            mBillingHeader.ReduceInvoiceValue = mBillingCollectionModel.ReduceInvoiceValue;
            mBillingHeader.TotalAfterTax = mBillingCollectionModel.TotalPayment;
            mBillingHeader.UpdatedBy = currentUserName;
            mBillingHeader.UpdatedDate = DateTime.UtcNow;
            mBillingHeader.TotalItem = mBillingCollectionModel.DetailCollections.Sum(x => x.BuyQuantity);
            mBillingHeader.DiscontTransaction = mBillingCollectionModel.DiscontTransaction;

            //    TotalAfterTax = mTransaction.TotalPayment,// not clear enough
            //    TotalBeforeDiscount = mTransaction.TotalPayment,//not clear enough

            //check for billing detail
            foreach (var currentBillingDetail in mBillingCollectionModel.DetailCollections)
            {
                
                BillingDetailCollectionModel detail = currentBillingDetail;
               
                BillingDetailModel mBillingDetail = db.BillingDetail.Single(x => x.NoBillingDetail == detail.BillingDetailId);
                
                ItemViewModels mItem = db.Item.Single(x => !x.Deleted && x.ItemId == detail.ItemId);

                
                /////Item
                if (detail.BuyQuantity != mBillingDetail.Quantity)
                {
                    if (detail.BuyQuantity == 0)
                    {
                        mBillingDetail.Deleted = true;
                        mItem.Stock += mBillingDetail.Quantity;
                    }
                    else if (detail.BuyQuantity > mBillingDetail.Quantity)
                    {
                        mItem.Stock -= detail.BuyQuantity;
                    }
                    else
                    {
                        mItem.Stock += detail.BuyQuantity;
                    }
                    
                    mItem.UpdatedBy = currentUserName;
                    mItem.UpdatedDate = DateTime.UtcNow;

                    db.Entry(mItem).State = EntityState.Modified;
                    db.SaveChanges();
                }

                mBillingDetail.DiscontPerItems = detail.DiscountPerItem;
                mBillingDetail.UpdatedBy = currentUserName;
                mBillingDetail.UpdatedDate = DateTime.UtcNow;
                
                db.Entry(mBillingDetail).State = EntityState.Modified;
                db.SaveChanges();
            }


            db.Entry(mBillingHeader).State = EntityState.Modified;
            db.SaveChanges();
        }
        public static void DeleteInvoice(ApplicationDbContext db, string currentUserName, int billingId,
            BillingHeaderModel mBillingHeaderModel)
        {
            BillingHeaderModel mBillingHeader = GetBillingHeader(db, billingId);
            mBillingHeader.Deleted = true;
            mBillingHeader.UpdatedBy = currentUserName;
            mBillingHeader.UpdatedDate = DateTime.UtcNow;
            db.Entry(mBillingHeader).State = EntityState.Modified;
            db.SaveChanges();
        }
        //public static void SendInvoice(ApplicationDbContext db, int? invoiceEmailId, int noBilling)
        //{
        //    var mBilling = db.BillingHeader.Single(x => x.NoBillingHeader == noBilling);

        //    var mInvoiceEmail = invoiceEmailId == null ? db.BillingHeader.Single(x => x.Merchant.MerchantId == mBilling.Merchant.MerchantId).Merchant.CreatedBy : 
        //        db.InvoiceEmail.Single(x => x.InvoiceEmailId == invoiceEmailId).InvoiceEmail;
            
            
            
        //    MailService mailService = MailService.GetInstance();
        //    mailService.SendEmail(mBilling, new MailModel()
        //    {
        //        FromMail = "no-reply@loyalti.com",
        //        FromName = "Loyalti Express Admin",
        //        RecipientList = new[] { mInvoiceEmail },
        //        Subject = "LoyaltiExpress Invoice"
        //    }, "invoicetemplate");


        //}

        //public static void GenerateAllInvoice(ApplicationDbContext db, int merchantId)
        //{
        //    var historyClaimModel = db.HistoryClaims.FirstOrDefault(x => x.UserCard.Card.Promo.Merchant.MerchantId == merchantId);
        //    var mMerchant = db.Merchants.Single(x => x.MerchantId == merchantId);


        //    int lowestYear =
        //        historyClaimModel.ClaimTime.Year;
        //    int maxYear = DateTime.UtcNow.Year;
        //    for (var i = lowestYear; i <= maxYear; i++)
        //    {
        //        for (var j = 1; j <= 12; j++)
        //        {
        //            var year = i;
        //            var minDate = new DateTime(year, j, 1);

        //            var currentTotalDays = DateTime.DaysInMonth(year, j);
        //            var maxDate = new DateTime(year, j, currentTotalDays).AddHours(23).AddMinutes(59).AddSeconds(59);
        //            var mPromo = db.Promos.Where(x => x.Merchant.MerchantId == mMerchant.MerchantId && x.EndDate >= minDate && x.StartDate <= maxDate).ToList();

        //            DateTime dueDate = j + 1 != 13 ? new DateTime(year, j + 1, 14) : new DateTime(year + 1, 0 + 1, 14);

        //            List<HistoryClaimModel> mHistoryClaim =
        //                db.HistoryClaims.Where(x => x.UserCard.Card.Promo.Merchant.MerchantId == merchantId && x.ClaimTime >= minDate && x.ClaimTime <= maxDate).ToList();
        //            List<HistoryRedeemModel> mHistoryRedeem =
        //                db.HistoryRedeems.Where(x => x.UserCard.Card.Promo.Merchant.MerchantId == merchantId && x.RedeemTime >= minDate && x.RedeemTime <= maxDate).ToList();


        //            if (mHistoryClaim.Count > 0 || mHistoryRedeem.Count > 0)
        //            {

        //                var mBillingHeader = new BillingHeaderModel()
        //                {
        //                    BillingStatus = db.BillingStatus.Single(x => x.BillingName == "Pending"),
        //                    MerchantEmail = mMerchant.CreatedBy,
        //                    StartTransactionDate = minDate,
        //                    EndTransactionDate = maxDate,
        //                    InvoiceDate = minDate.AddMonths(1),
        //                    NoMos = mMerchant.NoMos,
        //                    DueDate = dueDate,
        //                    TotalBeforeTax = (mMerchant.PricePerTransaction * mHistoryClaim.Count) + (mMerchant.PricePerTransaction * mHistoryRedeem.Count),
        //                    TotalTransaction = mHistoryClaim.Count + mHistoryRedeem.Count,
        //                    Tax = 10,
        //                    Merchant = mMerchant,
        //                    CreatedBy = "SYSTEM",
        //                    CreatedDate = DateTime.UtcNow,
        //                    UpdatedBy = "SYSTEM",
        //                    UpdatedDate = DateTime.UtcNow
        //                };
        //                db.BillingHeader.Add(mBillingHeader);
        //                db.SaveChanges();

        //                var mDataBillHeader = db.BillingHeader.FirstOrDefault(x => x.Merchant.MerchantId == merchantId && x.StartTransactionDate.Month == j && x.StartTransactionDate.Year == lowestYear);

        //                if (mDataBillHeader != null)
        //                {
        //                    mDataBillHeader.NoInvoice = mDataBillHeader.NoBillingHeader + "/LE/" + InvoiceControllerServices.ToRomanNumeral(mDataBillHeader.DueDate.Month) + "/" + InvoiceControllerServices.ToRomanNumeral(int.Parse(mDataBillHeader.StartTransactionDate.ToString("yy")));
        //                    mDataBillHeader.PaymentTax = mDataBillHeader.TotalBeforeTax / mDataBillHeader.Tax;
        //                    mDataBillHeader.TotalAfterTax = mDataBillHeader.TotalBeforeTax + (mDataBillHeader.TotalBeforeTax / mDataBillHeader.Tax);

        //                    db.Entry(mDataBillHeader).State = EntityState.Modified;
        //                    db.SaveChanges();

        //                    foreach (var promoModel in mPromo)
        //                    {
        //                        var transactionPromoClaimCount = mHistoryClaim.Count(x => x.UserCard.Card.Promo.PromoId == promoModel.PromoId);
        //                        var transactionPromoRedeemCount = mHistoryRedeem.Count(x => x.UserCard.Card.Promo.PromoId == promoModel.PromoId);
        //                        var totalCard = db.UserCards.Count(x => x.Card.Promo.PromoId == promoModel.PromoId && x.JoinDate >= minDate && x.JoinDate <= maxDate);

        //                        var mBillingDetail = new BillingDetailModel()
        //                        {
        //                            Promo = promoModel,
        //                            BillingHeader = mDataBillHeader,
        //                            //RemainingPromoTime = promoModel.EndDate - mD
        //                            TotalPayment = (mMerchant.PricePerTransaction * transactionPromoClaimCount) + (mMerchant.PricePerTransaction * transactionPromoRedeemCount),
        //                            TotalTransaction = transactionPromoClaimCount + transactionPromoRedeemCount,
        //                            TotalClaim = transactionPromoClaimCount,
        //                            TotalRedeem = transactionPromoRedeemCount,
        //                            TotalCard = totalCard,
        //                            CreatedBy = "SYSTEM",
        //                            CreatedDate = DateTime.UtcNow,
        //                            UpdatedBy = "SYSTEM",
        //                            UpdatedDate = DateTime.UtcNow,
        //                        };
        //                        db.BillingDetail.Add(mBillingDetail);
        //                        db.SaveChanges();
        //                    }

        //                }


        //            }
        //        }

        //    }
        //}

        //public static bool GenerateEmailInvoiceMerchant(ApplicationDbContext db, int merchantId)
        //{
            
        //    try
        //    {
        //        var mMerchant = db.Merchants.Single(x => x.MerchantId == merchantId);

        //        var mInvoiceEmail = new InvoiceEmailModel()
        //        {
        //            InvoiceEmail = mMerchant.CreatedBy,
        //            Merchant = mMerchant,
        //            CreatedBy = "SYSTEM",
        //            CreatedDate = DateTime.UtcNow,
        //            UpdatedBy = "SYSTEM",
        //            UpdatedDate = DateTime.UtcNow
        //        };

        //        db.InvoiceEmail.Add(mInvoiceEmail);
        //        db.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //        throw;
                
        //    }

           
        //}

        //public static void SendDueDate(ApplicationDbContext db, int? invoiceEmailId, int noBilling)
        //{
            
            
        //    try
        //    {
        //        var mBilling = db.BillingHeader.Single(x => x.NoBillingHeader == noBilling);
                
        //        var mInvoiceEmail = invoiceEmailId == null ? db.BillingHeader.Single(x => x.Merchant.MerchantId == mBilling.Merchant.MerchantId).Merchant.CreatedBy :
        //            db.InvoiceEmail.Single(x => x.InvoiceEmailId == invoiceEmailId).InvoiceEmail;

        //        var mStatusBilling = db.BillingStatus.Single(x => x.BillingName == "Late");

        //        mBilling.BillingStatus = mStatusBilling;

        //        db.Entry(mBilling).State = EntityState.Modified;
        //        db.SaveChanges();


        //        MailService mailService = MailService.GetInstance();
        //        mailService.SendEmail(mBilling, new MailModel()
        //        {
        //            FromMail = "no-reply@loyalti.com",
        //            FromName = "Loyalti Express Admin",
        //            RecipientList = new[] { mInvoiceEmail },
        //            Subject = "LoyaltiExpress Duedate"
        //        }, "duedatetemplate");

        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
            
        //}
    }
}