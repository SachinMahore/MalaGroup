using MalaGroupERP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using MalaGroupERP.Models;
using System.IO;
using OfficeOpenXml;

namespace MalaGroupERP.Models
{
    public class CustomReportModel
    {
        public long CustomReportID { get; set; }
        public int IsPublic { get; set; }
        public string CustomReportName { get; set; }
        public int CustomReportFor { get; set; }
        public string CustomReportForText { get; set; }
        public long CusReportFeildListID { get; set; }
        public string DisplayName { get; set; }
        public int IsDropDown { get; set; }
        public string CreatedBy { get; set; }
        public string ModiFiedBy { get; set; }
        public string CreatedDate { get; set; }

        public string ModiFiedDate { get; set; }
        public int IsSaved { get; set; }
        public string CIDs { get; set; }
        public List<CustomReportModel> CustomFieldsDisplayName { get; set; }
        public List<CustomFilterData> CustomFilterList { get; set; }
        public int RowDisplay { get; set; }
        public int PageNumber { get; set; }


        public long Step1(CustomReportModel model)
        {
            long id =0;
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            var addCusReport = new tbl_CustomReport()
            {
                ReportName = model.CustomReportName,
                TableID = Convert.ToInt32(model.CustomReportFor),
                IsPublic = Convert.ToInt32(model.IsPublic),
                CreatedtedBY = MalaGroupWebSession.CurrentUser.UserID,
                CreatdedDate = DateTime.Now,
                ModifiedBY = MalaGroupWebSession.CurrentUser.UserID,
                ModifiedDate = DateTime.Now,
                IsSaved=model.IsSaved
            };
            db.tbl_CustomReport.Add(addCusReport);
            db.SaveChanges();
            id = addCusReport.ID;
            db.Dispose();
           
            return id;
        }
        public long Step2(string CIDs, long CusReportID)
        {
            long id = 0;
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            var isExist = db.tbl_CustomReportFields.Where(p => p.ReportID == CusReportID).FirstOrDefault();
            if (isExist==null)
            {
                var addFdReport = new tbl_CustomReportFields()
                {
                    ReportID = CusReportID,
                    FieldID = CIDs
                };
                db.tbl_CustomReportFields.Add(addFdReport);
                db.SaveChanges();
               
                id = addFdReport.ID;
            }
            else
            {
                isExist.ReportID=CusReportID;
                isExist.FieldID = CIDs;
                db.SaveChanges();
                id = isExist.ID;
            }
            db.Dispose();
           
            return id;
        }

        public string SaveCusReFilterTxt(CustomReportModel model )
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            if (model.CustomFilterList != null)
            {
                foreach (var vl in model.CustomFilterList)
                {
                    var vData = new tbl_CustomReportFilters
                    {
                        ReportID = vl.ReportID,
                        FieldCriteria = vl.DataText,
                    };
                    db.tbl_CustomReportFilters.Add(vData);
                    db.SaveChanges();
                };
            }
            db.Dispose();
            msg = "Data Added Successfully..!";
            return msg;
        }


        public string  LastStep(CustomReportModel model)
        {

            MalaGroupERPEntities db = new MalaGroupERPEntities();
            long ReportID = 0;
            var addCusReport = new tbl_CustomReport()
            {
                ReportName = model.CustomReportName,
                TableID = Convert.ToInt32(model.CustomReportFor),
                IsPublic = Convert.ToInt32(model.IsPublic),
                CreatedtedBY = MalaGroupWebSession.CurrentUser.UserID,
                CreatdedDate = DateTime.Now,
                ModifiedBY = MalaGroupWebSession.CurrentUser.UserID,
                ModifiedDate = DateTime.Now,
                IsSaved = model.IsSaved
            };
            db.tbl_CustomReport.Add(addCusReport);
            db.SaveChanges();
            ReportID = addCusReport.ID;

            if (ReportID>0)
            {
                var addFdReport = new tbl_CustomReportFields()
                {
                    ReportID = ReportID,
                    FieldID = model.CIDs
                };
                db.tbl_CustomReportFields.Add(addFdReport);
                db.SaveChanges();

                if (model.CustomFilterList != null)
                {
                    foreach (var vl in model.CustomFilterList)
                    {
                        var vData = new tbl_CustomReportFilters
                        {
                            ReportID = ReportID,
                            FieldCriteria = vl.DataText,
                        };
                        db.tbl_CustomReportFilters.Add(vData);
                        db.SaveChanges();
                    };
                }
            }

            (new CommonModel()).DeleteFiles();
            string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            string fileName = "";
            fileName = Guid.NewGuid().ToString() + ".xlsx";
           DataTable dtTable = new DataTable();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    db.Database.CommandTimeout = 0;
                    cmd.CommandText = "usp_GetAccountReport";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramWdf = cmd.CreateParameter();
                    paramWdf.ParameterName = "ReportID";
                    paramWdf.Value = ReportID;
                    cmd.Parameters.Add(paramWdf);


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
           
            
            var  finalFileName = fileName;


            return finalFileName;
           

        }
        public List<CustomReportModel> GetCustomReportList(long TableID)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<CustomReportModel> model = new List<CustomReportModel>();

            DataTable dtTable = new DataTable();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();

                    cmd.CommandText = "usp_GetReportFieldList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "TableID";
                    paramName.Value = TableID;
                    cmd.Parameters.Add(paramName);
                  
                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();

                    foreach (DataRow dr in dtTable.Rows)
                    {

                        model.Add(new CustomReportModel()
                        {
                            CusReportFeildListID = Convert.ToInt64(dr["ID"].ToString()),
                            IsDropDown = Convert.ToInt32(dr["IsDropDown"].ToString()),
                            DisplayName = dr["DiaplayName"].ToString(),
                        });
                    }
                }
                catch
                {
                    db.Database.Connection.Close();
                }
            }
            return model.ToList();
        }
        public List<CustomReportModel> GetCusReportFieldLabelList(long CusID, int Type)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<CustomReportModel> model = new List<CustomReportModel>();

            DataTable dtTable = new DataTable();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();

                    cmd.CommandText = "usp_GetSelectReportTablesFields";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "CusID";
                    paramName.Value = CusID;
                    cmd.Parameters.Add(paramName);

                    DbParameter paramType = cmd.CreateParameter();
                    paramType.ParameterName = "Type";
                    paramType.Value = Type;
                    cmd.Parameters.Add(paramType);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();

                    foreach (DataRow dr in dtTable.Rows)
                    {

                        model.Add(new CustomReportModel()
                        {
                            DisplayName = dr["DiaplayName"].ToString(),
                        });
                    }
                }
                catch
                {
                    db.Database.Connection.Close();
                }
            }
            return model.ToList();
        }
        public List<DropDownModel> GetDmFilterField(long CusID)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<DropDownModel> model = new List<DropDownModel>();

            DataTable dtTable = new DataTable();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();

                    cmd.CommandText = "usp_GetDDFilterField";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "ReportFor";
                    paramName.Value = CusID;
                    cmd.Parameters.Add(paramName);

                   

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();

                    foreach (DataRow dr in dtTable.Rows)
                    {

                        model.Add(new DropDownModel() { Value = dr["ColumnName"].ToString() + "," + dr["DataType"].ToString(), Text = dr["DiaplayName"].ToString() });
                    }
                }
                catch
                {
                    db.Database.Connection.Close();
                }
            }
            return model;
        }

      

        public List<DropDownModel> GetCreatedBy()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<DropDownModel> model = new List<DropDownModel>();
            var user = db.tblLogins.ToList().OrderBy(p => p.FirstName);
            foreach (var vk in user)
            {
                model.Add(new DropDownModel() { Value = vk.UserID.ToString(), Text = vk.FirstName + " " + vk.LastName });
            }
            return model;
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
        public string DeleteCustomReportFilters(long ReportID)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            try
            {
                var cusData = db.tbl_CustomReportFilters.Where(p => p.ID == ReportID).FirstOrDefault();

                if (cusData != null)
                {
                    db.tbl_CustomReportFilters.Remove(cusData);
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


        public string ExportReport(long ReportID)
        {
            (new CommonModel()).DeleteFiles();
            string filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            string fileName = "";
            fileName = Guid.NewGuid().ToString() + ".xlsx";  
            MalaGroupERPEntities db = new MalaGroupERPEntities();
           
            DataTable dtTable = new DataTable();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    db.Database.CommandTimeout = 0;
                    cmd.CommandText = "usp_GetAccountReport";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramWdf = cmd.CreateParameter();
                    paramWdf.ParameterName = "ReportID";
                    paramWdf.Value = ReportID;
                    cmd.Parameters.Add(paramWdf);


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
            return fileName ;
        }


        public string GetCustomReportListRange(CustomReportModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            DataTable dtTable = new DataTable();
            string PageNumber = "0";
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCustomReportListRange";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramPub = cmd.CreateParameter();
                    paramPub.ParameterName = "IsPublic";
                    paramPub.Value = 1;
                    cmd.Parameters.Add(paramPub);

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

        public List<CustomReportModel> GetCustomReportPageList(CustomReportModel model)
        {
            try
            {
                List<CustomReportModel> listSearch = new List<CustomReportModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetCustomReportList";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramPub = cmd.CreateParameter();
                        paramPub.ParameterName = "IsPublic";
                        paramPub.Value = 1;
                        cmd.Parameters.Add(paramPub);

                        DbParameter paramRD = cmd.CreateParameter();
                        paramRD.ParameterName = "RowDisplay";
                        paramRD.Value = model.RowDisplay;
                        cmd.Parameters.Add(paramRD);

                        DbParameter paramPN = cmd.CreateParameter();
                        paramPN.ParameterName = "PageNumber";
                        paramPN.Value = model.PageNumber == 0 ? 1 : model.PageNumber;
                        cmd.Parameters.Add(paramPN);
                       
                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? createdDate = null;
                            try
                            {
                                createdDate = Convert.ToDateTime(dr["CreatdedDate"].ToString());
                            }
                            catch
                            {

                            }
                            DateTime? modifiedDate = null;
                            try
                            {
                                modifiedDate = Convert.ToDateTime(dr["ModifiedDate"].ToString());
                            }
                            catch
                            {

                            }
                            listSearch.Add(new CustomReportModel()
                            {
                                CustomReportID = Convert.ToInt64(dr["ReportID"].ToString()),
                                CustomReportName = dr["ReportName"].ToString(),
                                CustomReportForText = dr["ReportFor"].ToString(),
                                CreatedBy = dr["CreatedBy"].ToString(),
                                CreatedDate = createdDate.Value.ToString("MM/dd/yyyy"),
                                ModiFiedBy = dr["ModiFiedBy"].ToString(),
                                ModiFiedDate = modifiedDate.Value.ToString("MM/dd/yyyy"),
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
    
    }

    public class CustomFilterData
    {
        public string DataText { get; set; }
        public long ReportID { get; set; }
    }

  

}