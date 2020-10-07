using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MalaGroupERP.Data;

namespace MalaGroupERP.Models
{
    public class RoleManagementModel
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public string RoleCode { get; set; }

        public string RoleDescription { get; set; }
        public bool RoleStatus { get; set; }
        public string RoleStatusText { get; set; }

        public int HasUsers { get; set; }

        public List<UserInRole> UsersInRole { get; set; }
        public List<ModelRole> ModelRoles { get; set; }

        public List<RoleManagementModel> GetList()
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                List<RoleManagementModel> model = new List<RoleManagementModel>();
                var Roles = db.syRoles.ToList();
                foreach (var role in Roles)
                {
                    model.Add(new RoleManagementModel()
                    {
                        RoleId = role.RoleId,
                        RoleCode = role.RoleCode,
                        RoleDescription = role.RoleDescription,
                        RoleStatusText = (role.RoleStatus == true ? "Active" : "Inactive"),
                        HasUsers = (db.syUsersRoles.Where(p => p.RoleId == role.RoleId).Count())
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

        public RoleManagementModel GetRoleInfo(int RoleID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                RoleManagementModel model = new RoleManagementModel();
                var role = db.syRoles.Where(p => p.RoleId == RoleID).FirstOrDefault();
                if (role == null)
                {
                    model.RoleId = 0;
                    model.RoleCode = "";
                    model.RoleDescription = "";
                    model.RoleStatus = true;
                }
                else
                {
                    model.RoleId = role.RoleId;
                    model.RoleCode = role.RoleCode;
                    model.RoleDescription = role.RoleDescription;
                    model.RoleStatus = role.RoleStatus;
                }
                db.Dispose();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UserInRole> GetUsersRole(int RoleId)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                var UsersInRole = (from l in db.tblLogins
                                   join ur in db.syUsersRoles on l.UserID equals ur.UserId
                                   where ur.RoleId == RoleId
                                   select new UserInRole { ID = l.UserID, UserName = l.Username + " (" + l.FirstName + " " + l.LastName + ")" }).ToList();
                db.Dispose();
                return UsersInRole.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ModelRole> GetModelsRole(int RoleId)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                List<ModelRole> modelRoles = new List<ModelRole>();

                var ModelRoles = (from rs in db.syResources
                                  join rr in db.syRoleResources.Where(p => p.RoleId == RoleId) on rs.ResourceId equals rr.ResourceId into rslj
                                  from rj in rslj.DefaultIfEmpty()
                                  where rs.ResourceTypeId == 1
                                  select new ModelRole { ResourceId = rs.ResourceId, Resource = rs.Resource, IsAssigned = (rj.RoleId == RoleId ? 1 : 0) }).ToList();


                for (int i = 0; i < ModelRoles.ToList().Count; i++)
                {
                    modelRoles.Add(new ModelRole() { ID = i, ResourceId = ModelRoles.ToList()[i].ResourceId, Resource = ModelRoles.ToList()[i].Resource, IsAssigned = ModelRoles.ToList()[i].IsAssigned });
                }
                db.Dispose();
                return modelRoles.ToList();
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
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                var RoleExist = db.syRoles.Where(p => p.RoleId == this.RoleId).FirstOrDefault();
                if (RoleExist == null)
                {
                    var RoleNameExist = db.syRoles.Where(p => p.RoleCode == this.RoleCode).FirstOrDefault();
                    if (RoleNameExist == null)
                    {
                        var RoleAdd = new syRole() { RoleId = this.RoleId, RoleCode = this.RoleCode, RoleDescription = this.RoleDescription, RoleStatus = this.RoleStatus };
                        db.syRoles.Add(RoleAdd);
                        db.SaveChanges();
                        this.RoleId = RoleAdd.RoleId;

                        if (this.ModelRoles != null)
                        {
                            foreach (ModelRole mr in this.ModelRoles)
                            {
                                var RoleModel = db.syRoleResources.Where(p => p.RoleId == this.RoleId && p.ResourceId == mr.ResourceId).FirstOrDefault();
                                if (RoleModel == null && mr.IsAssigned == 1)
                                {
                                    var ModelRoleAdd = new syRoleResource() { RoleResourceId = 0, RoleId = this.RoleId, ResourceId = mr.ResourceId };
                                    db.syRoleResources.Add(ModelRoleAdd);
                                }
                            }

                        }
                        db.SaveChanges();
                        db.Dispose();
                    }
                    else
                    {
                        db.Dispose();
                        throw new Exception("Role Code : " + this.RoleCode + " is already exists in system.");
                    }
                }
                else
                {
                    db.Dispose();
                    throw new Exception(this.RoleId + " is already exists in system.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete()
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                var Role = db.syRoles.Where(p => p.RoleId == this.RoleId).FirstOrDefault();
                var RoleResources = db.syRoleResources.Where(p => p.RoleId == this.RoleId);
                var RoleResourceLevel = db.syRolesResourcesLevels.Where(p => p.RoleId == this.RoleId);
                var UsersRoles = db.syUsersRoles.Where(p => p.RoleId == this.RoleId);

                if (Role == null)
                {
                    db.Dispose();
                    throw new Exception("Role Not Found.");
                }
                else
                {
                    if (RoleResourceLevel != null)
                    {
                        db.syRolesResourcesLevels.RemoveRange(RoleResourceLevel);
                    }
                    if (RoleResources != null)
                    {
                        db.syRoleResources.RemoveRange(RoleResources);
                    }
                    if (UsersRoles != null)
                    {
                        db.syUsersRoles.RemoveRange(UsersRoles);
                    }
                    db.syRoles.Remove(Role);
                    db.SaveChanges();
                    db.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AssingAndDeleteRole(int RoleID, int AssignRoleID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                var Role = db.syRoles.Where(p => p.RoleId == RoleID).FirstOrDefault();
                var RoleResources = db.syRoleResources.Where(p => p.RoleId == RoleID);
                var RoleResourceLevel = db.syRolesResourcesLevels.Where(p => p.RoleId == RoleID);
                var RoleUsers = db.syUsersRoles.Where(p => p.RoleId == RoleID);
                var RoleUsersList = RoleUsers.ToList();
                if (Role == null)
                {
                    db.Dispose();
                    throw new Exception("Role Not Found.");
                }
                else
                {
                    db.syUsersRoles.RemoveRange(RoleUsers);
                    db.syRolesResourcesLevels.RemoveRange(RoleResourceLevel);
                    db.syRoleResources.RemoveRange(RoleResources);
                    db.syRoles.Remove(Role);
                    db.SaveChanges();
                    foreach (syUsersRole user in RoleUsersList)
                    {
                        var UserExist = db.syUsersRoles.Where(p => p.UserId == user.UserId).FirstOrDefault();
                        if (UserExist == null)
                        {
                            var UserAdd = new syUsersRole() { UserRoleId = 0, RoleId = AssignRoleID, UserId = user.UserId };
                            db.syUsersRoles.Add(UserAdd);
                        }
                    }
                    db.SaveChanges();
                    db.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update()
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                var Role = db.syRoles.Where(p => p.RoleId == this.RoleId).FirstOrDefault();

                if (Role == null)
                {
                    db.Dispose();
                    throw new Exception("Role Not Found");
                }
                else
                {
                    var RoleCodeExist = db.syRoles.Where(p => p.RoleId != this.RoleId && p.RoleCode == this.RoleCode).FirstOrDefault();
                    if (RoleCodeExist == null)
                    {
                        Role.RoleCode = this.RoleCode;
                        Role.RoleDescription = this.RoleDescription;
                        Role.RoleStatus = this.RoleStatus;
                        db.SaveChanges();
                        var RoleModels = db.syRoleResources.Where(p => p.RoleId == this.RoleId);
                        db.syRoleResources.RemoveRange(RoleModels);
                        db.SaveChanges();
                        if (this.ModelRoles != null)
                        {
                            foreach (ModelRole mr in this.ModelRoles)
                            {
                                var RoleModel = db.syRoleResources.Where(p => p.RoleId == this.RoleId && p.ResourceId == mr.ResourceId).FirstOrDefault();
                                if (RoleModel == null && mr.IsAssigned == 1)
                                {
                                    var ModelRoleAdd = new syRoleResource() { RoleResourceId = 0, RoleId = this.RoleId, ResourceId = mr.ResourceId };
                                    db.syRoleResources.Add(ModelRoleAdd);
                                }
                            }
                        }
                        db.SaveChanges();
                        db.Dispose();
                    }
                    else
                    {
                        db.Dispose();
                        throw new Exception("Role Code : " + this.RoleCode + " is already exists in system.");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RoleToAssign> GetRoleToAssign(int RoleId)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                db.Configuration.LazyLoadingEnabled = false;
                List<RoleToAssign> RolesToAssing = new List<RoleToAssign>();
                RolesToAssing.Add(new RoleToAssign() { IDValue = 0, TextValue = "Select Role" });
                var data = db.syRoles.Where(p => p.RoleId != RoleId).OrderBy(p => p.RoleCode).ToList();
                foreach (syRole role in data)
                {
                    RolesToAssing.Add(new RoleToAssign() { IDValue = role.RoleId, TextValue = role.RoleCode });
                }
                db.Dispose();
                return RolesToAssing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public class UserInRole
        {
            public int ID { get; set; }
            public string UserName { get; set; }
        }
        public class ModelRole
        {
            public int ID { get; set; }
            public int ResourceId { get; set; }
            public string Resource { get; set; }
            public int IsAssigned { get; set; }

        }
        public class RoleToAssign
        {
            public int IDValue { get; set; }
            public string TextValue { get; set; }
        }
    }
}