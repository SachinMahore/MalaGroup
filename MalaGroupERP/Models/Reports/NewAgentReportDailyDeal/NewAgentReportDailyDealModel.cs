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
    public class NewAgentReportDailyDealModel
    {
        public string CloseDate { get; set; }
        public string AccountStatus { get; set; }
        public string Product { get; set; }
        public string ExportFileName { get; set; }
        public string GreaterAmt { get; set; }
        public string LessAMT { get; set; }
        public string NotEqualAMT { get; set; }
        public List<AgentDailyData> agentDailyData { get; set; }
        public AgentDailyGraph agentDailyGraph { get; set; }
        public NewAgentReportDailyDealModel ExportReport(NewAgentReportDailyDealModel model)
        {
            (new CommonModel()).DeleteFiles();
            string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            string fileName = "";
            
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            DataSet dt1Table = new DataSet("Export Daily");

            DateTime dateTime = DateTime.UtcNow.Date;
            fileName = Guid.NewGuid().ToString() + ".xlsx";
            var filemergepath = HttpContext.Current.Server.MapPath("~/ReportStructure/NewAgentReportDailyDeals.xlsx");
            FileInfo file = new FileInfo(filemergepath);
            file.CopyTo(filePath + "/" + fileName, true);
            FileInfo excelFile = new FileInfo(filePath + "/" + fileName);
          
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    db.Database.CommandTimeout = 0;
                    cmd.CommandText = "usp_NewAgentReportDailyDeals";
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
                    
                    DbParameter paramTRN = cmd.CreateParameter();
                    paramTRN.ParameterName = "Package";
                    paramTRN.Value = model.Product != null ? model.Product.TrimEnd(',') : "0"; 
                    cmd.Parameters.Add(paramTRN);

                    DbParameter paramGrt = cmd.CreateParameter();
                    paramGrt.ParameterName = "ChargeAMTGrt";
                    paramGrt.Value = (model.GreaterAmt != null ? model.GreaterAmt : "0");
                    cmd.Parameters.Add(paramGrt);

                    DbParameter paramLess = cmd.CreateParameter();
                    paramLess.ParameterName = "ChargeAMTLess";
                    paramLess.Value = (model.LessAMT != null ? model.LessAMT : "0");
                    cmd.Parameters.Add(paramLess);

                    DbParameter paramNot = cmd.CreateParameter();
                    paramNot.ParameterName = "ChargeAMTNTEQL";
                    paramNot.Value = (model.NotEqualAMT != null ? model.NotEqualAMT : "0");
                    cmd.Parameters.Add(paramNot);

                    

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
             DataTable dtTable = dt1Table.Tables[0];
             DataTable dtTable2 = dt1Table.Tables[1];

             NewAgentReportDailyDealModel newAgentDailyModel = new NewAgentReportDailyDealModel();
             newAgentDailyModel.ExportFileName = fileName;
             List<AgentDailyData> newAgentData = new List<AgentDailyData>();
             AgentDailyGraph agentDailyG = new AgentDailyGraph();

             foreach (DataRow dr in dtTable.Rows)
             {
                 newAgentData.Add(new AgentDailyData()
                 {
                     PaymentCount              = dr["PaymentCount"].ToString(),
                     Product                   = dr["Product"].ToString(),
                     ListCode                  = dr["ListCode"].ToString(),
                     AccountName               = dr["AccountName"].ToString(),
                     GatewayDate               = dr["GatewayDate"].ToString(),
                     PersonAccountMailingState = dr["PersonAccountMailingState"].ToString(),
                     VehicleYear               = dr["VehicleYear"].ToString(),
                     ChargeAmount              = dr["ChargeAmount"].ToString(),
                     NumberofDecals            = dr["NumberofDecals"].ToString(),
                     AdditionalDecalCount      = dr["AdditionalDecalCount"].ToString(),
                     TotalDecalCount           = dr["TotalDecalCount"].ToString(),
                     IdentityTheftRecovery     = dr["IdentityTheftRecovery"].ToString(),
                     AdditionalDecals          = dr["AdditionalDecals"].ToString(),
                     CreatedBy                 = dr["CreatedBy"].ToString(),
                     CreadtedByID              = dr["CreadtedByID"].ToString(),


                     CreadtedCount      = dr["CreadtedCount"].ToString(),
                     IDTHEFTSUM         = dr["IDTHEFTSUM"].ToString(),
                     AdditionalDECALSUM = dr["AdditionalDECALSUM"].ToString(),
                     TotalDECALSUM      = dr["TotalDECALSUM"].ToString(),
                     AdditionalDECALPer = dr["AdditionalDECALPer"].ToString(),
                     IDTHEFTPer         = dr["IDTHEFTPer"].ToString(),

                    
                     GrandCreadtedCount      = dr["GrandCreadtedCount"].ToString(),
                     GRANDIDTHEFTSUM         = dr["GRANDIDTHEFTSUM"].ToString(),
                     GRANDAdditionalDECALSUM = dr["GRANDAdditionalDECALSUM"].ToString(),
                     GRANDTotalDECALSUM      = dr["GRANDTotalDECALSUM"].ToString(),
                     GRANDIDTHEFTPer         = dr["GRANDIDTHEFTPer"].ToString(),
                     GRANDAdditionalPER      = dr["GRANDAdditionalPER"].ToString(),

                     IsRenewal = dr["IsRenewal"].ToString(),
                     RenewalCount = dr["RenewalCount"].ToString(),
                     RenewalTotal = dr["TotalRenewal"].ToString(),

                 });
             }

             int i = 0;
             foreach (DataRow dr in dtTable2.Rows)
             {


                 if (i == 0)
                 {
                     agentDailyG.TotalDecalCountTotal  = dr["TotalDecalCountTotal"].ToString();
                     agentDailyG.AddDecalCountTotal    = dr["AddDecalCountTotal"].ToString();
                     agentDailyG.IdTheftCountTotal     = dr["IdTheftCountTotal"].ToString();
                     agentDailyG.RenewalTotal = dr["RenewalTotal"].ToString();
                     
                 }
                 agentDailyG.Labels          += (agentDailyG.Labels == null ? dr["CreatedBy"].ToString() : "," + dr["CreatedBy"].ToString());
                 agentDailyG.TotalDecalCount += (agentDailyG.TotalDecalCount == null ? dr["NoOfDecal"].ToString() : "," + dr["NoOfDecal"].ToString());
                 agentDailyG.AddDecalCount   += (agentDailyG.AddDecalCount == null ? dr["AddDecalCount"].ToString() : "," + dr["AddDecalCount"].ToString());
                 agentDailyG.IdTheftCount    += (agentDailyG.IdTheftCount == null ? dr["IdTheftCount"].ToString() : "," + dr["IdTheftCount"].ToString());
                 agentDailyG.RenewalCount += (agentDailyG.RenewalCount == null ? dr["RenewalCount"].ToString() : "," + dr["RenewalCount"].ToString());
                 i++;
                 //NoOfDecal
             }
             newAgentDailyModel.agentDailyData = newAgentData;
             newAgentDailyModel.agentDailyGraph = agentDailyG;

             using (ExcelPackage excelPackage = new ExcelPackage(excelFile))
             {
                 ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];

                // worksheet.Cells["A3"].Value = " As of " + DateTime.Now.ToString("MM/dd/yyyy") + " • Generated by " + CommonModel.GetUserFullName();
                 int colCount = 1;
                 var createdByID = "0";
                 var count = 0;
                 var isChange = false;
                 int rowNum = 2;

                 var CreatedCOUNT        = "0";
                 var IDTHEFTSUM          = "0";
                 var AdditionalDECALSUM  = "0";
                 var TotalDECALSUM       = "0";
                 var AdditionalDECALPer  = "0";
                 var IDTHEFTPer          = "0";
                 var RenewalCount = "0";
                 var RenewalTotal = "0";
                 var rowCount = dtTable.Rows.Count;


                 //worksheet.Column(10).Style.Numberformat.Format = "MM/dd/yyyy  HH:mm";
                 foreach (DataRow dr in dtTable.Rows)
                 {
                     worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Font.Size = 12;
                     worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Font.Name = "Calibri";
                     worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Font.Bold = false;


                     rowCount -= 1;
                     if (createdByID != dr["CreadtedByID"].ToString())
                     {
                         if (count == 0)
                         {
                             createdByID = dr["CreadtedByID"].ToString();
                             CreatedCOUNT = dr["CreadtedCount"].ToString();
                             IDTHEFTSUM = dr["IDTHEFTSUM"].ToString();
                             AdditionalDECALSUM = dr["AdditionalDECALSUM"].ToString();
                             TotalDECALSUM = dr["TotalDECALSUM"].ToString();
                             AdditionalDECALPer = dr["AdditionalDECALPer"].ToString();
                             IDTHEFTPer = dr["IDTHEFTPer"].ToString();

                             RenewalCount = dr["RenewalCount"].ToString();
                             RenewalTotal = dr["TotalRenewal"].ToString();


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

                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Top.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Right.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Bottom.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Left.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 249, 249, 247);
                         worksheet.Cells["A" + rowNum.ToString()].Value = "Subtotal";
                         worksheet.Cells["B" + rowNum.ToString()].Value = CreatedCOUNT;
                         worksheet.Cells["C" + rowNum.ToString()].Value = "";
                         worksheet.Cells["D" + rowNum.ToString()].Value = "";

                         worksheet.Cells["E" + rowNum.ToString()].Value = "";
                         worksheet.Cells["F" + rowNum.ToString()].Value = "";
                         worksheet.Cells["G" + rowNum.ToString()].Value = "";
                         worksheet.Cells["H" + rowNum.ToString()].Value = "";
                         worksheet.Cells["I" + rowNum.ToString()].Value = "";
                         worksheet.Cells["J" + rowNum.ToString()].Value = "";
                         worksheet.Cells["K" + rowNum.ToString()].Value = "";
                         worksheet.Cells["L" + rowNum.ToString()].Value = "";
                         worksheet.Cells["M" + rowNum.ToString()].Value = "";
                         worksheet.Cells["N" + rowNum.ToString()].Value = "";
                         worksheet.Cells["O" + rowNum.ToString()].Value = IDTHEFTSUM;
                         worksheet.Cells["P" + rowNum.ToString()].Value = AdditionalDECALSUM;
                         worksheet.Cells["Q" + rowNum.ToString()].Value = TotalDECALSUM;
                         worksheet.Cells["R" + rowNum.ToString()].Value = AdditionalDECALPer+"%";
                         worksheet.Cells["S" + rowNum.ToString()].Value = IDTHEFTPer+"%";
                         worksheet.Cells["T" + rowNum.ToString()].Value = RenewalCount;
                         rowNum += 1;

                         createdByID = dr["CreadtedByID"].ToString();
                         CreatedCOUNT = dr["CreadtedCount"].ToString();
                         IDTHEFTSUM = dr["IDTHEFTSUM"].ToString();
                         AdditionalDECALSUM = dr["AdditionalDECALSUM"].ToString();
                         TotalDECALSUM = dr["TotalDECALSUM"].ToString();
                         AdditionalDECALPer = dr["AdditionalDECALPer"].ToString();
                         IDTHEFTPer = dr["IDTHEFTPer"].ToString();

                         RenewalCount = dr["RenewalCount"].ToString();
                         RenewalTotal = dr["TotalRenewal"].ToString();
                     }

                     worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Font.Size = 12;
                     worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Font.Name = "Calibri";
                     worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Font.Bold = false;

                     
                     worksheet.Cells["A" + rowNum.ToString()].Value = dr[0].ToString();
                     worksheet.Cells["B" + rowNum.ToString()].Value = dr[1].ToString();
                     worksheet.Cells["C" + rowNum.ToString()].Value = dr[2].ToString();
                     worksheet.Cells["D" + rowNum.ToString()].Value = dr[3].ToString();
                     worksheet.Cells["E" + rowNum.ToString()].Value = dr[4].ToString();
                     worksheet.Cells["F" + rowNum.ToString()].Value = dr[5].ToString();
                     worksheet.Cells["G" + rowNum.ToString()].Value = dr[6].ToString();
                     worksheet.Cells["H" + rowNum.ToString()].Value = dr[7].ToString();
                     worksheet.Cells["I" + rowNum.ToString()].Value = "$"+Convert.ToDecimal(dr[8].ToString()).ToString("0.00");
                     worksheet.Cells["J" + rowNum.ToString()].Value = dr[9].ToString();
                     worksheet.Cells["K" + rowNum.ToString()].Value = dr[10].ToString();
                     worksheet.Cells["L" + rowNum.ToString()].Value = dr[11].ToString();
                     worksheet.Cells["M" + rowNum.ToString()].Value = Convert.ToInt32(dr[12].ToString())>0 ?"True":"False";
                     worksheet.Cells["N" + rowNum.ToString()].Value = dr[13].ToString()== "1" ? "True" : "False";
                     worksheet.Cells["O" + rowNum.ToString()].Value = dr[12].ToString();//new change on 28/02/2019
                     worksheet.Cells["P" + rowNum.ToString()].Value = dr[10].ToString();//new change on 28/02/2019
                     worksheet.Cells["Q" + rowNum.ToString()].Value = "";
                     worksheet.Cells["R" + rowNum.ToString()].Value = "";
                     worksheet.Cells["S" + rowNum.ToString()].Value = "";
                     worksheet.Cells["T" + rowNum.ToString()].Value = dr[27].ToString() == "1" ? "Renewed" : "";

                     rowNum += 1;

                     if (rowCount == 0)
                     {

                         isChange = false;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Top.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Right.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Bottom.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Left.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 249, 249, 247);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Font.Size = 12;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Font.Name = "Calibri";
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Font.Bold = false;

                         worksheet.Cells["A" + rowNum.ToString()].Value = "Subtotal";
                         worksheet.Cells["B" + rowNum.ToString()].Value = CreatedCOUNT;
                         worksheet.Cells["C" + rowNum.ToString()].Value = "";
                         worksheet.Cells["D" + rowNum.ToString()].Value = "";
                         worksheet.Cells["E" + rowNum.ToString()].Value = "";
                         worksheet.Cells["F" + rowNum.ToString()].Value = "";
                         worksheet.Cells["G" + rowNum.ToString()].Value = "";
                         worksheet.Cells["H" + rowNum.ToString()].Value = "";
                         worksheet.Cells["I" + rowNum.ToString()].Value = "";
                         worksheet.Cells["J" + rowNum.ToString()].Value = "";
                         worksheet.Cells["K" + rowNum.ToString()].Value = "";
                         worksheet.Cells["L" + rowNum.ToString()].Value = "";
                         worksheet.Cells["M" + rowNum.ToString()].Value = "";
                         worksheet.Cells["N" + rowNum.ToString()].Value = "";
                         worksheet.Cells["O" + rowNum.ToString()].Value = IDTHEFTSUM;
                         worksheet.Cells["P" + rowNum.ToString()].Value = AdditionalDECALSUM;
                         worksheet.Cells["Q" + rowNum.ToString()].Value = TotalDECALSUM;
                         worksheet.Cells["R" + rowNum.ToString()].Value = AdditionalDECALPer + "%";
                         worksheet.Cells["S" + rowNum.ToString()].Value = IDTHEFTPer + "%";
                         worksheet.Cells["T" + rowNum.ToString()].Value = RenewalCount;
                         rowNum += 1;


                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Top.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Right.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Bottom.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Border.Left.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 249, 249, 247);
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Font.Size = 12;
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Font.Name = "Calibri";
                         worksheet.Cells["A" + rowNum.ToString() + ":T" + rowNum.ToString()].Style.Font.Bold = false;
                         worksheet.Cells["A" + rowNum.ToString()].Value = "Total";
                         worksheet.Cells["B" + rowNum.ToString()].Value = dr[21].ToString();
                         worksheet.Cells["C" + rowNum.ToString()].Value = "";
                         worksheet.Cells["D" + rowNum.ToString()].Value = "";
                         worksheet.Cells["E" + rowNum.ToString()].Value = "";
                         worksheet.Cells["F" + rowNum.ToString()].Value = "";
                         worksheet.Cells["G" + rowNum.ToString()].Value = "";
                         worksheet.Cells["H" + rowNum.ToString()].Value = "";
                         worksheet.Cells["I" + rowNum.ToString()].Value = "";
                         worksheet.Cells["J" + rowNum.ToString()].Value = "";
                         worksheet.Cells["K" + rowNum.ToString()].Value = "";
                         worksheet.Cells["L" + rowNum.ToString()].Value = "";
                         worksheet.Cells["M" + rowNum.ToString()].Value = "";
                         worksheet.Cells["N" + rowNum.ToString()].Value = "";
                         worksheet.Cells["O" + rowNum.ToString()].Value = dr[22].ToString();
                         worksheet.Cells["P" + rowNum.ToString()].Value = dr[23].ToString();
                         worksheet.Cells["Q" + rowNum.ToString()].Value = dr[24].ToString();
                         worksheet.Cells["R" + rowNum.ToString()].Value = dr[25].ToString()+"%";
                         worksheet.Cells["S" + rowNum.ToString()].Value = dr[26].ToString()+"%";
                         worksheet.Cells["T" + rowNum.ToString()].Value = dr[29].ToString();
                         rowNum += 1;


                         createdByID = dr["CreadtedByID"].ToString();
                         CreatedCOUNT = dr["CreadtedCount"].ToString();
                         IDTHEFTSUM = dr["IDTHEFTSUM"].ToString();
                         AdditionalDECALSUM = dr["AdditionalDECALSUM"].ToString();
                         TotalDECALSUM = dr["TotalDECALSUM"].ToString();
                         AdditionalDECALPer = dr["AdditionalDECALPer"].ToString();
                         IDTHEFTPer = dr["IDTHEFTPer"].ToString();

                         RenewalCount = dr["RenewalCount"].ToString();
                         RenewalTotal = dr["TotalRenewal"].ToString();
                     }
                 }


                 FileInfo fi = new FileInfo(filePath + "/" + fileName);
                 excelPackage.SaveAs(fi);
             }
             return newAgentDailyModel;
        }

        public class AgentDailyGraph
        {
            public string Labels { get; set; }
            public string TotalDecalCount { get; set; }
            public string AddDecalCount { get; set; }
            public string IdTheftCount { get; set; }
            public string TotalDecalCountTotal { get; set; }
            public string AddDecalCountTotal { get; set; }
            public string IdTheftCountTotal { get; set; }

            public string RenewalCount { get; set; }
            public string RenewalTotal { get; set; }



        }

        public class AgentDailyData
        {
            public string PaymentCount { get; set; }
            public string Product { get; set; }
            public string ListCode { get; set; }
            public string AccountName { get; set; }
            public string GatewayDate { get; set; }
            public string PersonAccountMailingState { get; set; }
            public string VehicleYear { get; set; }
            public string ChargeAmount { get; set; }
            public string NumberofDecals { get; set; }
            public string AdditionalDecalCount { get; set; }
            public string TotalDecalCount { get; set; }
            public string IdentityTheftRecovery { get; set; }
            public string AdditionalDecals { get; set; }
            public string CreatedBy { get; set; }


            public string CreadtedByID { get; set; }
            public string CreadtedCount { get; set; }
            public string IDTHEFTSUM { get; set; }
            public string AdditionalDECALSUM { get; set; }
            public string TotalDECALSUM { get; set; }
            public string AdditionalDECALPer { get; set; }
            public string IDTHEFTPer { get; set; }

            public string GrandCreadtedCount { get; set; }
            public string GRANDIDTHEFTSUM { get; set; }
            public string GRANDAdditionalDECALSUM { get; set; }
            public string GRANDTotalDECALSUM { get; set; }
            public string GRANDIDTHEFTPer { get; set; }
            public string GRANDAdditionalPER { get; set; }

            public string IsRenewal { get; set; }
            public string RenewalCount { get; set; }
            public string RenewalTotal { get; set; }
           
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