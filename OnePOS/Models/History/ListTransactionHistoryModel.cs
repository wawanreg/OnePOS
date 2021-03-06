﻿
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace OnePOS.Models.History
{
    public class ListTransactionHistoryModel
    {
        public int BillingHeaderId { get; set; }
        public string NoInvoice { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime InvoiceDate { get; set; }
        public string BillingStatus { get; set; }
        public decimal TotalPayment { get; set; }
        public DayOfWeek DayOfWeek {
            get { return InvoiceDate.DayOfWeek; }
            set { }
        }
    }

    public class ListTransactionDetailHistoryModel
    {
        public int NoBillingDetail { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime CreateDate { get; set; }
        public string BillingStatus { get; set; }
        public decimal TotalItem { get; set; }
        public DayOfWeek DayOfWeek
        {
            get { return CreateDate.DayOfWeek; }
            set { }
        }
    }

    public class WeeklyHistory
    {
        [Column(TypeName = "DateTime2")]
        public DateTime Date { get; set; }
        public decimal TotalPayment { get; set; }
        public DayOfWeek Day {
            get { return Date.DayOfWeek; }
            set { }
        }
    }
    public class MonthlyHistory
    {
        [Column(TypeName = "DateTime2")]
        public DateTime Date { get; set; }
        public decimal TotalPayment { get; set; }
        public int Month
        {
            get { return Date.Month; }
            set { }
        }
    }

    public class ItemMonthHistory
    {
        [Column(TypeName = "DateTime2")]
        public DateTime Date { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal TotalItem { get; set; }
        public int Month
        {
            get { return Date.Month; }
            set { }
        }
    }

    public class TotalAssets
    {
        public decimal TotalAsset { get; set; }
        
    }
}