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
    public class PayrollModel
    {
      
        											
        public string  CreatedBy   {get; set;}
        public string  VOID   {get; set;}
        public string  CANCEL   {get; set;}
        public string   DECAL   {get; set;}
        public string   IDENTITY   {get; set;}

        public string GROSS { get; set; }
        public string NET { get; set; }
        public string CancelAndVoid { get; set; }


        public string CloseDate { get; set; }
        public string AccountStatus { get; set; }
        public string LastModified { get; set; }
        public string Product { get; set; }
        public string ExportFileName { get; set; }


        public List<PayRollData> payRollData { get; set; }

        public PayRollGraph payRollGraph { get; set; }

        public PayrollModel ExportReport(PayrollModel model)
        {
            (new CommonModel()).DeleteFiles();
            string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            string fileName = "";
            
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            DataSet dt1Table = new DataSet("Export Daily");

            DateTime dateTime = DateTime.UtcNow.Date;
            fileName = Guid.NewGuid().ToString() + ".xlsx";
            var filemergepath = HttpContext.Current.Server.MapPath("~/ReportStructure/2Payroll.xlsx");

            FileInfo file = new FileInfo(filemergepath);
            file.CopyTo(filePath + "/" + fileName, true);

            FileInfo excelFile = new FileInfo(filePath + "/" + fileName);

          
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    db.Database.CommandTimeout = 0;
                    cmd.CommandText = "usp_PayRollReport";
                    cmd.CommandType = CommandType.StoredProcedure;

                   
                    DbParameter paramSRDF = cmd.CreateParameter();
                    paramSRDF.ParameterName = "CloseDateFrom";
                    paramSRDF.Value = (model.CloseDate != null && model.CloseDate.Length > 0 ? model.CloseDate.Split('-')[0] : null);
                    cmd.Parameters.Add(paramSRDF);

                    DbParameter paramSRDT = cmd.CreateParameter();
                    paramSRDT.ParameterName = "CloseDateTo";
                    paramSRDT.Value = (model.CloseDate != null && model.CloseDate.Length > 0 ? model.CloseDate.Split('-')[1] : null);
                    cmd.Parameters.Add(paramSRDT);   

                    DbParameter paramACC = cmd.CreateParameter();
                    paramACC.ParameterName = "AccountStatus";
                    paramACC.Value = model.AccountStatus != null ? model.AccountStatus.TrimEnd(',') : "0"; 
                    cmd.Parameters.Add(paramACC);


                    DbParameter paramLLF = cmd.CreateParameter();
                    paramLLF.ParameterName = "LastModifiedFrom";
                    paramLLF.Value = (model.LastModified != null && model.LastModified.Length > 0 ? model.LastModified.Split('-')[0] : null);
                    cmd.Parameters.Add(paramLLF);

                    DbParameter paramLLT = cmd.CreateParameter();
                    paramLLT.ParameterName = "LastModifiedTo";
                    paramLLT.Value = (model.LastModified != null && model.LastModified.Length > 0 ? model.LastModified.Split('-')[1] : null);

                    
                    DbParameter paramTRN = cmd.CreateParameter();
                    paramTRN.ParameterName = "Package";
                    paramTRN.Value = model.Product!=null?model.Product.TrimEnd(','):"0";
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

             PayrollModel payRollModel = new PayrollModel();

             payRollModel.ExportFileName = fileName;

             List<PayRollData> payRollData = new List<PayRollData>();
             

             PayRollGraph payRollGraph = new PayRollGraph();
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

                 payRollData.Add(new PayRollData()
                 {
                     CreatedBy = dr["Created By"].ToString(),
                     LastModifiedBy = dr["Last Modified By"].ToString(),
                     AccountName = dr["Account Name"].ToString(),
                     TransactionTotal = dr["Transaction Total"].ToString(),
                     ChargeAmount = dr["Charge Amount"].ToString(),
                     Won = dr["Won"].ToString(),
                     PaymentStatus = dr["Payment Status"].ToString(),
                     Stage = dr["Stage"].ToString(),
                     VehicleYear = dr["Vehicle Year"].ToString(),
                     CreatedDate = createdDate.Value.ToString("MM/dd/yyyy"),
                     IdentityTheftRecovery = dr["Identity Theft Recovery"].ToString(),
                     AdditionalDecals = dr["Additional Decals"].ToString(),
                     AdditionalDecalCount = dr["Additional Decal Count"].ToString(),
                     CancelsAndVoids = dr["Cancels and Voids"].ToString(),
                     GrossDeals = dr["Gross Deals"].ToString(),
                     NetDeals = dr["Net Deals"].ToString(),
                     IdentityTheft = dr["Identity Theft"].ToString(),
                     TransType = dr["TransType"].ToString(),
                     CreatedByID = dr["CreatedByID"].ToString(),
                     ADDSUM = dr["ADDSUM"].ToString(),
                     CreatedCOUNT = dr["CreatedCOUNT"].ToString(),
                     ChargeTotal = dr["ChargeTotal"].ToString(),
                     DecalCount = dr["DecalCount"].ToString(),
                     VoidCount = dr["VoidCount"].ToString(),
                     GrossSUM = dr["GrossSUM"].ToString(),
                     NetSUM = dr["NetSUM"].ToString(),
                     IDenCount = dr["IDenCount"].ToString(),
                     TranTotal = dr["TranTotal"].ToString(),
                     CReatedTotal = dr["CReatedTotal"].ToString(),
                     GRTotal = dr["GRTotal"].ToString(),
                     NETotal = dr["NETotal"].ToString(),
                     CharTotal = dr["CharTotal"].ToString(),
                     Dectotal = dr["Dectotal"].ToString(),
                     VoiTotal = dr["VoiTotal"].ToString(),
                     IDenTotal = dr["IDenTotal"].ToString(),
                     CANCEL = dr["CANCEL"].ToString(),
                     VOID = dr["VOID"].ToString()
                 });
             }

             int i = 0;
             foreach (DataRow dr in dtTable2.Rows)
             {


                 if (i == 0)
                 {
                     payRollGraph.GrossDataTotal = dr["GrossTotal"].ToString();
                     payRollGraph.NetDataTotal = dr["NETTotal"].ToString();
                     payRollGraph.CancelVoidDataTotal = dr["CancelsandVoidsTotal"].ToString();
                     payRollGraph.AddDecalDataTotal = dr["ADDDecalTotal"].ToString();
                     payRollGraph.IdentityDataTotal = dr["IDETheftTotal"].ToString();
                 }
                 payRollGraph.Labels += (payRollGraph.Labels == null ? dr["CreatedBy"].ToString() : "," + dr["CreatedBy"].ToString());
                 payRollGraph.GrossData += (payRollGraph.GrossData == null ? dr["Gross"].ToString() : "," + dr["Gross"].ToString());
                 payRollGraph.NetData += (payRollGraph.NetData == null ? dr["NET"].ToString() : "," + dr["NET"].ToString());
                 payRollGraph.CancelVoidData += (payRollGraph.CancelVoidData == null ? dr["CancelsandVoids"].ToString() : "," + dr["CancelsandVoids"].ToString());
                 payRollGraph.AddDecalData += (payRollGraph.AddDecalData == null ? dr["ADDDecal"].ToString() : "," + dr["ADDDecal"].ToString());
                 payRollGraph.IdentityData += (payRollGraph.IdentityData == null ? dr["IDETheft"].ToString() : "," + dr["IDETheft"].ToString());
                 i++;
             }
             payRollModel.payRollData = payRollData;
             payRollModel.payRollGraph = payRollGraph;
             
            using (ExcelPackage excelPackage = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
                ExcelWorksheet worksheet2 = excelPackage.Workbook.Worksheets[2];

                //worksheet.Cells["A3"].Value = " As of " + DateTime.Now.ToString("MM/dd/yyyy") + " • Generated by " + CommonModel.GetUserFullName();
                int colCount = 1;
                var createdByID = "0";
                var count = 0;
                var isChange = false;
                int rowNum = 2;

                var CreatedCOUNT = "0";
                var ADDSUM = "0";
                var ChargeTotal = "0";
                var DecalCount = "0";
                var VoidCount = "0";
                var GrossCount = "0";
                var NetCount = "0";
                var IDenCount = "0";
                var rowCount = dtTable.Rows.Count;
                //worksheet.Column(10).Style.Numberformat.Format = "MM/dd/yyyy  HH:mm";
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
                        worksheet.Cells["O" + rowNum.ToString()].Value = GrossCount;
                        worksheet.Cells["P" + rowNum.ToString()].Value = NetCount;
                        //worksheet.Cells["O" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(GrossCount).ToString("0.00");
                        //worksheet.Cells["P" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(NetCount).ToString("0.00");
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
                    worksheet.Cells["O" + rowNum.ToString()].Value = "";
                    worksheet.Cells["P" + rowNum.ToString()].Value = "";
                    //worksheet.Cells["O" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(dr[14].ToString()).ToString("0.00");
                    //worksheet.Cells["P" + rowNum.ToString()].Value = "$" + Convert.ToDecimal(dr[15].ToString()).ToString("0.00");
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
                        worksheet.Cells["O" + rowNum.ToString()].Value = GrossCount;
                        worksheet.Cells["P" + rowNum.ToString()].Value = NetCount;
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
                        worksheet.Cells["O" + rowNum.ToString()].Value = dr[29].ToString();
                        worksheet.Cells["P" + rowNum.ToString()].Value = dr[30].ToString();
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
            return payRollModel;
        }

        public class PayRollGraph
        {
            public string Labels { get; set; }
            public string GrossData { get; set; }
            public string NetData { get; set; }
            public string CancelVoidData { get; set; }
            public string AddDecalData { get; set; }
            public string IdentityData { get; set; }
            public string GrossDataTotal { get; set; }
            public string NetDataTotal { get; set; }
            public string CancelVoidDataTotal { get; set; }
            public string AddDecalDataTotal { get; set; }
            public string IdentityDataTotal { get; set; }

        }

        public class PayRollData
        {
            public string CreatedBy { get; set; }
            public string LastModifiedBy { get; set; }
            public string AccountName { get; set; }
            public string TransactionTotal { get; set; }
            public string ChargeAmount { get; set; }
            public string Won { get; set; }
            public string PaymentStatus { get; set; }
            public string Stage { get; set; }
            public string VehicleYear { get; set; }
            public string CreatedDate { get; set; }
            public string IdentityTheftRecovery { get; set; }
            public string AdditionalDecals { get; set; }
            public string AdditionalDecalCount { get; set; }
            public string CancelsAndVoids { get; set; }
            public string GrossDeals { get; set; }
            public string NetDeals { get; set; }
            public string IdentityTheft { get; set; }
            public string TransType { get; set; }
            public string CreatedByID { get; set; }
            public string ADDSUM { get; set; }
            public string CreatedCOUNT { get; set; }
            public string ChargeTotal { get; set; }
            public string DecalCount { get; set; }
            public string VoidCount { get; set; }
            public string GrossSUM { get; set; }
            public string NetSUM { get; set; }
            public string IDenCount { get; set; }
            public string TranTotal { get; set; }
            public string CReatedTotal { get; set; }
            public string GRTotal { get; set; }
            public string NETotal { get; set; }
            public string CharTotal { get; set; }
            public string Dectotal { get; set; }
            public string VoiTotal { get; set; }
            public string IDenTotal { get; set; }
            public string CANCEL { get; set; }
            public string VOID { get; set; }

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