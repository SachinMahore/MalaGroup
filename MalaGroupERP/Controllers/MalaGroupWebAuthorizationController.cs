using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Data;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers
{
   
    public class MalaGroupWebAuthorizationController : AuthorizeAttribute
    {
        public string LoginPage { get; set; }
        public string AccessDeniedPage { get; set; }
        public Enums.UserRole UserRole { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    //Ajax request doesn't return to login page, it just returns 403 error.
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result = new HttpUnauthorizedResult();
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("~/Account/Login");
                    //filterContext.HttpContext.Response.Redirect("~/Home");
                }
            }
            else
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                var user = db.tblLogins.Where(p => p.Username == filterContext.HttpContext.User.Identity.Name).FirstOrDefault();

                MalaGroupWebSession _WebSession = new MalaGroupWebSession();
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
                db.Dispose();
                _WebSession.SetWebSession(currentUser);
            }

            base.OnAuthorization(filterContext);
        }
    }
}