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
    public class DayWiseReportModel
    {

        public string Date { get; set; }
        public string ExportFileName { get; set; }

        public string GreaterAmt { get; set; }

        public string LessAMT { get; set; }

        public string NotEqualAMT { get; set; }

        public string AccountStatus { get; set; }
        public List<DayWiseData> DayWiseDataList { get; set; }
        public class DayWiseData
        {
            public string PrimaryPhone { get; set; }
            public string PasswordId { get; set; }
            public string PinNumber { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MailingStreet { get; set; }
            public string MailingCity { get; set; }
            public string MailingState { get; set; }
            public string MailingZip { get; set; }
            public string CreatedBy { get; set; }
            public string Product { get; set; }
            public string PaymentFrequency { get; set; }
            public string AdditionalDecals { get; set; }
            public string IdentityTheft { get; set; }
            public string Recovery { get; set; }
            public string TransactionTotal { get; set; }
            public string ChargeAmount { get; set; }
            public string AfterFirstPayment { get; set; }
            public string VIN { get; set; }
            public string VehicleYear { get; set; }
            public string VehicleMake { get; set; }
            public string PaymentCount { get; set; }
            public string ChargeDate { get; set; }
            public string CloseDate { get; set; }
            public string Stage { get; set; }
            public string Probability { get; set; }
            public string Age { get; set; }
            public string CreatedDate { get; set; }
            public string DecalNumber { get; set; }
            public string DecalNumber2 { get; set; }
            public string DecalNumber3 { get; set; }
            public string DecalNumber4 { get; set; }
            public string GPSSKU1 { get; set; }
            public string GPSDN1 { get; set; }
            public string GPSSKU2 { get; set; }
            public string GPSDN2 { get; set; }
            public string GPSSKU3 { get; set; }
            public string GPSDN3 { get; set; }
            public string GPSSKU4 { get; set; }
            public string GPSDN4 { get; set; }
            public string Email { get; set; }
            public string AODID { get; set; }

        }


        public DayWiseReportModel GetExportDayWiseData(DayWiseReportModel model)
        {
            (new CommonModel()).DeleteFiles();
            string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            string fileName = "";
            
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            DataTable dtTable = new DataTable("Export Daily");
            fileName = Guid.NewGuid().ToString() + ".xlsx";

            var filemergepath = HttpContext.Current.Server.MapPath("~/ReportStructure/DailyMailProcessing.xlsx");

            FileInfo file = new FileInfo(filemergepath);
            file.CopyTo(filePath + "/" + fileName, true);

            FileInfo excelFile = new FileInfo(filePath + "/" + fileName);

            DayWiseReportModel dayWiseReportModel = new DayWiseReportModel();
            dayWiseReportModel.ExportFileName = fileName;
            List<DayWiseData> dayWiseData = new List<DayWiseData>();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    db.Database.CommandTimeout = 0;
                    cmd.CommandText = "usp_GetExportDailyMail";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramSRDF = cmd.CreateParameter();
                    paramSRDF.ParameterName = "CloseDateFrom ";
                    paramSRDF.Value = (model.Date != null && model.Date.Length > 0 ? model.Date.Split('-')[0] : null);
                    cmd.Parameters.Add(paramSRDF);

                    DbParameter paramSRDT = cmd.CreateParameter();
                    paramSRDT.ParameterName = "CloseDateTo";
                    paramSRDT.Value = (model.Date != null && model.Date.Length > 0 ? model.Date.Split('-')[1] : null);
                    cmd.Parameters.Add(paramSRDT);

                    DbParameter paramGrt = cmd.CreateParameter();
                    paramGrt.ParameterName = "ChargeAMTGrt";
                    paramGrt.Value = (model.GreaterAmt != null ? model.GreaterAmt : "0" );
                    cmd.Parameters.Add(paramGrt);

                    DbParameter paramLess = cmd.CreateParameter();
                    paramLess.ParameterName = "ChargeAMTLess";
                    paramLess.Value = (model.LessAMT != null ? model.LessAMT : "0");
                    cmd.Parameters.Add(paramLess);

                    DbParameter paramNot = cmd.CreateParameter();
                    paramNot.ParameterName = "ChargeAMTNTEQL";
                    paramNot.Value = (model.NotEqualAMT != null ? model.NotEqualAMT : "0");
                    cmd.Parameters.Add(paramNot);

                    DbParameter paramAcc = cmd.CreateParameter();
                    paramAcc.ParameterName = "AccountStatus";
                    paramAcc.Value = model.AccountStatus != null ? model.AccountStatus.TrimEnd(',') : "0"; 
                    cmd.Parameters.Add(paramAcc);

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
                //worksheet.Cells["A3"].Value = " As of " + DateTime.Now.ToString("MM/dd/yyyy") + " • Generated by " + CommonModel.GetUserFullName();

                int rowNum = 2;

                foreach (DataRow dr2 in dtTable.Rows)
                {
                    worksheet.Cells["A" + rowNum.ToString() + ":AN" + rowNum.ToString()].Style.Font.Size = 12;
                    worksheet.Cells["A" + rowNum.ToString() + ":AN" + rowNum.ToString()].Style.Font.Name = "Calibri";
                    worksheet.Cells["A" + rowNum.ToString() + ":AN" + rowNum.ToString()].Style.Font.Bold = false;

                   

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
                    worksheet.Cells["K" + rowNum.ToString()].Value = dr2[10].ToString();
                    worksheet.Cells["L" + rowNum.ToString()].Value = dr2[11].ToString();
                    worksheet.Cells["M" + rowNum.ToString()].Value = dr2[12].ToString();
                    worksheet.Cells["N" + rowNum.ToString()].Value = dr2[13].ToString();
                    worksheet.Cells["O" + rowNum.ToString()].Value = "$"+Convert.ToDecimal( dr2[14].ToString()).ToString("0.00");
                    worksheet.Cells["P" + rowNum.ToString()].Value ="$"+Convert.ToDecimal( dr2[15].ToString()).ToString("0.00");
                    worksheet.Cells["Q" + rowNum.ToString()].Value = dr2[16].ToString();
                    worksheet.Cells["R" + rowNum.ToString()].Value = dr2[17].ToString();
                    worksheet.Cells["S" + rowNum.ToString()].Value = dr2[18].ToString();

                    worksheet.Cells["T" + rowNum.ToString()].Value = dr2[19].ToString();
                    worksheet.Cells["U" + rowNum.ToString()].Value = dr2[20].ToString();
                    worksheet.Cells["V" + rowNum.ToString()].Value = dr2[21].ToString();
                    worksheet.Cells["W" + rowNum.ToString()].Value = dr2[22].ToString();
                    worksheet.Cells["X" + rowNum.ToString()].Value = dr2[23].ToString();
                    worksheet.Cells["Y" + rowNum.ToString()].Value = dr2[24].ToString();
                    worksheet.Cells["Z" + rowNum.ToString()].Value = dr2[25].ToString();
                    worksheet.Cells["AA" + rowNum.ToString()].Value = dr2[26].ToString();
                    worksheet.Cells["AB" + rowNum.ToString()].Value = dr2[27].ToString();
                    worksheet.Cells["AC" + rowNum.ToString()].Value = dr2[28].ToString();
                    worksheet.Cells["AD" + rowNum.ToString()].Value = dr2[29].ToString();
                    worksheet.Cells["AE" + rowNum.ToString()].Value = dr2[30].ToString();

                    worksheet.Cells["AF" + rowNum.ToString()].Value = dr2[31].ToString();
                    worksheet.Cells["AG" + rowNum.ToString()].Value = dr2[32].ToString();
                    worksheet.Cells["AH" + rowNum.ToString()].Value = dr2[33].ToString();
                    worksheet.Cells["AI" + rowNum.ToString()].Value = dr2[34].ToString();
                    worksheet.Cells["AJ" + rowNum.ToString()].Value = dr2[35].ToString();
                    worksheet.Cells["AK" + rowNum.ToString()].Value = dr2[36].ToString();
                    worksheet.Cells["AL" + rowNum.ToString()].Value = dr2[37].ToString();
                    worksheet.Cells["AM" + rowNum.ToString()].Value = dr2[38].ToString();
                    worksheet.Cells["AN" + rowNum.ToString()].Value = dr2[39].ToString();
                    //worksheet.Cells["AO" + rowNum.ToString()].Value = dr2[40].ToString();
                    //worksheet.Cells["AP" + rowNum.ToString()].Value = dr2[41].ToString();
                    //worksheet.Cells["AQ" + rowNum.ToString()].Value = dr2[42].ToString();

                    rowNum += 1;

                }
              
                FileInfo fi = new FileInfo(filePath + "/" + fileName);
                excelPackage.SaveAs(fi);
            }
            foreach (DataRow dr in dtTable.Rows)
            {
                DateTime? createdDate = null;
                try
                {
                    createdDate = Convert.ToDateTime(dr["Created Date"].ToString());
                }
                catch
                {

                }

                dayWiseData.Add(new DayWiseData()
                {
                    PrimaryPhone = dr["Primary Phone"].ToString(),
                    PasswordId = dr["Password Id"].ToString(),
                    PinNumber = dr["Pin Number"].ToString(),
                    FirstName = dr["Person Account: First Name"].ToString(),
                    LastName = dr["Person Account: Last Name"].ToString(),
                    MailingStreet = dr["Person Account: Mailing Street"].ToString(),
                    MailingCity = dr["Person Account: Mailing City"].ToString(),
                    MailingState = dr["Person Account: Mailing State/Province"].ToString(),
                    MailingZip = dr["Person Account: Mailing Zip/Postal Code"].ToString(),
                    CreatedBy = dr["Created By"].ToString(),
                    Product = dr["Product"].ToString(),
                    PaymentFrequency = dr["Payment Frequency"].ToString(),
                    AdditionalDecals = dr["Additional Decals"].ToString(),
                    IdentityTheft = dr["Identity Theft Recovery"].ToString(),
                    TransactionTotal = dr["Transaction Total"].ToString(),
                    ChargeAmount = dr["Charge Amount after First Payment"].ToString(),
                    
                    VIN = dr["VIN"].ToString(),
                    VehicleYear = dr["Vehicle Year"].ToString(),
                    VehicleMake = dr["Vehicle Make"].ToString(),
                    PaymentCount = dr["Payment Count"].ToString(),
                    ChargeDate = dr["Charge Date"].ToString(),
                    CloseDate = dr["Close Date"].ToString(),
                    Stage = dr["Stage"].ToString(),
                    Probability = dr["Probability (%)"].ToString(),
                    Age = dr["Age"].ToString(),
                    CreatedDate = dr["Created Date"].ToString(),
                    DecalNumber = dr["Decal Number"].ToString(),
                    DecalNumber2 = dr["Decal Number2"].ToString(),
                    DecalNumber3 = dr["Decal Number3"].ToString(),
                    DecalNumber4 = dr["Decal Number4"].ToString(),
                    GPSSKU1 = dr["GPS SKU1"].ToString(),
                    GPSDN1 = dr["GPS DN1"].ToString(),
                    GPSSKU2 = dr["GPS SKU2"].ToString(),
                    GPSDN2 = dr["GPS DN2"].ToString(),
                    GPSSKU3 = dr["GPS SKU3"].ToString(),
                    GPSDN3 = dr["GPS DN3"].ToString(),
                    GPSSKU4 = dr["GPS SKU4"].ToString(),
                    GPSDN4 = dr["GPS DN4"].ToString(),
                    Email = dr["Person Account: Email"].ToString(),
                    AODID = dr["AODID"].ToString()
                });
            }
            dayWiseReportModel.DayWiseDataList = dayWiseData;
            return dayWiseReportModel;
        }
    }
}