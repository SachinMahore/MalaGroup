using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MalaGroupERP.Data;
using System.Data;
using System.Data.Common;

namespace MalaGroupERP.Models
{
    public class AssignRoleModel
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public int ModuleId { get; set; }
        public int SubModuleId { get; set; }
        public List<RoleModuleRight> RoleModelRights { get; set; }
        public List<ARMData> RoleModels { get; set; }
        public List<RoleModuleRight> RoleModuleRightsList(int RoleID, int ModuleID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                List<RoleModuleRight> model = new List<RoleModuleRight>();
                var roleModelRights = GetRightsByRoleIDAndModuleID(RoleID, ModuleID).ToList();

                for (int i = 0; i < roleModelRights.Count; i++)
                {
                    model.Add(new RoleModuleRight()
                    {
                        ID = i,
                        ResourceId = roleModelRights[i].ResourceID,
                        IsForSave = roleModelRights[i].IsForSave,
                        IsSpecialRight = roleModelRights[i].IsSpecialRight,
                        LoopThrough = roleModelRights[i].LoopThrough,
                        Resource = roleModelRights[i].Resource,
                        SpecialRight = roleModelRights[i].SpecialRight,
                        FullRight = roleModelRights[i].FullRight,
                        EditRight = roleModelRights[i].EditRight,
                        AddRight = roleModelRights[i].AddRight,
                        DeleteRight = roleModelRights[i].DeleteRight,
                        DisplayRight = roleModelRights[i].DisplayRight
                    });
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Role> GetRoles()
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                List<Role> model = new List<Role>();
                model.Add(new Role() { RoleID = 0, RoleName = "Select Role" });
                var Roles = db.syRoles.ToList();
                foreach (var role in Roles)
                {
                    model.Add(new Role()
                    {
                        RoleID = role.RoleId,
                        RoleName = role.RoleCode
                    });
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Module> GetModules(int RoleID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                db.Configuration.LazyLoadingEnabled = false;
                List<Module> model = new List<Module>();
                model.Add(new Module() { ModuleID = 0, ModuleName = "Select Module" });
                var Modules = (from rs in db.syResources
                               join rr in db.syRoleResources on rs.ResourceId equals rr.ResourceId
                               where rr.RoleId ==RoleID
                               select new Module { ModuleID = rs.ResourceId, ModuleName = rs.Resource }).ToList();

                foreach (Module m in Modules)
                {
                    model.Add(new Module() { ModuleID = m.ModuleID, ModuleName = m.ModuleName });
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Module> GetSubModules(int ModuleID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                db.Configuration.LazyLoadingEnabled = false;
                List<Module> model = new List<Module>();
                model.Add(new Module() { ModuleID = 0, ModuleName = "All SubModule" });
                var Modules = (from rs in db.syResources
                               join nn in db.syNavigationNodes on rs.ResourceId equals nn.ResourceId
                               where nn.ParentId == ModuleID
                               select new Module { ModuleID = rs.ResourceId, ModuleName = rs.Resource }).ToList();

                foreach (Module m in Modules)
                {
                    model.Add(new Module() { ModuleID = m.ModuleID, ModuleName = m.ModuleName });
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Create()
        {
            try
            {
                string ralXML = "<RALS>";
                foreach (ARMData data in this.RoleModels)
                {
                    ralXML += "<RAL><RID>" + data.RoleID + "</RID><MID>" + data.ModuleID + "</MID><RESID>" + data.ResourceID + "</RESID><AL>" + data.AccessLevel + "</AL><HSR>" + data.HasSpecial + "</HSR></RAL>";
                }
                ralXML += "</RALS>";
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_SaveRolesResourcesLevels";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramRALXML = cmd.CreateParameter();
                        paramRALXML.DbType = DbType.Xml;
                        paramRALXML.ParameterName = "RALXML";
                        paramRALXML.Value = ralXML;
                        cmd.Parameters.Add(paramRALXML);

                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GetRightsByRoleIDAndModuleID_Result> GetRightsByRoleIDAndModuleID(int RoleID, int ModuleID)
        {
            List<GetRightsByRoleIDAndModuleID_Result> list = new List<GetRightsByRoleIDAndModuleID_Result>();
            DataTable dtTable = new DataTable();
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetRightsByRoleIDAndModuleID";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DbParameter paramRoleID = cmd.CreateParameter();
                    paramRoleID.ParameterName = "RoleID";
                    paramRoleID.Value = RoleID;
                    cmd.Parameters.Add(paramRoleID);

                    DbParameter paramModuleID = cmd.CreateParameter();
                    paramModuleID.ParameterName = "ModuleID";
                    paramModuleID.Value = ModuleID;
                    cmd.Parameters.Add(paramModuleID);

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
                    GetRightsByRoleIDAndModuleID_Result info = new GetRightsByRoleIDAndModuleID_Result();
                    info.RID = Convert.ToInt32(dtRow["RID"].ToString());
                    info.ResourceID = Convert.ToInt32(dtRow["ResourceID"].ToString());
                    info.ParentID = Convert.ToInt32(dtRow["ParentID"].ToString());
                    info.IsForSave = Convert.ToInt32(dtRow["IsForSave"].ToString());
                    info.IsSpecialRight = Convert.ToInt32(dtRow["IsSpecialRight"].ToString());
                    info.Resource = dtRow["Resource"].ToString();
                    info.SpecialRight = Convert.ToInt32(dtRow["SpecialRight"].ToString());
                    info.FullRight = Convert.ToInt32(dtRow["FullRight"].ToString());
                    info.EditRight = Convert.ToInt32(dtRow["EditRight"].ToString());
                    info.AddRight = Convert.ToInt32(dtRow["AddRight"].ToString());
                    info.DeleteRight = Convert.ToInt32(dtRow["DeleteRight"].ToString());
                    info.DisplayRight = Convert.ToInt32(dtRow["DisplayRight"].ToString());
                    info.DisplayIndex = Convert.ToInt32(dtRow["DisplayIndex"].ToString());
                    info.LoopThrough = dtRow["LoopThrough"].ToString();
                    info.IsProcess = Convert.ToInt32(dtRow["IsProcess"].ToString());
                    list.Add(info);
                }
            }
            db.Dispose();
            return list;
        }
        [Serializable]
        public class GetRightsByRoleIDAndModuleID_Result
        {
            public int RID { get; set; }
            public int ResourceID { get; set; }
            public int ParentID { get; set; }
            public int IsForSave { get; set; }
            public int IsSpecialRight { get; set; }
            public string Resource { get; set; }
            public int SpecialRight { get; set; }
            public int FullRight { get; set; }
            public int EditRight { get; set; }
            public int AddRight { get; set; }
            public int DeleteRight { get; set; }
            public int DisplayRight { get; set; }
            public int DisplayIndex { get; set; }
            public string LoopThrough { get; set; }
            public int IsProcess { get; set; }
        }
        public class RoleModuleRight
        {
            public int ID { get; set; }
            public int ResourceId { get; set; }
            public int IsForSave { get; set; }
            public int IsSpecialRight { get; set; }
            public string LoopThrough { get; set; }
            public string Resource { get; set; }
            public int SpecialRight { get; set; }
            public int FullRight { get; set; }
            public int EditRight { get; set; }
            public int AddRight { get; set; }
            public int DeleteRight { get; set; }
            public int DisplayRight { get; set; }
        }
        public class ARMData
        {
            public int ID { get; set; }
            public int RoleID { get; set; }
            public int ModuleID { get; set; }
            public int SubModuleID { get; set; }
            public int ResourceID { get; set; }
            public int AccessLevel { get; set; }
            public int HasSpecial { get; set; }

        }
        public class Role
        {
            public int RoleID { get; set; }
            public string RoleName { get; set; }
        }
        public class Module
        {
            public int ModuleID { get; set; }
            public string ModuleName { get; set; }
        }
    }
}