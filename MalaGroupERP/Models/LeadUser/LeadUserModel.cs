using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MalaGroupERP.Models;
using MalaGroupERP.Data;
using System.Data;
using System.Data.Common;


using System.IO;
using OfficeOpenXml;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Mail;

namespace MalaGroupERP.Models
{
    public class LeadUserModel
    {
        public long LeadID { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }
        public string CompanyEmail { get; set; }
        public int LeadStatus { get; set; }
        public string LeadStatusTxt { get; set; }
        public int LeadOwner { get; set; }
        public string LeadOwnerText { get; set; }
        public string Name { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public string LeadEmail { get; set; }
        public string ListCode { get; set; }
        public string ExpirationDate { get; set; }
        public string Warranty { get; set; }
        public string Language { get; set; }
        public string ListCode2 { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public string PinNo { get; set; }
        public int StepCompleted { get; set; }
        public Nullable<int> IsDiffBillingAdd { get; set; }
        public string BStreet { get; set; }
        public string BCity { get; set; }
        public string BState { get; set; }
        public string BZip { get; set; }
        public string PackageId { get; set; }
        public string Package { get; set; }
        public string Price { get; set; }
        public string FirstChargeDate { get; set; }
        public List<VehicleLeadModel> VehicleLeadList { get; set; }
        public int SearchOption { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> ExportedDate { get; set; }
        public int RowDisplay { get; set; }
        public int PageNumber { get; set; }

        public int FileUploadCount { get; set; }
        public string CreatedById { get; set; }
        public string LastModifiedById { get; set; }
        public string LastModifiedDate { get; set; }
        public int? TakeOffList { get; set; }
        public string CreatedDateText { get; set; }
        public LeadUserModel GetLeadInfo(long LeadID)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<VehicleLeadModel> vehicleList = new List<VehicleLeadModel>();

            LeadUserModel model = new LeadUserModel();

            model.LeadID = 0;
            model.CompanyName = "";
            model.Phone = "";
            model.CompanyEmail = "";
            model.LeadStatus = 0;
            model.LeadOwner = 0;
            model.LeadOwnerText = "";
            model.Name = "";
            model.PrimaryPhone = "";
            model.SecondaryPhone = "";
            model.LeadEmail = "";
            model.ListCode = "";
            model.ExpirationDate = "";
            model.Warranty = "";
            model.Language = "0";
            model.ListCode2 = "";
            model.Street = "";
            model.City = "";
            model.State = "";
            model.Zip = "";
            model.Country = "";
            model.Password = "";
            model.PinNo = "";
            model.CreatedDate = null;
            model.ExportedDate = null;
            model.FirstName = "";
            model.LastName = "";
            model.FileUploadCount = 0;
            model.CreatedById = "0";
            model.LastModifiedById = "0";
            model.LastModifiedDate = null;
            model.TakeOffList = 0;
            model.CreatedDateText = "";

            if (LeadID > 0)
            {
                DataSet dsDataSet = new DataSet();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetLeadInfoData";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter LID = cmd.CreateParameter();
                        LID.ParameterName = "LeadID";
                        LID.Value = LeadID;
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
                        DateTime? lastModifiedDate = null;
                        try
                        {
                            lastModifiedDate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["LastModifiedDate"].ToString());
                        }
                        catch
                        {

                        }
                        DateTime? createdDate = null;
                        try
                        {
                            createdDate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["CreatedDate"].ToString());
                        }
                        catch
                        {

                        }
                        model.LeadID = long.Parse(dsDataSet.Tables[0].Rows[0]["LeadID"].ToString());
                        model.CompanyName = dsDataSet.Tables[0].Rows[0]["Name"].ToString();
                        model.Phone = dsDataSet.Tables[0].Rows[0]["PrimaryPhone"].ToString();
                        model.CompanyEmail = dsDataSet.Tables[0].Rows[0]["LeadEmail"].ToString();
                        model.LeadStatus = Int32.Parse(dsDataSet.Tables[0].Rows[0]["LeadStatus"].ToString());
                        model.LeadOwner = Int32.Parse(dsDataSet.Tables[0].Rows[0]["LeadOwner"].ToString());
                        model.LeadOwnerText = dsDataSet.Tables[0].Rows[0]["LeadOwnerText"].ToString();
                        model.Name = dsDataSet.Tables[0].Rows[0]["Name"].ToString();
                        model.PrimaryPhone = dsDataSet.Tables[0].Rows[0]["PrimaryPhone"].ToString();
                        model.SecondaryPhone = dsDataSet.Tables[0].Rows[0]["SecondaryPhone"].ToString();
                        model.LeadEmail = dsDataSet.Tables[0].Rows[0]["LeadEmail"].ToString();
                        model.ListCode = dsDataSet.Tables[0].Rows[0]["ListCode"].ToString();
                        model.ExpirationDate = (expirationDate.HasValue ? expirationDate.Value.ToString("MM/dd/yyyy") : "");
                        model.Warranty = dsDataSet.Tables[0].Rows[0]["Warranty"].ToString();
                        model.Language = dsDataSet.Tables[0].Rows[0]["Language"].ToString();
                        model.ListCode2 = dsDataSet.Tables[0].Rows[0]["ListCode2"].ToString();
                        model.Street = dsDataSet.Tables[0].Rows[0]["Street"].ToString();
                        model.City = dsDataSet.Tables[0].Rows[0]["City"].ToString();
                        model.State = dsDataSet.Tables[0].Rows[0]["State"].ToString();
                        model.Zip = dsDataSet.Tables[0].Rows[0]["ZipCode"].ToString();
                        model.Country = dsDataSet.Tables[0].Rows[0]["Country"].ToString();
                        model.Password = dsDataSet.Tables[0].Rows[0]["Password"].ToString();
                        model.PinNo = dsDataSet.Tables[0].Rows[0]["PinNo"].ToString();
                        model.FirstName = dsDataSet.Tables[0].Rows[0]["FirstName"].ToString();
                        model.LastName = dsDataSet.Tables[0].Rows[0]["LastName"].ToString();
                        model.FileUploadCount = db.tbl_AttachedFiles.Where(p => p.PageID == 1 && p.AGID == LeadID).Count();
                        model.CreatedById = dsDataSet.Tables[0].Rows[0]["CreatedById"].ToString();
                        model.LastModifiedById = dsDataSet.Tables[0].Rows[0]["LastModifiedById"].ToString();
                        model.LastModifiedDate = (lastModifiedDate.HasValue ? lastModifiedDate.Value.ToString("MM/dd/yyyy h:mm tt") : "");
                        model.TakeOffList = Int32.Parse((dsDataSet.Tables[0].Rows[0]["TakeOffList"].ToString() != "" ? dsDataSet.Tables[0].Rows[0]["TakeOffList"].ToString() : "0"));
                        model.CreatedDateText = (createdDate.HasValue ? createdDate.Value.ToString("MM/dd/yyyy h:mm tt") : "");

                    }
                }
            }
            return model;
        }

        public List<VehicleLeadModel> GetVehicleLeadInfo(long LeadID)
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
                        cmd.CommandText = "usp_GetLeadVehicleInfoData";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramDN = cmd.CreateParameter();
                        paramDN.ParameterName = "LeadID";
                        paramDN.Value = LeadID;
                        cmd.Parameters.Add(paramDN);



                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            returnVehcileData.Add(new VehicleLeadModel()
                            {
                                LeadID = Convert.ToInt64(dr["LeadID"].ToString()),
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


        public string SaveUpdateLead(LeadUserModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string leadID = "0";

            DateTime? dtExpDate = null;
            if (model.ExpirationDate != null)
            {
                try
                {
                    dtExpDate = Convert.ToDateTime(model.ExpirationDate);
                }
                catch
                {
                    dtExpDate = null;
                }
            }
            else
            {
                dtExpDate = null;
            }

            if (model.LeadID == 0)
            {
                string fullName = model.Name;
                var names = fullName.Split(' ');
                string firstName = names[0];
                string lastName = names[1];
                var leadSave = new tbl_LeadInformation()
                {
                    LeadID = Convert.ToInt64(model.LeadID),
                    LeadStatus = model.LeadStatus,
                    LeadOwner = int.Parse(model.LeadOwner.ToString()),
                    Name = model.Name,
                    PrimaryPhone = model.PrimaryPhone,
                    SecondaryPhone = model.SecondaryPhone,
                    LeadEmail = model.LeadEmail,
                    ListCode = model.ListCode,
                    ExpirationDate = dtExpDate,
                    Warranty = model.Warranty,
                    Language = model.Language,
                    ListCode2 = model.ListCode2,
                    Street = model.Street,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.Zip,
                    Country = model.Country,
                    Password = model.Password,
                    PinNo = model.PinNo,
                    CreatedDate = DateTime.Now,
                    FirstName = firstName,
                    LastName = lastName,
                    CreatedById = MalaGroupWebSession.CurrentUser.UserID.ToString(),
                    LastModifiedById = MalaGroupWebSession.CurrentUser.UserID.ToString(),
                    LastModifiedDate = DateTime.Now,
                    Take_Off_List__c = model.TakeOffList
                };
                db.tbl_LeadInformation.Add(leadSave);
                db.SaveChanges();
                model.LeadID = leadSave.LeadID;
                leadID = leadSave.LeadID.ToString();
                if (model.VehicleLeadList != null)
                {
                    foreach (var vl in model.VehicleLeadList)
                    {
                        var vData = new tbl_VehicleLeads
                        {
                            LeadID = model.LeadID,
                            VehicleMake = vl.VehicleMake,
                            VehicleType = vl.VehicleType,
                            VehicleYear = vl.VehicleYear,
                            VINNo = vl.VINNO,
                            LicensePlate = vl.LicensePlate,
                            Dealership = vl.DealerShip,
                            FinanceCompany = vl.FinanceCompany
                        };
                        db.tbl_VehicleLeads.Add(vData);
                        db.SaveChanges();
                    };
                }
                db.Dispose();
            }
            else
            {
                var leadUpdate = db.tbl_LeadInformation.Where(p => p.LeadID == model.LeadID).FirstOrDefault();
                leadUpdate.LeadStatus = model.LeadStatus;
                leadUpdate.LeadOwner = model.LeadOwner;
                leadUpdate.Name = model.Name;
                leadUpdate.PrimaryPhone = model.PrimaryPhone;
                leadUpdate.SecondaryPhone = model.SecondaryPhone;
                leadUpdate.LeadEmail = model.LeadEmail;
                leadUpdate.ListCode = model.ListCode;
                leadUpdate.ExpirationDate =dtExpDate;
                leadUpdate.Warranty = model.Warranty;
                leadUpdate.Language = model.Language;
                leadUpdate.ListCode2 = model.ListCode2;
                leadUpdate.Street = model.Street;
                leadUpdate.City = model.City;
                leadUpdate.State = model.State;
                leadUpdate.ZipCode = model.Zip;
                leadUpdate.Country = model.Country;
                //leadUpdate.Password = model.Password;
                //leadUpdate.PinNo = model.PinNo;
                leadUpdate.LastModifiedById = MalaGroupWebSession.CurrentUser.UserID.ToString();
                leadUpdate.LastModifiedDate = DateTime.Now;
                leadUpdate.Take_Off_List__c = model.TakeOffList;
                db.SaveChanges();
                leadID = model.LeadID.ToString();
                VehicleLeadList = model.VehicleLeadList;
                if (VehicleLeadList != null)
                {
                    var vData = db.tbl_VehicleLeads.Where(v => v.LeadID == model.LeadID).ToList();
                    if (vData != null)
                    {
                        db.tbl_VehicleLeads.RemoveRange(vData);
                        db.SaveChanges();
                        foreach (var v in VehicleLeadList)
                        {
                            var tblData = new tbl_VehicleLeads()
                            {
                                LeadID = model.LeadID,
                                VehicleMake = v.VehicleMake,
                                VehicleType = v.VehicleType,
                                VehicleYear = v.VehicleYear,
                                VINNo = v.VINNO,
                                LicensePlate = v.LicensePlate,
                                Dealership = v.DealerShip,
                                FinanceCompany = v.FinanceCompany
                            };
                            db.tbl_VehicleLeads.Add(tblData);
                            db.SaveChanges();
                        }
                    }
                }
            }
            db.Dispose();
            return leadID;
        }

        public string DeleteLeadData(long LeadID)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            try
            {


                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_DeleteLeadInfoData";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramCat = cmd.CreateParameter();
                        paramCat.ParameterName = "LeadID";
                        paramCat.Value = LeadID;
                        cmd.Parameters.Add(paramCat);

                        cmd.ExecuteNonQuery();
                        db.Database.Connection.Close();
                    }
                    catch
                    {
                        db.Database.Connection.Close();

                    }

                }
            }
            catch (Exception ex)
            {

                msg = ex.Message;
            }
            return msg;
        }

        public List<DropDownModel> GetVehicleMakeList()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<DropDownModel> model = new List<DropDownModel>();
            var vehicleMakeList = db.tbl_VehicleMake.ToList().OrderBy(p => p.VehicleMake);
            foreach (var vk in vehicleMakeList)
            {
                model.Add(new DropDownModel() { Value = vk.ID.ToString(), Text = vk.VehicleMake });
            }
            return model;
        }


        public List<DropDownModel> GetVehicleTypeList(int VehcileMake)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<DropDownModel> model = new List<DropDownModel>();
            var vehicleTypeList = db.tbl_VehicleType.Where(m => m.VehicleMake == VehcileMake).ToList().OrderBy(p => p.VehicleType);
            foreach (var vt in vehicleTypeList)
            {
                model.Add(new DropDownModel() { Value = vt.ID.ToString(), Text = vt.VehicleType });
            }
            return model;
        }


        public class VehicleLeadModel
        {
            public long LeadID { get; set; }
            public int VehicleMake { get; set; }
            public string VehicleMakeText { get; set; }
            public int VehicleType { get; set; }
            public string VehicleTypeText { get; set; }

            public string VehicleYear { get; set; }
            public string VINNO { get; set; }
            public string LicensePlate { get; set; }
            public string DealerShip { get; set; }
            public string FinanceCompany { get; set; }

        }

        public List<LeadUserModel> GetLeadInfoPageList(LeadUserModel model)
        {
            try
            {
                List<LeadUserModel> listSearch = new List<LeadUserModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        if (model.SearchOption == 1)
                        {
                            cmd.CommandText = "usp_GetLeadInfoPageList";
                            cmd.CommandType = CommandType.StoredProcedure;

                            DbParameter paramName = cmd.CreateParameter();
                            paramName.ParameterName = "Name";
                            paramName.Value = model.Name;
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
                            cmd.CommandText = "usp_GetAdvLeadInfoPageList";
                            cmd.CommandType = CommandType.StoredProcedure;

                            DbParameter paramName = cmd.CreateParameter();
                            paramName.ParameterName = "Name";
                            paramName.Value = model.Name;
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
                            paramststus.ParameterName = "LeadStatus";
                            paramststus.Value = model.LeadStatus;
                            cmd.Parameters.Add(paramststus);

                            DbParameter paramTakeoff = cmd.CreateParameter();
                            paramTakeoff.ParameterName = "TakeOffList";
                            paramTakeoff.Value = model.TakeOffList;
                            cmd.Parameters.Add(paramTakeoff);

                        }
                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();



                        foreach (DataRow dr in dtTable.Rows)
                        {

                            DateTime? lastModifiedDate = null;
                            try
                            {
                                lastModifiedDate = Convert.ToDateTime(dr["LastModifiedDate"].ToString());
                            }
                            catch
                            {

                            }
                            DateTime? expirationDate = null;
                            try
                            {
                                expirationDate = Convert.ToDateTime(dr["ExpirationDate"].ToString());
                            }
                            catch
                            {

                            }

                            listSearch.Add(new LeadUserModel()
                            {
                                LeadID = Convert.ToInt64(dr["LeadID"].ToString()),
                                Name = dr["Name"].ToString(),
                                PinNo = dr["PinNo"].ToString(),
                                State = dr["State"].ToString(),
                                LastModifiedDate = (lastModifiedDate.HasValue ? lastModifiedDate.Value.ToString("MM/dd/yyyy h:mm tt") : ""),
                                LastModifiedById = dr["LastModifiedById"].ToString(),
                                ExpirationDate = (expirationDate.HasValue ? expirationDate.Value.ToString("MM/dd/yyyy h:mm tt") : ""),
                                ListCode = dr["ListCode"].ToString(),
                                CreatedById = dr["CreatedById"].ToString()
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

        public string GetLeadsFilterRangeList(LeadUserModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            DataTable dtTable = new DataTable();
            string PageNumber = "0";
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetLeadInfoPageListRange";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "Name";
                    paramName.Value = model.Name;
                    cmd.Parameters.Add(paramName);


                    DbParameter paramRow = cmd.CreateParameter();
                    paramRow.ParameterName = "RowDisplay";
                    paramRow.Value = model.RowDisplay;
                    cmd.Parameters.Add(paramRow);

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

        public string GetLeadAdvSearch(LeadUserModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            DataTable dtTable = new DataTable();
            string PageNumber = "0";
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetAdvLeadInfoPageListRange";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "Name";
                    paramName.Value = model.Name;
                    cmd.Parameters.Add(paramName);

                    DbParameter paramRow = cmd.CreateParameter();
                    paramRow.ParameterName = "RowDisplay";
                    paramRow.Value = model.RowDisplay;
                    cmd.Parameters.Add(paramRow);

                    DbParameter paramFromDate = cmd.CreateParameter();
                    paramFromDate.ParameterName = "CFromDate";
                    paramFromDate.Value = model.CreatedDate;
                    cmd.Parameters.Add(paramFromDate);

                    DbParameter paramToDate = cmd.CreateParameter();
                    paramToDate.ParameterName = "CToDate";
                    paramToDate.Value = model.LastModifiedDate;
                    cmd.Parameters.Add(paramToDate);

                    DbParameter paramststus = cmd.CreateParameter();
                    paramststus.ParameterName = "LeadStatus";
                    paramststus.Value = model.LeadStatus;
                    cmd.Parameters.Add(paramststus);

                    DbParameter paramTakeoff = cmd.CreateParameter();
                    paramTakeoff.ParameterName = "TakeOffList";
                    paramTakeoff.Value = model.TakeOffList;
                    cmd.Parameters.Add(paramTakeoff);

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
        //public string ExportToLead(HttpPostedFileBase fb)
        //{
        //    string msg = "";
        //    DataTable dt = new DataTable();
        //    MalaGroupERPEntities db = new MalaGroupERPEntities();
        //    string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
        //    string fileName = Guid.NewGuid().ToString() + ".csv";
        //    fb.SaveAs(filePath + "//" + fileName);
        //    FileInfo file = new FileInfo(filePath + "//" + fileName);
        //    using (var cmd = db.Database.Connection.CreateCommand())
        //    {
        //        try
        //        {
        //            db.Database.Connection.Open();
        //            cmd.CommandText = "usp_ImportCsvToLead";
        //            cmd.CommandTimeout = 3000;
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            DbParameter paramFIRST = cmd.CreateParameter();
        //            paramFIRST.ParameterName = "FilePath";
        //            paramFIRST.Value = filePath + "//" + fileName;
        //            cmd.Parameters.Add(paramFIRST);


        //            cmd.ExecuteNonQuery();
        //            db.Database.Connection.Close();
        //            msg = "Data Export Successfully..!";
        //        }
        //        catch (Exception ex)
        //        {
        //            db.Database.Connection.Close();
        //            msg = ex.InnerException.Message;
        //        }

        //    }
        //    return msg;
        //}
        public string ExportToLead(HttpPostedFileBase fb)
        {
            string msg = "";
            string ftp = "ftp://192.168.7.1/";
            string ftpFolder = "/Source/";
            byte[] fileBytes = null;
            string tableName = fb.FileName.Replace(".csv", "") + "_" + DateTime.Now.ToFileTime().ToString();
            string fileName = fb.FileName.Replace(".csv", "") + "_" + DateTime.Now.ToFileTime().ToString() + ".csv";
            using (StreamReader fileStream = new StreamReader(fb.InputStream))
            {
                fileBytes = Encoding.UTF8.GetBytes(fileStream.ReadToEnd());
                fileStream.Close();
            }
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("Administrator", "Witgroup2019!");
                request.ContentLength = fileBytes.Length;
                request.UsePassive = true;
                request.UseBinary = true;
                request.ServicePoint.ConnectionLimit = fileBytes.Length;
                request.EnableSsl = false;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileBytes, 0, fileBytes.Length);
                    requestStream.Close();
                }
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                var exportHistory = db.tbl_ExportHistory.Where(p => p.TableName == tableName).FirstOrDefault();
                if (exportHistory==null)
                {
                    var ehSave = new tbl_ExportHistory()
                    {
                        TableName = tableName,
                        UserID = MalaGroupWebSession.CurrentUser.UserID,
                        TotalCount = 0,
                        DuplicateCount = 0,
                        DateExported = DateTime.Now,
                        IsExported = 0,
                        IsViewed = 0
                    };
                    db.tbl_ExportHistory.Add(ehSave);
                    db.SaveChanges();

                }
                // New Code To Upload File //
                try
                {
                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        try
                        {
                            db.Database.Connection.Open();
                            cmd.CommandText = "sp_CreateJob";
                            cmd.CommandTimeout = 3000;
                            cmd.CommandType = CommandType.StoredProcedure;

                            DbParameter paramFN = cmd.CreateParameter();
                            paramFN.ParameterName = "FileName";
                            paramFN.Value = tableName;
                            cmd.Parameters.Add(paramFN);

                            cmd.ExecuteNonQuery();
                            db.Database.Connection.Close();
                        }
                        catch (Exception ex)
                        {
                            db.Database.Connection.Close();
                            msg = ex.InnerException.Message;
                        }
                    }

                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        try
                        {
                            db.Database.Connection.Open();
                            cmd.CommandText = "sp_RunJob";
                            cmd.CommandTimeout = 3000;
                            cmd.CommandType = CommandType.StoredProcedure;

                            DbParameter paramFN = cmd.CreateParameter();
                            paramFN.ParameterName = "FileName";
                            paramFN.Value = tableName;
                            cmd.Parameters.Add(paramFN);
                            cmd.ExecuteNonQuery();
                            db.Database.Connection.Close();
                        }
                        catch (Exception ex)
                        {
                            db.Database.Connection.Close();
                            msg = ex.InnerException.Message;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                db.Dispose();
                msg = "File Uploaded Successfully and Record Creation in Progress<br/>Please check progress on Dashboard.";
                // New Code To Upload File //
            }
            catch (WebException ex)
            {
                throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
            }
            return msg;
        }
        public List<DropDownModel> GetEmailTemplates()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<DropDownModel> model = new List<DropDownModel>();
            var emailTemplate = db.tbl_EmailTemplates.Where(p=>p.TemplateFor==1).ToList().OrderBy(p => p.TemplateName);
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
                    PageID.Value = 1;
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

        public string SaveToFolder(HttpPostedFileBase fb, long AccountID)
        {
            string msg = "0";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            if (fb != null && fb.ContentLength > 0)
            {
                string filePath = HttpContext.Current.Server.MapPath("~/FileAttachments/");
                string fileName = fb.FileName;
                string sysFileName = DateTime.Now.ToFileTime().ToString() +  Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);
                var uploadData = new tbl_AttachedFiles()
                {
                    PageID = 1,
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
                        paramPID.Value = 1;
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
                var uData = db.tbl_AttachedFiles.Where(m => m.PageID == 1 && m.ID == ID).FirstOrDefault();
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
                    msg = db.tbl_AttachedFiles.Where(m => m.PageID == 1 && m.AGID == AGID).Count().ToString();
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
}


