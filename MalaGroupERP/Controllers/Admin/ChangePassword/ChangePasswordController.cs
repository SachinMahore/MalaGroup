using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalaGroupERP.Controllers
{
    public class ChangePasswordController : Controller
    {
        // GET: UserManagement
        //public ActionResult Index()
        //{
        //    return View("..\\UserManagement\\Index");
        //}

        public ActionResult Edit()
        {
            return View("..\\Admin\\ChangePassword\\AddEdit");
        }
    }
}