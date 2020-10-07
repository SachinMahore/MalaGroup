using MalaGroupERP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using MalaGroupERP.Models;
using System.IO;

namespace MalaGroupERP.Models
{
    public class AgentOrderModel
    {

        public long OrderID { get; set; }
        public long AccountID { get; set; }
        public Nullable<int> AgentID { get; set; }
        public Nullable<int> StepCompleted { get; set; }
        public Nullable<int> FromStep { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string PinNo { get; set; }
        public string Password { get; set; }
        public long LeadID { get; set; }
        public string CompanyName { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public string LeadEmail { get; set; }
        public string ListCode { get; set; }

        public string ExpirationDate { get; set; }     
        public string Language { get; set; }   
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public Nullable<int> IsDiffBillingAdd { get; set; }
        public string BStreet { get; set; }
        public string BCity { get; set; }
        public string BState { get; set; }
        public string BZip { get; set; }
        public Nullable<int> PackageId { get; set; }
        public string Package { get; set; }
        public string AdditionalPackage { get; set; }
        public int VehicleMake { get; set; }
        public string VehicleMakeText { get; set; }
        public int VehicleType { get; set; }
        public string VehicleTypeText { get; set; }
        public string VehicleYear { get; set; }
        public string Dealership { get; set; }
        public Nullable<int> NoOfVehicle { get; set; }
        public Nullable<int> AddDecals { get; set; }
        public Nullable<int> IdentityTheft { get; set; }
        public string TotalAmt { get; set; }
        public string Price { get; set; }
        public string FirstChargeDate { get; set; }
        public int Status { get; set; }
        public int NoOfInstallment { get; set; }
        public int ChargeDay { get; set; }
        public long TransactionID { get; set; }
      
        public Nullable<int> CardType { get; set; }
        public Nullable<int> PaymentMethod { get; set; }
        public string CheckNumber { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardExpirationYear { get; set; }
        public string CardSecurityCode { get; set; }
        public Nullable<int> CompType { get; set; }
        public string OwnerText { get; set; }
        public string TransDate { get; set; }
        public string TransAmt { get; set; }
        public string CreatedBy { get; set; }
        public string CallHistory { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> AOStatus { get; set; }
        public Nullable<int> TooMuch { get; set; }
        public List<OrderTransactions> chargeDateList { get; set; }
        public List<VehicleLeadModel> VehicleAOList { get; set; }
        public Nullable<int> VehicleMake2 { get; set; }
        public Nullable<int> VehicleMake3 { get; set; }
        public Nullable<int> VehicleMake4 { get; set; }
        public string VehicleMakeText2 { get; set; }
        public string VehicleMakeText3 { get; set; }
        public string VehicleMakeText4 { get; set; }
        public string VehicleYear2 { get; set; }
        public string VehicleYear3 { get; set; }
        public string VehicleYear4 { get; set; }
        public string VINNo2 { get; set; }
        public string VINNo3 { get; set; }
        public string VINNo4 { get; set; }
        public string LicensePlate2 { get; set; }
        public string LicensePlate3 { get; set; }
        public string LicensePlate4 { get; set; }
        public AgentOrderModel GetLeadInfo(string PinNo)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            AgentOrderModel model = new AgentOrderModel();
           
                    model.LeadID = 0;
                    model.CompanyName = "";
                   
                    model.PrimaryPhone = "";
                    model.SecondaryPhone = "";
                    model.LeadEmail = "";
                    model.ListCode = "";
                    model.ExpirationDate = "";
                 
                    model.Language = "0";
                
                    model.Street = "";
                    model.City = "";
                    model.State = "";
                    model.Zip = "";
                    model.Country = "";

                    model.VehicleMake = 0;
                    model.VehicleType = 0;
                    model.VehicleYear = "";
                    model.PinNo = "";
                    model.FirstChargeDate =DateTime.Now.ToString("MM/dd/yyyy");
                    model.BStreet = "";
                    model.BCity = "";
                    model.BState = "";
                    model.BZip = "";
                    model.PackageId = 0;
                    model.Package = "";
                    model.IsDiffBillingAdd =0;
                    model.TotalAmt = "";
                    model.AdditionalPackage = "";
                    model.Password = "";

                    model.AccountID = 0;
                    model.TransDate = "";
                    model.TransAmt = "";
                    model.CreatedBy = "";
                    model.CallHistory = "";

                    model.FirstName = "";
                    model.LastName = "";

                    if (PinNo != "")
                    {
                        DataSet dsDataSet = new DataSet();
                        using (var cmd = db.Database.Connection.CreateCommand())
                        {
                            try
                            {
                                db.Database.Connection.Open();
                                cmd.CommandText = "usp_GetAgentLeadInfoData";
                                cmd.CommandType = CommandType.StoredProcedure;

                                DbParameter LID = cmd.CreateParameter();
                                LID.ParameterName = "PinNo";
                                LID.Value = PinNo;
                                cmd.Parameters.Add(LID);

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
                                DateTime? expirationDate = null;
                                try
                                {
                                    expirationDate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["ExpirationDate"].ToString());
                                }
                                catch
                                {

                                }
                                DateTime? firstChargeDate = null;
                                try
                                {
                                    firstChargeDate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["FirstChargeDate"].ToString());
                                }
                                catch
                                {
                                    firstChargeDate = DateTime.Now;
                                }
                                model.OrderID = long.Parse(dsDataSet.Tables[0].Rows[0]["OrderID"].ToString());
                                model.LeadID = long.Parse(dsDataSet.Tables[0].Rows[0]["LeadID"].ToString());
                                model.CompanyName = dsDataSet.Tables[0].Rows[0]["Name"].ToString();
                                model.FirstName = dsDataSet.Tables[0].Rows[0]["FirstName"].ToString();
                                model.LastName = dsDataSet.Tables[0].Rows[0]["LastName"].ToString();
                                model.Password = dsDataSet.Tables[0].Rows[0]["Password"].ToString();
                                model.PrimaryPhone = dsDataSet.Tables[0].Rows[0]["PrimaryPhone"].ToString();
                                model.SecondaryPhone = dsDataSet.Tables[0].Rows[0]["SecondaryPhone"].ToString();
                                model.LeadEmail = dsDataSet.Tables[0].Rows[0]["LeadEmail"].ToString();
                                model.ListCode = dsDataSet.Tables[0].Rows[0]["ListCode"].ToString();
                                model.ExpirationDate = (expirationDate.HasValue ? expirationDate.Value.ToString("MM/dd/yyyy") : "");
                             
                                model.Language = dsDataSet.Tables[0].Rows[0]["Language"].ToString();
                         
                                model.Street = dsDataSet.Tables[0].Rows[0]["Street"].ToString();
                                model.City = dsDataSet.Tables[0].Rows[0]["City"].ToString();
                                model.State = dsDataSet.Tables[0].Rows[0]["State"].ToString();
                                model.Zip = dsDataSet.Tables[0].Rows[0]["ZipCode"].ToString();
                                model.Country = dsDataSet.Tables[0].Rows[0]["Country"].ToString();

                                model.VehicleMake = Int32.Parse(dsDataSet.Tables[0].Rows[0]["VehicleMake"].ToString());
                                model.VehicleType = Int32.Parse(dsDataSet.Tables[0].Rows[0]["VehicleType"].ToString());
                                model.VehicleYear = dsDataSet.Tables[0].Rows[0]["VehicleYear"].ToString();
                                model.Dealership = dsDataSet.Tables[0].Rows[0]["Dealership"].ToString();
                                model.PinNo = dsDataSet.Tables[0].Rows[0]["PinNo"].ToString();
                                model.FirstChargeDate = (firstChargeDate.HasValue ? firstChargeDate.Value.ToString("MM/dd/yyyy") : "");
                                model.BStreet = dsDataSet.Tables[0].Rows[0]["BStreet"].ToString();
                                model.BCity = dsDataSet.Tables[0].Rows[0]["BCity"].ToString();
                                model.BState = dsDataSet.Tables[0].Rows[0]["BState"].ToString();
                                model.BZip = dsDataSet.Tables[0].Rows[0]["BZipCode"].ToString();
                                model.PackageId = Int32.Parse(dsDataSet.Tables[0].Rows[0]["PackageId"].ToString());
                                model.Package = dsDataSet.Tables[0].Rows[0]["Package"].ToString();
                                model.IsDiffBillingAdd = Int32.Parse(dsDataSet.Tables[0].Rows[0]["IsDiffBillingAdd"].ToString());
                                model.Price = (dsDataSet.Tables[0].Rows[0]["Price"].ToString() != "" ? dsDataSet.Tables[0].Rows[0]["Price"].ToString() : "0");
                                model.TotalAmt = dsDataSet.Tables[0].Rows[0]["TotalAmt"].ToString();
                                model.AdditionalPackage = dsDataSet.Tables[0].Rows[0]["AdditionalPackage"].ToString();
                                model.StepCompleted = Int32.Parse(dsDataSet.Tables[0].Rows[0]["StepCompleted"].ToString());
                                model.NoOfInstallment = Int32.Parse(dsDataSet.Tables[0].Rows[0]["NoOfInstallment"].ToString());
                                model.ChargeDay = Int32.Parse(dsDataSet.Tables[0].Rows[0]["ChargeDay"].ToString());
                                model.PaymentMethod = Int32.Parse(dsDataSet.Tables[0].Rows[0]["PaymentMode"].ToString());
                                model.CheckNumber = dsDataSet.Tables[0].Rows[0]["CheckNo"].ToString();
                                model.CardType = Int32.Parse(dsDataSet.Tables[0].Rows[0]["CardType"].ToString());
                                model.CardNumber = dsDataSet.Tables[0].Rows[0]["CardNumber"].ToString();
                                model.CardSecurityCode = dsDataSet.Tables[0].Rows[0]["CardSecurityCode"].ToString();
                                model.CardExpirationMonth = dsDataSet.Tables[0].Rows[0]["CardExpirationMonth"].ToString();
                                model.CardExpirationYear = dsDataSet.Tables[0].Rows[0]["CardExpirationYear"].ToString();
                                model.CompType = Int32.Parse(dsDataSet.Tables[0].Rows[0]["CompType"].ToString());

                                model.NoOfVehicle = Int32.Parse(dsDataSet.Tables[0].Rows[0]["NoOfVehicle"].ToString());
                                model.AddDecals = Int32.Parse(dsDataSet.Tables[0].Rows[0]["AddDecals"].ToString());
                                model.IdentityTheft = Int32.Parse(dsDataSet.Tables[0].Rows[0]["IdentityTheft"].ToString());
                                
                                model.OwnerText = dsDataSet.Tables[0].Rows[0]["LeadOwnerText"].ToString();

                                model.AccountID = long.Parse(dsDataSet.Tables[0].Rows[0]["AccountID"].ToString());
                                model.TransDate = dsDataSet.Tables[0].Rows[0]["TransDate"].ToString();
                                model.TransAmt = decimal.Parse(dsDataSet.Tables[0].Rows[0]["TransAmt"].ToString()).ToString("0.00");
                                model.CreatedBy = dsDataSet.Tables[0].Rows[0]["CreatedBy"].ToString();
                                model.CallHistory = dsDataSet.Tables[0].Rows[0]["CallHistory"].ToString();
                                model.AOStatus = Int32.Parse(dsDataSet.Tables[0].Rows[0]["AOStatus"].ToString());
                                model.TooMuch = Int32.Parse(dsDataSet.Tables[0].Rows[0]["TooMuch"].ToString());

                                model.VehicleMake2 = Int32.Parse(dsDataSet.Tables[0].Rows[0]["VehicleMake2"].ToString());
                                model.VehicleMake3 = Int32.Parse(dsDataSet.Tables[0].Rows[0]["VehicleMake3"].ToString());
                                model.VehicleMake4 = Int32.Parse(dsDataSet.Tables[0].Rows[0]["VehicleMake4"].ToString());
                                model.VehicleYear2 = dsDataSet.Tables[0].Rows[0]["VehicleYear2"].ToString();
                                model.VehicleYear3 = dsDataSet.Tables[0].Rows[0]["VehicleYear3"].ToString();
                                model.VehicleYear4 = dsDataSet.Tables[0].Rows[0]["VehicleYear4"].ToString();
                                model.VINNo2 = dsDataSet.Tables[0].Rows[0]["VINNo2"].ToString();
                                model.VINNo3 = dsDataSet.Tables[0].Rows[0]["VINNo3"].ToString();
                                model.VINNo4 = dsDataSet.Tables[0].Rows[0]["VINNo4"].ToString();
                                model.LicensePlate2 = dsDataSet.Tables[0].Rows[0]["LicensePlate2"].ToString();
                                model.LicensePlate3 = dsDataSet.Tables[0].Rows[0]["LicensePlate3"].ToString();
                                model.LicensePlate4 = dsDataSet.Tables[0].Rows[0]["LicensePlate4"].ToString();
                                model.VehicleMakeText2 = dsDataSet.Tables[0].Rows[0]["VehicleMakeText2"].ToString();
                                model.VehicleMakeText3 = dsDataSet.Tables[0].Rows[0]["VehicleMakeText3"].ToString();
                                model.VehicleMakeText4 = dsDataSet.Tables[0].Rows[0]["VehicleMakeText4"].ToString();
                                model.VehicleMakeText = dsDataSet.Tables[0].Rows[0]["VehicleMakeText"].ToString();

                                long lid = long.Parse(dsDataSet.Tables[0].Rows[0]["OrderID"].ToString());
                                string aoID = "0";
                                var orderExist = db.tbl_AgentOrder.Where(p => p.OrderID == lid && p.IsCompleted==0).FirstOrDefault();
                                if (orderExist == null)
                                {
                                    var orderSave = new tbl_AgentOrder()
                                    {
                                        LeadID = Convert.ToInt64(model.LeadID),
                                        AgentID = MalaGroupWebSession.CurrentUser.UserID,
                                        StepCompleted = 0,
                                        StartDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                                        EndDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                                        PinNo = model.PinNo,
                                        IsCompleted = 0
                                    };
                                    db.tbl_AgentOrder.Add(orderSave);
                                    db.SaveChanges();
                                    model.OrderID = orderSave.OrderID;
                                    aoID = orderSave.OrderID.ToString();


                                    var orderDetSave = new tbl_AgentOrderDetails()
                                    {
                                        AOID = Convert.ToInt64(aoID)

                                    };
                                    db.tbl_AgentOrderDetails.Add(orderDetSave);
                                    db.SaveChanges();

                                    var callHistorySave = new tbl_CallHistory()
                                    {
                                        LeadID = Convert.ToInt64(model.LeadID),
                                        PageID = 1,
                                        AOID = Convert.ToInt64(aoID),
                                        AgentID = MalaGroupWebSession.CurrentUser.UserID,
                                        CallDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"))

                                    };
                                    db.tbl_CallHistory.Add(callHistorySave);
                                    db.SaveChanges();

                                    db.Dispose();

                                }
                                else
                                {
                                    var callHistorySave = new tbl_CallHistory()
                                    {
                                        LeadID = Convert.ToInt64(orderExist.LeadID),
                                        PageID=1,
                                        AOID = Convert.ToInt64(orderExist.OrderID),
                                        AgentID = MalaGroupWebSession.CurrentUser.UserID,
                                        CallDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"))

                                    };
                                    db.tbl_CallHistory.Add(callHistorySave);
                                    db.SaveChanges();

                                }
                                db.Dispose();
                            }
                        }
                    }
                   
              
                return model;
            
        }
        public AgentOrderModel GetRenewalLeadInfo(string PinNo)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            AgentOrderModel model = new AgentOrderModel();

            model.LeadID = 0;
            model.CompanyName = "";

            model.PrimaryPhone = "";
            model.SecondaryPhone = "";
            model.LeadEmail = "";
            model.ListCode = "";
            model.ExpirationDate = "";

            model.Language = "0";

            model.Street = "";
            model.City = "";
            model.State = "";
            model.Zip = "";
            model.Country = "";

            model.VehicleMake = 0;
            model.VehicleType = 0;
            model.VehicleYear = "";
            model.PinNo = "";
            model.FirstChargeDate = DateTime.Now.ToString("MM/dd/yyyy");
            model.BStreet = "";
            model.BCity = "";
            model.BState = "";
            model.BZip = "";
            model.PackageId = 0;
            model.Package = "";
            model.IsDiffBillingAdd = 0;
            model.TotalAmt = "";
            model.AdditionalPackage = "";
            model.Password = "";

            model.AccountID = 0;
            model.TransDate = "";
            model.TransAmt = "";
            model.CreatedBy = "";
            model.CallHistory = "";

            model.FirstName = "";
            model.LastName = "";

            if (PinNo != "")
            {
                DataSet dsDataSet = new DataSet();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetRenewalLeadInfoData";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter LID = cmd.CreateParameter();
                        LID.ParameterName = "PinNo";
                        LID.Value = PinNo;
                        cmd.Parameters.Add(LID);

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
                        DateTime? expirationDate = null;
                        try
                        {
                            expirationDate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["ExpirationDate"].ToString());
                        }
                        catch
                        {

                        }
                        DateTime? firstChargeDate = null;
                        try
                        {
                            firstChargeDate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["FirstChargeDate"].ToString());
                        }
                        catch
                        {
                            firstChargeDate = DateTime.Now;
                        }
                        model.OrderID = long.Parse(dsDataSet.Tables[0].Rows[0]["OrderID"].ToString());
                        model.LeadID = long.Parse(dsDataSet.Tables[0].Rows[0]["LeadID"].ToString());
                        model.CompanyName = dsDataSet.Tables[0].Rows[0]["Name"].ToString();
                        model.FirstName = dsDataSet.Tables[0].Rows[0]["FirstName"].ToString();
                        model.LastName = dsDataSet.Tables[0].Rows[0]["LastName"].ToString();
                        model.Password = dsDataSet.Tables[0].Rows[0]["Password"].ToString();
                        model.PrimaryPhone = dsDataSet.Tables[0].Rows[0]["PrimaryPhone"].ToString();
                        model.SecondaryPhone = dsDataSet.Tables[0].Rows[0]["SecondaryPhone"].ToString();
                        model.LeadEmail = dsDataSet.Tables[0].Rows[0]["LeadEmail"].ToString();
                        model.ListCode = dsDataSet.Tables[0].Rows[0]["ListCode"].ToString();
                        model.ExpirationDate = (expirationDate.HasValue ? expirationDate.Value.ToString("MM/dd/yyyy") : "");

                        model.Language = dsDataSet.Tables[0].Rows[0]["Language"].ToString();

                        model.Street = dsDataSet.Tables[0].Rows[0]["Street"].ToString();
                        model.City = dsDataSet.Tables[0].Rows[0]["City"].ToString();
                        model.State = dsDataSet.Tables[0].Rows[0]["State"].ToString();
                        model.Zip = dsDataSet.Tables[0].Rows[0]["ZipCode"].ToString();
                        model.Country = dsDataSet.Tables[0].Rows[0]["Country"].ToString();

                        model.VehicleMake = Int32.Parse(dsDataSet.Tables[0].Rows[0]["VehicleMake"].ToString());
                        model.VehicleType = Int32.Parse(dsDataSet.Tables[0].Rows[0]["VehicleType"].ToString());
                        model.VehicleYear = dsDataSet.Tables[0].Rows[0]["VehicleYear"].ToString();
                        model.Dealership = dsDataSet.Tables[0].Rows[0]["Dealership"].ToString();
                        model.PinNo = dsDataSet.Tables[0].Rows[0]["PinNo"].ToString();
                        model.FirstChargeDate = (firstChargeDate.HasValue ? firstChargeDate.Value.ToString("MM/dd/yyyy") : "");
                        model.BStreet = dsDataSet.Tables[0].Rows[0]["BStreet"].ToString();
                        model.BCity = dsDataSet.Tables[0].Rows[0]["BCity"].ToString();
                        model.BState = dsDataSet.Tables[0].Rows[0]["BState"].ToString();
                        model.BZip = dsDataSet.Tables[0].Rows[0]["BZipCode"].ToString();
                        model.PackageId = Int32.Parse(dsDataSet.Tables[0].Rows[0]["PackageId"].ToString());
                        model.Package = dsDataSet.Tables[0].Rows[0]["Package"].ToString();
                        model.IsDiffBillingAdd = Int32.Parse(dsDataSet.Tables[0].Rows[0]["IsDiffBillingAdd"].ToString());
                        model.Price = (dsDataSet.Tables[0].Rows[0]["Price"].ToString() != "" ? dsDataSet.Tables[0].Rows[0]["Price"].ToString() : "0");
                        model.TotalAmt = dsDataSet.Tables[0].Rows[0]["TotalAmt"].ToString();
                       
                        model.PaymentMethod = Int32.Parse(dsDataSet.Tables[0].Rows[0]["PaymentMode"].ToString());
                        model.CheckNumber = dsDataSet.Tables[0].Rows[0]["CheckNo"].ToString();
                        model.CardType = Int32.Parse(dsDataSet.Tables[0].Rows[0]["CardType"].ToString());
                        model.CardNumber = dsDataSet.Tables[0].Rows[0]["CardNumber"].ToString();
                        model.CardSecurityCode = dsDataSet.Tables[0].Rows[0]["CardSecurityCode"].ToString();
                        model.CardExpirationMonth = dsDataSet.Tables[0].Rows[0]["CardExpirationMonth"].ToString();
                        model.CardExpirationYear = dsDataSet.Tables[0].Rows[0]["CardExpirationYear"].ToString();
                        model.CompType = Int32.Parse(dsDataSet.Tables[0].Rows[0]["CompType"].ToString());

                       
                        model.AccountID = long.Parse(dsDataSet.Tables[0].Rows[0]["AccountID"].ToString());
                        model.TransDate = dsDataSet.Tables[0].Rows[0]["TransDate"].ToString();
                        model.TransAmt = decimal.Parse(dsDataSet.Tables[0].Rows[0]["TransAmt"].ToString()).ToString("0.00");
                        model.CreatedBy = dsDataSet.Tables[0].Rows[0]["CreatedBy"].ToString();
                        model.CallHistory = dsDataSet.Tables[0].Rows[0]["CallHistory"].ToString();
                        model.AOStatus = Int32.Parse(dsDataSet.Tables[0].Rows[0]["AOStatus"].ToString());

                       
                        model.VehicleMakeText = dsDataSet.Tables[0].Rows[0]["VehicleMakeText"].ToString();

                        //long lid = long.Parse(dsDataSet.Tables[0].Rows[0]["OrderID"].ToString());
                        //string aoID = "0";
                        //var orderExist = db.tbl_AgentOrder.Where(p => p.OrderID == lid && p.IsCompleted == 0).FirstOrDefault();
                        //if (orderExist == null)
                        //{
                        //    var orderSave = new tbl_AgentOrder()
                        //    {
                        //        LeadID = Convert.ToInt64(model.LeadID),
                        //        AgentID = MalaGroupWebSession.CurrentUser.UserID,
                        //        StepCompleted = 0,
                        //        StartDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                        //        EndDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                        //        PinNo = model.PinNo,
                        //        IsCompleted = 0
                        //    };
                        //    db.tbl_AgentOrder.Add(orderSave);
                        //    db.SaveChanges();
                        //    model.OrderID = orderSave.OrderID;
                        //    aoID = orderSave.OrderID.ToString();


                        //    var orderDetSave = new tbl_AgentOrderDetails()
                        //    {
                        //        AOID = Convert.ToInt64(aoID)

                        //    };
                        //    db.tbl_AgentOrderDetails.Add(orderDetSave);
                        //    db.SaveChanges();

                        //    var callHistorySave = new tbl_CallHistory()
                        //    {
                        //        LeadID = Convert.ToInt64(model.LeadID),
                        //        PageID = 1,
                        //        AOID = Convert.ToInt64(aoID),
                        //        AgentID = MalaGroupWebSession.CurrentUser.UserID,
                        //        CallDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"))

                        //    };
                        //    db.tbl_CallHistory.Add(callHistorySave);
                        //    db.SaveChanges();

                        //    db.Dispose();

                        //}
                        //else
                        //{
                        //    var callHistorySave = new tbl_CallHistory()
                        //    {
                        //        LeadID = Convert.ToInt64(orderExist.LeadID),
                        //        PageID = 1,
                        //        AOID = Convert.ToInt64(orderExist.OrderID),
                        //        AgentID = MalaGroupWebSession.CurrentUser.UserID,
                        //        CallDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"))

                        //    };
                        //    db.tbl_CallHistory.Add(callHistorySave);
                        //    db.SaveChanges();

                        //}
                        //db.Dispose();
                    }
                }
            }


            return model;

        }
        public string UpdateAgentLead(AgentOrderModel model)
        {
            string aoID = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            if (model.LeadID != 0)
            {

                var leadUpdate = db.tbl_LeadInformation.Where(p => p.LeadID == model.LeadID).FirstOrDefault();
                var aoUpdate = db.tbl_AgentOrderDetails.Where(p => p.AOID == model.OrderID).FirstOrDefault();
                leadUpdate.PrimaryPhone = model.PrimaryPhone;
                leadUpdate.SecondaryPhone = model.SecondaryPhone;
                leadUpdate.Language = model.Language;
                leadUpdate.FirstName = model.FirstName;
                leadUpdate.LastName = model.LastName;
                leadUpdate.Name = model.FirstName+" "+model.LastName;

                if (leadUpdate.LeadStatus <7)
                {
                    leadUpdate.LeadStatus = 4;
                }
                if(!string.IsNullOrWhiteSpace(leadUpdate.CreatedById))
                {
                    leadUpdate.CreatedById = MalaGroupWebSession.CurrentUser.UserID.ToString();
                    leadUpdate.CreatedDate = DateTime.Now;
                }

                leadUpdate.LastModifiedById = MalaGroupWebSession.CurrentUser.UserID.ToString();
                leadUpdate.LastModifiedDate = DateTime.Now;

                if (model.StepCompleted == 2)
                {
                    aoUpdate.VehicleMake = model.VehicleMake;
                    aoUpdate.VehicleType = model.VehicleType;
                    aoUpdate.VehicleYear = model.VehicleYear;
                    var vehicleExist = db.tbl_VehicleLeads.Where(p => p.LeadID == model.LeadID && p.VehicleMake == model.VehicleMake && p.VehicleType==model.VehicleType && p.VehicleYear == model.VehicleYear).FirstOrDefault();
                    if(vehicleExist!=null)
                    {
                        aoUpdate.VINNo = vehicleExist.VINNo;
                        aoUpdate.LicensePlate = vehicleExist.LicensePlate;
                        aoUpdate.FinanceCompany = vehicleExist.FinanceCompany;
                        aoUpdate.Dealership = vehicleExist.Dealership;
                        
                    }
                }
                if (model.StepCompleted == 3)
                {
                    leadUpdate.AOStatus = model.AOStatus;
                    aoUpdate.PackageId = Convert.ToInt32(model.PackageId);
                    aoUpdate.AdditionalPackage = model.AdditionalPackage;
                   aoUpdate.TotalAmt = model.TotalAmt;
                   aoUpdate.PackageAmt =Convert.ToDecimal(model.Price);
                   if (model.AOStatus == 3)
                   {
                       leadUpdate.Take_Off_List__c = 1;
                       leadUpdate.LeadStatus = 3;
                   }
                   if (model.AOStatus == 1)
                   {
                       aoUpdate.TooMuch = 1;
                   }
                }
                if (model.StepCompleted == 4)
                {
                    leadUpdate.LeadEmail = model.LeadEmail;
                    leadUpdate.Street = model.Street;
                    leadUpdate.City = model.City;
                    leadUpdate.State = model.State;
                    leadUpdate.ZipCode = model.Zip;
                }
                if (model.StepCompleted == 5)
                {
                    aoUpdate.FirstChargeDate = Convert.ToDateTime(model.FirstChargeDate);
                    aoUpdate.IsDiffBillingAdd = model.IsDiffBillingAdd;
                    aoUpdate.BStreet = model.BStreet;
                    aoUpdate.BCity = model.BCity;
                    aoUpdate.BState = model.BState;
                    aoUpdate.BZipCode = model.BZip;
                    leadUpdate.LeadStatus = model.Status;
                    aoUpdate.ChargeDay = model.ChargeDay;

                    if (model.chargeDateList != null)
                    {
                        var dateExist = db.tbl_OrderTransactions.Where(p => p.AOID == model.OrderID);
                        db.tbl_OrderTransactions.RemoveRange(dateExist);
                        foreach (var cd in model.chargeDateList)
                        {
                            var cdData = new tbl_OrderTransactions
                            {
                                AOID = model.OrderID,
                                ChargeDate = cd.ChargeDate,
                                Status = 0,
                                ChargeNo = cd.ChargeNo,
                                ChargeAmt = cd.ChargeAmt,
                                MailSend=0,
                            };
                            db.tbl_OrderTransactions.Add(cdData);
                            db.SaveChanges();
                        }
                    }

                }
                if (model.StepCompleted == 7)
                {
                    aoUpdate.NoOfVehicle = model.NoOfVehicle;
                    aoUpdate.AddDecals = model.AddDecals;
                    aoUpdate.IdentityTheft = model.IdentityTheft;
                    aoUpdate.TotalAmt = model.TotalAmt;

                    var orderTrans = db.tbl_OrderTransactions.Where(p => p.AOID == model.OrderID && p.ChargeNo == 1).FirstOrDefault();
                    if (orderTrans != null)
                    {
                        orderTrans.ChargeAmt = Convert.ToDecimal(model.TotalAmt);
                        db.SaveChanges();
                    }

                    //if (model.VehicleAOList != null)
                    //{
                    //    var aovehiExist = db.tbl_VehicleAO.Where(p => p.AOID == model.OrderID);
                    //    db.tbl_VehicleAO.RemoveRange(aovehiExist);
                    //    foreach (var vl in model.VehicleAOList)
                    //    {
                    //        var vData = new tbl_VehicleAO
                    //        {
                    //            AOID = model.OrderID,
                    //            VehicleMake = vl.VehicleMake,
                    //            VehicleType = vl.VehicleType,
                    //            VehicleYear = vl.VehicleYear,
                    //            VINNo = vl.VINNO,
                    //            LicensePlate = vl.LicensePlate,
                    //            Dealership = vl.DealerShip,
                    //            FinanceCompany = vl.FinanceCompany
                    //        };
                    //        db.tbl_VehicleAO.Add(vData);
                    //        db.SaveChanges();
                    //    };
                    //}

                    //Added 25012019
                    aoUpdate.VehicleMake2 = model.VehicleMake2;
                    aoUpdate.VehicleMake3 = model.VehicleMake3;
                    aoUpdate.VehicleMake3 = model.VehicleMake3;
                    aoUpdate.VehicleYear2 = model.VehicleYear2;
                    aoUpdate.VehicleYear3 = model.VehicleYear3;
                    aoUpdate.VehicleYear3 = model.VehicleYear3;
                    aoUpdate.VINNo2 = model.VINNo2;
                    aoUpdate.VINNo3 = model.VINNo3;
                    aoUpdate.VINNo4 = model.VINNo4;
                    aoUpdate.LicensePlate2 = model.LicensePlate2;
                    aoUpdate.LicensePlate3 = model.LicensePlate3;
                    aoUpdate.LicensePlate4 = model.LicensePlate4;
                }


                db.SaveChanges();
                aoID = model.OrderID.ToString();
                var orderUpdate = db.tbl_AgentOrder.Where(p => p.LeadID == model.LeadID && p.IsCompleted==0).FirstOrDefault();
                orderUpdate.StepCompleted = model.StepCompleted;
                orderUpdate.AgentID = MalaGroupWebSession.CurrentUser.UserID;
                orderUpdate.EndDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                db.SaveChanges();
            }
            db.Dispose();
            return aoID;
        }
        public LeadToAccount CheckLeadOrder(string PinNo)
        {
            AgentOrderModel model = new AgentOrderModel().GetLeadInfo(PinNo);

            LeadToAccount lamodel = new LeadToAccount();
            lamodel.OrderID = model.OrderID;
            lamodel.ChargeDate = model.FirstChargeDate.ToString();
            lamodel.VehicleText = model.VehicleMakeText;
            lamodel.VehicleYear = model.VehicleYear;
            lamodel.PackageName = model.Package;
            lamodel.TotalAmt = model.TotalAmt;
            lamodel.CardCheckNumber = model.CheckNumber;
            lamodel.CompType = model.CompType == 1 ? "COMP - MNGR" : model.CompType == 2 ? "COMP - HARDSHIP" : model.CompType == 3 ? "Comp - USA" : model.CompType == 4 ? "COMP - LAW/GOV/1ST REP":"";
            lamodel.LeadID = model.LeadID;
            return lamodel;

        }
        public string SaveLeadToAccount(LeadToAccount lamodel)
        {
            string aoID = "";
             string accountID = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
           // AgentOrderModel model = new AgentOrderModel().GetLeadInfo(lamodel.PinNo); ;
            try
            {
                if (lamodel.LeadID != 0)
                {
                    if (lamodel.AccType == 1)
                    {
                        var orderSave = new tbl_AgentOrder()
                          {
                              LeadID = Convert.ToInt64(lamodel.LeadID),
                              AgentID = MalaGroupWebSession.CurrentUser.UserID,
                              StepCompleted = 8,
                              StartDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                              EndDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                              PinNo = lamodel.PinNo,
                              IsCompleted = 1
                          };
                        db.tbl_AgentOrder.Add(orderSave);
                        db.SaveChanges();
                        lamodel.OrderID = orderSave.OrderID;
                        aoID = orderSave.OrderID.ToString();

                        var orderDetSave = new tbl_AgentOrderDetails()
                        {
                            AOID = Convert.ToInt64(aoID),
                            VehicleMake = lamodel.VehicleMake,
                            VehicleYear = lamodel.VehicleYear,
                            VehicleType = 0,
                            PackageId = Convert.ToInt32(lamodel.PackageId),
                            AdditionalPackage = lamodel.AdditionalPackage,
                            TotalAmt = lamodel.TotalAmt,
                            FirstChargeDate = Convert.ToDateTime(lamodel.FirstChargeDate),
                            AddDecals = lamodel.AddDecals,
                            IdentityTheft = lamodel.IdentityTheft,
                            PackageAmt = Convert.ToDecimal(lamodel.TotalAmt),
                            BCity = lamodel.City,
                            BState = lamodel.State,
                            BStreet = lamodel.Street,
                            BZipCode = lamodel.Zip,
                            CompType = 0,

                        };
                        db.tbl_AgentOrderDetails.Add(orderDetSave);
                        db.SaveChanges();

                        var callHistorySave = new tbl_CallHistory()
                        {
                            LeadID = Convert.ToInt64(lamodel.LeadID),
                            PageID = 1,
                            AOID = Convert.ToInt64(aoID),
                            AgentID = MalaGroupWebSession.CurrentUser.UserID,
                            CallDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"))

                        };
                        db.tbl_CallHistory.Add(callHistorySave);
                        db.SaveChanges();

                        var aoUpdate = db.tbl_AgentOrderDetails.Where(p => p.AOID == lamodel.OrderID).FirstOrDefault();
                        var vehicleExist = db.tbl_VehicleLeads.Where(p => p.LeadID == lamodel.LeadID && p.VehicleMake == lamodel.VehicleMake && p.VehicleYear == lamodel.VehicleYear).FirstOrDefault();
                        if (vehicleExist != null)
                        {
                            aoUpdate.VINNo = vehicleExist.VINNo;
                            aoUpdate.LicensePlate = vehicleExist.LicensePlate;
                            aoUpdate.FinanceCompany = vehicleExist.FinanceCompany;
                            aoUpdate.Dealership = vehicleExist.Dealership;

                        }
                        var transData = new tbl_OrderCardCheckInfo()
                        {
                            CardType = Convert.ToInt32(lamodel.CardType),
                            CardNumber = lamodel.CardCheckNumber,
                            PaymentMode = 1,
                            //CardExpirationMonth = lamodel.CardExpirationMonth,
                            //CardExpirationYear = lamodel.CardExpirationYear,
                            //CardSecurityCode = lamodel.CardSecurityCode,
                            AOID = Convert.ToInt64(aoID),
                            LeadID = Convert.ToInt64(lamodel.LeadID),
                            IsDefault=1,
                        };
                        db.tbl_OrderCardCheckInfo.Add(transData);
                        db.SaveChanges();

                        var cdData = new tbl_OrderTransactions
                        {
                            AOID = Convert.ToInt64(aoID),
                            ChargeDate = Convert.ToDateTime(lamodel.FirstChargeDate),
                            Status = 1,
                            ChargeNo = 1,
                            PaymentMethod = 1,

                            ChargeAmt = Convert.ToDecimal(lamodel.TotalAmt),
                            TransactionID = lamodel.TransactionID,
                            AuthCode = lamodel.AuthCode,
                            RefundAmt = Convert.ToDecimal(lamodel.TotalAmt),
                            CardCheckNumber = lamodel.CardCheckNumber,
                            CardType = lamodel.CardType,
                            ResonText = "This transaction has been approved.",
                            StatusText = "Approved",
                            TransType = 1,
                            GatwayResponse = Convert.ToDateTime(lamodel.FirstChargeDate).ToString("MM/dd/yyyy") + "|Approved|This transaction has been approved.|" + lamodel.TransactionID,

                        };
                        db.tbl_OrderTransactions.Add(cdData);
                        db.SaveChanges();

                        var acctExist = db.tbl_Accounts.Where(p => p.PinNo == lamodel.PinNo).FirstOrDefault();
                        if (acctExist == null)
                        {
                            var acctSave = new tbl_Accounts()
                            {
                                AccountName = lamodel.AccountName,
                                Phone = lamodel.Phone,
                                AccountOwner = MalaGroupWebSession.CurrentUser.UserID,
                                BillingAddress = lamodel.Street,
                                BillingCity = lamodel.City,
                                BillingState = lamodel.State,
                                BillingZip = lamodel.Zip,
                                BillingCountry = lamodel.Country,
                                ShippingAddress = lamodel.Street,
                                ShippingCity = lamodel.City,
                                ShippingCountry = lamodel.Country,
                                ShippingState = lamodel.State,
                                ShippingZip = lamodel.Zip,
                                PinNo = lamodel.PinNo,
                                Password = lamodel.Password,
                                LeadID = lamodel.LeadID,
                                AccountStatus = lamodel.AccountStatus,
                                CreatedDate = DateTime.Now,
                                CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                                LastModifiedDate = DateTime.Now,
                                LastModifiedById = MalaGroupWebSession.CurrentUser.UserID,
                                IsDeleted = 0,
                                Type = 0,
                                SalesDate = Convert.ToDateTime(lamodel.FirstChargeDate),
                                EmailId=lamodel.LeadEmail,
                            };
                            db.tbl_Accounts.Add(acctSave);
                            db.SaveChanges();
                            lamodel.AccountID = acctSave.ID;
                            accountID = acctSave.ID.ToString();

                            var orderUpdate = db.tbl_AgentOrder.Where(p => p.OrderID == lamodel.OrderID).FirstOrDefault();
                            if (orderUpdate != null)
                            {

                                orderUpdate.EndDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                                orderUpdate.AccountID = Convert.ToInt64(accountID);
                                db.SaveChanges();
                            }

                            var leadInfo = db.tbl_LeadInformation.Where(p => p.LeadID == lamodel.LeadID).FirstOrDefault();
                            if (leadInfo != null)
                            {
                                leadInfo.LeadStatus = 1;
                                leadInfo.AccountID = Convert.ToInt64(accountID);
                                leadInfo.LeadEmail = lamodel.LeadEmail;
                                db.SaveChanges();
                            }
                            var chatNew = new tbl_Chattter()
                            {
                                AccountId = accountID,
                                Body = "Account Created against Online",
                                CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                                InsertedById = MalaGroupWebSession.CurrentUser.UserID,
                                LeadID = lamodel.LeadID,
                                Type = 1,
                                CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                                OriginalFileName = "",
                                SystemFileName = ""

                            };
                            db.tbl_Chattter.Add(chatNew);
                            db.SaveChanges();

                            var chatinfo = db.tbl_Chattter.Where(p => p.LeadID == lamodel.LeadID).ToList();
                            if (chatinfo != null)
                            {
                                foreach (var toc in chatinfo)
                                {
                                    toc.AccountId = accountID;
                                    db.SaveChanges();
                                }
                            }

                            //string filePath = HttpContext.Current.Server.MapPath("~/EmailTemplate");
                            //var bodyTemplate = File.ReadAllText(filePath + "\\transaction_success.html");
                            //var body = "";
                            //var emailSubject = "";
                            //if (lamodel.LeadEmail != null)
                            //{
                            //    body = bodyTemplate;
                            //    body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now registered into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services.");
                            //    body = body.Replace("{AccountFullName}", lamodel.AccountName);
                            //    body = body.Replace("{BillingAddress}", lamodel.Street);
                            //    body = body.Replace("{BillingCity}", lamodel.City);
                            //    body = body.Replace("{BillingState}",lamodel.State);
                            //    body = body.Replace("{BillingZip}", lamodel.Zip);
                            //    body = body.Replace("{CustomerEmail}", lamodel.LeadEmail);
                            //    body = body.Replace("{PrimaryPhone}", lamodel.Phone);

                            //    body = body.Replace("{ShippingAddress}", lamodel.Street);
                            //    body = body.Replace("{ShippingCity}", lamodel.City);
                            //    body = body.Replace("{ShippingState}", lamodel.State);
                            //    body = body.Replace("{ShippingZip}", lamodel.Zip);

                            //    body = body.Replace("{TodaysChargeAmount}", lamodel.TotalAmt);
                            //    body = body.Replace("{BalanceDue}", "0.00");
                            //    body = body.Replace("{ChargeDateTime}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));

                            //    body = body.Replace("{TransactionID}", lamodel.TransactionID);
                            //    body = body.Replace("{CardLastDigit}", lamodel.CardCheckNumber);
                            //    body = body.Replace("{TransactionType}", "Charge");
                            //    body = body.Replace("{TransactionTypeHeader}", "CHARGE");
                            //    body = body.Replace("{AuthCode}", lamodel.AuthCode);

                            //    emailSubject = "Transaction CHARGE from National Theft Search and Recovery for " + lamodel.TotalAmt + " (USD)";
                            //}
                            //else
                            //{
                            //    body = "";
                            //}

                            //if (body != "")
                            //{
                            //    new CommonModel().SendEmailPayments(lamodel.LeadEmail, emailSubject, body, Convert.ToInt64(accountID));
                            //    body = "";
                            //}
                        }
                    }

                    else
                    {
                        var aoUpdate = db.tbl_AgentOrderDetails.Where(p => p.AOID == lamodel.OrderID).FirstOrDefault();
                        var acctExist = db.tbl_Accounts.Where(p => p.PinNo == lamodel.PinNo).FirstOrDefault();
                        if (acctExist == null)
                        {
                            var acctSave = new tbl_Accounts()
                            {
                                AccountName = lamodel.AccountName,
                                Phone = lamodel.Phone,
                                AccountOwner = MalaGroupWebSession.CurrentUser.UserID,
                                BillingAddress = lamodel.Street,
                                BillingCity = lamodel.City,
                                BillingState = lamodel.State,
                                BillingZip = lamodel.Zip,
                                BillingCountry = lamodel.Country,
                                ShippingAddress = lamodel.Street,
                                ShippingCity = lamodel.City,
                                ShippingCountry = lamodel.Country,
                                ShippingState = lamodel.State,
                                ShippingZip = lamodel.Zip,
                                PinNo = lamodel.PinNo,
                                Password = lamodel.Password,
                                LeadID = lamodel.LeadID,
                                AccountStatus = 1,
                                CreatedDate = DateTime.Now,
                                CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                                LastModifiedDate = DateTime.Now,
                                LastModifiedById = MalaGroupWebSession.CurrentUser.UserID,
                                IsDeleted = 0,
                                Type =aoUpdate.CompType,
                                SalesDate = Convert.ToDateTime(lamodel.FirstChargeDate),
                            };
                            db.tbl_Accounts.Add(acctSave);
                            db.SaveChanges();
                            lamodel.AccountID = acctSave.ID;
                            accountID = acctSave.ID.ToString();

                            var orderUpdate = db.tbl_AgentOrder.Where(p => p.OrderID == lamodel.OrderID).FirstOrDefault();
                            if (orderUpdate != null)
                            {

                                orderUpdate.EndDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                                orderUpdate.AccountID = Convert.ToInt64(accountID);
                                orderUpdate.IsCompleted = 1;
                                orderUpdate.StepCompleted = 8;
                                db.SaveChanges();
                            }
                            var transUpdate = db.tbl_OrderTransactions.Where(p => p.AOID == lamodel.OrderID).FirstOrDefault();
                            if (transUpdate != null)
                            {
                                transUpdate.Status = 1;
                                transUpdate.StatusText = "Approved";
                               
                                db.SaveChanges();
                            }
                            var leadInfo = db.tbl_LeadInformation.Where(p => p.LeadID == lamodel.LeadID).FirstOrDefault();
                            if (leadInfo != null)
                            {
                                leadInfo.LeadStatus = 1;
                                leadInfo.AccountID = Convert.ToInt64(accountID);
                                leadInfo.LastModifiedDate=Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                                db.SaveChanges();
                            }

                            var chatNew = new tbl_Chattter()
                            {
                                AccountId = accountID,
                                Body = transUpdate.TransType ==6 ? "Account Created against Check Payment" : "Account Created against Comp",
                                CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                                InsertedById = MalaGroupWebSession.CurrentUser.UserID,
                                LeadID =lamodel.LeadID,
                                Type=1,
                                CreatedDate=Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                                OriginalFileName="",
                                SystemFileName=""

                            };
                            db.tbl_Chattter.Add(chatNew);
                            db.SaveChanges();

                            var chatinfo = db.tbl_Chattter.Where(p => p.LeadID == lamodel.LeadID).ToList();
                            if (chatinfo != null)
                            {
                                foreach (var toc in chatinfo)
                                {
                                    toc.AccountId = accountID;
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            db.Dispose();
            return accountID;
        }
        public List<VehicleLeadModel> GetVehicleAOInfo(long AOID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                List<VehicleLeadModel> returnVehcileData = new List<VehicleLeadModel>();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetAOVehicleInfoData";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramDN = cmd.CreateParameter();
                        paramDN.ParameterName = "AOID";
                        paramDN.Value = AOID;
                        cmd.Parameters.Add(paramDN);



                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            returnVehcileData.Add(new VehicleLeadModel()
                            {
                                LeadID = Convert.ToInt64(dr["AOID"].ToString()),
                                VehicleMake = Convert.ToInt32(dr["VehicleMake"].ToString()),
                                VehicleMakeText = dr["VehicleMakeText"].ToString(),
                                VehicleType = Convert.ToInt32(dr["VehicleType"].ToString()),
                                VehicleTypeText = dr["VehicleTypeText"].ToString(),
                                VehicleYear = dr["VehicleYear"].ToString(),
                                VINNO = dr["VINNo"].ToString(),
                                LicensePlate = dr["LicensePlate"].ToString(),
                                DealerShip = dr["Dealership"].ToString(),
                                FinanceCompany = dr["FinanceCompany"].ToString()
                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
                return returnVehcileData.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public AgentOrderModel GetVehicleLeadInfo(int VehicleMake, int VehicleType)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                AgentOrderModel model = new AgentOrderModel();
                DataSet dsDataSet = new DataSet();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetVehicleMakeModel";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramDN = cmd.CreateParameter();
                        paramDN.ParameterName = "VehicleMake";
                        paramDN.Value = VehicleMake;
                        cmd.Parameters.Add(paramDN);

                        DbParameter paramVT = cmd.CreateParameter();
                        paramVT.ParameterName = "VehicleType";
                        paramVT.Value = VehicleType;
                        cmd.Parameters.Add(paramVT);


                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsDataSet);
                        db.Database.Connection.Close();

                        if (dsDataSet.Tables[0] != null )
                        {

                            model.VehicleMakeText = dsDataSet.Tables[0].Rows[0]["VehicleMakeText"].ToString();

                            model.VehicleTypeText = dsDataSet.Tables[0].Rows[0]["VehicleTypeText"].ToString();
                           
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
        public List<VehicleLeadModel> GetVehicleLeadDetInfo(long VehicleID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                List<VehicleLeadModel> returnVehcileData = new List<VehicleLeadModel>();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetLeadVehicleDetInfoData";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramDN = cmd.CreateParameter();
                        paramDN.ParameterName = "VehicleID";
                        paramDN.Value = VehicleID;
                        cmd.Parameters.Add(paramDN);



                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            returnVehcileData.Add(new VehicleLeadModel()
                            {
                                ID = Convert.ToInt64(dr["ID"].ToString()),
                                LeadID = Convert.ToInt64(dr["LeadID"].ToString()),
                                VehicleMake = Convert.ToInt32(dr["VehicleMake"].ToString()),
                                VehicleMakeText = dr["VehicleMakeText"].ToString(),
                                VehicleType = Convert.ToInt32(dr["VehicleType"].ToString()),
                                VehicleTypeText = dr["VehicleTypeText"].ToString(),
                              
                                VehicleYear = dr["VehicleYear"].ToString()
                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
                return returnVehcileData.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<VehicleLeadModel> GetVehicleInfo(long ID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                List<VehicleLeadModel> returnVehcileData = new List<VehicleLeadModel>();
                
                var vehicleData = db.tbl_VehicleLeads.Where(m => m.ID == ID).FirstOrDefault();
                 returnVehcileData.Add(new VehicleLeadModel()
                    {
                        ID = vehicleData.ID,
                        LeadID = vehicleData.LeadID,
                        VehicleMake =vehicleData.VehicleMake ,
                        VehicleType = vehicleData.VehicleType,
                        
                        VehicleYear = vehicleData.VehicleYear,
                        VINNO=vehicleData.VINNo,
                        LicensePlate=vehicleData.LicensePlate,
                        DealerShip=vehicleData.Dealership,
                        FinanceCompany=vehicleData.FinanceCompany
                    });
               
                db.Dispose();
                return returnVehcileData.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string AuthorizeAdmin(String UserId, String Password)
        {
            string msg="";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            var authAdmin = db.tblLogins.Where(p => p.Username == UserId && p.Password == Password && p.IsSuperUser==1).FirstOrDefault();
            if(authAdmin !=null)
            {
                msg = "1";
            }else
            {
                authAdmin = db.tblLogins.Where(p => p.Username == UserId).FirstOrDefault();
                if (authAdmin == null)
                {
                   msg= "Invalid username.";
                }
                else 
                {
                    msg= "Invalid password.";
                }
                
            }
             return msg;
        }
        public string SendCompEmail(String EmailID,String PinNo)
        {
            string msg = "";
            if (EmailID != "")
            {
                AgentOrderModel lamodel = new AgentOrderModel().GetLeadInfo(PinNo);
               
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                string filePath = HttpContext.Current.Server.MapPath("~/EmailTemplate");
                var bodyTemplate = File.ReadAllText(filePath + "\\transaction_success.html");
                var body = "";
                var emailSubject = "";
                if (lamodel.LeadEmail != null)
                {
                    body = bodyTemplate;
                    body = body.Replace("{HeaderParagraph}", "Our customers are important to us. Your vehicle is now registered into the National Theft Search and Recovery database. This includes Vehicle Plate Recognition for every vehicle located at your address. In the event of a Theft, we will send out a search alert for Anti-Theft tracking services. Please provide us Document proof for COMP within 7 Days");
                    body = body.Replace("{AccountFullName}", lamodel.CompanyName);
                    body = body.Replace("{BillingAddress}", lamodel.Street);
                    body = body.Replace("{BillingCity}", lamodel.City);
                    body = body.Replace("{BillingState}", lamodel.State);
                    body = body.Replace("{BillingZip}", lamodel.Zip);
                    body = body.Replace("{CustomerEmail}", lamodel.LeadEmail);
                    body = body.Replace("{PrimaryPhone}", lamodel.PrimaryPhone);

                    body = body.Replace("{ShippingAddress}", lamodel.Street);
                    body = body.Replace("{ShippingCity}", lamodel.City);
                    body = body.Replace("{ShippingState}", lamodel.State);
                    body = body.Replace("{ShippingZip}", lamodel.Zip);

                    body = body.Replace("{TodaysChargeAmount}", lamodel.TotalAmt);
                    body = body.Replace("{BalanceDue}", "0.00");
                    body = body.Replace("{ChargeDateTime}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));

                    body = body.Replace("{TransactionID}", "Comp Type");
                    body = body.Replace("{CardLastDigit}", "");
                    body = body.Replace("{TransactionType}", "COMP");
                    body = body.Replace("{TransactionTypeHeader}", "COMP");
                    body = body.Replace("{AuthCode}", "");

                    emailSubject = "Transaction COMP from National Theft Search and Recovery for " + lamodel.TotalAmt + " (USD)";
                }
                else
                {
                    body = "";
                }

                if (body != "")
                {
                    new CommonModel().SendEmailPayments(lamodel.LeadEmail, emailSubject, body, lamodel.OrderID);
                    body = "";
                    msg = "1";
                }
                return msg;
            }
            else
            {
                return msg;
            }
        }
    }
    public class VehicleLeadModel
    {
        public long ID { get; set; }
        public long LeadID { get; set; }
        public int? VehicleMake { get; set; }
        public string VehicleMakeText { get; set; }
        public int? VehicleType { get; set; }
        public string VehicleTypeText { get; set; }
      
        public string VehicleYear { get; set; }
        public string VINNO { get; set; }
        public string LicensePlate { get; set; }
        public string DealerShip { get; set; }
        public string FinanceCompany { get; set; }

        public string UpdateVehicleLead(VehicleLeadModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string msg = "";
            var vehicleData = db.tbl_VehicleLeads.Where(p => p.ID == model.ID).FirstOrDefault();
            if (vehicleData != null)
            {
                vehicleData.VehicleMake = model.VehicleMake;
                vehicleData.VehicleType = model.VehicleType;
              
                vehicleData.VehicleYear = model.VehicleYear;
                vehicleData.VINNo = model.VINNO;
                vehicleData.LicensePlate = model.LicensePlate;
                vehicleData.Dealership = model.DealerShip;
                vehicleData.FinanceCompany = model.FinanceCompany;
                db.SaveChanges();
                msg="Data Updated Successfully..!";
            }
            else
            {
          var agVehicledata=new tbl_VehicleLeads()
          {
              LeadID=model.LeadID,
               VehicleMake = model.VehicleMake,
               VehicleType = model.VehicleType,
              
                VehicleYear = model.VehicleYear,
                VINNo = model.VINNO,
               LicensePlate = model.LicensePlate,
                Dealership = model.DealerShip,
                FinanceCompany = model.FinanceCompany,

            };
            db.tbl_VehicleLeads.Add(agVehicledata);
            db.SaveChanges();
            msg = "Data Save Successfully..!";
            }
            db.Dispose();
            return msg;
        }
        public string DeleteVehicleLead(int ID)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            try
            {
                var vDetail = db.tbl_VehicleLeads.Where(p => p.ID == ID).FirstOrDefault();

                if (vDetail != null)
                {
                    db.tbl_VehicleLeads.Remove(vDetail);
                    db.SaveChanges();
                    msg = "Data deleted successfully.";
                }
                else
                {
                    msg = "Data Not Found.";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            db.Dispose();
            return msg;
        }
    }
    public class TransactionModel
    {
        public long TransactionID { get; set; }
        public string CardDetail { get; set; }
        public Nullable<int> CardType { get; set; }
        public string CardNumber { get; set; }
        public string Token { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardExpirationYear { get; set; }
        public string CardSecurityCode { get; set; }
        public Nullable<long> AOID { get; set; }
        public Nullable<long> LeadID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<int> PaymentMethod { get; set; }
        public string CheckNumber { get; set; }
        public Nullable<long> AccountID { get; set; }
        public List<OrderTransactions> chargeDateList { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string BStreet { get; set; }
        public string BCity { get; set; }
        public string BState { get; set; }
        public string BZip { get; set; }
        public string PinNumber { get; set; }
        public string EmailID { get; set; }
        public Nullable<int> CompType { get; set; }
        public int VehicleMake { get; set; }
      
        public int VehicleType { get; set; }

        public string VehicleYear { get; set; }
        public Nullable<int> PackageId { get; set; }
        public int DefaultCreditCard { get; set; }
        public string SaleDate { get; set; }
        public string SaveTransaction(TransactionModel model)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            var transExists = db.tbl_OrderCardCheckInfo.Where(p => p.AOID == model.AOID).FirstOrDefault();
            var orderInfo = db.tbl_AgentOrderDetails.Where(p => p.AOID == model.AOID).FirstOrDefault();

            if (orderInfo != null)
            {
                orderInfo.CompType = model.CompType;
                db.SaveChanges();
            }

            if (transExists == null)
            {
                var otherCC = db.tbl_OrderCardCheckInfo.Where(p => p.LeadID == model.LeadID && p.IsDefault == 1).FirstOrDefault();
                if (otherCC != null)
                {
                    otherCC.IsDefault = 0;
                    db.SaveChanges();
                }

                var transData = new tbl_OrderCardCheckInfo()
                {
                    CardType = (model.PaymentMethod == 1 ? model.CardType : 0),
                    CardNumber = (model.PaymentMethod == 1 ? model.CardNumber : ""),
                    CheckNo = (model.PaymentMethod == 2 ? model.CheckNumber : ""),
                    PaymentMode = model.PaymentMethod,
                    CardExpirationMonth = (model.PaymentMethod == 1 ? model.CardExpirationMonth : "0"),
                    CardExpirationYear = (model.PaymentMethod == 1 ? model.CardExpirationYear : "0"),
                    CardSecurityCode = (model.PaymentMethod == 1 ? model.CardSecurityCode : ""),
                    AOID = model.AOID,
                    LeadID = model.LeadID,
                    IsDefault=1
                };
                db.tbl_OrderCardCheckInfo.Add(transData);
                db.SaveChanges();
                model.TransactionID = transData.CCID;
                msg = "Transaction is done Successfully..!";
            }
            else
            {

                transExists.CardType = (model.PaymentMethod == 1 ? model.CardType : 0);
                transExists.CardNumber =(model.PaymentMethod == 1 ?  model.CardNumber:"");
                transExists.CheckNo = (model.PaymentMethod ==2 ? model.CheckNumber:"");
                transExists.PaymentMode = model.PaymentMethod;
                transExists.CardExpirationMonth = (model.PaymentMethod == 1 ? model.CardExpirationMonth:"0");
                transExists.CardExpirationYear = (model.PaymentMethod == 1 ? model.CardExpirationYear:"0");
                transExists.CardSecurityCode = (model.PaymentMethod == 1 ? model.CardSecurityCode : "");
                transExists.AOID = model.AOID;
                transExists.LeadID = model.LeadID;
                

                db.SaveChanges();
                msg = "Transaction Updated Successfully";
            }
            db.Dispose();
            return msg;
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
                    db.tbl_AttachedFiles.Remove(uData);
                    db.SaveChanges();
                    db.Dispose();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
    }
    public class CallHistoryModel
    {
        public Nullable<long> CHID { get; set; }
        public Nullable<long> AOID { get; set; }
        public Nullable<long> LeadID { get; set; }
        public int AgentID { get; set; }
        public string CallDate { get; set; }
        public string UserName { get; set; }
        public List<CallHistoryModel> GetCallHistory(long LeadID,int PageID)
        {
            try
            {
                int UserID = MalaGroupWebSession.CurrentUser.UserID;
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                List<CallHistoryModel> model = new List<CallHistoryModel>();
                var cHistrory = db.tbl_CallHistory.Where(p=>p.LeadID==LeadID && p.PageID==PageID).ToList();
                foreach (var cHistrorys in cHistrory)
                {
                    model.Add(new CallHistoryModel()
                    {

                        LeadID=cHistrorys.LeadID,
                        AOID = cHistrorys.AOID,
                        CallDate = cHistrorys.CallDate.ToString(),
                        UserName = MalaGroupWebSession.CurrentUser.FullName,
                    });
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    public partial class OrderTransactions
    {
        public long CDID { get; set; }
        public Nullable<System.DateTime> ChargeDate { get; set; }
        public Nullable<long> AOID { get; set; }
        public Nullable<int> ChargeNo { get; set; }
        public Nullable<int> PaymentMethod { get; set; }
        public string TransactionID { get; set; }
        public string AuthCode { get; set; }
        public string TransHashCode { get; set; }
        public string CardCheckNumber { get; set; }
        public string CardType { get; set; }
        public Nullable<decimal> ChargeAmt { get; set; }
        public Nullable<int> Status { get; set; }
    }
    public partial class LeadToAccount
    {
        public int AccType { get; set; }
        public long LeadID { get; set; }
        public int AccountStatus { get; set; }
        
        public long OrderID { get; set; }
        public string PinNo { get; set; }
        public int VehicleMake { get; set; }
        public string VehicleYear { get; set; }
        public Nullable<int> PackageId { get; set; }
        public string AdditionalPackage { get; set; }
        public Nullable<int> AddDecals { get; set; }
        public Nullable<int> IdentityTheft { get; set; }
        public Nullable<System.DateTime> FirstChargeDate { get; set; }
        public string ChargeDate { get; set; }
        public string TotalAmt { get; set; }
        public string CardCheckNumber { get; set; }
        public string CardType { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardExpirationYear { get; set; }
        public string CardSecurityCode { get; set; }
        public string TransactionID { get; set; }
        public string AuthCode { get; set; }

        public long AccountID { get; set; }
        public string AccountName { get; set; }
      
        public string Phone { get; set; }
        public string LeadEmail { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string BStreet { get; set; }
        public string BCity { get; set; }
        public string BState { get; set; }
        public string BZip { get; set; }
        public string Password { get; set; }
        public string VehicleText { get; set; }
        public string PackageName { get; set; }
        public string CompType { get; set; }
    }
}