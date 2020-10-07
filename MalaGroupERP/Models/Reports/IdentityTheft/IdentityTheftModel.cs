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
    public class IdentityTheftModel
    {
       
        public string CloseDate { get; set; }
        public int?  IdentityTheft { get; set; }
        public string ExportFileName { get; set; }
        public int?  AddDecal { get; set; }
        public string AccountStatus { get; set; }
        public string Product { get; set; }
        public List<IdentityTheftData> IdentityTheftDataList { get; set; }
        public class IdentityTheftData
        {
            public string CloseDate { get; set; }
            public string IDTheft { get; set; }
            public string ADDDecal { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZIP { get; set; }
            public string PHONE { get; set; }
            public string Password { get; set; }
            public string PINNO { get; set; }
            public string CreadtedBy { get; set; }

            public string IsRenewal { get; set; }
            public string RenewalCount { get; set; }
        }


        public IdentityTheftModel GetIdentityTheftReport(IdentityTheftModel model)
        {
            (new CommonModel()).DeleteFiles();
            string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            string fileName = "";
            
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            DataTable dtTable = new DataTable("IDTHEFT");
            fileName = Guid.NewGuid().ToString() + ".xlsx";

            var filemergepath = HttpContext.Current.Server.MapPath("~/ReportStructure/IDTHEFT.xlsx");
           
            FileInfo file = new FileInfo(filemergepath);
            file.CopyTo(filePath + "/" + fileName, true);

            FileInfo excelFile = new FileInfo(filePath + "/" + fileName);

            IdentityTheftModel identityTheftModel = new IdentityTheftModel();
            identityTheftModel.ExportFileName = fileName;
            List<IdentityTheftData> Idata = new List<IdentityTheftData>();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    db.Database.CommandTimeout = 0;
                    cmd.CommandText = "usp_GetIDTheftList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramSRDF = cmd.CreateParameter();
                    paramSRDF.ParameterName = "CloseDateFrom";
                    paramSRDF.Value = (model.CloseDate != null && model.CloseDate.Length > 0 ? model.CloseDate.Split('-')[0] : null);
                    cmd.Parameters.Add(paramSRDF);

                    DbParameter paramSRDT = cmd.CreateParameter();
                    paramSRDT.ParameterName = "CloseDateTo";
                    paramSRDT.Value = (model.CloseDate != null && model.CloseDate.Length > 0 ? model.CloseDate.Split('-')[1] : null);
                    cmd.Parameters.Add(paramSRDT);

                    DbParameter paramGrt = cmd.CreateParameter();
                    paramGrt.ParameterName = "IdentityTheft";
                    paramGrt.Value = model.IdentityTheft;
                    cmd.Parameters.Add(paramGrt);

                    DbParameter paramLess = cmd.CreateParameter();
                    paramLess.ParameterName = "AddDecal";
                    paramLess.Value = model.AddDecal;
                    cmd.Parameters.Add(paramLess);

                    DbParameter paramLAcc = cmd.CreateParameter();
                    paramLAcc.ParameterName = "AccountStatus";
                    paramLAcc.Value = model.AccountStatus != null ? model.AccountStatus.TrimEnd(',') : "0"; 
                    cmd.Parameters.Add(paramLAcc);

                    DbParameter paramPro = cmd.CreateParameter();
                    paramPro.ParameterName = "Package";
                    paramPro.Value = model.Product!=null?model.Product.TrimEnd(','):"0"; 
                    cmd.Parameters.Add(paramPro);

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
               // worksheet.Cells["A3"].Value = " As of " + DateTime.Now.ToString("MM/dd/yyyy") + " • Generated by " + CommonModel.GetUserFullName();

                int rowNum = 2;

                foreach (DataRow dr2 in dtTable.Rows)
                {
                    
                        worksheet.Cells["A" + rowNum.ToString() + ":N" + rowNum.ToString()].Style.Font.Size = 12;
                        worksheet.Cells["A" + rowNum.ToString() + ":N" + rowNum.ToString()].Style.Font.Name = "Calibri";
                        worksheet.Cells["A" + rowNum.ToString() + ":N" + rowNum.ToString()].Style.Font.Bold = false;

                        worksheet.Cells["A" + rowNum.ToString()].Value = dr2[0].ToString();
                        worksheet.Cells["B" + rowNum.ToString()].Value = dr2[1].ToString()=="1" ?"True":"False";
                        worksheet.Cells["C" + rowNum.ToString()].Value = dr2[2].ToString()== "1" ? "True" : "False";
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
                        worksheet.Cells["N" + rowNum.ToString()].Value = dr2[15].ToString()== "1" ? "Renewed" : "";
                    rowNum += 1;
                }
              
                FileInfo fi = new FileInfo(filePath + "/" + fileName);
                excelPackage.SaveAs(fi);
            }
            foreach (DataRow dr in dtTable.Rows)
            {
                //Pin Number	Person Account: First Name	Person Account: Last Name	Shipping Street	Shipping City	Shipping State/Province	Shipping Zip/Postal Code	VIN	LastPurchase	
                DateTime? closeDate = null;
                try
                {
                    closeDate = Convert.ToDateTime(dr["Close Date"].ToString());
                }
                catch
                {
                    closeDate = null;

                }

                Idata.Add(new IdentityTheftData()
                {

                    CloseDate = closeDate != null ? closeDate.Value.ToString("MM/dd/yyyy") : " ",
                    IDTheft = dr["Identity Theft Recovery"].ToString(),
                    ADDDecal = dr["Additional Decals"].ToString(),
                    FirstName = dr["Person Account: First Name"].ToString(),
                    LastName = dr["Person Account: Last Name"].ToString(),
                    Address = dr["Person Account: Mailing Street"].ToString(),
                    City = dr["Person Account: Mailing City"].ToString(),
                    State = dr["Person Account: Mailing State/Province"].ToString(),
                    ZIP = dr["Person Account: Mailing Zip/Postal Code"].ToString(),
                    PHONE = dr["Primary Phone"].ToString(),
                    Password = dr["Password Id"].ToString(),
                    PINNO = dr["Pin Number"].ToString(),
                    CreadtedBy = dr["Created By"].ToString(),
                    IsRenewal = dr["IsRenewal"].ToString(),
                    RenewalCount = dr["RenewalCount"].ToString(),
                });
            }
            identityTheftModel.IdentityTheftDataList = Idata;
            return identityTheftModel;
        }
    }
}