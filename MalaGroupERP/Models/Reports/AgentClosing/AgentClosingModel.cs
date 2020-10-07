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
    public class AgentClosingModel
    {
        public string LastModiFiedBy { get; set; }
        public string LastModiFiedDate { get; set; }
        public string State { get; set; }
        public string ExportFileName { get; set; }
        public string AccountStatus { get; set; }
        public string Product { get; set; }
        public List<AgentClosingData> agentClosingData { get; set; }
        public AgentClosingGraph agentClosingGraph { get; set; }
        public AgentClosingModel ExportReport(AgentClosingModel model)
        {
            (new CommonModel()).DeleteFiles();
            string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            string fileName = "";
            
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            DataSet dt1Table = new DataSet("Export Daily");

            DateTime dateTime = DateTime.UtcNow.Date;
            fileName = Guid.NewGuid().ToString() + ".xlsx";
            var filemergepath = HttpContext.Current.Server.MapPath("~/ReportStructure/AGENTCLOSING.xlsx");
            FileInfo file = new FileInfo(filemergepath);
            file.CopyTo(filePath + "/" + fileName, true);
            FileInfo excelFile = new FileInfo(filePath + "/" + fileName);
          
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    db.Database.CommandTimeout = 0;
                    cmd.CommandText = "usp_AgentClosingReport";
                    cmd.CommandType = CommandType.StoredProcedure;

                   
                    DbParameter paramSRDF = cmd.CreateParameter();
                    paramSRDF.ParameterName = "LastModifiedFrom";
                    paramSRDF.Value = (model.LastModiFiedDate != null && model.LastModiFiedDate.Length > 0 ? model.LastModiFiedDate.Split('-')[0] : null);
                    cmd.Parameters.Add(paramSRDF);

                    DbParameter paramSRDT = cmd.CreateParameter();
                    paramSRDT.ParameterName = "LastModifiedTo";
                    paramSRDT.Value = (model.LastModiFiedDate != null && model.LastModiFiedDate.Length > 0 ? model.LastModiFiedDate.Split('-')[1] : null);
                    cmd.Parameters.Add(paramSRDT);   

                    DbParameter paramACC = cmd.CreateParameter();
                    paramACC.ParameterName = "LastModifiedBY";
                    paramACC.Value = model.LastModiFiedBy != null ? model.LastModiFiedBy.TrimEnd(',') : "0"; 
                    cmd.Parameters.Add(paramACC);
                    
                    DbParameter paramTRN = cmd.CreateParameter();
                    paramTRN.ParameterName = "State";
                    paramTRN.Value = model.State!=null? model.State:"0" ;
                    cmd.Parameters.Add(paramTRN);

                    DbParameter paramACCS = cmd.CreateParameter();
                    paramACCS.ParameterName = "AccountStatus";
                    paramACCS.Value = model.AccountStatus != null ? model.AccountStatus.TrimEnd(',') : "0"; 
                    cmd.Parameters.Add(paramACCS);

                    DbParameter paramPro = cmd.CreateParameter();
                    paramPro.ParameterName = "Package";
                    paramPro.Value = model.Product != null ? model.Product.TrimEnd(',') : "0";
                    cmd.Parameters.Add(paramPro);


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

             AgentClosingModel agentClosingModel = new AgentClosingModel();
             agentClosingModel.ExportFileName = fileName;
             List<AgentClosingData> newAgentData = new List<AgentClosingData>();
             AgentClosingGraph agentClosingG = new AgentClosingGraph();

             foreach (DataRow dr in dtTable.Rows)
             {
                 newAgentData.Add(new AgentClosingData()
                 {
                     LastModifiedBy      = dr["Last Modified By"].ToString(),
                     ListCode            = dr["List Code"].ToString(),
                     FirstName           = dr["First Name"].ToString(),
                     LastName            = dr["Last Name"].ToString(),
                     VehicleYear         = dr["Vehicle Year"].ToString(),
                     PinNumber           = dr["Pin Number"].ToString(),
                     State               = dr["State/Province"].ToString(),
                     Stage               = dr["Stage"].ToString(),
                     LeadStatus          = dr["Lead Status"].ToString(),
                     ConvertedDate       = dr["Converted Date"].ToString(),
                     OpportunityAccount  = dr["Opportunity: Account"].ToString(),
                     OpportunityAmount   = dr["Opportunity Amount"].ToString(),
                     OpptCloseDate       = dr["Oppt Close Date"].ToString(),
                     FileOpen            = dr["FileOpen"].ToString(),
                     Deal                = dr["Deal"].ToString(),
                     ClosingPer          = dr["ClosingPer"].ToString(),
                     FileOpenTotal       = dr["FileOpenTotal"].ToString(),
                     DealTotal           = dr["DealTotal"].ToString(),
                     ClosingPerTotal     = dr["ClosingPerTotal"].ToString(),
                     LastModifiedByID    = dr["LastModifiedByID"].ToString(),
                     StepCompletedPer    = dr["StepCompletedPer"].ToString(),
                 });
             }

             int i = 0;
             foreach (DataRow dr in dtTable2.Rows)
             {
                 				
      		

                 if (i == 0)
                 {
                     agentClosingG.FOpenTotal = dr["FOpenTotal"].ToString();
                     agentClosingG.DSUMTOtal = dr["DSUMTOtal"].ToString();
                     agentClosingG.CPER = dr["CPER"].ToString();
                     
                 }
                 agentClosingG.Labels += (agentClosingG.Labels == null ? dr["LastModiBY"].ToString() : "," + dr["LastModiBY"].ToString());
                 agentClosingG.FOpenCount += (agentClosingG.FOpenCount == null ? dr["FOpenCount"].ToString() : "," + dr["FOpenCount"].ToString());
                 
                 i++;
             }
             agentClosingModel.agentClosingData = newAgentData;
             agentClosingModel.agentClosingGraph = agentClosingG;

             using (ExcelPackage excelPackage = new ExcelPackage(excelFile))
             {
                 ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];

                // worksheet.Cells["A3"].Value = " As of " + DateTime.Now.ToString("MM/dd/yyyy") + " • Generated by " + CommonModel.GetUserFullName();
                 int colCount = 1;
                 var lastmodifiedByID = "0";
                 var count = 0;
                 var isChange = false;
                 int rowNum = 2;

                 var FileOpen        = "0";
                 var Deal            = "0";
                 var ClosingPer      = "0";
                
                 var rowCount = dtTable.Rows.Count;
                             
                 //worksheet.Column(10).Style.Numberformat.Format = "MM/dd/yyyy  HH:mm";
                 foreach (DataRow dr in dtTable.Rows)
                 {
                     worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Font.Size = 12;
                     worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Font.Name = "Calibri";
                     worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Font.Bold = false;


                     rowCount -= 1;
                     if (lastmodifiedByID != dr["LastModifiedByID"].ToString())
                     {
                         if (count == 0)
                         {
                             lastmodifiedByID = dr["LastModifiedByID"].ToString();
                             FileOpen = dr["FileOpen"].ToString();
                             Deal = dr["Deal"].ToString();
                             ClosingPer = dr["ClosingPer"].ToString();
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

                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Top.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Right.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Bottom.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Left.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 249, 249, 247);
                         worksheet.Cells["A" + rowNum.ToString()].Value = "Subtotal     Count";
                         worksheet.Cells["B" + rowNum.ToString()].Value = FileOpen;
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
                         worksheet.Cells["N" + rowNum.ToString()].Value =FileOpen;
                         worksheet.Cells["O" + rowNum.ToString()].Value = Deal;
                         worksheet.Cells["P" + rowNum.ToString()].Value =ClosingPer+"%";
                         rowNum += 1;

                         lastmodifiedByID = dr["LastModifiedByID"].ToString();
                         FileOpen = dr["FileOpen"].ToString();
                         Deal = dr["Deal"].ToString();
                         ClosingPer = dr["ClosingPer"].ToString();
                     }

                     worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Font.Size = 12;
                     worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Font.Name = "Calibri";
                     worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Font.Bold = false;

              												
                     worksheet.Cells["A" + rowNum.ToString()].Value = dr[0].ToString(); 
                     worksheet.Cells["B" + rowNum.ToString()].Value = dr[1].ToString(); 
                     worksheet.Cells["C" + rowNum.ToString()].Value = dr[2].ToString(); 
                     worksheet.Cells["D" + rowNum.ToString()].Value = dr[3].ToString(); 
                     worksheet.Cells["E" + rowNum.ToString()].Value = dr[4].ToString(); 
                     worksheet.Cells["F" + rowNum.ToString()].Value = dr[5].ToString(); 
                     worksheet.Cells["G" + rowNum.ToString()].Value = dr[6].ToString(); 
                     worksheet.Cells["H" + rowNum.ToString()].Value = dr[7].ToString(); 
                     worksheet.Cells["I" + rowNum.ToString()].Value = dr[8].ToString(); 
                     worksheet.Cells["J" + rowNum.ToString()].Value = dr[9].ToString(); 
                     worksheet.Cells["K" + rowNum.ToString()].Value = dr[10].ToString();
                     worksheet.Cells["L" + rowNum.ToString()].Value = dr[11].ToString();
                     worksheet.Cells["M" + rowNum.ToString()].Value = dr[12].ToString(); ;
                     worksheet.Cells["N" + rowNum.ToString()].Value ="";
                     worksheet.Cells["O" + rowNum.ToString()].Value = "";
                     worksheet.Cells["P" + rowNum.ToString()].Value = "";
                     rowNum += 1;

                     if (rowCount == 0)
                     {

                         isChange = false;
                         worksheet.Cells["A" + rowNum.ToString()].Value = "Subtotal     Count";
                         worksheet.Cells["B" + rowNum.ToString()].Value = FileOpen;
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
                         worksheet.Cells["N" + rowNum.ToString()].Value = FileOpen;
                         worksheet.Cells["O" + rowNum.ToString()].Value = Deal;
                         worksheet.Cells["P" + rowNum.ToString()].Value = ClosingPer + "%";
                         rowNum += 1;


                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Top.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Right.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Bottom.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Border.Left.Color.SetColor(0, 213, 211, 209);
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Fill.BackgroundColor.SetColor(0, 249, 249, 247);
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Font.Size = 12;
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Font.Name = "Calibri";
                         worksheet.Cells["A" + rowNum.ToString() + ":P" + rowNum.ToString()].Style.Font.Bold = false;
                         worksheet.Cells["A" + rowNum.ToString()].Value = "Total   Count";
                         worksheet.Cells["B" + rowNum.ToString()].Value = dr[17].ToString();
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
                         worksheet.Cells["N" + rowNum.ToString()].Value =dr[17].ToString() ;
                         worksheet.Cells["O" + rowNum.ToString()].Value =dr[18].ToString();
                         worksheet.Cells["P" + rowNum.ToString()].Value = dr[19].ToString() + "%";
                        
                         rowNum += 1;


                         lastmodifiedByID = dr["LastModifiedByID"].ToString();
                         FileOpen = dr["FileOpen"].ToString();
                         Deal = dr["Deal"].ToString();
                         ClosingPer = dr["ClosingPer"].ToString();
                     }
                 }


                 FileInfo fi = new FileInfo(filePath + "/" + fileName);
                 excelPackage.SaveAs(fi);
             }
             return agentClosingModel;
        }

        public class AgentClosingGraph
        {
            public string LastModiID { get; set; }
            public string Labels { get; set; }
            public string FOpenCount { get; set; }
            public string DSUM { get; set; }
            public string FOpenTotal { get; set; }
            public string DSUMTOtal { get; set; }
            public string CPER { get; set; }

        }

        public class AgentClosingData
        {
            public string ListCode { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string VehicleYear { get; set; }
            public string PinNumber { get; set; }
            public string State { get; set; }
            public string Stage { get; set; }
            public string LeadStatus { get; set; }
            public string ConvertedDate { get; set; }
            public string OpportunityAccount { get; set; }
            public string OpportunityAmount { get; set; }
            public string OpptCloseDate { get; set; }
            public string LastModifiedBy { get; set; }
            public string LastModifiedByID { get; set; }
            public string FileOpen { get; set; }
            public string Deal { get; set; }
            public string ClosingPer { get; set; }
            public string FileOpenTotal { get; set; }
            public string DealTotal { get; set; }
            public string ClosingPerTotal { get; set; }
            public string StepCompletedPer { get; set; }

            public string StepCompletedPerTotal { get; set; }
        }

        public List<DropDownModel> GetLastModified()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<DropDownModel> model = new List<DropDownModel>();
            var user = db.tblLogins.ToList().OrderBy(p => p.FirstName);
            foreach (var vk in user)
            {
                model.Add(new DropDownModel() { Value = vk.UserID.ToString(), Text = vk.FirstName+" "+vk.LastName });
            }
            return model;
        }

        //public List<DropDownModel> GetState()
        //{
        //    MalaGroupERPEntities db = new MalaGroupERPEntities();
        //    List<DropDownModel> model = new List<DropDownModel>();
        //    var state = db.tbl_Accounts.GroupBy(p=>p.ShippingState).Select(p=>p.FirstOrDefault()).ToList().OrderBy(p => p.ShippingState);


        //    foreach (var vk in state)
        //    {
        //        model.Add(new DropDownModel() { Value = vk.ShippingState.ToString(), Text = vk.ShippingState});
        //    }
        //    return model;
        //}
       
    }
}