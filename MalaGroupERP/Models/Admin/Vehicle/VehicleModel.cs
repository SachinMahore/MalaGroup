using MalaGroupERP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace MalaGroupERP.Models
{
    public class VehicleModel
    {
        public int VID { get; set; }
        public int VTID { get; set; }
        public int VMID { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleType { get; set; }
        public int RowDisplay { get; set; }
        public int PageNumber { get; set; }
        //public string VehicleModal { get; set; }
        public List<DropDownModel> GetVehicleMakeList()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<DropDownModel> model = new List<DropDownModel>();
            var vehicleMakeList = db.tbl_VehicleMake.ToList();
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
            var vehicleTypeList = db.tbl_VehicleType.Where(m => m.VehicleMake == VehcileMake).ToList();
            foreach (var vt in vehicleTypeList)
            {
                model.Add(new DropDownModel() { Value = vt.ID.ToString(), Text = vt.VehicleType });
            }
            return model;
        }
        public List<VehicleModel> GetVTypeGrid(string VehicleType)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<VehicleModel> model = new List<VehicleModel>();           
            DataTable dtTable = new DataTable();
          
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetVehicleTypes";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DbParameter paramRoleID = cmd.CreateParameter();
                    paramRoleID.ParameterName = "VehicleType";
                    paramRoleID.Value = VehicleType;
                    cmd.Parameters.Add(paramRoleID);
                 
                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                catch
                {
                    db.Database.Connection.Close();
                }
                foreach (DataRow dtRow in dtTable.Rows)
                {
                    VehicleModel info = new VehicleModel();
                    info.VTID=Convert.ToInt32(dtRow["VTID"].ToString());
                    info.VehicleMake =dtRow["VehicleMake"].ToString();
                    info.VehicleType = dtRow["VehicleType"].ToString();
                  
                    model.Add(info);
                }
            }
            db.Dispose();
            return model;
           
        }
        //public List<DropDownModel> GetVehicleModelList(int VehcileMake, int VehicleType)
        //{
        //    MalaGroupERPEntities db = new MalaGroupERPEntities();
        //    List<DropDownModel> model = new List<DropDownModel>();
        //    var vehicleModelList = db.tbl_VehicleModel.Where(m => m.VehicleMake == VehcileMake && m.VehicleType == VehicleType).ToList();
        //    foreach (var vm in vehicleModelList)
        //    {
        //        model.Add(new DropDownModel() { Value = vm.ID.ToString(), Text = vm.VehicleModel });
        //    }
        //    return model;
        //}
        //public List<VehicleModel> GetVModelGrid(string VehicleModal)
        //{
        //    MalaGroupERPEntities db = new MalaGroupERPEntities();
        //    List<VehicleModel> model = new List<VehicleModel>();
        //    DataTable dtTable = new DataTable();

        //    using (var cmd = db.Database.Connection.CreateCommand())
        //    {
        //        try
        //        {
        //            db.Database.Connection.Open();
        //            cmd.CommandText = "usp_GetVehicleModels";
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            DbParameter paramRoleID = cmd.CreateParameter();
        //            paramRoleID.ParameterName = "VehicleModel";
        //            paramRoleID.Value = VehicleModal;
        //            cmd.Parameters.Add(paramRoleID);

        //            DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
        //            da.SelectCommand = cmd;
        //            da.Fill(dtTable);
        //            db.Database.Connection.Close();
        //        }
        //        catch
        //        {
        //            db.Database.Connection.Close();
        //        }
        //        foreach (DataRow dtRow in dtTable.Rows)
        //        {
        //            VehicleModel info = new VehicleModel();
        //            info.VMID = Convert.ToInt32(dtRow["VMID"].ToString());
        //            info.VehicleMake = dtRow["VehicleMake"].ToString();
        //            info.VehicleType = dtRow["VehicleType"].ToString();
        //            info.VehicleModal = dtRow["VehicleModel"].ToString();
        //            model.Add(info);
        //        }
        //    }
        //    db.Dispose();
        //    return model;

        //}
        public string SaveUpdateVMake(VehicleModel model)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string vMakeID = "0";
            if (model.VID == 0)
            {

                var vMakeSave = new tbl_VehicleMake()
                {
                    ID = Convert.ToInt32(model.VID),
                    VehicleMake = model.VehicleMake,
                    IsDeleted=0,
                  
                };
                db.tbl_VehicleMake.Add(vMakeSave);
                db.SaveChanges();
                model.VID = vMakeSave.ID;
                vMakeID = vMakeSave.ID.ToString();
                db.Dispose();
                msg = "Vehicle Make Added Successfully";
            }

            else
            {
                var vMakeUpdate = db.tbl_VehicleMake.Where(p => p.ID == model.VID).FirstOrDefault();
                vMakeUpdate.VehicleMake = model.VehicleMake;
                db.SaveChanges();
                vMakeID = model.VID.ToString();
                msg = "Vehicle Make Updated Successfully";
            }

            db.Dispose();
            return msg;
        }
        public string DeleteVMake(VehicleModel model)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
           
            if (model.VID != 0)
       
            {
                var vMakeUpdate = db.tbl_VehicleMake.Where(p => p.ID == model.VID).FirstOrDefault();
                db.tbl_VehicleMake.Remove(vMakeUpdate);
                db.SaveChanges();
                //if (vMakeUpdate != null)
                //{
                //    vMakeUpdate.IsDeleted = 1;
                //    db.SaveChanges();
                //}
                msg = "Vehicle Make Deleted Successfully";
            }

            db.Dispose();
            return msg;
        }
        public string SaveUpdateVType(VehicleModel model)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string vTypeID = "0";
            if (model.VTID == 0)
            {

                var vTypeSave = new tbl_VehicleType()
                {
                    VehicleMake = Convert.ToInt32(model.VehicleMake),
                    VehicleType = model.VehicleType,
                  
                };
                db.tbl_VehicleType.Add(vTypeSave);
                db.SaveChanges();
                model.VTID = vTypeSave.ID;
                vTypeID = vTypeSave.ID.ToString();
                db.Dispose();
                msg = "Vehicle Type Added Successfully";
            }

            else
            {
                var vTypeUpdate = db.tbl_VehicleType.Where(p => p.ID == model.VTID).FirstOrDefault();
                vTypeUpdate.VehicleType = model.VehicleType;
                db.SaveChanges();
                vTypeID = model.VTID.ToString();
                msg = "Vehicle Type Updated Successfully";
            }

            db.Dispose();
            return msg;
        }
        //public string SaveUpdateVModel(VehicleModel model)
        //{
        //    string msg = "";
        //    MalaGroupERPEntities db = new MalaGroupERPEntities();
        //    string vModelID = "0";
        //    if (model.VMID == 0)
        //    {

        //        var vModelSave = new tbl_VehicleModel()
        //        {
        //            VehicleMake = Convert.ToInt32(model.VehicleMake),
        //            VehicleType = Convert.ToInt32(model.VehicleType),
        //            VehicleModel = model.VehicleModal,

        //        };
        //        db.tbl_VehicleModel.Add(vModelSave);
        //        db.SaveChanges();
        //        model.VMID = vModelSave.ID;
        //        vModelID = vModelSave.ID.ToString();
        //        db.Dispose();
        //        msg = "Vehicle Model Added Successfully";
        //    }

        //    else
        //    {
        //        var vModelUpdate = db.tbl_VehicleModel.Where(p => p.ID==model.VMID).FirstOrDefault();
        //        vModelUpdate.VehicleModel = model.VehicleModal;
        //        db.SaveChanges();
        //        vModelID = model.VMID.ToString();
        //        msg = "Vehicle Model Updated Successfully";
        //    }

        //    db.Dispose();
        //    return msg;
        //}

        public VehicleModel GetVehicleMakeInfo(int VID)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            VehicleModel model = new VehicleModel();
            var vehicleMakeList = db.tbl_VehicleMake.Where(p => p.ID == VID).FirstOrDefault();
            model.VID = vehicleMakeList.ID;
            model.VehicleMake = vehicleMakeList.VehicleMake;
            return model;
        }
        public VehicleModel GetVehicleTypeInfo(int VTID)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            VehicleModel model = new VehicleModel();
            var vehicleMakeList = db.tbl_VehicleType.Where(p => p.ID == VTID).FirstOrDefault();
            model.VTID = vehicleMakeList.ID;
            model.VehicleType = vehicleMakeList.VehicleType;
            model.VehicleMake = vehicleMakeList.VehicleMake.ToString();
            return model;
        }
        //public VehicleModel GetVehicleModelInfo(int VMID)
        //{
        //    MalaGroupERPEntities db = new MalaGroupERPEntities();
        //    VehicleModel model = new VehicleModel();
        //    var vehicleMakeList = db.tbl_VehicleModel.Where(p => p.ID == VMID).FirstOrDefault();
        //    model.VTID = vehicleMakeList.ID;
        //    model.VehicleMake = vehicleMakeList.VehicleMake.ToString();
        //    model.VehicleType = vehicleMakeList.VehicleType.ToString() ;
        //    model.VehicleModal = vehicleMakeList.VehicleModel;
        //    return model;
        //}

        public List<VehicleModel> GetMakeInfoPageList(VehicleModel model)
        {
            try
            {
                List<VehicleModel> listSearch = new List<VehicleModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetMakeInfoPageList";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramName = cmd.CreateParameter();
                        paramName.ParameterName = "MakeName";
                        paramName.Value = model.VehicleMake;
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

                            listSearch.Add(new VehicleModel()
                            {
                                VID = Convert.ToInt32(dr["MakeID"].ToString()),
                                VehicleMake = dr["MakeName"].ToString(),
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
        public string GetMakeFilterRangeList(VehicleModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            DataTable dtTable = new DataTable();
            string PageNumber = "0";
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetMakeInfoPageListRange";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "MakeName";
                    paramName.Value = model.VehicleMake;
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


        public List<VehicleModel> GetTypeInfoPageList(VehicleModel model)
        {
            try
            {
                List<VehicleModel> listSearch = new List<VehicleModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetTypeInfoPageList";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramName = cmd.CreateParameter();
                        paramName.ParameterName = "TypeName";
                        paramName.Value = model.VehicleType;
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

                            listSearch.Add(new VehicleModel()
                            {
                                VTID = Convert.ToInt32(dr["TypeID"].ToString()),
                                VehicleMake = dr["MakeName"].ToString(),
                                VehicleType = dr["TypeName"].ToString()
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
        public string GetTypeFilterRangeList(VehicleModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            DataTable dtTable = new DataTable();
            string PageNumber = "0";
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTypeInfoPageListRange";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "TypeName";
                    paramName.Value = model.VehicleType;
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
    }

}