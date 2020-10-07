using MalaGroupERP.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace MalaGroupERP.Models
{
    public class UserManagementModel
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Company { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }
        public string UserLicense { get; set; }
        public string Password { get; set; }
        public string Address1 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StateID { get; set; }
        public string ZipCode { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> UserType { get; set; }
        public string Extension { get; set; }

        public int ClientOrVendorID { get; set; }
        public int IsClientOrVendor { get; set; }

        public string ClientOrVendorName { get; set; }
        public int RowDisplay { get; set; }
        public int PageNumber { get; set; }

    
        public long EmployeeID { get; set; }
        public string EmployeeName { get; set; }

        public Nullable<int> IsSuperUser { get; set; }
        public string UserCode { get; set; }
        public string Timezone { get; set; }
        public string Language { get; set; }
        public string Locale { get; set; }
        public string FederationID { get; set; }
        public string StartDay { get; set; }
        public string EndDay { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<int> LastModifiedById { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public List<UserRoles> UserRole { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
        public List<UserManagementModel> GetOwnerList(string UserName)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<UserManagementModel> model = new List<UserManagementModel>();
            DataTable dtTable = new DataTable();

            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetOwnerList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DbParameter paramRoleID = cmd.CreateParameter();
                    paramRoleID.ParameterName = "UserName";
                    paramRoleID.Value = UserName;
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
                    UserManagementModel info = new UserManagementModel();
                    info.UserID =Convert.ToInt32(dtRow["UserID"].ToString());
                    info.UserName = dtRow["UserName"].ToString();
                    info.FirstName = dtRow["FirstName"].ToString();
                    info.LastName = dtRow["LastName"].ToString();
                    model.Add(info);
                }
            }
            db.Dispose();
            return model;

        }
        public string Create(UserManagementModel model)
        {
            string msg = "";
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                var UserExist = db.tblLogins.Where(p => p.UserID == model.UserID).FirstOrDefault();
                if (UserExist == null)
                {
                    var UserNameExist = db.tblLogins.Where(p => p.Username == model.UserName).FirstOrDefault();
                    if (UserNameExist != null)
                    {
                        msg = "";
                    }
                    else
                    {
                        if (model.UserID == 0)
                        {
                            var UserAdd = new tblLogin()
                           {
                               //UserID = model.UserID,
                               FirstName = model.FirstName,
                               LastName = model.LastName,
                               Email = model.Email,
                               Username = model.UserName,
                               Password = model.Password,
                               Company = model.Company,
                               Department = model.Department,
                               Address1 = model.Address1,
                               Country = model.Country,
                               City = model.City,
                               StateID = model.StateID,
                               ZipCode = model.ZipCode,
                               CellPhone = model.CellPhone,
                               Extension = model.Extension,
                               WorkPhone = model.WorkPhone,
                               IsActive = model.IsActive,
                               IsSuperUser = 0,
                               EmployeeID = model.EmployeeID,
                               AddToGroup = 0,
                               UserType = model.UserType,
                               FederationID = model.FederationID,

                               UserCode = model.UserCode,
                               Timezone = model.Timezone,
                               Language = model.Language,
                               Locale = model.Locale,
                               ShowNotification = 0,
                               StartDay = model.StartDay,
                               EndDay = model.EndDay,
                               CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                               CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                               LastModifiedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                               LastModifiedById = MalaGroupWebSession.CurrentUser.UserID,
                               IsDeleted = 0,
                               SMTPPassword=model.SMTPPassword,
                               SMTPUserName=model.SMTPUserName,
                           };
                            db.tblLogins.Add(UserAdd);
                            db.SaveChanges();
                            model.UserID = UserAdd.UserID;
                            msg = model.UserID.ToString();

                            foreach (UserRoles ur in model.UserRole)
                            {
                                var UserRole = db.syUsersRoles.Where(p => p.UserId == model.UserID && p.RoleId == ur.RoleID).FirstOrDefault();
                                if (UserRole == null && ur.IsAssign == 1)
                                {
                                    var UserRoleAdd = new syUsersRole() { UserRoleId = 0, UserId = model.UserID, RoleId = ur.RoleID };
                                    db.syUsersRoles.Add(UserRoleAdd);
                                }
                            }
                            db.SaveChanges();
                            db.Dispose();
                        }
                    }
                }
                else
                {
                    var UserRoles = db.syUsersRoles.Where(p => p.UserId == model.UserID);

                    UserExist.FirstName = model.FirstName;
                    UserExist.LastName = model.LastName;
                    UserExist.Email = model.Email;
                    UserExist.Username = model.UserName;
                    UserExist.Password = model.Password;
                    UserExist.Company = model.Company;

                    UserExist.Department = model.Department;
                    UserExist.Address1 = model.Address1;
                    UserExist.Country = model.Country;
                    UserExist.City = model.City;
                    UserExist.StateID = model.StateID;
                    UserExist.ZipCode = model.ZipCode;
                    UserExist.CellPhone = model.CellPhone;
                    UserExist.Extension = model.Extension;
                    UserExist.WorkPhone = model.WorkPhone;
                    UserExist.IsActive = model.IsActive;
                    //UserExist.IsSuperUser = 0;
                    UserExist.EmployeeID = model.EmployeeID;
                    UserExist.AddToGroup = 0;
                    UserExist.UserType = model.UserType;
                    UserExist.FederationID = model.FederationID;
                    UserExist.ShowNotification = 0;
                    UserExist.UserCode = model.UserCode;
                    UserExist.Timezone = model.Timezone;
                    UserExist.Language = model.Language;
                    UserExist.Locale = model.Locale;
                    UserExist.StartDay = model.StartDay;
                    UserExist.EndDay = model.EndDay;
                    UserExist.SMTPPassword = model.SMTPPassword;
                    UserExist.SMTPUserName = model.SMTPUserName;
                    UserExist.LastModifiedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                    UserExist.LastModifiedById = MalaGroupWebSession.CurrentUser.UserID;
                    db.SaveChanges();

                    db.syUsersRoles.RemoveRange(UserRoles);
                    db.SaveChanges();
                    foreach (UserRoles ur in model.UserRole)
                    {
                        var UserRole = db.syUsersRoles.Where(p => p.UserId == model.UserID && p.RoleId == ur.RoleID).FirstOrDefault();
                        if (UserRole == null && ur.IsAssign == 1)
                        {
                            var UserRoleAdd = new syUsersRole() { UserRoleId = 0, UserId = model.UserID, RoleId = ur.RoleID };
                            db.syUsersRoles.Add(UserRoleAdd);
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
            return msg;
        }
        public void Delete(int UserID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                var User = db.tblLogins.Where(p => p.UserID == UserID).FirstOrDefault();
                var UserRoles = db.syUsersRoles.Where(p => p.UserId == UserID);
                if (User == null)
                {
                    db.Dispose();
                    throw new Exception("User Not Found.");
                }
                else
                {
                    db.syUsersRoles.RemoveRange(UserRoles);
                    db.tblLogins.Remove(User);
                    db.SaveChanges();
                    db.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public UserManagementModel GetUserInfo(int UserID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                UserManagementModel model = new UserManagementModel();
                var user = db.tblLogins.Where(p => p.UserID == UserID).FirstOrDefault();
                if (user == null)
                {
                    model.UserID = 0;
                    
                    model.FirstName = "";
                    model.LastName = "";
                    model.Email = "";
                    model.UserName = "";
                    model.Password = "";
                    model.Address1 = "";
                    model.Country = "";
                    model.City = "";
                    model.StateID = "";
                    model.ZipCode = "";
                    model.WorkPhone = "";
                    model.CellPhone = "";
                    model.IsActive = 1;
                    model.Extension = "";
                    model.ClientOrVendorName = "";
                    model.EmployeeID = 0;
                    model.EmployeeName = "";
                    model.IsSuperUser = 0;
                    model.UserCode = "";
                    model.Timezone = "";
                    model.UserType = 0;
                    model.Language = "";
                    model.Locale ="";
                }
                else
                {
                    model.UserID = user.UserID;
                    //model.IsClientOrVendor = user.IsClientOrVendor;
                    //model.ClientOrVendorID = (user.ClientOrVendorID.HasValue ? user.ClientOrVendorID.Value : 0);
                    model.FirstName = user.FirstName;
                    model.LastName = user.LastName;
                    model.Email = user.Email;
                    model.UserName = user.Username;
                    model.Password = user.Password;
                    model.Address1 = user.Address1;
                    model.Country = user.Country;
                    model.City = user.City;
                    model.StateID = user.StateID;
                    model.ZipCode = user.ZipCode;
                    model.WorkPhone = user.WorkPhone;
                    model.CellPhone = user.CellPhone;
                    model.IsActive = user.IsActive;
                    model.Extension = user.Extension;
                   // model.ClientOrVendorName = GetClientOrVendorName((user.ClientOrVendorID.HasValue ? user.ClientOrVendorID.Value : 0), user.IsClientOrVendor);
                    model.EmployeeID = (user.EmployeeID.HasValue ? user.EmployeeID.Value : 0);
                  //  model.EmployeeName = GetEmployeerName((user.EmployeeID.HasValue ? user.EmployeeID.Value : 0));
                    model.IsSuperUser = (user.IsSuperUser.HasValue ? user.IsSuperUser.Value : 0);
                    model.UserCode = user.UserCode;
                    model.Timezone = user.Timezone;
                    model.Department = user.Department;
                    
                    model.Company = user.Company;
                    model.Country = user.Country;
                    model.FederationID = user.FederationID;
                    model.UserType = user.UserType;
                    model.Language = user.Language;
                    model.Locale = user.Locale;
                    model.StartDay = user.StartDay;
                    model.EndDay = user.EndDay;
                    model.CreatedById = user.CreatedById;
                    model.CreatedDate = user.CreatedDate;
                    model.LastModifiedById = user.LastModifiedById;
                    model.LastModifiedDate = user.LastModifiedDate;
                }
                db.Dispose();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public List<UserManagementModel> GetList()
        //{
        //    try
        //    {
        //        MalaGroupERPEntities db = new MalaGroupERPEntities();
        //        List<UserManagementModel> model = new List<UserManagementModel>();
        //        var Users = db.tblLogins.ToList();
        //        foreach (var user in Users)
        //        {
        //            model.Add(new UserManagementModel()
        //            {
        //                UserID = user.UserID,
        //                FirstName = user.FirstName,
        //                LastName = user.LastName,
        //                Email = user.Email,
        //                CellPhone=user.CellPhone,
        //                UserName = user.Username,
        //                Password = user.Password
        //            });
        //        }
        //        db.Dispose();
        //        return model.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public List<UserManagementModel> GetUserList(UserManagementModel model)
        {

            try
            {
                List<UserManagementModel> listSearch = new List<UserManagementModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetUserInfoPageList";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramName = cmd.CreateParameter();
                        paramName.ParameterName = "Name";
                        paramName.Value = model.Name;
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

                            listSearch.Add(new UserManagementModel()
                            {
                                UserID = Convert.ToInt32(dr["UserID"].ToString()),
                                Name = dr["Name"].ToString(),
                                Email = dr["Email"].ToString(),
                                CellPhone = dr["Phone"].ToString(),
                                UserName = dr["UserName"].ToString()
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

        public string GetUserFilterRangeList(UserManagementModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();

            DataTable dtTable = new DataTable();
            string PageNumber = "0";
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUserInfoPageListRange";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramName = cmd.CreateParameter();
                    paramName.ParameterName = "Name";
                    paramName.Value = model.Name;
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
        public List<UserRoles> GetRoleList(int UserID)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                List<UserRoles> userRoles = new List<UserRoles>();

                var UserRoles = (from r in db.syRoles
                                 join u in db.syUsersRoles.Where(p => p.UserId == UserID) on r.RoleId equals u.RoleId into rslj
                                 from rj in rslj.DefaultIfEmpty()
                                 select new UserRoles { RoleID = r.RoleId, RoleName = r.RoleCode, IsAssign = (rj.UserId == UserID ? 1 : 0) }).ToList();


                for (int i = 0; i < UserRoles.ToList().Count; i++)
                {
                    userRoles.Add(new UserRoles() { ID = i, RoleID = UserRoles.ToList()[i].RoleID, RoleName = UserRoles.ToList()[i].RoleName, IsAssign = UserRoles.ToList()[i].IsAssign });
                }
                db.Dispose();
                return userRoles.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public class UserRoles
        {
            public int ID { get; set; }
            public int RoleID { get; set; }
            public string RoleName { get; set; }
            public int IsAssign { get; set; }
        }
    }
}