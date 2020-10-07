using MalaGroupERP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace MalaGroupERP.Models
{
    public class PackageModel
    {
        public int PackageID { get; set; }
        public string Package { get; set; }
        public string Price { get; set; }
        public string ExpiryDate { get; set; }
        public int NoOfInstallment { get; set; }
        public string Additional { get; set; }
        public int InPackageAdditional { get; set; }
        public int RowDisplay { get; set; }
        public int PageNumber { get; set; }
        public int IsRenewal { get; set; }
        public int Decal { get; set; }
        public int IdentityTheft { get; set; }
        public int DecalNumber { get; set; }
        public PackageModel GetPackageInfo(int PackageID)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            PackageModel model = new PackageModel();

            model.PackageID = 0;
            model.Package = "";
            model.Price = "";
            model.NoOfInstallment = 0;
            model.Decal = 0;
            model.IdentityTheft = 0;
            model.DecalNumber = 0;
            
            model.ExpiryDate = DateTime.Now.ToString("MM/dd/yyyy");
            model.InPackageAdditional = 0;

            if (PackageID > 0)
            {

                DataSet dsDataSet = new DataSet();              
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetPackageInfoData";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter AID = cmd.CreateParameter();
                        AID.ParameterName = "PackageID ";
                        AID.Value = PackageID;
                        cmd.Parameters.Add(AID);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dsDataSet);
                        db.Database.Connection.Close();
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                    DateTime? expirationDate = null;
                    try
                    {
                        expirationDate = Convert.ToDateTime(dsDataSet.Tables[0].Rows[0]["ExpiryDate"].ToString());
                    }
                    catch
                    {

                    }
                    if (dsDataSet.Tables[0] != null && dsDataSet.Tables[0].Rows.Count > 0)
                    {

                        model.PackageID = int.Parse(dsDataSet.Tables[0].Rows[0]["PackageID"].ToString());
                        model.Package = dsDataSet.Tables[0].Rows[0]["Package"].ToString();
                        model.Price = dsDataSet.Tables[0].Rows[0]["Price"].ToString();
                        model.ExpiryDate = (expirationDate.HasValue ? expirationDate.Value.ToString("MM/dd/yyyy") : "");
                        model.NoOfInstallment =Convert.ToInt32(dsDataSet.Tables[0].Rows[0]["NoOfInstallment"].ToString());
                        model.InPackageAdditional = Convert.ToInt32(dsDataSet.Tables[0].Rows[0]["Additional"].ToString());
                        model.Decal = Convert.ToInt32(dsDataSet.Tables[0].Rows[0]["Decal"].ToString());
                        model.IdentityTheft = Convert.ToInt32(dsDataSet.Tables[0].Rows[0]["IdentityTheft"].ToString());
                        model.DecalNumber = Convert.ToInt32(dsDataSet.Tables[0].Rows[0]["DecalNumber"].ToString());
                        model.IsRenewal = Convert.ToInt32(dsDataSet.Tables[0].Rows[0]["IsRenewal"].ToString());
                    }
                }
            }
            return model;
        }
        public string SaveUpdatePackage(PackageModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string packageID = "0";
            if (model.PackageID == 0)
            {

                var pckSave = new tbl_Package()
                {
                    PackageID = Convert.ToInt32(model.PackageID),
                    Package = model.Package,
                    Price = model.Price,
                    ExpiryDate=Convert.ToDateTime(model.ExpiryDate),
                    NoOfInstallment=model.NoOfInstallment,
                    Additional = model.InPackageAdditional,
                    AddDecal=model.Decal,
                    DecalNumber=model.DecalNumber,
                    AddIdentityTheft=model.IdentityTheft,
                    IsRenewal=model.IsRenewal,
                };
                db.tbl_Package.Add(pckSave);
                db.SaveChanges();
                model.PackageID = pckSave.PackageID;
                packageID = pckSave.PackageID.ToString();
                db.Dispose();
            }
            else
            {
                var packUpdate = db.tbl_Package.Where(p => p.PackageID == model.PackageID).FirstOrDefault();

                packUpdate.Package = model.Package;
                packUpdate.Price = model.Price;
                packUpdate.ExpiryDate=Convert.ToDateTime(model.ExpiryDate);
                packUpdate.NoOfInstallment=model.NoOfInstallment;
                packUpdate.Additional = model.InPackageAdditional;
                packUpdate.AddDecal = model.Decal;
                packUpdate.DecalNumber = model.DecalNumber;
                packUpdate.AddIdentityTheft = model.IdentityTheft;
                packUpdate.IsRenewal = model.IsRenewal;
                db.SaveChanges();
                packageID = model.PackageID.ToString();
            }
            db.Dispose();
            return packageID;
        }
        public string DeletePackageData(int PackageID)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            try
            {
                var pckData = db.tbl_Package.Where(p => p.PackageID == PackageID).FirstOrDefault();

                if (pckData != null)
                {
                    db.tbl_Package.Remove(pckData);
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
        public List<PackageModel> GetPackageList()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<PackageModel> model = new List<PackageModel>();
            var packageList = db.tbl_Package.Where(p =>p.Additional==0 && p.ExpiryDate >DateTime.Now).ToList();
            foreach (var vk in packageList)
            {
                model.Add(new PackageModel() 
                {   PackageID = vk.PackageID,
                    Package = vk.Package,
                    Price=vk.Price ,
                    NoOfInstallment=Convert.ToInt32(vk.NoOfInstallment),
                    Additional=vk.Additional.ToString(),
                   IsRenewal=Convert.ToInt32(vk.IsRenewal),
                }
                );
            }
            return model.ToList();
        }
        public List<PackageModel> GetAllPackageList()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<PackageModel> model = new List<PackageModel>();
            var packageList = db.tbl_Package.Where(p =>  p.ExpiryDate > DateTime.Now).ToList();
            foreach (var vk in packageList)
            {
                model.Add(new PackageModel()
                {
                    PackageID = vk.PackageID,
                    Package = vk.Package,
                    Price = vk.Price,
                    //NoOfInstallment = Convert.ToInt32(vk.NoOfInstallment),
                   // Additional = vk.Additional.ToString(),
                    IsRenewal = Convert.ToInt32(vk.IsRenewal),
                }
                );
            }
            return model.ToList();
        }
        public List<PackageModel> GetPackageListDet(string AdditionalPackage)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                List<PackageModel> returnPackageModel = new List<PackageModel>();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetAdditionalPackage";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramDN = cmd.CreateParameter();
                        paramDN.ParameterName = "AdditionalPackage";
                        paramDN.Value = AdditionalPackage;
                        cmd.Parameters.Add(paramDN);



                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            returnPackageModel.Add(new PackageModel()
                            {
                                Package = dr["Package"].ToString(),
                                Price = dr["Price"].ToString(),
                              
                            });
                        }
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
                return returnPackageModel.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PackageModel> GetAddPackageList()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<PackageModel> model = new List<PackageModel>();
            var packageList = db.tbl_Package.Where(p =>p.Additional==1 && p.ExpiryDate > DateTime.Now).ToList();
            foreach (var vk in packageList)
            {
                model.Add(new PackageModel()
                {
                    PackageID = vk.PackageID,
                    Package = vk.Package,
                    Price = vk.Price,
                    NoOfInstallment = Convert.ToInt32(vk.NoOfInstallment),
                    Additional = vk.Additional.ToString(),
                    
                }
                );
            }
            return model.ToList();
        }

        public List<PackageModel> GetFullPackageList(PackageModel model)
        {

            try
            {
                List<PackageModel> listSearch = new List<PackageModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetPackageInfoPageList";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramName = cmd.CreateParameter();
                        paramName.ParameterName = "Package";
                        paramName.Value = model.Package;
                        cmd.Parameters.Add(paramName);

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
                            DateTime? expirationDate = null;
                            try
                            {
                                expirationDate = Convert.ToDateTime(dr["ExpiryDate"].ToString());
                            }
                            catch
                            {

                            }

                            listSearch.Add(new PackageModel()
                            {
                                PackageID = Convert.ToInt32(dr["PackageID"].ToString()),
                                Package = dr["Package"].ToString(),
                                Price = dr["Price"].ToString(),
                                NoOfInstallment = Convert.ToInt32(dr["NoOfInstallment"].ToString()),
                                Additional = dr["Additional"].ToString(),
                                ExpiryDate = (expirationDate.HasValue ? expirationDate.Value.ToString("MM/dd/yyyy") : "")
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
        public string GetPackageFilterRangeList(PackageModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            DataTable dtTable = new DataTable();
            string PageNumber = "0";
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPackageInfoPageListRange";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "Package";
                    paramName.Value = model.Package;
                    cmd.Parameters.Add(paramName);

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
    }
}