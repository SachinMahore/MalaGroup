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
    public class ReportsModel
    {
      
        											
        public string  CreatedBy   {get; set;}
        public string  VOID   {get; set;}
        public string  CANCEL   {get; set;}
        public string   DECAL   {get; set;}
        public string   IDENTITY   {get; set;}

        public string GROSS { get; set; }
        public string NET { get; set; }
        public string CancelAndVoid { get; set; }
       

       

        public DataTable ExportReport(string ExportDate, string ExportDateTo,   string PopValue, ref string ExportFileName)
        {
            (new CommonModel()).DeleteFiles();
            string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            string fileName = "";
            string uspName = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            DataTable dtTable = new DataTable("Export Daily");
            if (PopValue=="1")
            {
                fileName = Guid.NewGuid().ToString() + ".csv";  
                uspName = "usp_GetExportDailyMailOLD";
            }else if(PopValue=="2")
            {
                fileName = Guid.NewGuid().ToString() + ".csv"; 
                uspName = "usp_NewAgentReportDailyDealsOLD";
                
            }
            else if (PopValue == "3")
            {
                fileName = Guid.NewGuid().ToString() + ".csv"; 
                uspName = "usp_AgentClosingReportOLD";
            }
            //else if (PopValue == "4")
            //{
            //    fileName = Guid.NewGuid().ToString() + ".csv"; 
            //    uspName = "usp_PayRollReportOLD";
            //}
            ExportFileName = fileName;
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    db.Database.CommandTimeout = 0;
                    cmd.CommandText = uspName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramWdf = cmd.CreateParameter();
                    paramWdf.ParameterName = "Date";
                    paramWdf.Value = ExportDate;
                    cmd.Parameters.Add(paramWdf);

                    if (PopValue=="3")
                    {
                     DbParameter paramToDAte = cmd.CreateParameter();
                     paramToDAte.ParameterName = "ToDate";
                     paramToDAte.Value = ExportDateTo;
                     cmd.Parameters.Add(paramToDAte);
                    }

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
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].LoadFromDataTable(dtTable, true);
                FileInfo fi = new FileInfo(filePath + "/" + fileName);
                excelPackage.SaveAs(fi);
            }
            return dtTable;
        }


        public DataTable ExportReportPayRoll(string clDateFrom, string clDateto, string PopValue, string lstDateFrom, string lstDateto, string accStatus, string chargeatm, string product, ref string ExportFileName, ref DataTable BarGraph)
        {
            (new CommonModel()).DeleteFiles();
            string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            string fileName = "";
            
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            DataSet dt1Table = new DataSet("Export Daily");

            DateTime dateTime = DateTime.UtcNow.Date;
            fileName = Guid.NewGuid().ToString() + ".xlsx";
            var filemergepath = HttpContext.Current.Server.MapPath("~/ReportStructure/2PayrollOLD.xlsx");

            FileInfo file = new FileInfo(filemergepath);
            file.CopyTo(filePath + "/" + fileName, true);

            FileInfo excelFile = new FileInfo(filePath + "/" + fileName);

            ExportFileName = fileName;
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    db.Database.CommandTimeout = 0;
                    cmd.CommandText = "usp_PayRollReportOLD";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramExtF = cmd.CreateParameter();
                    paramExtF.ParameterName = "CloseDateFrom";
                    paramExtF.Value = clDateFrom;
                    cmd.Parameters.Add(paramExtF);

                    DbParameter paramEXTT = cmd.CreateParameter();
                    paramEXTT.ParameterName = "CloseDateTo";
                    paramEXTT.Value = clDateto;
                    cmd.Parameters.Add(paramEXTT);

                    DbParameter paramACC = cmd.CreateParameter();
                    paramACC.ParameterName = "AccountStatus";
                    paramACC.Value = accStatus;
                    cmd.Parameters.Add(paramACC);

                    DbParameter paramLLF = cmd.CreateParameter();
                    paramLLF.ParameterName = "LastModifiedFrom";
                    paramLLF.Value = lstDateFrom;
                    cmd.Parameters.Add(paramLLF);


                    DbParameter paramLLT = cmd.CreateParameter();
                    paramLLT.ParameterName = "LastModifiedTo";
                    paramLLT.Value = lstDateto;
                    cmd.Parameters.Add(paramLLT);

                    
                    DbParameter paramTRN = cmd.CreateParameter();
                    paramTRN.ParameterName = "Package";
                    paramTRN.Value = product;
                    cmd.Parameters.Add(paramTRN);

                    

                    cmd.CommandTimeout = 0;

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt1Table);
                    db.Database.Connection.Close();
                }
                catch
                {
                    db.Database.Connection.Close();
                }
            }

            db.Dispose();
             //dt1Table.Tables[0].TableName = "ExcelTable";
             DataTable dtTable = dt1Table.Tables[0];
             DataTable dtTable2 = dt1Table.Tables[1];
             BarGraph = dtTable2;
            
             
            using (ExcelPackage excelPackage = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
                ExcelWorksheet worksheet2 = excelPackage.Workbook.Worksheets[2];

                worksheet.Cells["A3"].Value = " As of " + DateTime.Now.ToString("MM/dd/yyyy") + " • Generated by " + CommonModel.GetUserFullName();
                int colCount = 1;
                var createdByID = "0";
                var count = 0;
                var isChange = false;
                int rowNum = 6;

                var CreatedCOUNT = "0";
                var ADDSUM = "0";
                var ChargeTotal = "0";
                var DecalCount = "0";
                var VoidCount = "0";
                var GrossCount = "0";
                var NetCount = "0";
                var IDenCount = "0";
                var rowCount = dtTable.Rows.Count;
                worksheet.Column(10).Style.Numberformat.Format = "MM/dd/yyyy  HH:mm";
                foreach (DataRow dr in dtTable.Rows)
                {
                    worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Size = 12;
                    worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Name = "Calibri";
                    worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Bold = false;

                  
                    rowCount -= 1;
                    if (createdByID != dr["CreatedByID"].ToString())
                    {
                        if (count == 0)
                        {
                            createdByID = dr["CreatedByID"].ToString();
                            CreatedCOUNT = dr["CreatedCOUNT"].ToString();
                            ADDSUM = dr["ADDSUM"].ToString();
                            ChargeTotal = dr["ChargeTotal"].ToString();
                            DecalCount = dr["DecalCount"].ToString();
                            VoidCount = dr["VoidCount"].ToString();
                            GrossCount = dr["GrossSUM"].ToString();
                            NetCount = dr["NetSUM"].ToString();
                            IDenCount = dr["IDenCount"].ToString();

                        }
                        if (count != 0)
                        {
                            isChange = true;
                        }

                    }
                    count += 1;
                    if (isChange == true)
                    {
                        isChange = false;

                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Top.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Right.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Bottom.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Left.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 249, 249, 247);
                        worksheet.Cells["A" + rowNum.ToString()].Value = "Sum";
                        worksheet.Cells["B" + rowNum.ToString()].Value = "";
                        worksheet.Cells["C" + rowNum.ToString()].Value = "";
                        worksheet.Cells["D" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(ADDSUM).ToString("0.00");
                        
                        worksheet.Cells["E" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(ChargeTotal).ToString("0.00");
                        worksheet.Cells["F" + rowNum.ToString()].Value = "";
                        worksheet.Cells["G" + rowNum.ToString()].Value = "";
                        worksheet.Cells["H" + rowNum.ToString()].Value = "";
                        worksheet.Cells["I" + rowNum.ToString()].Value = "";
                        worksheet.Cells["J" + rowNum.ToString()].Value = "";
                        worksheet.Cells["K" + rowNum.ToString()].Value = "";
                        worksheet.Cells["L" + rowNum.ToString()].Value = "";
                        worksheet.Cells["M" + rowNum.ToString()].Value = "";
                        worksheet.Cells["N" + rowNum.ToString()].Value = "";
                        worksheet.Cells["O" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(GrossCount).ToString("0.00");
                        worksheet.Cells["P" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(NetCount).ToString("0.00");
                        worksheet.Cells["Q" + rowNum.ToString()].Value = "";
                        worksheet.Cells["R" + rowNum.ToString()].Value = "";
                        rowNum += 1;


                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Top.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Right.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Bottom.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Left.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 249, 249, 247);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Size = 12;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Name = "Calibri";
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Bold = false;
                        worksheet.Cells["A" + rowNum.ToString()].Value = "Count";
                        worksheet.Cells["B" + rowNum.ToString()].Value = "";
                        worksheet.Cells["C" + rowNum.ToString()].Value = CreatedCOUNT;
                        worksheet.Cells["D" + rowNum.ToString()].Value = "";
                        worksheet.Cells["E" + rowNum.ToString()].Value = "";
                        worksheet.Cells["F" + rowNum.ToString()].Value = "";
                        worksheet.Cells["G" + rowNum.ToString()].Value = "";
                        worksheet.Cells["H" + rowNum.ToString()].Value = "";
                        worksheet.Cells["I" + rowNum.ToString()].Value = "";
                        worksheet.Cells["J" + rowNum.ToString()].Value = "";
                        worksheet.Cells["K" + rowNum.ToString()].Value = "";
                        worksheet.Cells["L" + rowNum.ToString()].Value = "";
                        worksheet.Cells["M" + rowNum.ToString()].Value = DecalCount;
                        worksheet.Cells["N" + rowNum.ToString()].Value = VoidCount;
                        worksheet.Cells["O" + rowNum.ToString()].Value = "";
                        worksheet.Cells["P" + rowNum.ToString()].Value = "";
                        worksheet.Cells["Q" + rowNum.ToString()].Value = IDenCount;
                        worksheet.Cells["R" + rowNum.ToString()].Value = "";
                        rowNum += 1;

                        createdByID = dr["CreatedByID"].ToString();
                        CreatedCOUNT = dr["CreatedCOUNT"].ToString();
                        ADDSUM = dr["ADDSUM"].ToString();
                        ChargeTotal = dr["ChargeTotal"].ToString();
                        DecalCount = dr["DecalCount"].ToString();
                        VoidCount = dr["VoidCount"].ToString();
                        GrossCount = dr["GrossSUM"].ToString();
                        NetCount = dr["NetSUM"].ToString();
                        IDenCount = dr["IDenCount"].ToString();
                    }

                    worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Size = 12;
                    worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Name = "Calibri";
                    worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Bold = false;

                    if (dr[35].ToString()=="1")
                    {
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 255, 255, 0);
                    }
                    else if (dr[36].ToString() =="1" )
                    {
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 255, 0, 0);
                    }
                    worksheet.Cells["A" + rowNum.ToString()].Value = dr[0].ToString();
                    worksheet.Cells["B" + rowNum.ToString()].Value = dr[1].ToString();
                    worksheet.Cells["C" + rowNum.ToString()].Value = dr[2].ToString();
                    worksheet.Cells["D" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(dr[3].ToString()).ToString("0.00"); 
                    worksheet.Cells["E" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(dr[4].ToString()).ToString("0.00"); 
                    worksheet.Cells["F" + rowNum.ToString()].Value = dr[5].ToString();
                    worksheet.Cells["G" + rowNum.ToString()].Value = dr[6].ToString();
                    worksheet.Cells["H" + rowNum.ToString()].Value = dr[7].ToString();
                    worksheet.Cells["I" + rowNum.ToString()].Value = dr[8].ToString();
                    worksheet.Cells["J" + rowNum.ToString()].Value = dr[9].ToString();
                    worksheet.Cells["J" + rowNum.ToString()].Style.Numberformat.Format = "MM/dd/yyyy hh:mm tt";
                    worksheet.Cells["K" + rowNum.ToString()].Value = dr[10].ToString();
                    worksheet.Cells["L" + rowNum.ToString()].Value = dr[11].ToString();
                    worksheet.Cells["M" + rowNum.ToString()].Value = dr[12].ToString();
                    worksheet.Cells["N" + rowNum.ToString()].Value = dr[13].ToString();
                    worksheet.Cells["O" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(dr[14].ToString()).ToString("0.00");
                    worksheet.Cells["P" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(dr[15].ToString()).ToString("0.00");
                    worksheet.Cells["Q" + rowNum.ToString()].Value = dr[16].ToString();
                    worksheet.Cells["R" + rowNum.ToString()].Value = dr[17].ToString();
                    
                    rowNum += 1;

                    if (rowCount == 0)
                    {

                        isChange = false;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Top.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Right.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Bottom.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Left.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 249, 249, 247);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Size = 12;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Name = "Calibri";
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Bold = false;
                        
                        worksheet.Cells["A" + rowNum.ToString()].Value = "Sum";
                        worksheet.Cells["B" + rowNum.ToString()].Value = "";
                        worksheet.Cells["C" + rowNum.ToString()].Value = "";
                        worksheet.Cells["D" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(ADDSUM).ToString("0.00");
                        worksheet.Cells["E" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(ChargeTotal).ToString("0.00");
                        worksheet.Cells["F" + rowNum.ToString()].Value = "";
                        worksheet.Cells["G" + rowNum.ToString()].Value = "";
                        worksheet.Cells["H" + rowNum.ToString()].Value = "";
                        worksheet.Cells["I" + rowNum.ToString()].Value = "";
                        worksheet.Cells["J" + rowNum.ToString()].Value = "";
                        worksheet.Cells["K" + rowNum.ToString()].Value = "";
                        worksheet.Cells["L" + rowNum.ToString()].Value = "";
                        worksheet.Cells["M" + rowNum.ToString()].Value = "";
                        worksheet.Cells["N" + rowNum.ToString()].Value = "";
                        worksheet.Cells["O" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(GrossCount).ToString("0.00");
                        worksheet.Cells["P" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(NetCount).ToString("0.00");
                        worksheet.Cells["Q" + rowNum.ToString()].Value = "";
                        worksheet.Cells["R" + rowNum.ToString()].Value = "";
                        rowNum += 1;


                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Top.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Right.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Bottom.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Left.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 249, 249, 247);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Size = 12;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Name = "Calibri";
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Bold = false;
                        worksheet.Cells["A" + rowNum.ToString()].Value = "Count";
                        worksheet.Cells["B" + rowNum.ToString()].Value = "";
                        worksheet.Cells["C" + rowNum.ToString()].Value = CreatedCOUNT;
                        worksheet.Cells["D" + rowNum.ToString()].Value = "";
                        worksheet.Cells["E" + rowNum.ToString()].Value = "";
                        worksheet.Cells["F" + rowNum.ToString()].Value = "";
                        worksheet.Cells["G" + rowNum.ToString()].Value = "";
                        worksheet.Cells["H" + rowNum.ToString()].Value = "";
                        worksheet.Cells["I" + rowNum.ToString()].Value = "";
                        worksheet.Cells["J" + rowNum.ToString()].Value = "";
                        worksheet.Cells["K" + rowNum.ToString()].Value = "";
                        worksheet.Cells["L" + rowNum.ToString()].Value = "";
                        worksheet.Cells["M" + rowNum.ToString()].Value = DecalCount;
                        worksheet.Cells["N" + rowNum.ToString()].Value = VoidCount;
                        worksheet.Cells["O" + rowNum.ToString()].Value = "";
                        worksheet.Cells["P" + rowNum.ToString()].Value = "";
                        worksheet.Cells["Q" + rowNum.ToString()].Value = IDenCount;
                        worksheet.Cells["R" + rowNum.ToString()].Value = "";
                        rowNum += 1;

                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Top.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Right.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Bottom.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Left.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 249, 249, 247);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Size = 12;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Name = "Calibri";
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Bold = false;
                        worksheet.Cells["A" + rowNum.ToString()].Value = "Total Sum";
                        worksheet.Cells["B" + rowNum.ToString()].Value = "";
                        worksheet.Cells["C" + rowNum.ToString()].Value = "";
                        worksheet.Cells["D" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(dr[27].ToString()).ToString("0.00");
                        worksheet.Cells["E" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(dr[31].ToString()).ToString("0.00"); 
                        worksheet.Cells["F" + rowNum.ToString()].Value = "";
                        worksheet.Cells["G" + rowNum.ToString()].Value = "";
                        worksheet.Cells["H" + rowNum.ToString()].Value = "";
                        worksheet.Cells["I" + rowNum.ToString()].Value = "";
                        worksheet.Cells["J" + rowNum.ToString()].Value = "";
                        worksheet.Cells["K" + rowNum.ToString()].Value = "";
                        worksheet.Cells["L" + rowNum.ToString()].Value = "";
                        worksheet.Cells["M" + rowNum.ToString()].Value = "";
                        worksheet.Cells["N" + rowNum.ToString()].Value = "";
                        worksheet.Cells["O" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(dr[29].ToString()).ToString("0.00");
                        worksheet.Cells["P" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(dr[30].ToString()).ToString("0.00"); 
                        worksheet.Cells["Q" + rowNum.ToString()].Value = "";
                        worksheet.Cells["R" + rowNum.ToString()].Value = "";
                        rowNum += 1;

                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Top.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Right.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Bottom.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Border.Left.Color.SetColor(0, 213, 211, 209);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 249, 249, 247);
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Size = 12;
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Name = "Calibri";
                        worksheet.Cells["A" + rowNum.ToString() + ":R" + rowNum.ToString()].Style.Font.Bold = false;
                        worksheet.Cells["A" + rowNum.ToString()].Value = "Total Count";
                        worksheet.Cells["B" + rowNum.ToString()].Value = "";
                        worksheet.Cells["C" + rowNum.ToString()].Value =  dr[28].ToString(); 
                        worksheet.Cells["D" + rowNum.ToString()].Value = "";
                        worksheet.Cells["E" + rowNum.ToString()].Value = "";
                        worksheet.Cells["F" + rowNum.ToString()].Value = "";
                        worksheet.Cells["G" + rowNum.ToString()].Value = "";
                        worksheet.Cells["H" + rowNum.ToString()].Value = "";
                        worksheet.Cells["I" + rowNum.ToString()].Value = "";
                        worksheet.Cells["J" + rowNum.ToString()].Value = "";
                        worksheet.Cells["K" + rowNum.ToString()].Value = "";
                        worksheet.Cells["L" + rowNum.ToString()].Value = "";
                        worksheet.Cells["M" + rowNum.ToString()].Value = dr[32].ToString();
                        worksheet.Cells["N" + rowNum.ToString()].Value = dr[33].ToString(); 
                        worksheet.Cells["O" + rowNum.ToString()].Value = "";
                        worksheet.Cells["P" + rowNum.ToString()].Value = "";
                        worksheet.Cells["Q" + rowNum.ToString()].Value = dr[34].ToString();
                        worksheet.Cells["R" + rowNum.ToString()].Value = "";
                        rowNum += 1;



                        createdByID = dr["CreatedByID"].ToString();
                        CreatedCOUNT = dr["CreatedCOUNT"].ToString();
                        ADDSUM = dr["ADDSUM"].ToString();
                        ChargeTotal = dr["ChargeTotal"].ToString();
                        DecalCount = dr["DecalCount"].ToString();
                        VoidCount = dr["VoidCount"].ToString();
                        GrossCount = dr["GrossSUM"].ToString();
                        NetCount = dr["NetSUM"].ToString();
                        IDenCount = dr["IDenCount"].ToString();
                    }
                }
                //var GrossTotal="0";
                //var NETTotal="0";
                //var CancelTotal="0";
                //var VoidTotal="0";
                //var ADDDecalTotal="0";
                //var IDETheftTotal = "0";
                var newRowNum = 2;
                int lenDt = dtTable2.Rows.Count;
                foreach (DataRow dr2 in dtTable2.Rows)
                {
                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Font.Size = 12;
                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Font.Name = "Calibri";
                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Font.Bold = false;

                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Border.Top.Color.SetColor(0, 213, 211, 209);
                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Border.Right.Color.SetColor(0, 213, 211, 209);
                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Border.Bottom.Color.SetColor(0, 213, 211, 209);
                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Border.Left.Color.SetColor(0, 213, 211, 209);
                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 249, 249, 247);
                    worksheet2.Cells["A" + newRowNum.ToString()].Value = dr2[1].ToString();
                    worksheet2.Cells["B" + newRowNum.ToString()].Value = dr2[2].ToString();
                    worksheet2.Cells["C" + newRowNum.ToString()].Value = dr2[3].ToString();
                    worksheet2.Cells["D" + newRowNum.ToString()].Value = dr2[4].ToString();
                    worksheet2.Cells["E" + newRowNum.ToString()].Value = dr2[5].ToString();
                    worksheet2.Cells["F" + newRowNum.ToString()].Value = dr2[6].ToString();
                    worksheet2.Cells["G" + newRowNum.ToString()].Value = dr2[7].ToString();

                    if (newRowNum == (lenDt + 1))
                    {
                        newRowNum += 1;
                        worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 166, 166, 166);
                        worksheet2.Cells["A" + newRowNum.ToString()].Value = "";
                        worksheet2.Cells["B" + newRowNum.ToString()].Value = "GROSS";
                        worksheet2.Cells["C" + newRowNum.ToString()].Value = "NET";
                        worksheet2.Cells["D" + newRowNum.ToString()].Value = "CANCELS";
                        worksheet2.Cells["E" + newRowNum.ToString()].Value = "VOIDS";
                        worksheet2.Cells["F" + newRowNum.ToString()].Value = "ADD DECALS";
                        worksheet2.Cells["G" + newRowNum.ToString()].Value = "ID";
                        newRowNum += 1;

                        worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet2.Cells["A" + newRowNum.ToString() + ":G" + newRowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 191, 191, 191);
                        worksheet2.Cells["A" + newRowNum.ToString()].Value = "Total ";
                        worksheet2.Cells["B" + newRowNum.ToString()].Value = dr2[8].ToString();
                        worksheet2.Cells["C" + newRowNum.ToString()].Value = dr2[9].ToString();
                        worksheet2.Cells["D" + newRowNum.ToString()].Value = dr2[10].ToString();
                        worksheet2.Cells["E" + newRowNum.ToString()].Value = dr2[11].ToString();
                        worksheet2.Cells["F" + newRowNum.ToString()].Value = dr2[12].ToString();
                        worksheet2.Cells["G" + newRowNum.ToString()].Value = dr2[13].ToString();
                    }


                    newRowNum += 1;

                }
                
                FileInfo fi = new FileInfo(filePath + "/" + fileName);
                excelPackage.SaveAs(fi);
            }
            return dtTable;
        }
        public static DataTable ExcelPackageToDataTable(ExcelPackage excelPackage)
        {
            DataTable dt = new DataTable();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
            //check if the worksheet is completely empty
                if (worksheet.Dimension == null)
                {
                return dt;
                }
                                //create a list to hold the column names
                List<string> columnNames = new List<string>();
                //needed to keep track of empty column headers
                int currentColumn = 1;
                //loop all columns in the sheet and add them to the datatable
                foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    string columnName = cell.Text.Trim();
                    //check if the previous header was empty and add it if it was
                    if (cell.Start.Column != currentColumn)
                    {
                        columnNames.Add("Header_" + currentColumn);
                        dt.Columns.Add("Header_" + currentColumn);
                        currentColumn++;
                    }
                    //add the column name to the list to count the duplicates
                    columnNames.Add(columnName);
                    //count the duplicate column names and make them unique to avoid the exception
                    //A column named 'Name' already belongs to this DataTable
                    int occurrences = columnNames.Count(x => x.Equals(columnName));
                    if (occurrences > 1)
                    {
                        columnName = columnName + "_" + occurrences;
                    }
                    //add the column to the datatable
                    dt.Columns.Add(columnName);
                    currentColumn++;
                }
                //start adding the contents of the excel file to the datatable
                for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                {
                    var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                    DataRow newRow = dt.NewRow();
                    //loop all cells in the row
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }
                    dt.Rows.Add(newRow);
                }
                return dt;
            }

        public List<DropDownModel> GetProductList()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<DropDownModel> model = new List<DropDownModel>();
            var productList = db.tbl_Package.ToList().OrderBy(p => p.Package);
            foreach (var vk in productList)
            {
                model.Add(new DropDownModel() { Value = vk.PackageID.ToString(), Text = vk.Package });
            }
            return model;
        }
       
    }
}