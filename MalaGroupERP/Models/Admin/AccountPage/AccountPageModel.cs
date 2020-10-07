using MalaGroupERP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using System.IO;
using OfficeOpenXml;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web.Mvc;

namespace MalaGroupERP.Models
{
    public class AccountPageModel
    {
        public long AccountID { get; set; }
        public string AccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> AccountOwner { get; set; }
        public Nullable<int> OldAccountOwner { get; set; }
        public string AccountOwnerName { get; set; }
        public Nullable<int> ParentAccount { get; set; }
        public Nullable<int> Industry { get; set; }
        public Nullable<int> Employee { get; set; }
        public string Description { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string BillingCountry { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        public string ShippingCountry { get; set; }
        public string Password { get; set; }
        public string PinNo { get; set; }
        public int RowDisplay { get; set; }
        public int PageNumber { get; set; }
        public string AccountEmail { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<int> OldCreatedById { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<int> LastModifiedById { get; set; }
        public Nullable<int> OldLastModifiedById { get; set; }
        public Nullable<int> PersonContactId { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public string SalesDate { get; set; }
        public int SRNO { get; set; }
        public string Decal { get; set; }
        public string GPSSKN { get; set; }
        public string GPSDN { get; set; }
        public long AOID { get; set; }
        public long LeadID { get; set; }
        public int FileUploadCount { get; set; }
        public string CreatedByText { get; set; }
        public string LastModifiedByIdText { get; set; }
        public string LastModifiedDateText { get; set; }
        public string CreatedDateText { get; set; }
        public int AccountStatus { get; set; }
        public string Package { get; set; }
        public string TotalCost { get; set; }
        public string Language { get; set; }
        public string AccountStatusText { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardType { get; set; }
        public string CardExpirationYear { get; set; }
        public string CardNumber { get; set; }
        public int SearchOption { get; set; }
        public string CardSecurityCode { get; set; }
        public int VehicleMake { get; set; }
        public string VehicleYear { get; set; }
        public string VINNO { get; set; }
        public string LicensePlate { get; set; }
        public int DecalCount { get; set; }
        public int TakeOffList { get; set; }
        public string ListCode { get; set; }
        public string ListCode2 { get; set; }
        public string SecondaryPhone { get; set; }
        public string Warranty { get; set; }
        public string AccountCancellationDate { get; set; }
        public List<AuditTrail> AuditTrailList { get; set; }
        public string UpDateDetail { get; set; }
        public string UpdateDate { get; set; }
        public string UserName { get; set; }
        public string CloseDate { get; set; }
        public string RenewalDate { get; set; }
        public int CreatedSale { get; set; }
        public class CardCheckInfo
        {
            public long CCID { get; set; }
            public long AOID { get; set; }
            public long LeadID { get; set; }
            public string PaymentMethod { get; set; }
            public string CardDetails { get; set; }
            public string CardExpirationMonth { get; set; }
            public string CardType { get; set; }
            public string CardExpirationYear { get; set; }
            public string CardNumber { get; set; }

            public string CardNumberOriginal { get; set; }
            public string CheckNumber { get; set; }
            public string CardSecurityCode { get; set; }
            public string Token { get; set; }
            public string CardLast { get; set; }
            public string CompType { get; set; }
            public int? IsDefault { get; set; }
        }
        public class DecalData
        {

            public long AOID { get; set; }

            public int? AddDecals { get; set; }
            public int? IdentityTheft { get; set; }
            public int? NoOfVehicle { get; set; }
            public int? NoOfDecals { get; set; }
            public string VehicleMakeText { get; set; }

            public string VehicleTypeText { get; set; }


            public string VehicleYear { get; set; }
            public string VINNO { get; set; }
            public string LicensePlate { get; set; }
            public string DealerShip { get; set; }
            public string FinanceCompany { get; set; }

            public string Decal1 { get; set; }
            public string GPSSKN1 { get; set; }
            public string GPSDN1 { get; set; }

            public int VehicleMake1 { get; set; }
            public string VehicleYear1 { get; set; }
            public string VINNNO1 { get; set; }
            public string LicensePlate1 { get; set; }

            public string Decal2 { get; set; }
            public string GPSSKN2 { get; set; }
            public string GPSDN2 { get; set; }

            public int VehicleMake2 { get; set; }
            public string VehicleYear2 { get; set; }
            public string VINNNO2 { get; set; }
            public string LicensePlate2 { get; set; }

            public string Decal3 { get; set; }
            public string GPSSKN3 { get; set; }
            public string GPSDN3 { get; set; }

            public int VehicleMake3 { get; set; }
            public string VehicleYear3 { get; set; }
            public string VINNNO3 { get; set; }
            public string LicensePlate3 { get; set; }


            public int VehicleMake4 { get; set; }
            public string VehicleYear4 { get; set; }
            public string VINNNO4 { get; set; }
            public string LicensePlate4 { get; set; }

            public string Decal4 { get; set; }
            public string GPSSKN4 { get; set; }
            public string GPSDN4 { get; set; }

            public string NumberOfRows { get; set; }
        }

        public class TransactionHistory
        {
            public long AccountID { get; set; }
            public long AOID { get; set; }
            public long CDID { get; set; }
            public int PaymentMethod { get; set; }
            public string PaymentType { get; set; }
            public string CardCheckNumber { get; set; }
            public string UserName { get; set; }
            public string ChargeDate { get; set; }
            public string TransactionID { get; set; }
            public string AuthCode { get; set; }
            public string ChargeAmt { get; set; }
            public string TotalAmount { get; set; }
            public string BalanceAmount { get; set; }
            public string PaidAmount { get; set; }
            public int? ChargeNo { get; set; }
            public string Status { get; set; }
            public string TransType { get; set; }
            public string StatusText { get; set; }
            public string IsTransDone { get; set; }
            public string PackageDetails { get; set; }

            public int? RecurringRefund { get; set; }
            public int? PackageId { get; set; }
            public long LeadID { get; set; }
            public string PinNo { get; set; }
            public string CardType { get; set; }
        }
        public AccountPageModel GetAccountsInfo(long AccountID)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            AccountPageModel model = new AccountPageModel();

            model.AccountID = 0;
            model.AccountName = "";
            model.Website = "";
            model.Phone = "";

            model.Type = 0;
            model.AccountOwner = 0;
            model.Industry = 0;
            model.ParentAccount = 0;
            model.Description = "";
            model.BillingAddress = "";
            model.BillingCity = "";
            model.BillingState = "";
            model.BillingZip = "";
            model.BillingCountry = "";
            model.ShippingAddress = "";
            model.ShippingCity = "";
            model.ShippingState = "";
            model.ShippingZip = "";
            model.ShippingCountry = "";
            model.PinNo = "";
            model.Password = "";
            model.FileUploadCount = 0;
            model.AccountEmail = "";
            model.CreatedByText = "";
            model.LastModifiedByIdText = "";
            model.LastModifiedDateText = null;
            model.CreatedDateText = null;
            model.AccountStatus = 0;
            model.SecondaryPhone = "";
            model.ListCode = "";
            model.ListCode2 = "";
            model.Warranty = "";
            model.AccountCancellationDate = "";
            model.AccountEmail = "";
            model.CreatedById = 0;
            model.LastModifiedById = 0;

            if (AccountID > 0)
            {
                DataSet dsDataSet = new DataSet();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetAccountInfoData";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter AID = cmd.CreateParameter();
                        AID.ParameterName = "AccountID";
                        AID.Value = AccountID;
                        cmd.Parameters.Add(AID);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsDataSet);
                        db.Database.Connection.Close();
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }

                    if (dsDataSet.Tables[0] != null && dsDataSet.Tables[0].Rows.Count > 0)
                    {
                        DateTime? createdDate = null;
                        try
                        {
                            createdDate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["CreatedDate"].ToString());
                        }
                        catch
                        {

                        }
                        DateTime? lastModifiedDate = null;
                        try
                        {
                            lastModifiedDate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["LastModifiedDate"].ToString());
                        }
                        catch
                        {

                        }
                        DateTime? salesdate = null;
                        try
                        {
                            salesdate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["SalesDate"].ToString());
                        }
                        catch
                        {

                        }
                        DateTime? canceldate = null;
                        try
                        {
                            canceldate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["AccountCancellationDate"].ToString());
                        }
                        catch
                        {

                        }
                        DateTime? closedate = null;
                        try
                        {
                            closedate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["EndDate"].ToString());
                        }
                        catch
                        {

                        }
                        DateTime? renewaldate = null;
                        try
                        {
                            renewaldate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["RenewalDate"].ToString());
                        }
                        catch
                        {

                        }
                        model.AccountID = long.Parse(dsDataSet.Tables[0].Rows[0]["ID"].ToString());
                        model.AccountName = dsDataSet.Tables[0].Rows[0]["AccountName"].ToString();
                        model.AccountOwner = Int32.Parse(dsDataSet.Tables[0].Rows[0]["AccountOwner"].ToString());
                        model.AccountOwnerName = dsDataSet.Tables[0].Rows[0]["Username"].ToString();
                        model.Phone = dsDataSet.Tables[0].Rows[0]["Phone"].ToString();
                        model.Website = dsDataSet.Tables[0].Rows[0]["Website"].ToString();
                        model.Employee = Int32.Parse(dsDataSet.Tables[0].Rows[0]["Employee"].ToString());
                        model.Type = Int32.Parse(dsDataSet.Tables[0].Rows[0]["Type"].ToString());
                        model.Industry = Int32.Parse(dsDataSet.Tables[0].Rows[0]["Industry"].ToString());
                        model.ParentAccount = Int32.Parse(dsDataSet.Tables[0].Rows[0]["ParentAccount"].ToString());
                        model.Description = dsDataSet.Tables[0].Rows[0]["Description"].ToString();
                        model.Language = dsDataSet.Tables[0].Rows[0]["Language"].ToString();
                        model.TakeOffList = Int32.Parse(dsDataSet.Tables[0].Rows[0]["TakeOffList"].ToString());

                        model.BillingAddress = dsDataSet.Tables[0].Rows[0]["BillingAddress"].ToString() == "" ? dsDataSet.Tables[0].Rows[0]["ShippingAddress"].ToString() : dsDataSet.Tables[0].Rows[0]["BillingAddress"].ToString();
                        model.BillingCity = dsDataSet.Tables[0].Rows[0]["BillingCity"].ToString() == "" ? dsDataSet.Tables[0].Rows[0]["ShippingCity"].ToString() : dsDataSet.Tables[0].Rows[0]["BillingCity"].ToString();
                        model.BillingState = dsDataSet.Tables[0].Rows[0]["BillingState"].ToString() == "" ? dsDataSet.Tables[0].Rows[0]["ShippingState"].ToString() : dsDataSet.Tables[0].Rows[0]["BillingState"].ToString();
                        model.BillingZip = dsDataSet.Tables[0].Rows[0]["BillingZip"].ToString() == "" ? dsDataSet.Tables[0].Rows[0]["ShippingZip"].ToString() : dsDataSet.Tables[0].Rows[0]["BillingZip"].ToString();

                        model.BillingCountry = dsDataSet.Tables[0].Rows[0]["BillingCountry"].ToString();
                        model.ShippingAddress = dsDataSet.Tables[0].Rows[0]["ShippingAddress"].ToString();
                        model.ShippingCity = dsDataSet.Tables[0].Rows[0]["ShippingCity"].ToString();
                        model.ShippingCountry = dsDataSet.Tables[0].Rows[0]["ShippingCountry"].ToString();
                        model.ShippingState = dsDataSet.Tables[0].Rows[0]["ShippingState"].ToString();
                        model.ShippingZip = dsDataSet.Tables[0].Rows[0]["ShippingZip"].ToString();
                        model.PinNo = dsDataSet.Tables[0].Rows[0]["PinNo"].ToString();
                        model.Password = dsDataSet.Tables[0].Rows[0]["Password"].ToString();
                        model.AccountEmail = dsDataSet.Tables[0].Rows[0]["AccountEmail"].ToString();
                        model.FileUploadCount = db.tbl_AttachedFiles.Where(p => p.PageID == 2 && p.AGID == AccountID).Count();

                        model.LeadID = long.Parse(dsDataSet.Tables[0].Rows[0]["LeadID"].ToString());
                        model.FirstName = dsDataSet.Tables[0].Rows[0]["FirstName"].ToString();
                        model.LastName = dsDataSet.Tables[0].Rows[0]["LastName"].ToString();
                        model.CreatedByText = dsDataSet.Tables[0].Rows[0]["CreatedByText"].ToString();
                        model.LastModifiedByIdText = dsDataSet.Tables[0].Rows[0]["LastModifiedByIdText"].ToString();
                        model.LastModifiedDateText = (lastModifiedDate.HasValue ? lastModifiedDate.Value.ToString("MM/dd/yyyy h:mm tt") : "");
                        model.CreatedDateText = (createdDate.HasValue ? createdDate.Value.ToString("MM/dd/yyyy h:mm tt") : "");
                        model.AccountStatus = (dsDataSet.Tables[0].Rows[0]["AccountStatus"].ToString() != "" ? Int32.Parse(dsDataSet.Tables[0].Rows[0]["AccountStatus"].ToString()) : 0);
                        model.Language = dsDataSet.Tables[0].Rows[0]["Language"].ToString();
                        model.SalesDate = (salesdate.HasValue ? salesdate.Value.ToString("MM/dd/yyyy") : "");

                        model.AccountEmail = dsDataSet.Tables[0].Rows[0]["AccountEmail"].ToString();
                        model.CardNumber = dsDataSet.Tables[0].Rows[0]["CardNumber"].ToString();
                        model.CardExpirationMonth = dsDataSet.Tables[0].Rows[0]["CardExpirationMonth"].ToString();
                        model.CardExpirationYear = dsDataSet.Tables[0].Rows[0]["CardExpirationYear"].ToString();
                        model.CardType = dsDataSet.Tables[0].Rows[0]["CardType"].ToString();
                        model.CardSecurityCode = dsDataSet.Tables[0].Rows[0]["CardSecurityCode"].ToString();

                        model.SecondaryPhone = dsDataSet.Tables[0].Rows[0]["SecondaryPhone"].ToString();
                        model.ListCode = dsDataSet.Tables[0].Rows[0]["ListCode"].ToString();
                        model.ListCode2 = dsDataSet.Tables[0].Rows[0]["ListCode2"].ToString();
                        model.Warranty = dsDataSet.Tables[0].Rows[0]["Warranty"].ToString();
                        model.AccountCancellationDate = (canceldate.HasValue ? canceldate.Value.ToString("MM/dd/yyyy") : "");
                        model.CloseDate = (closedate.HasValue ? closedate.Value.ToString("MM/dd/yyyy") : "");
                        model.RenewalDate = (renewaldate.HasValue ? renewaldate.Value.ToString("MM/dd/yyyy") : "");
                        model.CreatedById = Int32.Parse(dsDataSet.Tables[0].Rows[0]["CreatedById"].ToString());
                        model.LastModifiedById = Int32.Parse(dsDataSet.Tables[0].Rows[0]["LastModifiedById"].ToString());
                    }

                }
            }
            return model;
        }

        public string SaveUpdateAccounts(AccountPageModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string accountID = "0";
            string OldEmail = "";
            string OldMobile = "";
            string OldFName = "";

            var acctUpdate = db.tbl_Accounts.Where(p => p.ID == model.AccountID).FirstOrDefault();
            if (acctUpdate != null)
            {
                OldEmail = acctUpdate.EmailId;
                OldMobile = acctUpdate.Phone;

                acctUpdate.AccountName = model.FirstName + " " + model.LastName;
                acctUpdate.Website = model.Website;
                acctUpdate.Phone = model.Phone;
                acctUpdate.Type = model.Type;
                acctUpdate.Industry = model.Industry;
                acctUpdate.Employee = model.Employee;
                acctUpdate.ParentAccount = model.ParentAccount;
                acctUpdate.AccountOwner = model.AccountOwner;
                acctUpdate.Description = model.Description;
                acctUpdate.BillingAddress = model.BillingAddress;
                acctUpdate.BillingCity = model.BillingCity;
                acctUpdate.BillingState = model.BillingState;
                acctUpdate.BillingZip = model.BillingZip;
                acctUpdate.BillingCountry = model.BillingCountry;
                acctUpdate.ShippingAddress = model.ShippingAddress;
                acctUpdate.ShippingCity = model.ShippingCity;
                acctUpdate.ShippingState = model.ShippingState;
                acctUpdate.ShippingZip = model.ShippingZip;
                acctUpdate.ShippingCountry = model.ShippingCountry;
                acctUpdate.PinNo = model.PinNo;
                acctUpdate.Password = model.Password;

                acctUpdate.CreatedDate = Convert.ToDateTime(model.CreatedDate);
                acctUpdate.CreatedById = model.CreatedById;
                if(model.LastModifiedDate==null)
                {
                    model.LastModifiedDate = DateTime.Now;
                }

                if (Convert.ToDateTime(model.LastModifiedDate) != acctUpdate.LastModifiedDate)
                {
                    acctUpdate.LastModifiedDate = Convert.ToDateTime(model.LastModifiedDate);
                }
                else
                {
                    acctUpdate.LastModifiedDate = DateTime.Now;
                }

                if (model.LastModifiedById != acctUpdate.LastModifiedById)
                {
                    acctUpdate.LastModifiedById = model.LastModifiedById;
                }
                else
                {
                    acctUpdate.LastModifiedById = MalaGroupWebSession.CurrentUser.UserID;
                }

                acctUpdate.AccountStatus = model.AccountStatus;
                if (model.RenewalDate != null)
                {
                    acctUpdate.RenewalDate = Convert.ToDateTime(model.RenewalDate);
                }

                acctUpdate.EmailId = model.AccountEmail;
                if (model.SalesDate != null)
                {
                    acctUpdate.SalesDate = Convert.ToDateTime(model.SalesDate);
                }
                if (model.AccountStatus == 1)
                {
                    acctUpdate.AccountCancellationDate = null;
                }
                else if (model.AccountStatus == 3 || model.AccountStatus == 4 || model.AccountStatus == 5)
                {
                    if (acctUpdate.AccountCancellationDate == null)
                    {
                        acctUpdate.AccountCancellationDate = DateTime.Now;
                        acctUpdate.SalesDate = DateTime.Now;
                    }
                    else
                    {
                        acctUpdate.AccountCancellationDate = Convert.ToDateTime(model.AccountCancellationDate);
                    }
                    //acctUpdate.SalesDate = DateTime.Now;
                    var adOrder = db.tbl_AgentOrder.Where(m => m.AccountID == model.AccountID && m.IsCompleted == 1).OrderByDescending(m => m.EndDate).FirstOrDefault();
                    if (adOrder != null)
                    {
                        var otherTransactions = db.tbl_OrderTransactions.Where(p => p.AOID == adOrder.OrderID && p.TransactionID == null).ToList();
                        if (otherTransactions != null)
                        {
                            db.tbl_OrderTransactions.RemoveRange(otherTransactions);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    //DateTime? dtColseDate = null;
                    //if (model.AccountCancellationDate != null)
                    //{
                    //    try
                    //    {
                    //        dtColseDate = Convert.ToDateTime(model.AccountCancellationDate);
                    //    }
                    //    catch
                    //    {
                    //        dtColseDate = null;
                    //    }
                    //}

                    //acctUpdate.AccountCancellationDate = dtColseDate;
                }

                db.SaveChanges();
                accountID = model.AccountID.ToString();

                var leadUpdate = db.tbl_LeadInformation.Where(p => p.PinNo == model.PinNo).FirstOrDefault();
                if (leadUpdate != null)
                {
                    leadUpdate.FirstName = model.FirstName;
                    leadUpdate.LastName = model.LastName;
                    leadUpdate.Language = model.Language;
                    leadUpdate.Take_Off_List__c = model.TakeOffList;
                    leadUpdate.LeadEmail = model.AccountEmail;
                    leadUpdate.SecondaryPhone = model.SecondaryPhone;
                    leadUpdate.Warranty = model.Warranty;
                    leadUpdate.Name = model.FirstName + " " + model.LastName;
                    leadUpdate.ListCode = model.ListCode;
                    leadUpdate.ListCode2 = model.ListCode2;
                    leadUpdate.LastModifiedDate = DateTime.Now;
                    leadUpdate.LastModifiedById = MalaGroupWebSession.CurrentUser.UserID.ToString();
                    if (model.AccountStatus == 1)
                    {
                        leadUpdate.Take_Off_List__c = 0;
                    }
                    db.SaveChanges();
                }


              var orderdet = db.tbl_AgentOrder.Where(p => p.AccountID == model.AccountID).ToList();
               
              foreach(var aoid in orderdet)
                {
                    var addressUpdate = db.tbl_AgentOrderDetails.Where(p => p.AOID == aoid.OrderID).FirstOrDefault();
                    addressUpdate.BStreet = model.ShippingAddress;
                    addressUpdate.BCity = model.ShippingCity;
                    addressUpdate.BState = model.ShippingState;
                    addressUpdate.BZipCode = model.ShippingZip;
                 
                    db.SaveChanges();
                }
                if (model.OldAccountOwner != model.AccountOwner)
                {
                    var ownerUpdate = new tbl_AccountOwner()
                    {
                        OldOwnerId = model.OldAccountOwner,
                        NewOwnerId = model.AccountOwner,
                        AccountID = model.AccountID,
                        UpdatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                        UpdatedBy = MalaGroupWebSession.CurrentUser.UserID,

                    };
                    db.tbl_AccountOwner.Add(ownerUpdate);
                    db.SaveChanges();
                }
                //Audit History
                //List<string> AuditTrails = new List<string>();
                //{
                //    if (OldMobile != model.Phone)
                //    {                     
                //        AuditTrails.Add("Phone Change from " + OldMobile);
                //    };
                //    if (OldEmail != model.AccountEmail)
                //    {
                //        AuditTrails.Add("Email Change from " + OldEmail);
                //    };
                //    if (acctUpdate.EmailId.ToString() != model.AccountEmail)
                //    {
                //        AuditTrails.Add("Phone Change from " + OldMobile);
                //    }; 
                //}

                //foreach (var alist in AuditTrails)
                //{
                //    var upAuditrail = new tbl_AuditTrail()
                //    {
                //        AGID = model.AccountID,
                //        AuditDetails = "",
                //        DateUpdate = DateTime.Now,
                //        PageID = 1,
                //        UpdatedByUserID = MalaGroupWebSession.CurrentUser.UserID,
                //    };
                //    db.tbl_AuditTrail.Add(upAuditrail);
                //    db.SaveChanges();
                //}
            }
            db.Dispose();
            return accountID;
        }
        public string UpdateDecal(AccountPageModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string aoID = "0";
            var decalData = db.tbl_AgentOrderDetails.Where(p => p.AOID == model.AOID).FirstOrDefault();

            if (decalData != null)
            {
                if (model.SRNO == 1)
                {
                    decalData.DecalNumber1 = model.Decal;
                    decalData.GPSSKU1 = model.GPSSKN;
                    decalData.GPSDN1 = model.GPSDN;
                    decalData.VehicleMake = model.VehicleMake;
                    decalData.VehicleYear = model.VehicleYear;
                    decalData.LicensePlate = model.LicensePlate;
                    decalData.VINNo = model.VINNO;
                    decalData.AddDecals = 0;
                }
                else if (model.SRNO == 2)
                {
                    decalData.DecalNumber2 = model.Decal;
                    decalData.GPSSKU2 = model.GPSSKN;
                    decalData.GPSDN2 = model.GPSDN;
                    decalData.VehicleMake2 = model.VehicleMake;
                    decalData.VehicleYear2 = model.VehicleYear;
                    decalData.LicensePlate2 = model.LicensePlate;
                    decalData.VINNo2 = model.VINNO;
                    decalData.AddDecals = 1;
                    aoID = decalData.AOID.ToString();
                }
                else if (model.SRNO == 3)
                {
                    decalData.DecalNumber3 = model.Decal;
                    decalData.GPSSKU3 = model.GPSSKN;
                    decalData.GPSDN3 = model.GPSDN;
                    decalData.VehicleMake3 = model.VehicleMake;
                    decalData.VehicleYear3 = model.VehicleYear;
                    decalData.LicensePlate3 = model.LicensePlate;
                    decalData.VINNo3 = model.VINNO;
                    decalData.AddDecals = 2;
                    aoID = decalData.AOID.ToString();
                }
                else if (model.SRNO == 4)
                {
                    decalData.DecalNumber4 = model.Decal;
                    decalData.GPSSKU4 = model.GPSSKN;
                    decalData.GPSDN4 = model.GPSDN;
                    decalData.VehicleMake4 = model.VehicleMake;
                    decalData.VehicleYear4 = model.VehicleYear;
                    decalData.LicensePlate4 = model.LicensePlate;
                    decalData.VINNo4 = model.VINNO;
                    decalData.AddDecals = 3;
                    aoID = decalData.AOID.ToString();
                }
                db.SaveChanges();

            }
            db.Dispose();
            return aoID;
        }
        public string UpdateDecalTheft(long AOID, int DecTheft, int DecTheftValue)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string MSG = "";
            var decalData = db.tbl_AgentOrderDetails.Where(p => p.AOID == AOID).FirstOrDefault();
            if (decalData != null)
            {
                if (DecTheft == 1)
                {
                    decalData.IdentityTheft = (DecTheftValue == 1 ? 1 : 0);
                    db.SaveChanges();
                    MSG = "Identity Theft Updated";
                }
                else
                {
                    decalData.AddDecals = (DecTheftValue == 1 ? 1 : 0);
                    db.SaveChanges();
                    MSG = "Additional Decal updated";
                }

            }
            return MSG;
        }
        public string SaveDecal(AccountPageModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string aoID = "0";
            var decalData = db.tbl_AgentOrderDetails.Where(p => p.AOID == model.AOID).FirstOrDefault();

            if (decalData != null)
            {
                if (model.DecalCount == 0)
                {
                    decalData.DecalNumber1 = model.Decal;
                    decalData.GPSSKU1 = model.GPSSKN;
                    decalData.GPSDN1 = model.GPSDN;
                    decalData.VehicleMake = model.VehicleMake;
                    decalData.VehicleYear = model.VehicleYear;
                    decalData.LicensePlate = model.LicensePlate;
                    decalData.VINNo = model.VINNO;
                    decalData.AddDecals = 0;
                    aoID = decalData.AOID.ToString();
                }
                else if (model.DecalCount == 1)
                {
                    decalData.DecalNumber2 = model.Decal;
                    decalData.GPSSKU2 = model.GPSSKN;
                    decalData.GPSDN2 = model.GPSDN;
                    decalData.VehicleMake2 = model.VehicleMake;
                    decalData.VehicleYear2 = model.VehicleYear;
                    decalData.LicensePlate2 = model.LicensePlate;
                    decalData.VINNo2 = model.VINNO;
                    decalData.AddDecals = 1;
                    aoID = decalData.AOID.ToString();
                }
                else if (model.DecalCount == 2)
                {
                    decalData.DecalNumber3 = model.Decal;
                    decalData.GPSSKU3 = model.GPSSKN;
                    decalData.GPSDN3 = model.GPSDN;
                    decalData.VehicleMake3 = model.VehicleMake;
                    decalData.VehicleYear3 = model.VehicleYear;
                    decalData.LicensePlate3 = model.LicensePlate;
                    decalData.VINNo3 = model.VINNO;
                    decalData.AddDecals = 2;
                    aoID = decalData.AOID.ToString();
                }
                else if (model.DecalCount == 3)
                {
                    decalData.DecalNumber4 = model.Decal;
                    decalData.GPSSKU4 = model.GPSSKN;
                    decalData.GPSDN4 = model.GPSDN;
                    decalData.VehicleMake4 = model.VehicleMake;
                    decalData.VehicleYear4 = model.VehicleYear;
                    decalData.LicensePlate4 = model.LicensePlate;
                    decalData.VINNo4 = model.VINNO;
                    decalData.AddDecals = 3;
                    aoID = decalData.AOID.ToString();
                }
                db.SaveChanges();

            }
            db.Dispose();
            return aoID;
        }

        public string DeleteDecal(AccountPageModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string aoID = "0";
            var decalData = db.tbl_AgentOrderDetails.Where(p => p.AOID == model.AOID).FirstOrDefault();

            if (decalData != null)
            {
                if (model.SRNO == 1)
                {
                    decalData.DecalNumber1 = "";
                    decalData.GPSSKU1 = "";
                    decalData.GPSDN1 = "";
                    decalData.VehicleMake = 0;
                    decalData.VehicleYear = "";
                    decalData.LicensePlate = "";
                    decalData.VINNo = "";
                    decalData.AddDecals = 0;
                }
                else if (model.SRNO == 2)
                {
                    decalData.DecalNumber2 = "";
                    decalData.GPSSKU2 = "";
                    decalData.GPSDN2 = "";
                    decalData.VehicleMake2 = null;
                    decalData.VehicleYear2 = "";
                    decalData.LicensePlate2 = "";
                    decalData.VINNo2 = model.VINNO;
                    decalData.AddDecals = 0;
                    aoID = decalData.AOID.ToString();
                }
                else if (model.SRNO == 3)
                {
                    decalData.DecalNumber3 = "";
                    decalData.GPSSKU3 = "";
                    decalData.GPSDN3 = "";
                    decalData.VehicleMake3 = null;
                    decalData.VehicleYear3 = "";
                    decalData.LicensePlate3 = "";
                    decalData.VINNo3 = "";
                    decalData.AddDecals = 1;
                    aoID = decalData.AOID.ToString();
                }
                else if (model.SRNO == 4)
                {
                    decalData.DecalNumber4 = "";
                    decalData.GPSSKU4 = "";
                    decalData.GPSDN4 = "";
                    decalData.VehicleMake4 = null;
                    decalData.VehicleYear4 = "";
                    decalData.LicensePlate4 = "";
                    decalData.VINNo4 = "";
                    decalData.AddDecals = 2;
                    aoID = decalData.AOID.ToString();
                }
                db.SaveChanges();

            }
            db.Dispose();
            return aoID;
        }
        public string UpdateVehicleInfo(DecalData model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string aoID = "0";
            var vehiData = db.tbl_AgentOrderDetails.Where(p => p.AOID == model.AOID).FirstOrDefault();

            if (vehiData != null)
            {
                //vehiData.VINNo = model.VINNO;
                vehiData.Dealership = model.DealerShip;
                vehiData.FinanceCompany = model.FinanceCompany;
                vehiData.LicensePlate = model.LicensePlate;
                db.SaveChanges();

            }
            db.Dispose();
            return aoID;
        }
        public string OrderToAccounts(string PinNo)
        {
            try
            {
                AgentOrderModel model = new AgentOrderModel().GetLeadInfo(PinNo); ;
                MalaGroupERPEntities db = new MalaGroupERPEntities();

                int createAccount = 0;
                string accountID = "0";
                CardModel card = new CardModel();
                int statusCode = 0;
                string statusText = "";
                string reasonText = "";
                string gatewayResponse = "";

                string filePath = HttpContext.Current.Server.MapPath("~/EmailTemplate");
                var bodyTemplate = File.ReadAllText(filePath + "\\transaction_success.html");
                var body = "";
                var emailSubject = "";
                var transUpdate = db.tbl_OrderTransactions.Where(p => p.AOID == model.OrderID && p.ChargeNo == 1).FirstOrDefault();

                if (transUpdate != null)
                {
                    var ccDetails = db.tbl_OrderCardCheckInfo.Where(p => p.AOID == model.OrderID).FirstOrDefault();
                    if (ccDetails != null)
                    {
                        if (ccDetails.PaymentMode == 1)
                        {
                            try
                            {
                                card.CardNumber = ccDetails.CardNumber;
                                card.ExpirationDate = ccDetails.CardExpirationMonth + ccDetails.CardExpirationYear;
                                card.CardCode = ccDetails.CardSecurityCode;
                                card.Amount = transUpdate.ChargeAmt.Value;
                                card.OrderID = model.OrderID.ToString();
                                card.AOModel = model;

                                createTransactionResponse response = new PaymentTransactionModel().ChargeCreditCard(card);

                                if (response != null)
                                {
                                    if (response.transactionResponse.messages != null)
                                    {
                                        if (response.transactionResponse.messages[0].code == "1")
                                        {
                                            statusCode = 1;
                                        }
                                        else
                                        {
                                            statusCode = 2;
                                        }
                                        statusText = response.transactionResponse.messages[0].code == "1" ? "Approved" : "Declined";
                                        reasonText = response.transactionResponse.messages[0].description;
                                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                                    }
                                    else if (response.transactionResponse.errors != null)
                                    {
                                        if (response.transactionResponse.errors[0].errorCode == "1")
                                        {
                                            statusCode = 1;
                                        }
                                        else
                                        {
                                            statusCode = 2;
                                        }
                                        statusText = response.transactionResponse.errors[0].errorCode == "1" ? "Approved" : "Declined";
                                        reasonText = response.transactionResponse.errors[0].errorText;
                                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                                    }
                                    else
                                    {
                                        statusCode = -1;
                                        statusText = "Unknown";
                                        reasonText = "Unknown";
                                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";

                                    }

                                    if (statusCode == 1 && statusText == "Approved")
                                    {
                                        transUpdate.AuthCode = response.transactionResponse.authCode;
                                        transUpdate.TransactionID = response.transactionResponse.transId;
                                        transUpdate.TransHashCode = response.transactionResponse.transHash;
                                        transUpdate.CardCheckNumber = response.transactionResponse.accountNumber;
                                        transUpdate.PaymentMethod = 1;
                                        transUpdate.CardType = response.transactionResponse.accountType;
                                        transUpdate.Status = statusCode;
                                        transUpdate.StatusText = statusText;
                                        transUpdate.GatwayResponse = gatewayResponse;
                                        transUpdate.ChargeDate = DateTime.Now;
                                        transUpdate.TransType = 1;
                                        db.SaveChanges();

                                        card.TransactionID = response.transactionResponse.transId;

                                        var transOtherCharge = db.tbl_OrderTransactions.Where(p => p.AOID == model.OrderID && p.TransactionID == null).ToList();

                                        decimal todaysCharge = 0;
                                        decimal balanceCharge = 0;

                                        todaysCharge = transUpdate.ChargeAmt.Value;
                                        foreach (var toc in transOtherCharge)
                                        {
                                            balanceCharge += toc.ChargeAmt.Value;
                                            toc.PaymentMethod = 1;
                                            db.SaveChanges();
                                        }
                                        if (response.transactionResponse.messages[0].code == "1" && string.IsNullOrWhiteSpace(card.AOModel.LeadEmail) == false)
                                        {
                                            body = bodyTemplate;
                                            body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now registered into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services.");
                                            body = body.Replace("{AccountFullName}", card.AOModel.FirstName + " " + card.AOModel.LastName);
                                            body = body.Replace("{BillingAddress}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Street : card.AOModel.BStreet));
                                            body = body.Replace("{BillingCity}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.City : card.AOModel.BCity));
                                            body = body.Replace("{BillingState}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.State : card.AOModel.BState));
                                            body = body.Replace("{BillingZip}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Zip : card.AOModel.BZip));
                                            body = body.Replace("{CustomerEmail}", card.AOModel.LeadEmail);
                                            body = body.Replace("{PrimaryPhone}", card.AOModel.PrimaryPhone);

                                            body = body.Replace("{ShippingAddress}", card.AOModel.Street);
                                            body = body.Replace("{ShippingCity}", card.AOModel.City);
                                            body = body.Replace("{ShippingState}", card.AOModel.State);
                                            body = body.Replace("{ShippingZip}", card.AOModel.Zip);

                                            body = body.Replace("{TodaysChargeAmount}", card.Amount.ToString("0.00"));
                                            body = body.Replace("{BalanceDue}", balanceCharge.ToString("0.00"));
                                            body = body.Replace("{ChargeDateTime}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));

                                            body = body.Replace("{TransactionID}", response.transactionResponse.transId);
                                            body = body.Replace("{CardLastDigit}", response.transactionResponse.accountNumber);
                                            body = body.Replace("{TransactionType}", "Charge");
                                            body = body.Replace("{TransactionTypeHeader}", "CHARGE");
                                            body = body.Replace("{AuthCode}", response.transactionResponse.authCode);

                                            emailSubject = "Transaction CHARGE from National Theft Search and Recovery for " + todaysCharge.ToString("0.00") + " (USD)";
                                        }
                                        else
                                        {
                                            body = "";
                                        }


                                        createAccount = 1;
                                    }
                                    else
                                    {
                                        createAccount = 0;
                                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                                        return accountID + "|" + gatewayResponse + "|Transaction Failed..";
                                    }
                                }
                                else
                                {
                                    createAccount = 0;
                                    gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                                    return accountID + "|" + gatewayResponse + "|Transaction Failed..";
                                }
                            }
                            catch (Exception ex)
                            {
                                new CommonModel().LogAgentOrder("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n" + ex.StackTrace + "\r\n==============================\r\n");
                            }
                        }
                        else if (ccDetails.PaymentMode == 2)
                        {
                            createAccount = 2;
                            gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|Pending|" + "Check Pending" + "|0";

                            card.AOModel = model;
                            transUpdate.AuthCode = "";
                            transUpdate.TransactionID = "";
                            transUpdate.TransHashCode = "";
                            transUpdate.CardCheckNumber = ccDetails.CheckNo;
                            transUpdate.PaymentMethod = 2;
                            transUpdate.CardType = "";
                            //transUpdate.Status = 1;
                            transUpdate.TransType = 6;
                            transUpdate.StatusText = "Check Pending";
                            transUpdate.GatwayResponse = gatewayResponse;
                            transUpdate.ChargeDate = DateTime.Now;
                            db.SaveChanges();
                        }
                        else
                        {
                            createAccount = 3;
                            gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|Pending|" + "Comp Pending" + "|0";

                            card.AOModel = model;
                            transUpdate.AuthCode = "";
                            transUpdate.TransactionID = "";
                            transUpdate.TransHashCode = "";
                            transUpdate.CardCheckNumber = "";
                            transUpdate.PaymentMethod = 3;
                            transUpdate.CardType = "";
                            //transUpdate.Status = 1;
                            transUpdate.TransType = 7;
                            transUpdate.StatusText = "Comp Pending";
                            transUpdate.GatwayResponse = gatewayResponse;
                            transUpdate.ChargeDate = DateTime.Now;
                            db.SaveChanges();
                        }
                    }

                    var leadInfo = db.tbl_LeadInformation.Where(p => p.LeadID == model.LeadID).FirstOrDefault();
                    if (createAccount == 1)
                    {
                        var acctExist = db.tbl_Accounts.Where(p => p.PinNo == model.PinNo || p.Password == model.PinNo).FirstOrDefault();
                        if (acctExist == null)
                        {
                            var acctSave = new tbl_Accounts()
                            {
                                AccountName = model.CompanyName,
                                Phone = model.PrimaryPhone,
                                AccountOwner = model.AgentID,

                                BillingAddress = model.BStreet,
                                BillingCity = model.BCity,
                                BillingState = model.BState,
                                BillingZip = model.BZip,
                                BillingCountry = model.Country,
                                ShippingAddress = model.Street,
                                ShippingCity = model.City,
                                ShippingCountry = model.Country,
                                ShippingState = model.State,
                                ShippingZip = model.Zip,
                                PinNo = model.PinNo,
                                Password = model.Password,
                                LeadID = model.LeadID,
                                AccountStatus = (model.PaymentMethod == 1) ? 1 : (model.PaymentMethod == 2) ? 7 : 8,
                                CreatedDate = DateTime.Now,
                                CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                                LastModifiedDate = DateTime.Now,
                                LastModifiedById = MalaGroupWebSession.CurrentUser.UserID,
                                IsDeleted = 0,
                                Type = model.CompType,
                                SalesDate = DateTime.Now,
                            };
                            db.tbl_Accounts.Add(acctSave);
                            db.SaveChanges();
                            model.AccountID = acctSave.ID;
                            accountID = acctSave.ID.ToString();

                            var chatinfo = db.tbl_Chattter.Where(p => p.LeadID == model.LeadID).ToList();
                            if (chatinfo != null)
                            {
                                foreach (var toc in chatinfo)
                                {
                                    toc.AccountId = accountID;
                                    db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            accountID = acctExist.ID.ToString();
                            acctExist.AccountStatus = (model.PaymentMethod == 1) ? 1 : (model.PaymentMethod == 2) ? 7 : 8;
                            acctExist.Type = model.CompType;
                            acctExist.SalesDate = DateTime.Now;
                            db.SaveChanges();

                            card.CustomerProfileID = acctExist.CustomerProfileID;
                            card.CustomerPaymentProfileID = acctExist.CustomerPaymentID;
                            card.CustomerAddressID = acctExist.CustomerAddressID;

                        }


                        var orderdetUpdate = db.tbl_AgentOrderDetails.Where(p => p.AOID == model.OrderID).FirstOrDefault();
                        var orderUpdate = db.tbl_AgentOrder.Where(p => p.OrderID == model.OrderID).FirstOrDefault();


                        if (orderUpdate != null)
                        {
                            orderUpdate.StepCompleted = 8;
                            orderUpdate.IsCompleted = 1;
                            orderUpdate.EndDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                            orderUpdate.AccountID = Convert.ToInt64(accountID);
                            db.SaveChanges();
                        }
                        if (leadInfo != null)
                        {
                            leadInfo.LeadStatus = 1;
                            db.SaveChanges();
                        }

                        if (body != "")
                        {
                            new CommonModel().SendEmailPayments(card.AOModel.LeadEmail, emailSubject, body, Convert.ToInt64(accountID));
                            body = "";
                            transUpdate.MailSend = 1;
                            db.SaveChanges();
                        }

                        db.Dispose();
                        return accountID + "|" + gatewayResponse + "|Account Created Successfully.";
                    }
                    else if (createAccount == 2)
                    {
                        //lead status update Check Pending                  
                        if (leadInfo != null)
                        {
                            leadInfo.LeadStatus = 7;
                            db.SaveChanges();
                        }
                        return "-1|" + gatewayResponse + "|Account Check Pending.";

                    }
                    else if (createAccount == 3)
                    {
                        //lead status update Comp Pending
                        if (leadInfo != null)
                        {
                            leadInfo.LeadStatus = 8;
                            db.SaveChanges();
                        }
                        return "-2|" + gatewayResponse + "|Account Comp Pending.";
                    }
                    else
                    {
                        db.Dispose();
                        return "-2|Error Occured |Account Not Created...";
                    }
                }
                else
                {
                    db.Dispose();
                    return accountID + "|No Transaction Found|Account Not Created...";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteAccountData(long AccountID)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            try
            {
                var accountData = db.tbl_Accounts.Where(p => p.ID == AccountID).FirstOrDefault();

                if (accountData != null)
                {
                    accountData.IsDeleted = 1;
                    db.SaveChanges();
                    msg = "Data deleted successfully.";
                }
                else
                { }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            db.Dispose();
            return msg;
        }
        public List<AccountPageModel> GetAccountsList(AccountPageModel model)
        {
            try
            {
                List<AccountPageModel> listSearch = new List<AccountPageModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        if (model.SearchOption == 1)
                        {
                            cmd.CommandText = "usp_GetAccountInfoPageList";
                            cmd.CommandType = CommandType.StoredProcedure;

                            DbParameter paramName = cmd.CreateParameter();
                            paramName.ParameterName = "AccountName";
                            paramName.Value = model.AccountName;
                            cmd.Parameters.Add(paramName);

                            DbParameter paramRD = cmd.CreateParameter();
                            paramRD.ParameterName = "RowDisplay";
                            paramRD.Value = model.RowDisplay;
                            cmd.Parameters.Add(paramRD);

                            DbParameter paramPN = cmd.CreateParameter();
                            paramPN.ParameterName = "PageNumber";
                            paramPN.Value = model.PageNumber == 0 ? 1 : model.PageNumber;
                            cmd.Parameters.Add(paramPN);
                        }
                        else
                        {
                            cmd.CommandText = "usp_GetAdvAccountInfoPageList";
                            cmd.CommandType = CommandType.StoredProcedure;

                            DbParameter paramName = cmd.CreateParameter();
                            paramName.ParameterName = "AccountName";
                            paramName.Value = model.AccountName;
                            cmd.Parameters.Add(paramName);

                            DbParameter paramRD = cmd.CreateParameter();
                            paramRD.ParameterName = "RowDisplay";
                            paramRD.Value = model.RowDisplay;
                            cmd.Parameters.Add(paramRD);

                            DbParameter paramPN = cmd.CreateParameter();
                            paramPN.ParameterName = "PageNumber";
                            paramPN.Value = model.PageNumber == 0 ? 1 : model.PageNumber;
                            cmd.Parameters.Add(paramPN);

                            DbParameter paramFromDate = cmd.CreateParameter();
                            paramFromDate.ParameterName = "CFromDate";
                            paramFromDate.Value = model.CreatedDate;
                            cmd.Parameters.Add(paramFromDate);

                            DbParameter paramToDate = cmd.CreateParameter();
                            paramToDate.ParameterName = "CToDate";
                            paramToDate.Value = model.LastModifiedDate;
                            cmd.Parameters.Add(paramToDate);

                            DbParameter paramststus = cmd.CreateParameter();
                            paramststus.ParameterName = "AccountStatus";
                            paramststus.Value = model.AccountStatus;
                            cmd.Parameters.Add(paramststus);

                            DbParameter paramcs = cmd.CreateParameter();
                            paramcs.ParameterName = "CreatedSale";
                            paramcs.Value = model.CreatedSale;
                            cmd.Parameters.Add(paramcs);
                        }
                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? createdDate = null;
                            try
                            {
                                createdDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                            }
                            catch
                            {

                            }
                            DateTime? lastModifiedDate = null;
                            try
                            {
                                lastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"].ToString());
                            }
                            catch
                            {

                            }
                            listSearch.Add(new AccountPageModel()
                            {
                                AccountID = Convert.ToInt64(dr["AccountID"].ToString()),
                                AccountName = dr["AccountName"].ToString(),
                                PinNo = dr["PinNo"].ToString(),
                                AccountStatusText = dr["AccountStatus"].ToString(),
                                Package = dr["Package"].ToString(),
                                TotalCost = dr["TotalCost"].ToString(),
                                FirstName = dr["FirstName"].ToString(),
                                CreatedDateText = createdDate.Value.ToString("MM/dd/yyyy h:mm tt"),
                                LastModifiedDateText = lastModifiedDate.Value.ToString("MM/dd/yyyy h:mm tt"),
                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }

                db.Dispose();
                return listSearch.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ImportToAccount(HttpPostedFileBase fb)
        {
            string msg = "";
            DataTable dt = new DataTable();
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            string fileName = Guid.NewGuid().ToString() + ".csv";
            fb.SaveAs(filePath + "//" + fileName);

            FileInfo file = new FileInfo(filePath + "//" + fileName);

            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_ImportCsvToAccount";
                    cmd.CommandTimeout = 3000;
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramFIRST = cmd.CreateParameter();
                    paramFIRST.ParameterName = "FilePath";
                    paramFIRST.Value = filePath + "//" + fileName;
                    cmd.Parameters.Add(paramFIRST);
                    cmd.ExecuteNonQuery();
                    db.Database.Connection.Close();
                    msg = "Data Export Successfully..!";
                }
                catch (Exception ex)
                {
                    db.Database.Connection.Close();
                    msg = ex.InnerException.Message.ToString();

                }

            }
            return msg;
        }
        public List<TransactionHistory> GetTransactionHistoryDeatil(long AccountID)
        {
            try
            {
                List<TransactionHistory> listSearch = new List<TransactionHistory>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetLastTransactionDetails";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramName = cmd.CreateParameter();
                        paramName.ParameterName = "AccountID";
                        paramName.Value = AccountID;
                        cmd.Parameters.Add(paramName);


                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? createdDate = null;
                            try
                            {
                                createdDate = Convert.ToDateTime(dr["ChargeDate"].ToString());
                            }
                            catch
                            {

                            }
                            listSearch.Add(new TransactionHistory()
                            {
                                CDID = Convert.ToInt64(dr["CDID"].ToString()),
                                ChargeDate = (createdDate.HasValue ? createdDate.Value.ToString("MM/dd/yyyy hh:mm tt") : ""),
                                TransactionID = dr["TransactionID"].ToString(),
                                AuthCode = dr["AuthCode"].ToString(),
                                ChargeAmt = dr["ChargeAmt"].ToString(),
                                PaymentMethod = Convert.ToInt32(dr["PaymentMethod"].ToString()),
                                CardCheckNumber = dr["CardCheckNumber"].ToString(),
                                PaymentType = dr["PaymentType"].ToString(),
                                TransType = dr["TransType"].ToString(),
                                CardType = dr["CardType"].ToString(),
                                StatusText = dr["Status"].ToString()
                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }

                db.Dispose();
                return listSearch.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TransactionHistory> GetTransactionDetails(long AccountID)
        {
            try
            {
                List<TransactionHistory> listSearch = new List<TransactionHistory>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetTransactionDetails";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramName = cmd.CreateParameter();
                        paramName.ParameterName = "AccountID";
                        paramName.Value = AccountID;
                        cmd.Parameters.Add(paramName);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? createdDate = null;
                            try
                            {
                                createdDate = Convert.ToDateTime(dr["ChargeDate"].ToString());
                            }
                            catch
                            {

                            }
                            listSearch.Add(new TransactionHistory()
                            {
                                AOID = Convert.ToInt64(dr["AOID"].ToString()),
                                TotalAmount = dr["TotalAmount"].ToString(),
                                BalanceAmount = dr["BalanceAmount"].ToString(),
                                ChargeDate = (createdDate.HasValue ? createdDate.Value.ToString("MM/dd/yyyy") : ""),
                                UserName = dr["UserName"].ToString(),
                                PaidAmount = dr["PaidAmount"].ToString(),
                                TransType = dr["TransType"].ToString(),
                                CardType = dr["CardType"].ToString(),
                                RecurringRefund = Convert.ToInt32(dr["RecurringRefund"].ToString()),
                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
                return listSearch.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AccountPageModel> HistoryDetail(long AccountID, long LeadID)
        {
            try
            {
                List<AccountPageModel> model = new List<AccountPageModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetHistoryDetail";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter AID = cmd.CreateParameter();
                        AID.ParameterName = "AccountID";
                        AID.Value = AccountID;
                        cmd.Parameters.Add(AID);

                        DbParameter LID = cmd.CreateParameter();
                        LID.ParameterName = "LeadID";
                        LID.Value = LeadID;
                        cmd.Parameters.Add(LID);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? uploadDate = null;
                            try
                            {
                                uploadDate = Convert.ToDateTime(dr["DateUpdate"].ToString());
                            }
                            catch
                            {

                            }
                            model.Add(new AccountPageModel()
                            {
                                UpDateDetail = dr["AuditDetails"].ToString(),
                                UpdateDate = (uploadDate.HasValue ? uploadDate.Value.ToString("MM/dd/yyyy hh:mm tt") : ""),
                                UserName = dr["UserName"].ToString(),
                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TransactionHistory> GetTransOrderDetails(long AOID)
        {
            try
            {
                List<TransactionHistory> listSearch = new List<TransactionHistory>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetTransOrderDetails";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramName = cmd.CreateParameter();
                        paramName.ParameterName = "AOID";
                        paramName.Value = AOID;
                        cmd.Parameters.Add(paramName);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? createdDate = null;
                            try
                            {
                                createdDate = Convert.ToDateTime(dr["ChargeDate"].ToString());
                            }
                            catch
                            {
                            }
                            listSearch.Add(new TransactionHistory()
                            {
                                CDID = Convert.ToInt32(dr["CDID"].ToString()),
                                ChargeNo = Convert.ToInt32(dr["ChargeNo"].ToString()),
                                ChargeDate = (createdDate.HasValue ? createdDate.Value.ToString("MM/dd/yyyy") : ""),
                                ChargeAmt = dr["ChargeAmt"].ToString(),
                                TransactionID = dr["TransactionID"].ToString(),
                                Status = dr["Status"].ToString(),
                                TransType = dr["TransType"].ToString(),
                                IsTransDone = dr["IsTransDone"].ToString()
                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }

                db.Dispose();
                return listSearch.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DecalData GetDecalInfoData(long AOID)
        {
            DecalData model = new DecalData();
            model.AOID = 0;
            model.Decal1 = "";
            model.GPSSKN1 = "";
            model.GPSDN1 = "";

            model.Decal2 = "";
            model.GPSSKN2 = "";
            model.GPSDN2 = "";

            model.Decal3 = "";
            model.GPSSKN3 = "";
            model.GPSDN3 = "";

            model.Decal4 = "";
            model.GPSSKN4 = "";
            model.GPSDN4 = "";
            model.VehicleMake1 = 0;
            model.VehicleYear1 = "";
            model.VINNNO1 = "";
            model.LicensePlate1 = "";

            model.VehicleMake2 = 0;
            model.VehicleYear2 = "";
            model.VINNNO2 = "";
            model.LicensePlate2 = "";


            model.VehicleMake3 = 0;
            model.VehicleYear3 = "";
            model.VINNNO3 = "";
            model.LicensePlate3 = "";

            model.VehicleMake4 = 0;
            model.VehicleYear4 = "";
            model.VINNNO4 = "";
            model.LicensePlate4 = "";
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataSet dsDataSet = new DataSet();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetDecalInfoData";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter AID = cmd.CreateParameter();
                        AID.ParameterName = "AOID";
                        AID.Value = AOID;
                        cmd.Parameters.Add(AID);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsDataSet);
                        db.Database.Connection.Close();
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }

                    if (dsDataSet.Tables[0] != null && dsDataSet.Tables[0].Rows.Count > 0)
                    {
                        model.AOID = long.Parse(dsDataSet.Tables[0].Rows[0]["AOID"].ToString());
                        model.Decal1 = dsDataSet.Tables[0].Rows[0]["DecalNumber1"].ToString();
                        model.GPSSKN1 = dsDataSet.Tables[0].Rows[0]["GPSSKU1"].ToString();
                        model.GPSDN1 = dsDataSet.Tables[0].Rows[0]["GPSDN1"].ToString();

                        model.Decal2 = dsDataSet.Tables[0].Rows[0]["DecalNumber2"].ToString();
                        model.GPSSKN2 = dsDataSet.Tables[0].Rows[0]["GPSSKU2"].ToString();
                        model.GPSDN2 = dsDataSet.Tables[0].Rows[0]["GPSDN2"].ToString();

                        model.Decal3 = dsDataSet.Tables[0].Rows[0]["DecalNumber3"].ToString();
                        model.GPSSKN3 = dsDataSet.Tables[0].Rows[0]["GPSSKU3"].ToString();
                        model.GPSDN3 = dsDataSet.Tables[0].Rows[0]["GPSDN3"].ToString();

                        model.Decal4 = dsDataSet.Tables[0].Rows[0]["DecalNumber4"].ToString();
                        model.GPSSKN4 = dsDataSet.Tables[0].Rows[0]["GPSSKU4"].ToString();
                        model.GPSDN4 = dsDataSet.Tables[0].Rows[0]["GPSDN4"].ToString();


                        model.VehicleMake1 = Convert.ToInt32(dsDataSet.Tables[0].Rows[0]["VehicleMake"].ToString());
                        model.VehicleYear1 = dsDataSet.Tables[0].Rows[0]["VehicleYear"].ToString();
                        model.VINNNO1 = dsDataSet.Tables[0].Rows[0]["VINNo"].ToString();
                        model.LicensePlate1 = dsDataSet.Tables[0].Rows[0]["LicensePlate"].ToString();

                        model.VehicleMake2 = Convert.ToInt32(dsDataSet.Tables[0].Rows[0]["VehicleMake2"].ToString());
                        model.VehicleYear2 = dsDataSet.Tables[0].Rows[0]["VehicleYear2"].ToString();
                        model.VINNNO2 = dsDataSet.Tables[0].Rows[0]["VINNo2"].ToString();
                        model.LicensePlate2 = dsDataSet.Tables[0].Rows[0]["LicensePlate2"].ToString();


                        model.VehicleMake3 = Convert.ToInt32(dsDataSet.Tables[0].Rows[0]["VehicleMake3"].ToString());
                        model.VehicleYear3 = dsDataSet.Tables[0].Rows[0]["VehicleYear3"].ToString();
                        model.VINNNO3 = dsDataSet.Tables[0].Rows[0]["VINNo3"].ToString();
                        model.LicensePlate3 = dsDataSet.Tables[0].Rows[0]["LicensePlate3"].ToString();

                        model.VehicleMake4 = Convert.ToInt32(dsDataSet.Tables[0].Rows[0]["VehicleMake4"].ToString());
                        model.VehicleYear4 = dsDataSet.Tables[0].Rows[0]["VehicleYear4"].ToString();
                        model.VINNNO4 = dsDataSet.Tables[0].Rows[0]["VINNo4"].ToString();
                        model.LicensePlate4 = dsDataSet.Tables[0].Rows[0]["LicensePlate4"].ToString();



                    }
                }
            }
            catch (Exception ex)
            {

            }
            return model;
        }
        public List<DecalData> GetTotalOrderDecal(long AccountID)
        {
            try
            {
                List<DecalData> listSearch = new List<DecalData>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetTotalOrderDecal";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramName = cmd.CreateParameter();
                        paramName.ParameterName = "AccountID";
                        paramName.Value = AccountID;
                        cmd.Parameters.Add(paramName);


                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            listSearch.Add(new DecalData()
                            {
                                AOID = Convert.ToInt64(dr["AOID"].ToString()),
                                AddDecals = Convert.ToInt32(dr["AddDecals"].ToString()),
                                IdentityTheft = Convert.ToInt32(dr["IdentityTheft"].ToString()),
                                NoOfVehicle = Convert.ToInt32(dr["NoOfVehicle"].ToString()),

                                VehicleMakeText = dr["VehicleMakeText"].ToString(),
                                VehicleTypeText = dr["VehicleTypeText"].ToString(),

                                VehicleYear = dr["VehicleYear"].ToString(),
                                VINNO = dr["VINNo"].ToString(),
                                LicensePlate = dr["LicensePlate"].ToString(),
                                DealerShip = dr["Dealership"].ToString(),
                                FinanceCompany = dr["FinanceCompany"].ToString(),
                                NoOfDecals = Convert.ToInt32(dr["NumberOfDecals"].ToString()),
                                NumberOfRows = dr["NumberOfRows"].ToString()

                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
                return listSearch.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CardCheckInfo> GetCardCheckInfo(long AccountID)
        {
            try
            {
                List<CardCheckInfo> listSearch = new List<CardCheckInfo>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetCardCheckInfo";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramName = cmd.CreateParameter();
                        paramName.ParameterName = "AccountID";
                        paramName.Value = AccountID;
                        cmd.Parameters.Add(paramName);


                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            listSearch.Add(new CardCheckInfo()
                            {
                                //CCID = Convert.ToInt64(dr["CCID"].ToString()),
                                LeadID = Convert.ToInt64(dr["LeadID"].ToString()),
                                PaymentMethod = dr["PaymentMode"].ToString(),
                                CardType = dr["CardType"].ToString(),
                                CardNumber = dr["CardNumber"].ToString(),
                                CardNumberOriginal = dr["CardNumberOriginal"].ToString(),
                                CardSecurityCode = dr["CardSecurityCode"].ToString(),
                                CheckNumber = dr["CheckNo"].ToString(),
                                CardExpirationMonth = dr["CardExpirationMonth"].ToString(),
                                CardExpirationYear = dr["CardExpirationYear"].ToString(),
                                IsDefault = Convert.ToInt32(dr["IsDefault"].ToString()),
                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }

                db.Dispose();
                return listSearch.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string EditCardCheckDetail(int isDefault, long lid, string CCNUM, string CCCODE, string CCMM, string CCYY, string OldCardNumber, string OldCardSecCode, string OldCardMonth, string OldCardYear)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            try
            {
                if (CCNUM.Contains("*"))
                {
                    CCNUM = OldCardNumber;
                }
                if (isDefault == 1)
                {

                    var otherCC = db.tbl_OrderCardCheckInfo.Where(p => p.LeadID == lid && p.IsDefault == 1).ToList();
                    foreach (var occ in otherCC)
                    {
                        occ.IsDefault = 0;
                        db.SaveChanges();
                    }
                }

                if (OldCardNumber == "")
                {
                    var ccData = new tbl_OrderCardCheckInfo()
                    {

                        CardType = 0,
                        CardNumber = CCNUM,
                        PaymentMode = 1,
                        CardExpirationMonth = CCMM,
                        CardExpirationYear = CCYY,
                        CardSecurityCode = CCCODE,
                        //AOID = model.AOID,
                        LeadID = lid,
                        IsDefault = isDefault,
                        CheckNo = "",
                    };
                    db.tbl_OrderCardCheckInfo.Add(ccData);
                    db.SaveChanges();
                    msg = "Card Added Successfully";
                }
                else
                {
                    var ccUpData = db.tbl_OrderCardCheckInfo.Where(p => p.LeadID == lid && p.CardNumber == OldCardNumber && p.CardSecurityCode == OldCardSecCode && p.CardExpirationMonth == OldCardMonth && p.CardExpirationYear == OldCardYear).ToList();
                    foreach (var otran in ccUpData)
                    {
                        otran.CardNumber = CCNUM;
                        otran.CardSecurityCode = CCCODE;
                        otran.CardExpirationMonth = CCMM;
                        otran.CardExpirationYear = CCYY;
                        otran.IsDefault = isDefault;
                        db.SaveChanges();
                    }
                    msg = "Card Updated Successfully";
                }
                var defCC = db.tbl_OrderCardCheckInfo.Where(p => p.LeadID == lid && p.IsDefault == 1).ToList();
                if (defCC.Count == 0)
                {
                    var setdefCC = db.tbl_OrderCardCheckInfo.Where(p => p.LeadID == lid).OrderByDescending(o => o.CCID).FirstOrDefault();
                    setdefCC.IsDefault = 1;
                    db.SaveChanges();
                }
                db.Dispose();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        public string GetAccountFilterRangeList(AccountPageModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            DataTable dtTable = new DataTable();
            string PageNumber = "0";
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetAccountInfoPageListRange";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "AccountName";
                    paramName.Value = model.AccountName;
                    cmd.Parameters.Add(paramName);

                    DbParameter paramRD = cmd.CreateParameter();
                    paramRD.ParameterName = "RowDisplay";
                    paramRD.Value = model.RowDisplay;
                    cmd.Parameters.Add(paramRD);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                    PageNumber = dtTable.Rows[0]["PageNumber"].ToString() + "|" + dtTable.Rows[0]["TotalRows"].ToString();


                }
                catch
                {
                    db.Database.Connection.Close();
                }

            }
            db.Dispose();
            return PageNumber;
        }
        public string GetAccountAdvSearch(AccountPageModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            DataTable dtTable = new DataTable();
            string PageNumber = "0";
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetAdvAccountInfoPageListRange";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "AccountName";
                    paramName.Value = model.AccountName;
                    cmd.Parameters.Add(paramName);

                    DbParameter paramRD = cmd.CreateParameter();
                    paramRD.ParameterName = "RowDisplay";
                    paramRD.Value = model.RowDisplay;
                    cmd.Parameters.Add(paramRD);

                    DbParameter paramFromDate = cmd.CreateParameter();
                    paramFromDate.ParameterName = "CFromDate";
                    paramFromDate.Value = model.CreatedDate;
                    cmd.Parameters.Add(paramFromDate);

                    DbParameter paramToDate = cmd.CreateParameter();
                    paramToDate.ParameterName = "CToDate";
                    paramToDate.Value = model.LastModifiedDate;
                    cmd.Parameters.Add(paramToDate);

                    DbParameter paramststus = cmd.CreateParameter();
                    paramststus.ParameterName = "AccountStatus";
                    paramststus.Value = model.AccountStatus;
                    cmd.Parameters.Add(paramststus);

                    DbParameter paramcs = cmd.CreateParameter();
                    paramcs.ParameterName = "CreatedSale";
                    paramcs.Value = model.CreatedSale;
                    cmd.Parameters.Add(paramcs);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                    PageNumber = dtTable.Rows[0]["PageNumber"].ToString() + "|" + dtTable.Rows[0]["TotalRows"].ToString();


                }
                catch
                {
                    db.Database.Connection.Close();
                }

            }
            db.Dispose();
            return PageNumber;
        }
        public string ExportDailyMail(string ExportDate)
        {
            (new CommonModel()).DeleteFiles();
            string fileName = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            DataTable dtServiceData = new DataTable("Export Daily");
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    db.Database.CommandTimeout = 0;
                    cmd.CommandText = "usp_GetExportDailyMail";
                    cmd.CommandType = CommandType.StoredProcedure;


                    DbParameter paramWdf = cmd.CreateParameter();
                    paramWdf.ParameterName = "Date";
                    paramWdf.Value = ExportDate;
                    cmd.Parameters.Add(paramWdf);

                    cmd.CommandTimeout = 0;

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtServiceData);
                    db.Database.Connection.Close();
                }
                catch
                {
                    db.Database.Connection.Close();
                }
            }
            db.Dispose();

            var filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            fileName = Guid.NewGuid().ToString() + ".csv";
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].LoadFromDataTable(dtServiceData, true);
                FileInfo fi = new FileInfo(filePath + "/" + fileName);
                excelPackage.SaveAs(fi);
                excelPackage.Dispose();
            }
            File.SetAttributes(filePath + "/" + fileName, FileAttributes.Normal);
            byte[] buffer = null;
            using (FileStream fs = new FileStream(filePath + "/" + fileName, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                fs.Close();
            }
            fileName = Guid.NewGuid().ToString() + ".csv";
            File.WriteAllBytes(filePath + "/" + fileName, buffer);
            return fileName;
        }
        public List<DropDownModel> GetEmailTemplates()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<DropDownModel> model = new List<DropDownModel>();
            var emailTemplate = db.tbl_EmailTemplates.Where(p => p.TemplateFor == 2).ToList().OrderBy(p => p.TemplateName);
            foreach (var et in emailTemplate)
            {
                model.Add(new DropDownModel() { Value = et.TemplateID.ToString(), Text = et.TemplateName });
            }
            return model;
        }
        public void GetEmailData(long TemplateId, long AccountId, ref string EmailData, ref string EmailSubject, ref string AttachmentFile)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            var emailTemplate = db.tbl_EmailTemplates.Where(p => p.TemplateID == TemplateId).FirstOrDefault();
            EmailData = emailTemplate.TemplateHTML;
            EmailSubject = emailTemplate.TemplateSubject;
            AttachmentFile = emailTemplate.AttachmentFileName;
            DataTable dtLeadInfo = new DataTable();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetEmailData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter AGID = cmd.CreateParameter();
                    AGID.ParameterName = "AGID";
                    AGID.Value = AccountId;
                    cmd.Parameters.Add(AGID);

                    DbParameter PageID = cmd.CreateParameter();
                    PageID.ParameterName = "PageID";
                    PageID.Value = 2;
                    cmd.Parameters.Add(PageID);

                    DbParameter UID = cmd.CreateParameter();
                    UID.ParameterName = "UserID";
                    UID.Value = MalaGroupWebSession.CurrentUser.UserID;
                    cmd.Parameters.Add(UID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtLeadInfo);
                    db.Database.Connection.Close();
                }
                catch
                {
                    db.Database.Connection.Close();
                }
            }
            foreach (DataRow dr in dtLeadInfo.Rows)
            {
                foreach (DataColumn dc in dtLeadInfo.Columns)
                {
                    EmailData = EmailData.Replace("{" + dc.ColumnName.ToString() + "}", dr[dc.ColumnName].ToString());
                }
            }
        }
        public string SaveToFolder(HttpPostedFileBase fb, long LeadID)
        {
            string msg = "0";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            if (fb != null && fb.ContentLength > 0)
            {
                string filePath = HttpContext.Current.Server.MapPath("~/FileAttachments/");
                string fileName = fb.FileName;
                string sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);
                var uploadData = new tbl_AttachedFiles()
                {
                    PageID = 2,
                    AGID = LeadID,
                    SystemFileName = sysFileName,
                    OriginalFileName = fileName,
                    CreatedDate = DateTime.Now,
                    CreatedByID = MalaGroupWebSession.CurrentUser.UserID
                };
                db.tbl_AttachedFiles.Add(uploadData);
                db.SaveChanges();

                msg = db.tbl_AttachedFiles.Where(p => p.PageID == 1 && p.AGID == LeadID).Count().ToString();
            }
            return msg;
        }
        public List<AttachedFiles> GetAttachedLeadFiles(long LeadID)
        {
            try
            {
                List<AttachedFiles> listSearch = new List<AttachedFiles>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetAttachedLeadFiles";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramName = cmd.CreateParameter();
                        paramName.ParameterName = "AGID";
                        paramName.Value = LeadID;
                        cmd.Parameters.Add(paramName);

                        DbParameter paramPID = cmd.CreateParameter();
                        paramPID.ParameterName = "PageID";
                        paramPID.Value = 2;
                        cmd.Parameters.Add(paramPID);


                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? createdDate = null;
                            try
                            {
                                createdDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                            }
                            catch
                            {

                            }
                            listSearch.Add(new AttachedFiles()
                            {
                                ID = Convert.ToInt64(dr["ID"].ToString()),
                                AGID = Convert.ToInt64(dr["AGID"].ToString()),
                                SystemFileName = dr["SystemFileName"].ToString(),
                                OriginalFileName = dr["OriginalFileName"].ToString(),
                                CreatedDate = (createdDate.HasValue ? createdDate.Value.ToString("MM/dd/yyyy") : ""),
                                CreatedBy = dr["Username"].ToString()
                            });
                        }

                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
                return listSearch.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteFiles(long ID)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            try
            {

                var uData = db.tbl_AttachedFiles.Where(m => m.PageID == 2 && m.ID == ID).FirstOrDefault();
                if (uData != null)
                {
                    long? AGID = uData.AGID;
                    string filePath = HttpContext.Current.Server.MapPath("~/FileAttachments/" + uData.SystemFileName);
                    FileInfo fi = new FileInfo(filePath);
                    if (fi.Exists)
                    {
                        fi.Delete();
                    }
                    db.tbl_AttachedFiles.Remove(uData);
                    db.SaveChanges();
                    msg = db.tbl_AttachedFiles.Where(m => m.PageID == 2 && m.AGID == AGID).Count().ToString();
                    db.Dispose();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        public string SavePaymentConsoleTrans(TransactionModel model)
        {
            string transDetails = "";
            int isFullPay = 0;
            string filePath = HttpContext.Current.Server.MapPath("~/EmailTemplate");
            var body = "";
            var bodyTemplate = File.ReadAllText(filePath + "\\transaction_success.html");
            var emailSubject = "";
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                var orderExist = db.tbl_AgentOrder.Where(p => p.OrderID == model.AOID).FirstOrDefault();
                var accountInfo = db.tbl_Accounts.Where(p => p.ID == model.AccountID).FirstOrDefault();
                if (model.AOID != 0)
                {
                    isFullPay = 1;
                }

                if (orderExist == null)
                {
                    var orderSave = new tbl_AgentOrder()
                    {
                        AccountID = model.AccountID,
                        LeadID = Convert.ToInt64(model.LeadID),
                        AgentID = MalaGroupWebSession.CurrentUser.UserID,
                        StepCompleted = 8,
                        StartDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                        EndDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                        PinNo = model.PinNumber,
                        IsCompleted = 1
                    };
                    db.tbl_AgentOrder.Add(orderSave);
                    db.SaveChanges();
                    model.AOID = orderSave.OrderID;

                    var orderDetSave = new tbl_AgentOrderDetails()
                    {
                        AOID = model.AOID,
                        BCity = model.BCity,
                        BStreet = model.BStreet,
                        BZipCode = model.BZip,
                        BState = model.BState,
                        PackageAmt = model.TotalAmount,
                        TotalAmt = model.TotalAmount.ToString(),
                        CompType = 0,
                        ChargeDay = 0,
                        PackageId = model.PackageId,
                        FirstChargeDate = DateTime.Now,
                        VehicleMake = model.VehicleMake,
                        VehicleType = model.VehicleType,
                        VehicleYear = model.VehicleYear

                    };
                    db.tbl_AgentOrderDetails.Add(orderDetSave);
                    db.SaveChanges();
                }

                //if (model.DefaultCreditCard == 1)
                //{
                //    var otherCC = db.tbl_OrderCardCheckInfo.Where(p => p.LeadID == model.LeadID && p.IsDefault==1).FirstOrDefault();
                //    if(otherCC!=null)
                //    {
                //        otherCC.IsDefault = 0;
                //        db.SaveChanges();
                //    }

                //    var ccData = new tbl_OrderCardCheckInfo()
                //    {
                //        CardName = model.CardDetail,
                //        CardType = 0,
                //        CardNumber = model.CardNumber,
                //        PaymentMode = 1,
                //        CardExpirationMonth = model.CardExpirationMonth,
                //        CardExpirationYear = model.CardExpirationYear,
                //        CardSecurityCode = model.CardSecurityCode,
                //        AOID = model.AOID,
                //        LeadID = model.LeadID,
                //        IsDefault=1,
                //    };
                //    db.tbl_OrderCardCheckInfo.Add(ccData);
                //    db.SaveChanges();
                //}


                CardModel card = new CardModel();
                card.CardNumber = model.CardNumber;
                card.ExpirationDate = model.CardExpirationMonth + model.CardExpirationYear;
                card.CardCode = model.CardSecurityCode;
                card.Amount = model.TotalAmount.Value;
                card.OrderID = model.AOID.ToString();

                AgentOrderModel aoMode = new AgentOrderModel();
                aoMode.IsDiffBillingAdd = 0;
                aoMode.Street = model.BStreet;
                aoMode.City = model.BCity;
                aoMode.State = model.BState;
                aoMode.Zip = model.BZip;
                aoMode.Country = "USA";
                aoMode.PrimaryPhone = model.Phone;
                aoMode.FirstName = model.FirstName;
                aoMode.LastName = model.LastName;
                aoMode.AccountID = Convert.ToInt64(model.AccountID);
                aoMode.PinNo = model.PinNumber;
                aoMode.LeadEmail = model.EmailID;

                // aoMode.VehicleMakeText
                // aoMode.VehicleYear

                card.AOModel = aoMode;

                createTransactionResponse response = new PaymentTransactionModel().ChargeCreditCard(card);

                int statusCode = 0;
                string statusText = "";
                string reasonText = "";
                string gatewayResponse = "";

                if (response != null)
                {
                    card.TransactionID = response.transactionResponse.transId;

                    if (response.transactionResponse.messages != null)
                    {
                        if (response.transactionResponse.messages[0].code == "1")
                        {
                            statusCode = 1;
                        }
                        else
                        {
                            statusCode = 2;
                        }
                        statusText = response.transactionResponse.messages[0].code == "1" ? (isFullPay == 1 ? "Approved- Full Pay" : "Approved") : "Declined";
                        reasonText = response.transactionResponse.messages[0].description;
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                    }
                    else if (response.transactionResponse.errors != null)
                    {
                        if (response.transactionResponse.errors[0].errorCode == "1")
                        {
                            statusCode = 1;
                        }
                        else
                        {
                            statusCode = 2;
                        }
                        statusText = response.transactionResponse.errors[0].errorCode == "1" ? (isFullPay == 1 ? "Approved- Full Pay" : "Approved") : "Declined";
                        reasonText = response.transactionResponse.errors[0].errorText;
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                    }
                    else
                    {
                        statusCode = -1;
                        statusText = "Unknown";
                        reasonText = "The transaction has been failed due to unknown reason.";
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";
                    }
                    transDetails = gatewayResponse;

                    int transCount = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID).Count();
                    var transList = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID && p.TransactionID == null).ToList();
                    transCount = transCount + 1;



                    var transSave = new tbl_OrderTransactions()
                    {
                        GatwayResponse = transDetails,
                        AuthCode = response.transactionResponse.authCode,
                        TransactionID = response.transactionResponse.transId,
                        TransHashCode = response.transactionResponse.transHash,
                        CardCheckNumber = response.transactionResponse.accountNumber,
                        PaymentMethod = 1,
                        CardType = response.transactionResponse.accountType,
                        Status = statusCode,
                        StatusText = statusText,
                        ChargeAmt = model.TotalAmount,
                        RefundAmt = model.TotalAmount,
                        AOID = model.AOID,
                        ChargeDate = DateTime.Now,
                        ChargeNo = (isFullPay == 1 ? transCount + 1 : 1),
                        TransType = (isFullPay == 1 ? 6 : 1)
                    };
                    db.tbl_OrderTransactions.Add(transSave);
                    db.SaveChanges();

                    if (statusCode == 1)
                    {
                        var otherTransactions = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID && p.TransactionID == null).ToList();
                        foreach (var otran in otherTransactions)
                        {
                            otran.GatwayResponse = transDetails;
                            otran.AuthCode = "";
                            otran.TransactionID = "0";
                            otran.TransHashCode = "";
                            otran.CardCheckNumber = "";
                            otran.PaymentMethod = 1;
                            otran.CardType = "";
                            otran.Status = statusCode;
                            otran.StatusText = statusText;
                            otran.TransType = 1;
                            db.SaveChanges();
                        }
                        if (model.PackageId == 20)
                        {
                            accountInfo.RenewalDate = DateTime.Now;
                            accountInfo.SalesDate = DateTime.Now;
                        }
                        accountInfo.AccountStatus = 1;
                        if (isFullPay == 0)
                        {
                            accountInfo.SalesDate = DateTime.Now;
                        }
                        accountInfo.LastModifiedDate = DateTime.Now;
                        db.SaveChanges();
                    }
                    else
                    {
                        accountInfo.AccountStatus = 2;
                        accountInfo.LastModifiedDate = DateTime.Now;
                        db.SaveChanges();
                    }

                    if (statusCode == 1 && string.IsNullOrWhiteSpace(card.AOModel.LeadEmail) == false)
                    {

                        var transOtherCharge = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID && p.TransactionID == null).ToList();

                        decimal todaysCharge = 0;
                        decimal balanceCharge = 0;

                        todaysCharge = card.Amount;
                        foreach (var toc in transOtherCharge)
                        {
                            balanceCharge += toc.ChargeAmt.Value;
                            toc.PaymentMethod = 1;
                            db.SaveChanges();
                        }
                        body = bodyTemplate;

                        body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now registered into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services.");
                        body = body.Replace("{AccountFullName}", card.AOModel.FirstName + " " + card.AOModel.LastName);
                        body = body.Replace("{BillingAddress}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Street : card.AOModel.BStreet));
                        body = body.Replace("{BillingCity}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.City : card.AOModel.BCity));
                        body = body.Replace("{BillingState}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.State : card.AOModel.BState));
                        body = body.Replace("{BillingZip}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Zip : card.AOModel.BZip));
                        body = body.Replace("{CustomerEmail}", card.AOModel.LeadEmail);
                        body = body.Replace("{PrimaryPhone}", card.AOModel.PrimaryPhone);

                        body = body.Replace("{ShippingAddress}", card.AOModel.Street);
                        body = body.Replace("{ShippingCity}", card.AOModel.City);
                        body = body.Replace("{ShippingState}", card.AOModel.State);
                        body = body.Replace("{ShippingZip}", card.AOModel.Zip);

                        body = body.Replace("{TodaysChargeAmount}", card.Amount.ToString("0.00"));
                        body = body.Replace("{BalanceDue}", balanceCharge.ToString("0.00"));
                        body = body.Replace("{ChargeDateTime}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));

                        body = body.Replace("{TransactionID}", response.transactionResponse.transId);
                        body = body.Replace("{CardLastDigit}", response.transactionResponse.accountNumber);
                        body = body.Replace("{TransactionType}", "Charge");
                        body = body.Replace("{TransactionTypeHeader}", "CHARGE");
                        body = body.Replace("{AuthCode}", response.transactionResponse.authCode);

                        emailSubject = "Transaction CHARGE from National Theft Search and Recovery for " + model.TotalAmount.Value.ToString("0.00") + " (USD)";

                        if (body != "")
                        {
                            new CommonModel().SendEmailPayments(card.AOModel.LeadEmail, emailSubject, body, model.AccountID.Value);
                            body = "";
                            var transEmail = db.tbl_OrderTransactions.Where(p => p.TransactionID == response.transactionResponse.transId).FirstOrDefault();
                            if (transEmail != null)
                            {
                                transEmail.MailSend = 1;
                                db.SaveChanges();
                            }

                        }
                        if (isFullPay == 1)
                        {

                            var fullpayTemplate = db.tbl_EmailTemplates.Where(p => p.TemplateID == 20).FirstOrDefault();
                            body = fullpayTemplate.TemplateHTML;

                            body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now registered into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services.");
                            body = body.Replace("{Account_Name}", card.AOModel.FirstName + " " + card.AOModel.LastName);
                            body = body.Replace("{Account_PersonMailingStreet}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Street : card.AOModel.BStreet));
                            body = body.Replace("{Account_PersonMailingCity}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.City : card.AOModel.BCity));
                            body = body.Replace("{Account_PersonMailingState}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.State : card.AOModel.BState));
                            body = body.Replace("{Account_PersonMailingPostalCode}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Zip : card.AOModel.BZip));
                            body = body.Replace("{CustomerEmail}", card.AOModel.LeadEmail);
                            body = body.Replace("{Account_Phone}", card.AOModel.PrimaryPhone);
                            body = body.Replace("{Account_Pin_Number}", card.AOModel.PinNo);
                            body = body.Replace("{Account_Vehicle_Year}", model.VehicleYear);
                            body = body.Replace("{Account_Vehicle_Make}", card.AOModel.VehicleMakeText);
                            body = body.Replace("{Account_Sale_Date}", model.SaleDate);

                            body = body.Replace("{Account_LastModifiedDate}", DateTime.Now.ToString("MM/dd/yyyy"));
                            body = body.Replace("{Transaction_ID}", response.transactionResponse.transId);

                            emailSubject = "Confirmation of Final Payment - NTSR for " + model.TotalAmount.Value.ToString("0.00") + " (USD)";


                        }
                    }
                    else
                    {
                        //body = "";
                        var declinedTemplate = File.ReadAllText(filePath + "\\transaction_declined.html");
                        body = declinedTemplate;

                        body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now registered into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services.");
                        body = body.Replace("{Account_LastName}", card.AOModel.LastName);
                        body = body.Replace("{Account_FirstName}", card.AOModel.FirstName);
                        body = body.Replace("{Account_Pin_Number}", card.AOModel.PinNo);
                        body = body.Replace("{Account_Vehicle_Year}", model.VehicleYear);
                        body = body.Replace("{Account_Vehicle_Make}", card.AOModel.VehicleMakeText);

                        body = body.Replace("{Account_Sale_Date}", model.SaleDate);
                        body = body.Replace("{Account_LastModifiedDate}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));


                        emailSubject = "Transaction DECLINED from National Theft Search and Recovery for " + model.TotalAmount.Value.ToString("0.00") + " (USD)";
                    }

                    if (body != "")
                    {
                        new CommonModel().SendEmailPayments(card.AOModel.LeadEmail, emailSubject, body, model.AccountID.Value);
                        body = "";
                    }
                }
                else
                {
                    statusCode = -1;
                    statusText = "Unknown";
                    reasonText = "The transaction has been failed due to unknown reason.";
                    gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";
                    accountInfo.AccountStatus = 2;
                    accountInfo.LastModifiedDate = DateTime.Now;
                    db.SaveChanges();

                    transDetails = gatewayResponse;
                    int transCount = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID).Count();
                    var transChargeDate = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID && p.TransactionID == null && p.ChargeNo == transCount).FirstOrDefault();
                    transCount = transCount + 1;

                    var transSave = new tbl_OrderTransactions()
                    {
                        GatwayResponse = transDetails,
                        AuthCode = "",
                        TransactionID = "0",
                        TransHashCode = "",
                        CardCheckNumber = "",
                        PaymentMethod = 1,
                        CardType = "",
                        Status = 1,
                        StatusText = statusText,
                        ChargeAmt = model.TotalAmount,
                        AOID = model.AOID,
                        ChargeDate = DateTime.Now,
                        ChargeNo = transCount + 1,
                        TransType = (isFullPay == 1 ? 6 : 1)
                    };
                    db.tbl_OrderTransactions.Add(transSave);
                    db.SaveChanges();
                }

                return transDetails;
            }
            catch
            {
                return transDetails;
            }
        }

        public string SaveRenewalTrans(TransactionModel model)
        {
            string transDetails = "";
            string filePath = HttpContext.Current.Server.MapPath("~/EmailTemplate");
            var body = "";
            var bodyTemplate = File.ReadAllText(filePath + "\\transaction_success.html");
            var emailSubject = "";
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();

                var accountInfo = db.tbl_Accounts.Where(p => p.ID == model.AccountID).FirstOrDefault();


                var orderSave = new tbl_AgentOrder()
                {
                    AccountID = model.AccountID,
                    LeadID = Convert.ToInt64(model.LeadID),
                    AgentID = MalaGroupWebSession.CurrentUser.UserID,
                    StepCompleted = 8,
                    StartDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                    EndDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                    PinNo = model.PinNumber,
                    IsCompleted = 1
                };
                db.tbl_AgentOrder.Add(orderSave);
                db.SaveChanges();
                model.AOID = orderSave.OrderID;


                var ccData = new tbl_OrderCardCheckInfo()
                {
                    CardName = model.CardDetail,
                    CardType = 0,
                    CardNumber = model.CardNumber,
                    PaymentMode = 1,
                    CardExpirationMonth = model.CardExpirationMonth,
                    CardExpirationYear = model.CardExpirationYear,
                    CardSecurityCode = model.CardSecurityCode,
                    AOID = model.AOID,
                    LeadID = model.LeadID,

                };
                db.tbl_OrderCardCheckInfo.Add(ccData);
                db.SaveChanges();

                var orderDetSave = new tbl_AgentOrderDetails()
                {
                    AOID = model.AOID,
                    BCity = model.BCity,
                    BStreet = model.BStreet,
                    BZipCode = model.BZip,
                    BState = model.BState,
                    PackageAmt = model.TotalAmount,
                    TotalAmt = model.TotalAmount.ToString(),
                    CompType = 0,
                    ChargeDay = 0,
                    PackageId = model.PackageId,
                    FirstChargeDate = DateTime.Now,
                    VehicleMake = model.VehicleMake,
                    VehicleType = model.VehicleType,
                    VehicleYear = model.VehicleYear

                };
                db.tbl_AgentOrderDetails.Add(orderDetSave);
                db.SaveChanges();


                CardModel card = new CardModel();
                card.CardNumber = model.CardNumber;
                card.ExpirationDate = model.CardExpirationMonth + model.CardExpirationYear;
                card.CardCode = model.CardSecurityCode;
                card.Amount = Convert.ToDecimal(model.TotalAmount);
                card.OrderID = model.AOID.ToString();

                AgentOrderModel aoMode = new AgentOrderModel();
                aoMode.IsDiffBillingAdd = 0;
                aoMode.Street = model.BStreet;
                aoMode.City = model.BCity;
                aoMode.State = model.BState;
                aoMode.Zip = model.BZip;
                aoMode.Country = "USA";
                aoMode.PrimaryPhone = model.Phone;
                aoMode.FirstName = model.FirstName;
                aoMode.LastName = model.LastName;
                aoMode.AccountID = Convert.ToInt64(model.AccountID);
                aoMode.PinNo = model.PinNumber;
                aoMode.LeadEmail = model.EmailID;

                card.AOModel = aoMode;

                createTransactionResponse response = new PaymentTransactionModel().ChargeCreditCard(card);

                int statusCode = 0;
                string statusText = "";
                string reasonText = "";
                string gatewayResponse = "";

                if (response != null)
                {
                    card.TransactionID = response.transactionResponse.transId;

                    if (response.transactionResponse.messages != null)
                    {
                        if (response.transactionResponse.messages[0].code == "1")
                        {
                            statusCode = 1;
                        }
                        else
                        {
                            statusCode = 2;
                        }
                        statusText = response.transactionResponse.messages[0].code == "1" ? "Approved" : "Declined";
                        reasonText = response.transactionResponse.messages[0].description;
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                    }
                    else if (response.transactionResponse.errors != null)
                    {
                        if (response.transactionResponse.errors[0].errorCode == "1")
                        {
                            statusCode = 1;
                        }
                        else
                        {
                            statusCode = 2;
                        }
                        statusText = response.transactionResponse.errors[0].errorCode == "1" ? "Approved" : "Declined";
                        reasonText = response.transactionResponse.errors[0].errorText;
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                    }
                    else
                    {
                        statusCode = -1;
                        statusText = "Unknown";
                        reasonText = "The transaction has been failed due to unknown reason.";
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";
                    }
                    transDetails = gatewayResponse;



                    var transSave = new tbl_OrderTransactions()
                    {
                        GatwayResponse = transDetails,
                        AuthCode = response.transactionResponse.authCode,
                        TransactionID = response.transactionResponse.transId,
                        TransHashCode = response.transactionResponse.transHash,
                        CardCheckNumber = response.transactionResponse.accountNumber,
                        PaymentMethod = 1,
                        CardType = response.transactionResponse.accountType,
                        Status = statusCode,
                        StatusText = statusText,
                        ChargeAmt = model.TotalAmount,
                        RefundAmt = model.TotalAmount,
                        AOID = model.AOID,
                        ChargeDate = DateTime.Now,
                        ChargeNo = 1,
                        TransType = 1
                    };
                    db.tbl_OrderTransactions.Add(transSave);
                    db.SaveChanges();

                    var chatNew = new tbl_Chattter()
                    {
                        AccountId = model.AccountID.ToString(),
                        Body = "Account Renewed",
                        CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                        InsertedById = MalaGroupWebSession.CurrentUser.UserID,
                        LeadID = model.LeadID,
                        Type = 1,
                        CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                        OriginalFileName = "",
                        SystemFileName = ""

                    };
                    db.tbl_Chattter.Add(chatNew);
                    db.SaveChanges();

                    accountInfo.AccountStatus = 1;
                    accountInfo.RenewalDate = DateTime.Now;

                    accountInfo.SalesDate = DateTime.Now;
                    accountInfo.LastModifiedDate = DateTime.Now;
                    db.SaveChanges();

                    if (statusCode == 1 && string.IsNullOrWhiteSpace(card.AOModel.LeadEmail) == false)
                    {

                        decimal todaysCharge = 0;
                        decimal balanceCharge = 0;

                        todaysCharge = card.Amount;
                        body = bodyTemplate;

                        body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now renewed into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services.");
                        body = body.Replace("{AccountFullName}", card.AOModel.FirstName + " " + card.AOModel.LastName);
                        body = body.Replace("{BillingAddress}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Street : card.AOModel.BStreet));
                        body = body.Replace("{BillingCity}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.City : card.AOModel.BCity));
                        body = body.Replace("{BillingState}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.State : card.AOModel.BState));
                        body = body.Replace("{BillingZip}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Zip : card.AOModel.BZip));
                        body = body.Replace("{CustomerEmail}", card.AOModel.LeadEmail);
                        body = body.Replace("{PrimaryPhone}", card.AOModel.PrimaryPhone);

                        body = body.Replace("{ShippingAddress}", card.AOModel.Street);
                        body = body.Replace("{ShippingCity}", card.AOModel.City);
                        body = body.Replace("{ShippingState}", card.AOModel.State);
                        body = body.Replace("{ShippingZip}", card.AOModel.Zip);

                        body = body.Replace("{TodaysChargeAmount}", card.Amount.ToString("0.00"));
                        body = body.Replace("{BalanceDue}", balanceCharge.ToString("0.00"));
                        body = body.Replace("{ChargeDateTime}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));

                        body = body.Replace("{TransactionID}", response.transactionResponse.transId);
                        body = body.Replace("{CardLastDigit}", response.transactionResponse.accountNumber);
                        body = body.Replace("{TransactionType}", "Renewal");
                        body = body.Replace("{TransactionTypeHeader}", "RENEWAL");
                        body = body.Replace("{AuthCode}", response.transactionResponse.authCode);

                        emailSubject = "Transaction CHARGE from National Theft Search and Recovery RENEWAL for " + model.TotalAmount.Value.ToString("0.00") + " (USD)";
                    }
                    else
                    {
                        body = "";
                    }

                    if (body != "")
                    {
                        new CommonModel().SendEmailPayments(card.AOModel.LeadEmail, emailSubject, body, model.AccountID.Value);
                        body = "";
                        var transEmail = db.tbl_OrderTransactions.Where(p => p.TransactionID == response.transactionResponse.transId).FirstOrDefault();
                        if (transEmail != null)
                        {
                            transEmail.MailSend = 1;
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    statusCode = -1;
                    statusText = "Unknown";
                    reasonText = "The transaction has been failed due to unknown reason.";
                    gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";
                    accountInfo.AccountStatus = 2;
                    accountInfo.LastModifiedDate = DateTime.Now;
                    db.SaveChanges();

                    transDetails = gatewayResponse;
                    int transCount = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID).Count();
                    var transChargeDate = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID && p.TransactionID == null && p.ChargeNo == transCount).FirstOrDefault();
                    transCount = transCount + 1;

                    var transSave = new tbl_OrderTransactions()
                    {
                        GatwayResponse = transDetails,
                        AuthCode = "",
                        TransactionID = "0",
                        TransHashCode = "",
                        CardCheckNumber = "",
                        PaymentMethod = 1,
                        CardType = "",
                        Status = 1,
                        StatusText = statusText,
                        ChargeAmt = model.TotalAmount,
                        AOID = model.AOID,
                        ChargeDate = DateTime.Now,
                        ChargeNo = transCount + 1,
                        TransType = 1
                    };
                    db.tbl_OrderTransactions.Add(transSave);
                    db.SaveChanges();
                }

                return transDetails;
            }
            catch
            {
                return transDetails;
            }
        }
        public List<TransactionHistory> GetRefundVoidInfo(long AccountID, int RVCType)
        {
            try
            {
                List<TransactionHistory> listSearch = new List<TransactionHistory>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetSuccessfullyTransInfo";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramAID = cmd.CreateParameter();
                        paramAID.ParameterName = "ACCOUNTID";
                        paramAID.Value = AccountID;
                        cmd.Parameters.Add(paramAID);

                        DbParameter paramType = cmd.CreateParameter();
                        paramType.ParameterName = "RVCType";
                        paramType.Value = RVCType;
                        cmd.Parameters.Add(paramType);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? createdDate = null;
                            try
                            {
                                createdDate = Convert.ToDateTime(dr["ChargeDate"].ToString());
                            }
                            catch
                            {
                            }
                            listSearch.Add(new TransactionHistory()
                            {
                                AccountID = Convert.ToInt64(dr["ID"].ToString()),
                                CDID = Convert.ToInt64(dr["CDID"].ToString()),
                                ChargeNo = Convert.ToInt32(dr["ChargeNo"].ToString()),
                                ChargeDate = (createdDate.HasValue ? createdDate.Value.ToString("MM/dd/yyyy") : ""),
                                ChargeAmt = dr["ChargeAmt"].ToString(),
                                TransactionID = dr["TransactionID"].ToString(),
                                Status = dr["Status"].ToString(),
                                PackageDetails = dr["Package"].ToString(),
                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }

                db.Dispose();
                return listSearch.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveRefundVoidInfo(string CDID, int RVCType, string PinNumber, decimal RefundAmt)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            string filePath = HttpContext.Current.Server.MapPath("~/EmailTemplate");
            var body = "";
            var bodyTemplate = File.ReadAllText(filePath + "\\transaction_success.html");
            string[] cdids = CDID.Split(',');
            string transDetails = "";
            string returnTransDetails = "";
            string transType = "";
            string emailSubject = "";
            foreach (string cdid in cdids)
            {
                long otID = Convert.ToInt64(cdid);
                var transData = db.tbl_OrderTransactions.Where(p => p.CDID == otID).FirstOrDefault();

                var model = db.tbl_OrderCardCheckInfo.Where(p => p.AOID == transData.AOID).FirstOrDefault();
                var aoData = db.tbl_AgentOrder.Where(p => p.OrderID == transData.AOID).FirstOrDefault();
                var accountInfo = db.tbl_Accounts.Where(p => p.ID == aoData.AccountID).FirstOrDefault();
                AgentOrderModel billData = new AgentOrderModel().GetLeadInfo(PinNumber); ;

                CardModel card = new CardModel();
                card.CardNumber = model.CardNumber;
                card.ExpirationDate = model.CardExpirationMonth + model.CardExpirationYear;
                card.CardCode = model.CardSecurityCode;
                card.Amount = transData.ChargeAmt.Value;
                card.OrderID = model.AOID.ToString();

                card.TransactionID = transData.TransactionID;
                AgentOrderModel aoMode = new AgentOrderModel();
                aoMode.IsDiffBillingAdd = billData.IsDiffBillingAdd;
                aoMode.Street = billData.Street;
                aoMode.City = billData.City;
                aoMode.State = billData.State;
                aoMode.Zip = billData.Zip;
                aoMode.Country = billData.Country;
                aoMode.PrimaryPhone = billData.PrimaryPhone;
                aoMode.LeadEmail = billData.LeadEmail;
                aoMode.FirstName = billData.FirstName;
                aoMode.LastName = billData.LastName;
                aoMode.AccountID = Convert.ToInt64(billData.AccountID);
                aoMode.PinNo = billData.PinNo;
                card.AOModel = aoMode;

                createTransactionResponse response;
                if (RVCType == 2)
                {
                    decimal chargeAmt = card.Amount;
                    transType = "Refund";
                    card.Amount = RefundAmt;

                    response = new PaymentTransactionModel().RefundTransaction(card);

                    int statusCode = 0;
                    string statusText = "";
                    string reasonText = "";
                    string gatewayResponse = "";
                    if (response != null)
                    {
                        card.TransactionID = response.transactionResponse.transId;
                        if (response.transactionResponse.messages != null)
                        {
                            if (response.transactionResponse.messages[0].code == "1")
                            {
                                statusCode = 1;
                            }
                            else
                            {
                                statusCode = 2;
                            }
                            statusText = response.transactionResponse.messages[0].code == "1" ? "Approved" : "Declined";
                            reasonText = response.transactionResponse.messages[0].description;
                            gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                        }
                        else if (response.transactionResponse.errors != null)
                        {
                            if (response.transactionResponse.errors[0].errorCode == "1")
                            {
                                statusCode = 1;
                            }
                            else
                            {
                                statusCode = 2;
                            }
                            statusText = response.transactionResponse.errors[0].errorCode == "1" ? "Approved" : "Declined";
                            reasonText = response.transactionResponse.errors[0].errorText;
                            gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                        }
                        else
                        {
                            statusCode = -1;
                            statusText = "Unknown";
                            reasonText = "The transaction has been failed due to unknown reason.";
                            gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";
                        }

                        transDetails = gatewayResponse;
                        returnTransDetails += transDetails;

                        int transCount = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID).Count();
                        var transList = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID && p.TransactionID == null).ToList();
                        transCount = transCount + 1;

                        var transSave = new tbl_OrderTransactions()
                        {
                            GatwayResponse = transDetails,
                            AuthCode = response.transactionResponse.authCode,
                            TransactionID = response.transactionResponse.transId,
                            TransHashCode = response.transactionResponse.transHash,
                            CardCheckNumber = response.transactionResponse.accountNumber,
                            PaymentMethod = 1,
                            CardType = response.transactionResponse.accountType,
                            Status = statusCode,
                            StatusText = reasonText,
                            ChargeAmt = -1 * RefundAmt,
                            RefundAmt = RefundAmt,
                            AOID = model.AOID,
                            ChargeDate = DateTime.Now,
                            ChargeNo = transCount + 1,
                            ResonText = reasonText,
                            TransType = RVCType
                        };
                        db.tbl_OrderTransactions.Add(transSave);
                        db.SaveChanges();

                        if (statusCode == 1)
                        {
                            var leadUpdate = db.tbl_LeadInformation.Where(p => p.LeadID == model.LeadID).FirstOrDefault();
                            if (leadUpdate != null)
                            {
                                leadUpdate.Take_Off_List__c = 1;
                                db.SaveChanges();
                            }
                            accountInfo.AccountCancellationDate = DateTime.Now;
                            accountInfo.LastModifiedDate = DateTime.Now;
                            db.SaveChanges();
                            //if (RefundAmt < chargeAmt)
                            //{
                            //    accountInfo.AccountStatus = 5;
                            //    accountInfo.AccountCancellationDate = DateTime.Now;
                            //    db.SaveChanges();
                            //}
                            //else if (RefundAmt == chargeAmt)
                            //{
                            //    accountInfo.AccountStatus = 4;
                            //    accountInfo.AccountCancellationDate = DateTime.Now;
                            //    db.SaveChanges();
                            //}
                            //else if (RefundAmt == 0)
                            //{
                            //    accountInfo.AccountStatus = 3;
                            //    accountInfo.AccountCancellationDate = DateTime.Now;
                            //    db.SaveChanges();
                            //}

                            //var otherTransactions = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID && p.TransactionID == null).ToList();
                            //transDetails = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                            //foreach (var otran in otherTransactions)
                            //{
                            //    otran.GatwayResponse = transDetails;
                            //    otran.AuthCode = "";
                            //    otran.TransactionID = "0";
                            //    otran.TransHashCode = "";
                            //    otran.CardCheckNumber = "";
                            //    otran.PaymentMethod = 1;
                            //    otran.CardType = "";
                            //    otran.Status = statusCode;
                            //    otran.StatusText = statusText;
                            //    otran.TransType = 1;
                            //    otran.ResonText = reasonText;
                            //    db.SaveChanges();
                            //}
                        }

                        if (statusCode == 1 && string.IsNullOrWhiteSpace(card.AOModel.LeadEmail) == false)
                        {
                            var transOtherCharge = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID && p.TransactionID == null).ToList();

                            decimal todaysCharge = 0;
                            decimal balanceCharge = 0;

                            todaysCharge = card.Amount;
                            foreach (var toc in transOtherCharge)
                            {
                                balanceCharge += toc.ChargeAmt.Value;
                                toc.PaymentMethod = 1;
                                db.SaveChanges();
                            }
                            body = bodyTemplate;

                            body = body.Replace("{HeaderParagraph}", "We at National Theft Search and Recovery appreciate your time and patience. A transaction towards the previous registration of services is being refunded. Please keep in mind that refunds may show up as a pending credit for a short duration of time. The credit can take up to 7 business days to be released and available, (mattering on your financial institutions refund policy). Please, feel free to contact our customer care department with any questions or requests. We’re happy to help!");
                            body = body.Replace("{AccountFullName}", card.AOModel.FirstName + " " + card.AOModel.LastName);
                            body = body.Replace("{BillingAddress}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Street : card.AOModel.BStreet));
                            body = body.Replace("{BillingCity}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.City : card.AOModel.BCity));
                            body = body.Replace("{BillingState}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.State : card.AOModel.BState));
                            body = body.Replace("{BillingZip}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Zip : card.AOModel.BZip));
                            body = body.Replace("{CustomerEmail}", card.AOModel.LeadEmail);
                            body = body.Replace("{PrimaryPhone}", card.AOModel.PrimaryPhone);

                            body = body.Replace("{ShippingAddress}", card.AOModel.Street);
                            body = body.Replace("{ShippingCity}", card.AOModel.City);
                            body = body.Replace("{ShippingState}", card.AOModel.State);
                            body = body.Replace("{ShippingZip}", card.AOModel.Zip);

                            body = body.Replace("{TodaysChargeAmount}", card.Amount.ToString("0.00"));
                            body = body.Replace("{BalanceDue}", balanceCharge.ToString("0.00"));
                            body = body.Replace("{ChargeDateTime}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));

                            body = body.Replace("{TransactionID}", response.transactionResponse.transId);
                            body = body.Replace("{CardLastDigit}", response.transactionResponse.accountNumber);
                            body = body.Replace("{TransactionType}", "Refund");
                            body = body.Replace("{TransactionTypeHeader}", "REFUND");
                            body = body.Replace("{AuthCode}", response.transactionResponse.authCode);

                            emailSubject = "Transaction REFUND from National Theft Search and Recovery for " + RefundAmt.ToString("0.00") + " (USD)";
                        }
                        else
                        {
                            body = "";
                        }

                        if (body != "")
                        {
                            new CommonModel().SendEmailPayments(card.AOModel.LeadEmail, emailSubject, body, accountInfo.ID);
                            body = "";
                            var transEmail = db.tbl_OrderTransactions.Where(p => p.TransactionID == response.transactionResponse.transId).FirstOrDefault();
                            if (transEmail != null)
                            {
                                transEmail.MailSend = 1;
                                db.SaveChanges();
                            }

                        }
                    }
                    else
                    {
                        accountInfo.AccountStatus = 2;
                        db.SaveChanges();

                        statusCode = -1;
                        statusText = "Unknown";
                        reasonText = "The transaction has been failed due to unknown reason.";
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";

                        transDetails = gatewayResponse;
                        returnTransDetails += transDetails;

                        int transCount = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID).Count();
                        var transChargeDate = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID && p.TransactionID == null && p.ChargeNo == transCount).FirstOrDefault();
                        transCount = transCount + 1;

                        var transSave = new tbl_OrderTransactions()
                        {
                            GatwayResponse = transDetails,
                            AuthCode = "",
                            TransactionID = "0",
                            TransHashCode = "",
                            CardCheckNumber = "",
                            PaymentMethod = 1,
                            CardType = "",
                            Status = statusCode,
                            StatusText = statusText,
                            ChargeAmt = -1 * RefundAmt,
                            AOID = model.AOID,
                            ChargeDate = DateTime.Now,
                            ChargeNo = transCount + 1,
                            TransType = RVCType,
                            ResonText = reasonText
                        };
                        db.tbl_OrderTransactions.Add(transSave);
                        db.SaveChanges();
                    }
                }
                else if (RVCType == 3)
                {
                    transType = "Void";
                    response = new PaymentTransactionModel().VoidTransaction(card);

                    int statusCode = 0;
                    string statusText = "";
                    string reasonText = "";
                    string gatewayResponse = "";
                    if (response != null)
                    {
                        card.TransactionID = response.transactionResponse.transId;
                        if (response.transactionResponse.messages != null)
                        {
                            if (response.transactionResponse.messages[0].code == "1")
                            {
                                statusCode = 1;
                            }
                            else
                            {
                                statusCode = 2;
                            }
                            statusText = response.transactionResponse.messages[0].code == "1" ? "Approved" : "Declined";
                            reasonText = response.transactionResponse.messages[0].description;
                            gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                        }
                        else if (response.transactionResponse.errors != null)
                        {
                            if (response.transactionResponse.errors[0].errorCode == "1")
                            {
                                statusCode = 1;
                            }
                            else
                            {
                                statusCode = 2;
                            }
                            statusText = response.transactionResponse.errors[0].errorCode == "1" ? "Approved" : "Declined";
                            reasonText = response.transactionResponse.errors[0].errorText;
                            gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                        }
                        else
                        {
                            statusCode = -1;
                            statusText = "Unknown";
                            reasonText = "The transaction has been failed due to unknown reason.";
                            gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";
                        }

                        transDetails = gatewayResponse;
                        returnTransDetails += transDetails;
                        int transCount = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID).Count();
                        var transList = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID && p.TransactionID == null).ToList();
                        transCount = transCount + 1;

                        var transSave = new tbl_OrderTransactions()
                        {
                            GatwayResponse = transDetails,
                            AuthCode = response.transactionResponse.authCode,
                            TransactionID = response.transactionResponse.transId,
                            TransHashCode = response.transactionResponse.transHash,
                            CardCheckNumber = response.transactionResponse.accountNumber,
                            PaymentMethod = 1,
                            CardType = response.transactionResponse.accountType,
                            Status = statusCode,
                            StatusText = statusText,
                            ChargeAmt = -1 * transData.ChargeAmt.Value,
                            AOID = model.AOID,
                            ChargeDate = DateTime.Now,
                            ChargeNo = transCount + 1,
                            ResonText = reasonText,
                            TransType = RVCType
                        };
                        db.tbl_OrderTransactions.Add(transSave);
                        db.SaveChanges();

                        if (statusCode == 1)
                        {
                            //accountInfo.AccountStatus = 4;
                            accountInfo.AccountCancellationDate = DateTime.Now;
                            db.SaveChanges();

                            transDetails = gatewayResponse;
                            //var otherTransactions = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID && p.TransactionID == null).ToList();
                            //foreach (var otran in otherTransactions)
                            //{
                            //    otran.GatwayResponse = transDetails;
                            //    otran.AuthCode = "";
                            //    otran.TransactionID = "0";
                            //    otran.TransHashCode = "";
                            //    otran.CardCheckNumber = "";
                            //    otran.PaymentMethod = 1;
                            //    otran.CardType = "";
                            //    otran.Status = statusCode;
                            //    otran.StatusText = reasonText;
                            //    otran.TransType = 1;
                            //    db.SaveChanges();
                            //}

                            var leadUpdate = db.tbl_LeadInformation.Where(p => p.LeadID == model.LeadID).FirstOrDefault();
                            if (leadUpdate != null)
                            {
                                leadUpdate.Take_Off_List__c = 1;
                                db.SaveChanges();
                            }
                        }
                        if (statusCode == 1 && string.IsNullOrWhiteSpace(card.AOModel.LeadEmail) == false)
                        {
                            var transOtherCharge = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID && p.TransactionID == null).ToList();

                            decimal todaysCharge = 0;
                            decimal balanceCharge = 0;

                            todaysCharge = card.Amount;
                            foreach (var toc in transOtherCharge)
                            {
                                balanceCharge += toc.ChargeAmt.Value;
                                toc.PaymentMethod = 1;
                                db.SaveChanges();
                            }

                            body = bodyTemplate;

                            body = body.Replace("{HeaderParagraph}", "We at National Theft Search and Recovery appreciate your time and patience. A transaction towards the registration of services has now been voided. Please keep in mind that voided transactions may show up as pending in your account for a short duration of time after receiving this message. You may expect the credit to be released to the account holder within 24 – 48 hours. Please, feel free to contact our customer care department with any questions or requests. We’re happy to help!");
                            body = body.Replace("{AccountFullName}", card.AOModel.FirstName + " " + card.AOModel.LastName);
                            body = body.Replace("{BillingAddress}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Street : card.AOModel.BStreet));
                            body = body.Replace("{BillingCity}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.City : card.AOModel.BCity));
                            body = body.Replace("{BillingState}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.State : card.AOModel.BState));
                            body = body.Replace("{BillingZip}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Zip : card.AOModel.BZip));
                            body = body.Replace("{CustomerEmail}", card.AOModel.LeadEmail);
                            body = body.Replace("{PrimaryPhone}", card.AOModel.PrimaryPhone);

                            body = body.Replace("{ShippingAddress}", card.AOModel.Street);
                            body = body.Replace("{ShippingCity}", card.AOModel.City);
                            body = body.Replace("{ShippingState}", card.AOModel.State);
                            body = body.Replace("{ShippingZip}", card.AOModel.Zip);

                            body = body.Replace("{TodaysChargeAmount}", card.Amount.ToString("0.00"));
                            body = body.Replace("{BalanceDue}", balanceCharge.ToString("0.00"));
                            body = body.Replace("{ChargeDateTime}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));

                            body = body.Replace("{TransactionID}", response.transactionResponse.transId);
                            body = body.Replace("{CardLastDigit}", response.transactionResponse.accountNumber);
                            body = body.Replace("{TransactionType}", "Void");
                            body = body.Replace("{TransactionTypeHeader}", "VOID");
                            body = body.Replace("{AuthCode}", response.transactionResponse.authCode);

                            emailSubject = "Transaction VOID from National Theft Search and Recovery for " + todaysCharge.ToString("0.00") + " (USD)";
                        }
                        else
                        {
                            body = "";
                        }

                        if (body != "")
                        {
                            new CommonModel().SendEmailPayments(card.AOModel.LeadEmail, emailSubject, body, accountInfo.ID);
                            body = "";
                            var transEmail = db.tbl_OrderTransactions.Where(p => p.TransactionID == response.transactionResponse.transId).FirstOrDefault();
                            if (transEmail != null)
                            {
                                transEmail.MailSend = 1;
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        accountInfo.AccountStatus = 2;
                        accountInfo.LastModifiedDate = DateTime.Now;
                        db.SaveChanges();


                        statusCode = -1;
                        statusText = "Unknown";
                        reasonText = "The transaction has been failed due to unknown reason.";
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";

                        transDetails = gatewayResponse;
                        returnTransDetails += transDetails;

                        int transCount = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID).Count();
                        var transChargeDate = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID && p.TransactionID == null && p.ChargeNo == transCount).FirstOrDefault();
                        transCount = transCount + 1;
                        var transSave = new tbl_OrderTransactions()
                        {
                            GatwayResponse = transDetails,
                            AuthCode = "",
                            TransactionID = "0",
                            TransHashCode = "",
                            CardCheckNumber = "",
                            PaymentMethod = 1,
                            CardType = "",
                            Status = statusCode,
                            StatusText = statusText,
                            ChargeAmt = -1 * transData.ChargeAmt.Value,
                            AOID = model.AOID,
                            ChargeDate = DateTime.Now,
                            ChargeNo = transCount + 1,
                            ResonText = reasonText,
                            TransType = RVCType
                        };
                        db.tbl_OrderTransactions.Add(transSave);
                        db.SaveChanges();
                    }
                }
                else if (RVCType == 4)
                {
                    transDetails = DateTime.Now.ToString("MM/dd/yyyy") + "|Waive Off|";
                    returnTransDetails += transDetails;
                    transType = "WaiveOff";

                    transData.TransactionID = "0";

                    transData.PaymentMethod = 1;

                    transData.ChargeDate = DateTime.Now;
                    transData.Status = 1;
                    transData.StatusText = "Waive Off";
                    transData.TransType = 7;
                    db.SaveChanges();

                    decimal todaysCharge = 0;
                    decimal balanceCharge = 0;

                    todaysCharge = card.Amount;

                    //if (string.IsNullOrWhiteSpace(card.AOModel.LeadEmail) == false)
                    //{
                    //    body = bodyTemplate;
                    //    body = body.Replace("{HeaderParagraph}", "We at National Theft Search and Recovery appreciate your time and patience. A transaction towards the registration of services has now been waive off.");
                    //    body = body.Replace("{AccountFullName}", card.AOModel.FirstName + " " + card.AOModel.LastName);
                    //    body = body.Replace("{BillingAddress}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Street : card.AOModel.BStreet));
                    //    body = body.Replace("{BillingCity}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.City : card.AOModel.BCity));
                    //    body = body.Replace("{BillingState}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.State : card.AOModel.BState));
                    //    body = body.Replace("{BillingZip}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Zip : card.AOModel.BZip));
                    //    body = body.Replace("{CustomerEmail}", card.AOModel.LeadEmail);
                    //    body = body.Replace("{PrimaryPhone}", card.AOModel.PrimaryPhone);

                    //    body = body.Replace("{ShippingAddress}", card.AOModel.Street);
                    //    body = body.Replace("{ShippingCity}", card.AOModel.City);
                    //    body = body.Replace("{ShippingState}", card.AOModel.State);
                    //    body = body.Replace("{ShippingZip}", card.AOModel.Zip);

                    //    body = body.Replace("{TodaysChargeAmount}", card.Amount.ToString("0.00"));
                    //    body = body.Replace("{BalanceDue}", balanceCharge.ToString("0.00"));
                    //    body = body.Replace("{ChargeDateTime}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));

                    //    body = body.Replace("{TransactionID}", "0");
                    //    body = body.Replace("{CardLastDigit}", "");
                    //    body = body.Replace("{TransactionType}", "Waive Off");
                    //    body = body.Replace("{TransactionTypeHeader}", "WAIVE OFF");
                    //    body = body.Replace("{AuthCode}", "");

                    //    emailSubject = "Transaction WAIVE OFF from National Theft Search and Recovery for " + todaysCharge.ToString("0.00") + " (USD)";

                    //}
                    //else
                    //{
                    //    body = "";
                    //}

                    //if (body != "")
                    //{
                    //    new CommonModel().SendEmailPayments("sachinmahore@gmail.com", emailSubject, body, accountInfo.ID);
                    //    body = "";
                    //}
                    accountInfo.LastModifiedDate = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {

                }

            }
            return returnTransDetails;
        }
        public string SaveChargeInfo(string CDID, int RVCType, string PinNumber, decimal RefundAmt)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            string filePath = HttpContext.Current.Server.MapPath("~/EmailTemplate");
            var body = "";
            var bodyTemplate = File.ReadAllText(filePath + "\\transaction_success.html");
            string[] cdids = CDID.Split(',');
            string transDetails = "";
            string returnTransDetails = "";
            string transType = "";
            string emailSubject = "";


            decimal chargeAmount = 0;
            if (cdids.Length > 1)
            {
                foreach (string cdid in cdids)
                {
                    long otID = Convert.ToInt64(cdid);
                    var transMultData = db.tbl_OrderTransactions.Where(p => p.CDID == otID).FirstOrDefault();
                    chargeAmount += Convert.ToDecimal(transMultData.ChargeAmt);

                }
                long ootID = Convert.ToInt64(cdids[0]);
                var transData = db.tbl_OrderTransactions.Where(p => p.CDID == ootID).FirstOrDefault();
                var aoData = db.tbl_AgentOrder.Where(p => p.OrderID == transData.AOID).FirstOrDefault();
                // default credit card
                var model = db.tbl_OrderCardCheckInfo.Where(p => p.LeadID == aoData.LeadID && p.IsDefault == 1).FirstOrDefault();
                if (model == null)
                {
                    model = db.tbl_OrderCardCheckInfo.Where(p => p.AOID == transData.AOID).FirstOrDefault();
                }
                var accountInfo = db.tbl_Accounts.Where(p => p.ID == aoData.AccountID).FirstOrDefault();
                int transCount = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID).Count();
                transCount = transCount + 1;

                AgentOrderModel billData = new AgentOrderModel().GetLeadInfo(PinNumber); ;

                CardModel card = new CardModel();
                card.CardNumber = model.CardNumber;
                card.ExpirationDate = model.CardExpirationMonth + model.CardExpirationYear;
                card.CardCode = model.CardSecurityCode;
                card.Amount = chargeAmount;
                card.OrderID = model.AOID.ToString();

                // card.TransactionID = transData.TransactionID;
                AgentOrderModel aoMode = new AgentOrderModel();
                aoMode.IsDiffBillingAdd = billData.IsDiffBillingAdd;
                aoMode.Street = billData.Street;
                aoMode.City = billData.City;
                aoMode.State = billData.State;
                aoMode.Zip = billData.Zip;
                aoMode.Country = billData.Country;
                aoMode.PrimaryPhone = billData.PrimaryPhone;
                aoMode.LeadEmail = billData.LeadEmail;
                aoMode.FirstName = billData.FirstName;
                aoMode.LastName = billData.LastName;
                aoMode.AccountID = Convert.ToInt64(billData.AccountID);
                aoMode.PinNo = billData.PinNo;
                card.AOModel = aoMode;

                createTransactionResponse response;

                transType = "Charge";
                response = new PaymentTransactionModel().ChargeCreditCard(card);

                int statusCode = 0;
                string statusText = "";
                string reasonText = "";
                string gatewayResponse = "";
                if (response != null)
                {
                    card.TransactionID = response.transactionResponse.transId;
                    if (response.transactionResponse.messages != null)
                    {
                        if (response.transactionResponse.messages[0].code == "1")
                        {
                            statusCode = 1;
                        }
                        else
                        {
                            statusCode = 2;
                        }
                        statusText = response.transactionResponse.messages[0].code == "1" ? "Approved" : "Declined";
                        reasonText = response.transactionResponse.messages[0].description;
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                    }
                    else if (response.transactionResponse.errors != null)
                    {
                        if (response.transactionResponse.errors[0].errorCode == "1")
                        {
                            statusCode = 1;
                        }
                        else
                        {
                            statusCode = 2;
                        }
                        statusText = response.transactionResponse.errors[0].errorCode == "1" ? "Approved" : "Declined";
                        reasonText = response.transactionResponse.errors[0].errorText;
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                    }
                    else
                    {
                        statusCode = -1;
                        statusText = "Unknown";
                        reasonText = "The transaction has been failed due to unknown reason.";
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";
                    }

                    transDetails = gatewayResponse;
                    returnTransDetails += transDetails;


                    var transSave = new tbl_OrderTransactions()
                    {
                        PaymentMethod = 1,
                        Status = statusCode,
                        StatusText = statusText,
                        ChargeAmt = chargeAmount,
                        AOID = model.AOID,

                        ChargeNo = transCount,
                        GatwayResponse = transDetails,
                        AuthCode = response.transactionResponse.authCode,
                        TransactionID = response.transactionResponse.transId,
                        TransHashCode = response.transactionResponse.transHash,
                        CardCheckNumber = response.transactionResponse.accountNumber,

                        CardType = response.transactionResponse.accountType,
                        ChargeDate = DateTime.Now,

                        TransType = RVCType,
                        ResonText = reasonText,
                    };
                    db.tbl_OrderTransactions.Add(transSave);
                    db.SaveChanges();



                    if (statusCode == 1 && string.IsNullOrWhiteSpace(card.AOModel.LeadEmail) == false)
                    {
                        foreach (string cdid in cdids)
                        {
                            long otID = Convert.ToInt64(cdid);
                            var transMultData = db.tbl_OrderTransactions.Where(p => p.CDID == otID).FirstOrDefault();
                            db.tbl_OrderTransactions.Remove(transMultData);

                        }


                        accountInfo.AccountStatus = 1;
                        db.SaveChanges();

                        var transOtherCharge = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID && p.TransactionID == null).ToList();

                        decimal todaysCharge = 0;
                        decimal balanceCharge = 0;

                        todaysCharge = card.Amount;
                        foreach (var toc in transOtherCharge)
                        {
                            balanceCharge += toc.ChargeAmt.Value;
                            toc.PaymentMethod = 1;
                            db.SaveChanges();
                        }
                        body = bodyTemplate;

                        body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now registered into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services.");
                        body = body.Replace("{AccountFullName}", card.AOModel.FirstName + " " + card.AOModel.LastName);
                        body = body.Replace("{BillingAddress}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Street : card.AOModel.BStreet));
                        body = body.Replace("{BillingCity}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.City : card.AOModel.BCity));
                        body = body.Replace("{BillingState}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.State : card.AOModel.BState));
                        body = body.Replace("{BillingZip}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Zip : card.AOModel.BZip));
                        body = body.Replace("{CustomerEmail}", card.AOModel.LeadEmail);
                        body = body.Replace("{PrimaryPhone}", card.AOModel.PrimaryPhone);

                        body = body.Replace("{ShippingAddress}", card.AOModel.Street);
                        body = body.Replace("{ShippingCity}", card.AOModel.City);
                        body = body.Replace("{ShippingState}", card.AOModel.State);
                        body = body.Replace("{ShippingZip}", card.AOModel.Zip);

                        body = body.Replace("{TodaysChargeAmount}", card.Amount.ToString("0.00"));
                        body = body.Replace("{BalanceDue}", balanceCharge.ToString("0.00"));
                        body = body.Replace("{ChargeDateTime}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));

                        body = body.Replace("{TransactionID}", response.transactionResponse.transId);
                        body = body.Replace("{CardLastDigit}", response.transactionResponse.accountNumber);
                        body = body.Replace("{TransactionType}", "Charge");
                        body = body.Replace("{TransactionTypeHeader}", "CHARGE");
                        body = body.Replace("{AuthCode}", response.transactionResponse.authCode);

                        emailSubject = "Transaction CHARGE from National Theft Search and Recovery for " + todaysCharge.ToString("0.00") + " (USD)";
                    }
                    else
                    {
                        body = "";
                    }

                    if (body != "")
                    {
                        new CommonModel().SendEmailPayments(card.AOModel.LeadEmail, emailSubject, body, accountInfo.ID);
                        body = "";
                        var transEmail = db.tbl_OrderTransactions.Where(p => p.TransactionID == response.transactionResponse.transId).FirstOrDefault();
                        if (transEmail != null)
                        {
                            transEmail.MailSend = 1;
                            db.SaveChanges();
                        }
                    }
                    if (statusCode != 1)
                    {
                        accountInfo.AccountStatus = 2;
                        accountInfo.LastModifiedDate = DateTime.Now;
                        db.SaveChanges();




                        var transFailSave = new tbl_OrderTransactions()
                        {
                            PaymentMethod = 1,
                            Status = 0,
                            ChargeAmt = chargeAmount,
                            AOID = model.AOID,

                            ChargeNo = transCount,
                            GatwayResponse = transDetails,
                            AuthCode = response.transactionResponse.authCode,
                            TransactionID = response.transactionResponse.transId,
                            TransHashCode = response.transactionResponse.transHash,
                            CardCheckNumber = response.transactionResponse.accountNumber,

                            CardType = response.transactionResponse.accountType,
                            ChargeDate = DateTime.Now,

                            StatusText = statusText,
                            TransType = RVCType,
                            ResonText = reasonText,
                        };
                        db.tbl_OrderTransactions.Add(transFailSave);
                        db.SaveChanges();
                    }
                }
                else
                {
                    statusCode = -1;
                    statusText = "Unknown";
                    reasonText = "The transaction has been failed due to unknown reason.";
                    gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";

                    transDetails = gatewayResponse;
                    returnTransDetails += transDetails;

                    var transFailSave = new tbl_OrderTransactions()
                    {
                        PaymentMethod = 1,
                        Status = 0,
                        ChargeAmt = chargeAmount,
                        AOID = model.AOID,

                        ChargeNo = transCount,
                        GatwayResponse = transDetails,
                        AuthCode = response.transactionResponse.authCode,
                        TransactionID = response.transactionResponse.transId,
                        TransHashCode = response.transactionResponse.transHash,
                        CardCheckNumber = response.transactionResponse.accountNumber,

                        CardType = response.transactionResponse.accountType,
                        ChargeDate = DateTime.Now,

                        StatusText = statusText,
                        TransType = RVCType,
                        ResonText = reasonText,
                    };
                    db.tbl_OrderTransactions.Add(transFailSave);
                    db.SaveChanges();


                    card.TransactionID = "0";
                    accountInfo.AccountStatus = 2;
                    db.SaveChanges();
                }
            }

            else
            {
                long otID = Convert.ToInt64(cdids[0]);
                var transData = db.tbl_OrderTransactions.Where(p => p.CDID == otID).FirstOrDefault();
                var aoData = db.tbl_AgentOrder.Where(p => p.OrderID == transData.AOID).FirstOrDefault();
                // default credit card
                var model = db.tbl_OrderCardCheckInfo.Where(p => p.LeadID == aoData.LeadID && p.IsDefault == 1).FirstOrDefault();
                if (model == null)
                {
                    model = db.tbl_OrderCardCheckInfo.Where(p => p.AOID == transData.AOID).FirstOrDefault();
                }
                var accountInfo = db.tbl_Accounts.Where(p => p.ID == aoData.AccountID).FirstOrDefault();
                AgentOrderModel billData = new AgentOrderModel().GetLeadInfo(PinNumber); ;

                CardModel card = new CardModel();
                card.CardNumber = model.CardNumber;
                card.ExpirationDate = model.CardExpirationMonth + model.CardExpirationYear;
                card.CardCode = model.CardSecurityCode;
                card.Amount = transData.ChargeAmt.Value;
                card.OrderID = model.AOID.ToString();

                card.TransactionID = transData.TransactionID;
                AgentOrderModel aoMode = new AgentOrderModel();
                aoMode.IsDiffBillingAdd = billData.IsDiffBillingAdd;
                aoMode.Street = billData.Street;
                aoMode.City = billData.City;
                aoMode.State = billData.State;
                aoMode.Zip = billData.Zip;
                aoMode.Country = billData.Country;
                aoMode.PrimaryPhone = billData.PrimaryPhone;
                aoMode.LeadEmail = billData.LeadEmail;
                aoMode.FirstName = billData.FirstName;
                aoMode.LastName = billData.LastName;
                aoMode.AccountID = Convert.ToInt64(billData.AccountID);
                aoMode.PinNo = billData.PinNo;
                card.AOModel = aoMode;

                createTransactionResponse response;

                transType = "Charge";
                response = new PaymentTransactionModel().ChargeCreditCard(card);

                int statusCode = 0;
                string statusText = "";
                string reasonText = "";
                string gatewayResponse = "";
                if (response != null)
                {
                    card.TransactionID = response.transactionResponse.transId;
                    if (response.transactionResponse.messages != null)
                    {
                        if (response.transactionResponse.messages[0].code == "1")
                        {
                            statusCode = 1;
                        }
                        else
                        {
                            statusCode = 2;
                        }
                        statusText = response.transactionResponse.messages[0].code == "1" ? "Approved" : "Declined";
                        reasonText = response.transactionResponse.messages[0].description;
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                    }
                    else if (response.transactionResponse.errors != null)
                    {
                        if (response.transactionResponse.errors[0].errorCode == "1")
                        {
                            statusCode = 1;
                        }
                        else
                        {
                            statusCode = 2;
                        }
                        statusText = response.transactionResponse.errors[0].errorCode == "1" ? "Approved" : "Declined";
                        reasonText = response.transactionResponse.errors[0].errorText;
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|" + response.transactionResponse.transId;
                    }
                    else
                    {
                        statusCode = -1;
                        statusText = "Unknown";
                        reasonText = "The transaction has been failed due to unknown reason.";
                        gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";
                    }

                    transDetails = gatewayResponse;
                    returnTransDetails += transDetails;

                    transData.GatwayResponse = transDetails;
                    transData.AuthCode = response.transactionResponse.authCode;
                    transData.TransactionID = response.transactionResponse.transId;
                    transData.TransHashCode = response.transactionResponse.transHash;
                    transData.CardCheckNumber = response.transactionResponse.accountNumber;
                    transData.PaymentMethod = 1;
                    transData.CardType = response.transactionResponse.accountType;
                    transData.ChargeDate = DateTime.Now;
                    transData.Status = statusCode;
                    transData.StatusText = statusText;
                    transData.TransType = RVCType;
                    transData.ResonText = reasonText;
                    db.SaveChanges();

                    if (statusCode == 1 && string.IsNullOrWhiteSpace(card.AOModel.LeadEmail) == false)
                    {
                        accountInfo.AccountStatus = 1;
                        accountInfo.LastModifiedDate = DateTime.Now;
                        db.SaveChanges();

                        var transOtherCharge = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID && p.TransactionID == null).ToList();

                        decimal todaysCharge = 0;
                        decimal balanceCharge = 0;

                        todaysCharge = card.Amount;
                        foreach (var toc in transOtherCharge)
                        {
                            balanceCharge += toc.ChargeAmt.Value;
                            toc.PaymentMethod = 1;
                            db.SaveChanges();
                        }
                        body = bodyTemplate;

                        body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now registered into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services.");
                        body = body.Replace("{AccountFullName}", card.AOModel.FirstName + " " + card.AOModel.LastName);
                        body = body.Replace("{BillingAddress}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Street : card.AOModel.BStreet));
                        body = body.Replace("{BillingCity}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.City : card.AOModel.BCity));
                        body = body.Replace("{BillingState}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.State : card.AOModel.BState));
                        body = body.Replace("{BillingZip}", (card.AOModel.IsDiffBillingAdd == 0 ? card.AOModel.Zip : card.AOModel.BZip));
                        body = body.Replace("{CustomerEmail}", card.AOModel.LeadEmail);
                        body = body.Replace("{PrimaryPhone}", card.AOModel.PrimaryPhone);

                        body = body.Replace("{ShippingAddress}", card.AOModel.Street);
                        body = body.Replace("{ShippingCity}", card.AOModel.City);
                        body = body.Replace("{ShippingState}", card.AOModel.State);
                        body = body.Replace("{ShippingZip}", card.AOModel.Zip);

                        body = body.Replace("{TodaysChargeAmount}", card.Amount.ToString("0.00"));
                        body = body.Replace("{BalanceDue}", balanceCharge.ToString("0.00"));
                        body = body.Replace("{ChargeDateTime}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));

                        body = body.Replace("{TransactionID}", response.transactionResponse.transId);
                        body = body.Replace("{CardLastDigit}", response.transactionResponse.accountNumber);
                        body = body.Replace("{TransactionType}", "Charge");
                        body = body.Replace("{TransactionTypeHeader}", "CHARGE");
                        body = body.Replace("{AuthCode}", response.transactionResponse.authCode);

                        emailSubject = "Transaction CHARGE from National Theft Search and Recovery for " + todaysCharge.ToString("0.00") + " (USD)";
                    }
                    else
                    {
                        body = "";
                    }

                    if (body != "")
                    {
                        new CommonModel().SendEmailPayments(card.AOModel.LeadEmail, emailSubject, body, accountInfo.ID);
                        body = "";
                        var transEmail = db.tbl_OrderTransactions.Where(p => p.TransactionID == response.transactionResponse.transId).FirstOrDefault();
                        if (transEmail != null)
                        {
                            transEmail.MailSend = 1;
                            db.SaveChanges();
                        }
                    }
                    if (statusCode != 1)
                    {
                        accountInfo.AccountStatus = 2;
                        db.SaveChanges();

                        int transCount = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID).Count();
                        var transChargeDate = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID && p.TransactionID == null && p.ChargeNo == transCount).FirstOrDefault();
                        transCount = transCount + 1;

                        DateTime dtChargeDate = Convert.ToDateTime(transData.ChargeDate.Value);
                        if (transChargeDate != null)
                        {
                            dtChargeDate = transChargeDate.ChargeDate.Value;
                        }
                        dtChargeDate = dtChargeDate.AddMonths(1);


                        var transSave = new tbl_OrderTransactions()
                        {
                            PaymentMethod = 1,
                            Status = 0,
                            ChargeAmt = transData.ChargeAmt.Value,
                            AOID = model.AOID,
                            ChargeDate = dtChargeDate,
                            ChargeNo = transCount
                        };
                        db.tbl_OrderTransactions.Add(transSave);
                        db.SaveChanges();
                    }
                }
                else
                {
                    statusCode = -1;
                    statusText = "Unknown";
                    reasonText = "The transaction has been failed due to unknown reason.";
                    gatewayResponse = DateTime.Now.ToString("MM/dd/yyyy") + "|" + statusText + "|" + reasonText + "|0";

                    transDetails = gatewayResponse;
                    returnTransDetails += transDetails;

                    transData.GatwayResponse = transDetails;
                    transData.AuthCode = "";
                    transData.TransactionID = "0";
                    transData.TransHashCode = "";
                    transData.CardCheckNumber = "";
                    transData.PaymentMethod = 1;
                    transData.CardType = "";
                    transData.Status = statusCode;
                    transData.StatusText = statusText;
                    transData.ResonText = reasonText;
                    transData.ChargeDate = DateTime.Now;
                    transData.TransType = RVCType;
                    db.SaveChanges();

                    int transCount = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID).Count();
                    var transChargeDate = db.tbl_OrderTransactions.Where(p => p.AOID == transData.AOID && p.TransactionID == null && p.ChargeNo == transCount).FirstOrDefault();
                    transCount = transCount + 1;

                    DateTime dtChargeDate = Convert.ToDateTime(transData.ChargeDate.Value);
                    if (transChargeDate != null)
                    {
                        dtChargeDate = transChargeDate.ChargeDate.Value;
                    }
                    dtChargeDate = dtChargeDate.AddMonths(1);

                    var transSave = new tbl_OrderTransactions()
                    {
                        PaymentMethod = 1,
                        Status = 0,
                        ChargeAmt = transData.ChargeAmt.Value,
                        AOID = model.AOID,
                        ChargeDate = dtChargeDate,
                        ChargeNo = transCount
                    };
                    db.tbl_OrderTransactions.Add(transSave);
                    db.SaveChanges();

                    card.TransactionID = "0";
                    accountInfo.AccountStatus = 2;
                    db.SaveChanges();
                }

            }
            return returnTransDetails;
        }
        public string ReenableRefund(long AOID)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            string returnTransDetails = "";

            //long ootID = Convert.ToInt64(cdids[0]);
            // var transData = db.tbl_OrderTransactions.Where(p => p.CDID == ootID).FirstOrDefault();
            // orderId =  Convert.ToInt64(transData.AOID);
            var aoData = db.tbl_AgentOrder.Where(p => p.OrderID == AOID).FirstOrDefault();
            var accountInfo = db.tbl_Accounts.Where(p => p.ID == aoData.AccountID).FirstOrDefault();

            int transCount = db.tbl_OrderTransactions.Where(p => p.AOID == AOID).Count();


            accountInfo.AccountStatus = 1;
            accountInfo.AccountCancellationDate = null;
            db.SaveChanges();

            var leadUpdate = db.tbl_LeadInformation.Where(p => p.LeadID == accountInfo.LeadID).FirstOrDefault();
            if (leadUpdate != null)
            {
                leadUpdate.Take_Off_List__c = 0;

                db.SaveChanges();
            }

            var otherTransactions = db.tbl_OrderTransactions.Where(p => p.AOID == AOID && p.TransactionID == "0").ToList();

            foreach (var otran in otherTransactions)
            {
                otran.GatwayResponse = null;
                otran.AuthCode = null;
                otran.TransactionID = null;
                otran.TransHashCode = null;
                otran.CardCheckNumber = "";
                otran.PaymentMethod = 1;
                otran.CardType = "";
                otran.Status = 0;
                otran.StatusText = null;
                otran.TransType = null;
                otran.ResonText = null;
                db.SaveChanges();
            }

            returnTransDetails = "Account Transaction Re-enabled";
            return returnTransDetails;
        }
        public string SetScheduleStatus(long CDID, int TransType)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            var transDetail = db.tbl_OrderTransactions.Where(p => p.CDID == CDID).FirstOrDefault();
            transDetail.TransType = TransType;
            transDetail.StatusText = (TransType == 5 ? "Paused" : "");
            db.SaveChanges();

            return "1";
        }
        public string UpdateDateAmountTransaction(List<OrderTransactions> model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            if (model != null)
            {
                foreach (var ot in model)
                {
                    var transInfo = db.tbl_OrderTransactions.Where(p => p.CDID == ot.CDID).FirstOrDefault();
                    transInfo.ChargeDate = ot.ChargeDate;
                    transInfo.ChargeAmt = ot.ChargeAmt;
                    db.SaveChanges();
                }
            }
            return "1";
        }
        public List<EmailModel> GetEmailHistory(long AccountID, int PageID)
        {
            try
            {
                List<EmailModel> model = new List<EmailModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetEmailList";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter AID = cmd.CreateParameter();
                        AID.ParameterName = "AccountID";
                        AID.Value = AccountID;
                        cmd.Parameters.Add(AID);

                        DbParameter oPageID = cmd.CreateParameter();
                        oPageID.ParameterName = "PageID";
                        oPageID.Value = PageID;
                        cmd.Parameters.Add(oPageID);


                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {


                            model.Add(new EmailModel()
                            {
                                ID = Convert.ToInt64(dr["ID"].ToString()),
                                EmailSubject = dr["EmailSubject"].ToString(),
                                CreatedDate = dr["CreatedDate"].ToString(),
                            });
                        }


                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EmailModel GetEmailDet(int ID)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            EmailModel model = new EmailModel();
            var emailDet = db.tbl_EMails.Where(p => p.ID == ID).FirstOrDefault();
            model.EmailSubject = emailDet.EmailSubject;
            model.EmailHTMLBody = emailDet.EmailHTMLBody;
            model.ToEmails = emailDet.ToEmails;
            model.BCCEmails = emailDet.BCCEmails;
            model.CCEmails = emailDet.CCEmails;
            model.FromEmail = emailDet.FromEmail;
            model.AttachedFileName = emailDet.AttachedFileName;
            model.OriginalFileName = emailDet.OriginalFileName;

            int uid = Convert.ToInt32(MalaGroupWebSession.CurrentUser.UserID);
            var viewedChat = db.tbl_ChatViewedBy.Where(p => p.CID == ID && p.ViewedById == uid).FirstOrDefault();
            if (viewedChat == null)
            {
                var saveViewdBy = new tbl_ChatViewedBy()
                {
                    CID = ID,
                    ViewedById = Convert.ToInt32(MalaGroupWebSession.CurrentUser.UserID),
                    ViewedDate = DateTime.Now,

                };
                db.tbl_ChatViewedBy.Add(saveViewdBy);
                db.SaveChanges();
            }

            return model;
        }
        public string SaveNewTrans(TransactionHistory model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            if (model.AOID == 0)
            {
                var orderSave = new tbl_AgentOrder()
                {
                    LeadID = Convert.ToInt64(model.LeadID),
                    AgentID = MalaGroupWebSession.CurrentUser.UserID,
                    StepCompleted = 8,
                    StartDate = Convert.ToDateTime(model.ChargeDate),
                    EndDate = Convert.ToDateTime(model.ChargeDate),
                    PinNo = model.PinNo,
                    IsCompleted = 1,
                    AccountID = model.AccountID,
                };
                db.tbl_AgentOrder.Add(orderSave);
                db.SaveChanges();
                model.AOID = orderSave.OrderID;
                var orderDetSave = new tbl_AgentOrderDetails()
                {
                    AOID = model.AOID,
                    VehicleMake = 0,
                    VehicleYear = "",
                    VehicleType = 0,
                    PackageId = Convert.ToInt32(model.PackageId),
                    TotalAmt = model.ChargeAmt,
                    FirstChargeDate = Convert.ToDateTime(model.ChargeDate),
                    AddDecals = 0,
                    IdentityTheft = 0,
                    PackageAmt = Convert.ToDecimal(model.ChargeAmt),
                    CompType = 0,

                };
                db.tbl_AgentOrderDetails.Add(orderDetSave);
                db.SaveChanges();
            }
            int transCount = db.tbl_OrderTransactions.Where(p => p.AOID == model.AOID).Count();
            transCount = transCount + 1;

            var cdData = new tbl_OrderTransactions
            {
                AOID = model.AOID,
                ChargeDate = Convert.ToDateTime(model.ChargeDate),
                Status = Convert.ToInt32(model.Status),
                ChargeNo = transCount,
                ChargeAmt = Convert.ToDecimal(model.ChargeAmt),
                CardCheckNumber = model.CardCheckNumber,
                AuthCode = model.AuthCode,
                TransactionID = model.TransactionID,
                TransType = Convert.ToInt32(model.TransType),
                RefundAmt = Convert.ToDecimal(model.ChargeAmt),
                StatusText = Convert.ToInt32(model.Status) == 1 ? "Approved" : Convert.ToInt32(model.Status) == 2 ? "Declined" : " ",
                PaymentMethod = 1
            };
            db.tbl_OrderTransactions.Add(cdData);
            db.SaveChanges();


            db.Dispose();
            return "Transaction Added";
        }
    }

}
public class EmailModel
{
    public long ID { get; set; }
    public Nullable<int> PageID { get; set; }
    public Nullable<long> AutoGenID { get; set; }
    public string FromEmail { get; set; }
    public string ToEmails { get; set; }
    public string CCEmails { get; set; }
    public string BCCEmails { get; set; }
    public string EmailSubject { get; set; }
    [AllowHtml]
    public string EmailHTMLBody { get; set; }
    public string EmailPlainText { get; set; }
    public Nullable<int> CreatedById { get; set; }
    public string CreatedDate { get; set; }
    public Nullable<int> ModifiedById { get; set; }
    public Nullable<System.DateTime> ModifiedDate { get; set; }
    public string AttachedFileName { get; set; }
    public string OriginalFileName { get; set; }
    public long AGID { get; set; }
}
public class AuditTrail
{
    public long ATID { get; set; }
    public string AuditDetails { get; set; }
    public Nullable<System.DateTime> DateUpdate { get; set; }
    public Nullable<int> UpdatedByUserID { get; set; }
    public Nullable<int> PageID { get; set; }
    public Nullable<long> AGID { get; set; }
}