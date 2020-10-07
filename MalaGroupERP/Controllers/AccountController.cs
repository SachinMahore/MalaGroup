using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MalaGroupERP.Models;
using MalaGroupERP.Data;
using System.Web.Security;
using System.Data;
using System.Data.Common;

namespace MalaGroupERP.Controllers
{
    
    [Authorize]
    public class AccountController : Controller
    {
        private MalaGroupERPEntities db = new MalaGroupERPEntities();
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                if (returnUrl.ToString().ToLower().Contains("logoff"))
                    returnUrl = null;
            }
            catch
            {
                returnUrl = null;
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = db.tblLogins.Where(p => p.Username == model.UserName && p.Password == model.Password && p.IsActive==1).FirstOrDefault();
                if (user != null)
                {
                    SignIn(model.UserName, model.RememberMe);
                    // Set Current User
                    var currentUser = new CurrentUser();
                    currentUser.UserID = user.UserID;
                    currentUser.Username = user.Username;
                    currentUser.FullName = user.FirstName + " " + user.LastName;
                    currentUser.EmailAddress = user.Email;
                    currentUser.IsAdmin = (user.IsSuperUser.HasValue ? user.IsSuperUser.Value : 0);
                    currentUser.EmailAddress = user.Email;
                    currentUser.UserType = (user.UserType.HasValue ? user.UserType.Value : 0);
                    currentUser.LoggedInUser = user.FirstName;
                    currentUser.Extension = user.Extension;
                    currentUser.Timezone = user.Timezone;
                    currentUser.SMPTUserName = user.SMTPUserName;
                    currentUser.SMTPPassword = user.SMTPPassword;
                    (new MalaGroupWebSession()).SetWebSession(currentUser);
                    // Store the Log.
                    var loginHistory = new tblLoginHistory
                    {
                        UserID = user.UserID,
                        IPAddress = Request.UserHostAddress,
                        PageName = "Home",
                        LoginDateTime = DateTime.Now,
                        SessionID = Session.SessionID.ToString()
                    };

                    db.tblLoginHistories.Add(loginHistory);
                    db.SaveChanges();

                    //using (var cmd = db.Database.Connection.CreateCommand())
                    //{
                    //    try
                    //    {
                    //        db.Database.Connection.Open();
                    //        cmd.CommandText = "usp_GetNotificationCounts";
                    //        cmd.CommandType = CommandType.StoredProcedure;

                    //        DbParameter paramUserID = cmd.CreateParameter();
                    //        paramUserID.ParameterName = "UserID";
                    //        paramUserID.Value = user.UserID;
                    //        cmd.Parameters.Add(paramUserID);
                    //        cmd.ExecuteNonQuery();
                    //    }
                    //    catch
                    //    {
                    //        db.Database.Connection.Close();
                    //    }
                    //}

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    user = db.tblLogins.Where(p => p.Username == model.UserName).FirstOrDefault();
                    if (user == null)
                    {
                        ModelState.AddModelError("", "Invalid username.");
                    }
                    else if (user == null)
                    {
                        ModelState.AddModelError("", "Invalid password.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User is not active.");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/ForgotPasswordConfirmation
     
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/ResetPasswordConfirmation
     
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin



        //
        // POST: /Account/LogOff


        public ActionResult LogOff()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            (new MalaGroupWebSession()).RemoveWebSession();

            new CommonModel().AddPageLoginHistory("");

            var loginHistory = db.tblLoginHistories.Where(p => p.UserID == MalaGroupWebSession.CurrentUser.UserID && p.SessionID == Session.SessionID.ToString() && p.LogoutDateTime == null).FirstOrDefault();

            if (loginHistory != null)
            {
                loginHistory.LogoutDateTime = DateTime.Now;
                db.SaveChanges();
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult IsSession()
        {
            if (Session["CurrentUser"] != null)
            {
                return Json(new { IsLogOut = "0" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsLogOut = "1" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult KeepLive()
        {
            return Json(new { result = "OK" }, JsonRequestBehavior.AllowGet);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
           
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public void SignIn(string userName, bool rememberMe)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(userName, rememberMe);
        }

    
    }
}