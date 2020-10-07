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
using OfficeOpenXml.Style;

namespace MalaGroupERP.Models
{
    public class TakeOffListModel
    {

        public string LastModiFiedDate { get; set; }
        public int  Show { get; set; }
        public string ExportFileName { get; set; }
        public int  TakeOffList { get; set; }
        public string AccountStatus { get; set; }
        public string LessAMT { get; set; }

        public string NotEqualAMT { get; set; }
        public List<TakeOFFListData> TakeOFFDataList { get; set; }
        public class TakeOFFListData
        {
            public string PinNumber { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string CreatedBy { get; set; }
            public string VIN { get; set; }
            public string LastProduct { get; set; }
            public string LastModiFiedDate { get; set; }
        }

        //public TakeOffListModel GetTakeOffListReport(TakeOffListModel model)
        //{
        //    (new CommonModel()).DeleteFiles();
        //    string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
        //    string fileName = "";
            
        //    MalaGroupERPEntities db = new MalaGroupERPEntities();
        //    DataTable dtTable = new DataTable("Take Off List");
        //    fileName = Guid.NewGuid().ToString() + ".xlsx";
        //    var filemergepath = "";
        //    if(model.Show==1 || model.Show==3)
        //    {
        //         filemergepath = HttpContext.Current.Server.MapPath("~/ReportStructure/TakeOffListAccounts.xlsx");
        //    }
        //    else
        //    {
        //         filemergepath = HttpContext.Current.Server.MapPath("~/ReportStructure/TakeOffListLead.xlsx");
        //    }


        //    FileInfo file = new FileInfo(filemergepath);
        //    file.CopyTo(filePath + "/" + fileName, true);

        //    FileInfo excelFile = new FileInfo(filePath + "/" + fileName);

        //    TakeOffListModel takeOffListModel = new TakeOffListModel();
        //    takeOffListModel.ExportFileName = fileName;
        //    List<TakeOFFListData> takeOffData = new List<TakeOFFListData>();
        //    using (var cmd = db.Database.Connection.CreateCommand())
        //    {
        //        try
        //        {
        //            db.Database.Connection.Open();
        //            db.Database.CommandTimeout = 0;
        //            cmd.CommandText = "usp_GetTakeOffList";
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            DbParameter paramSRDF = cmd.CreateParameter();
        //            paramSRDF.ParameterName = "LastModifiedFrom";
        //            paramSRDF.Value = (model.LastModiFiedDate != null && model.LastModiFiedDate.Length > 0 ? model.LastModiFiedDate.Split('-')[0] : null);
        //            cmd.Parameters.Add(paramSRDF);

        //            DbParameter paramSRDT = cmd.CreateParameter();
        //            paramSRDT.ParameterName = "LastModifiedTo";
        //            paramSRDT.Value = (model.LastModiFiedDate != null && model.LastModiFiedDate.Length > 0 ? model.LastModiFiedDate.Split('-')[1] : null);
        //            cmd.Parameters.Add(paramSRDT);

        //            DbParameter paramGrt = cmd.CreateParameter();
        //            paramGrt.ParameterName = "IsTakeOFF";
        //            paramGrt.Value = model.TakeOffList;
        //            cmd.Parameters.Add(paramGrt);

        //            DbParameter paramLess = cmd.CreateParameter();
        //            paramLess.ParameterName = "Show";
        //            paramLess.Value = model.Show==0 ? 3: model.Show;
        //            cmd.Parameters.Add(paramLess);

        //            DbParameter paramLAcc = cmd.CreateParameter();
        //            paramLAcc.ParameterName = "AccountStatus";
        //            paramLAcc.Value = model.AccountStatus != null ? model.AccountStatus.TrimEnd(',') : "0"; 
        //            cmd.Parameters.Add(paramLAcc);

        //            cmd.CommandTimeout = 0;

        //            DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
        //            da.SelectCommand = cmd;
        //            da.Fill(dtTable);
        //            db.Database.Connection.Close();
        //        }
        //        catch
        //        {
        //            db.Database.Connection.Close();
        //        }
        //    }

        //    db.Dispose();
        //    using (ExcelPackage excelPackage = new ExcelPackage(excelFile))
        //    {
        //        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
        //        worksheet.Cells["A3"].Value = " As of " + DateTime.Now.ToString("MM/dd/yyyy") + " • Generated by " + CommonModel.GetUserFullName();

        //        int rowNum = 6;

        //        foreach (DataRow dr2 in dtTable.Rows)
        //        {
        //            if (model.Show == 1 || model.Show == 3)
        //            {
        //                worksheet.Cells["A" + rowNum.ToString() + ":J" + rowNum.ToString()].Style.Font.Size = 12;
        //                worksheet.Cells["A" + rowNum.ToString() + ":J" + rowNum.ToString()].Style.Font.Name = "Calibri";
        //                worksheet.Cells["A" + rowNum.ToString() + ":J" + rowNum.ToString()].Style.Font.Bold = false;

        //                worksheet.Cells["A" + rowNum.ToString()].Value = dr2[0].ToString();
        //                worksheet.Cells["B" + rowNum.ToString()].Value = dr2[1].ToString();
        //                worksheet.Cells["C" + rowNum.ToString()].Value = dr2[2].ToString();
        //                worksheet.Cells["D" + rowNum.ToString()].Value = dr2[3].ToString();
        //                worksheet.Cells["E" + rowNum.ToString()].Value = dr2[4].ToString();
        //                worksheet.Cells["F" + rowNum.ToString()].Value = dr2[5].ToString();
        //                worksheet.Cells["G" + rowNum.ToString()].Value = dr2[6].ToString();
        //                worksheet.Cells["H" + rowNum.ToString()].Value = dr2[7].ToString();
        //                worksheet.Cells["I" + rowNum.ToString()].Value = dr2[8].ToString();
        //                worksheet.Cells["J" + rowNum.ToString()].Value = dr2[9].ToString();

        //            }
        //            if (model.Show == 2)
        //            {
        //                worksheet.Cells["A" + rowNum.ToString() + ":H" + rowNum.ToString()].Style.Font.Size = 12;
        //                worksheet.Cells["A" + rowNum.ToString() + ":H" + rowNum.ToString()].Style.Font.Name = "Calibri";
        //                worksheet.Cells["A" + rowNum.ToString() + ":H" + rowNum.ToString()].Style.Font.Bold = false;

        //                worksheet.Cells["A" + rowNum.ToString()].Value = dr2[0].ToString();
        //                worksheet.Cells["B" + rowNum.ToString()].Value = dr2[1].ToString();
        //                worksheet.Cells["C" + rowNum.ToString()].Value = dr2[2].ToString();
        //                worksheet.Cells["D" + rowNum.ToString()].Value = dr2[3].ToString();
        //                worksheet.Cells["E" + rowNum.ToString()].Value = dr2[4].ToString();
        //                worksheet.Cells["F" + rowNum.ToString()].Value = dr2[5].ToString();
        //                worksheet.Cells["G" + rowNum.ToString()].Value = dr2[6].ToString();
        //                worksheet.Cells["H" + rowNum.ToString()].Value = dr2[7].ToString();
        //            }

        //            rowNum += 1;
        //        }
              
        //        FileInfo fi = new FileInfo(filePath + "/" + fileName);
        //        excelPackage.SaveAs(fi);
        //    }
        //    foreach (DataRow dr in dtTable.Rows)
        //    {
        //        //Pin Number	Person Account: First Name	Person Account: Last Name	Shipping Street	Shipping City	Shipping State/Province	Shipping Zip/Postal Code	VIN	LastPurchase	
        //        DateTime? lastModiDate = null;
        //        try
        //        {
        //            lastModiDate = Convert.ToDateTime(dr["LastModified"].ToString());
        //        }
        //        catch
        //        {
        //            lastModiDate = null;
        //        }

        //        takeOffData.Add(new TakeOFFListData()
        //        {
        //            PinNumber = dr["Pin Number"].ToString(),
        //            FirstName = dr["Person Account: First Name"].ToString(),
        //            LastName = dr["Person Account: Last Name"].ToString(),
        //            Street = dr["Shipping Street"].ToString(),
        //            City = dr["Shipping City"].ToString(),
        //            State = dr["Shipping State/Province"].ToString(),
        //            Zip = dr["Shipping Zip/Postal Code"].ToString(),
        //            VIN = dr["VIN"].ToString(),
        //            LastProduct = dr["LastPurchase"].ToString(),
        //            LastModiFiedDate =lastModiDate!=null ? lastModiDate.Value.ToString("MM/dd/yyyy h:mm tt"):" "
        //        });
        //    }
        //    takeOffListModel.TakeOFFDataList = takeOffData;
        //    return takeOffListModel;
        //}
        public string GetTakeOffListReport(TakeOffListModel model)
        {
            string msg = "";
            long reportID = 0;
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                
                    var rtolSave = new tbl_ReportTakeOffListCriteria()
                    {
                        LastModifiedFrom = (model.LastModiFiedDate != null && model.LastModiFiedDate.Length > 0 ? Convert.ToDateTime(model.LastModiFiedDate.Split('-')[0]) : Convert.ToDateTime("01/01/1900")),
                        LastModifiedTo = (model.LastModiFiedDate != null && model.LastModiFiedDate.Length > 0 ? Convert.ToDateTime(model.LastModiFiedDate.Split('-')[1]) : Convert.ToDateTime("12/31/2050")),
                        IsTakeOFF =model.TakeOffList,
                        Show = model.Show == 0 ? 3 : model.Show,
                        AccountStatus = model.AccountStatus != null ? model.AccountStatus.TrimEnd(',') : "0",
                        ReportStatus = 0,
                        ReportDate = DateTime.Now,
                        CreatedBy=MalaGroupWebSession.CurrentUser.UserID
                    };
                    db.tbl_ReportTakeOffListCriteria.Add(rtolSave);
                    db.SaveChanges();
                    reportID = rtolSave.ReportID;
                try
                {
                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        try
                        {
                            db.Database.Connection.Open();
                            cmd.CommandText = "sp_CreateJobTakeOffList";
                            cmd.CommandTimeout = 3000;
                            cmd.CommandType = CommandType.StoredProcedure;

                            DbParameter paramRID = cmd.CreateParameter();
                            paramRID.ParameterName = "ReportID";
                            paramRID.Value = reportID.ToString();
                            cmd.Parameters.Add(paramRID);

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
                            cmd.CommandText = "sp_RunJobTakeOffList";
                            cmd.CommandTimeout = 3000;
                            cmd.CommandType = CommandType.StoredProcedure;

                            DbParameter paramRID = cmd.CreateParameter();
                            paramRID.ParameterName = "ReportID";
                            paramRID.Value = reportID.ToString();
                            cmd.Parameters.Add(paramRID);
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
                msg = "Take Off List Schedule Created Successfully and Report Generation in Progress<br/>Please check progress on Dashboard.";
            }
            catch (WebException ex)
            {
                throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
            }
            return msg;
        }
        public string GetTakeOffListReportByReportID(long ReportID)
        {
            (new CommonModel()).DeleteFiles();
            string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            string fileName = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            var model = db.tbl_ReportTakeOffListCriteria.Where(p => p.ReportID == ReportID).FirstOrDefault();
            if(model!=null)
            {
                DataTable dtTable = new DataTable("Take Off List");
                fileName = Guid.NewGuid().ToString() + ".xlsx";
                var filemergepath = "";
                if (model.Show == 1 || model.Show == 3)
                {
                    filemergepath = HttpContext.Current.Server.MapPath("~/ReportStructure/TakeOffListAccounts.xlsx");
                }
                else
                {
                    filemergepath = HttpContext.Current.Server.MapPath("~/ReportStructure/TakeOffListLead.xlsx");
                }

                FileInfo file = new FileInfo(filemergepath);
                file.CopyTo(filePath + "/" + fileName, true);

                FileInfo excelFile = new FileInfo(filePath + "/" + fileName);

                TakeOffListModel takeOffListModel = new TakeOffListModel();
                takeOffListModel.ExportFileName = fileName;
                List<TakeOFFListData> takeOffData = new List<TakeOFFListData>();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        db.Database.CommandTimeout = 0;
                        cmd.CommandText = "usp_GetTakeOffListByReportID";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramRID = cmd.CreateParameter();
                        paramRID.ParameterName = "ReportID";
                        paramRID.Value = model.ReportID;
                        cmd.Parameters.Add(paramRID);

                        cmd.CommandTimeout = 0;

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }

                db.Dispose();
                using (ExcelPackage excelPackage = new ExcelPackage(excelFile))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];

                    int rowNum = 2;

                    foreach (DataRow dr2 in dtTable.Rows)
                    {
                        if (model.Show == 1 || model.Show == 3)
                        {
                            worksheet.Cells["A" + rowNum.ToString() + ":J" + rowNum.ToString()].Style.Font.Size = 12;
                            worksheet.Cells["A" + rowNum.ToString() + ":J" + rowNum.ToString()].Style.Font.Name = "Calibri";
                            worksheet.Cells["A" + rowNum.ToString() + ":J" + rowNum.ToString()].Style.Font.Bold = false;

                            worksheet.Cells["A" + rowNum.ToString()].Value = dr2[0].ToString();
                            worksheet.Cells["B" + rowNum.ToString()].Value = dr2[1].ToString();
                            worksheet.Cells["C" + rowNum.ToString()].Value = dr2[2].ToString();
                            worksheet.Cells["D" + rowNum.ToString()].Value = dr2[3].ToString();
                            worksheet.Cells["E" + rowNum.ToString()].Value = dr2[4].ToString();
                            worksheet.Cells["F" + rowNum.ToString()].Value = dr2[5].ToString();
                            worksheet.Cells["G" + rowNum.ToString()].Value = dr2[6].ToString();
                            worksheet.Cells["H" + rowNum.ToString()].Value = dr2[7].ToString();
                            worksheet.Cells["I" + rowNum.ToString()].Value = dr2[8].ToString();
                            worksheet.Cells["J" + rowNum.ToString()].Value = dr2[9].ToString();

                        }
                        if (model.Show == 2)
                        {
                            worksheet.Cells["A" + rowNum.ToString() + ":H" + rowNum.ToString()].Style.Font.Size = 12;
                            worksheet.Cells["A" + rowNum.ToString() + ":H" + rowNum.ToString()].Style.Font.Name = "Calibri";
                            worksheet.Cells["A" + rowNum.ToString() + ":H" + rowNum.ToString()].Style.Font.Bold = false;

                            worksheet.Cells["A" + rowNum.ToString()].Value = dr2[0].ToString();
                            worksheet.Cells["B" + rowNum.ToString()].Value = dr2[1].ToString();
                            worksheet.Cells["C" + rowNum.ToString()].Value = dr2[2].ToString();
                            worksheet.Cells["D" + rowNum.ToString()].Value = dr2[3].ToString();
                            worksheet.Cells["E" + rowNum.ToString()].Value = dr2[4].ToString();
                            worksheet.Cells["F" + rowNum.ToString()].Value = dr2[5].ToString();
                            worksheet.Cells["G" + rowNum.ToString()].Value = dr2[6].ToString();
                            worksheet.Cells["H" + rowNum.ToString()].Value = dr2[7].ToString();
                        }
                        rowNum += 1;
                    }
                    FileInfo fi = new FileInfo(filePath + "/" + fileName);
                    excelPackage.SaveAs(fi);
                }
            }
            return fileName;
        }
    }
}