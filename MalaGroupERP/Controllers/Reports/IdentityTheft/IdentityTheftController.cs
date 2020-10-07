using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;
using System.Web.Routing;
using System.Data;

namespace MalaGroupERP.Controllers
{
    [MalaGroupWebAuthorizationController]
    public class IdentityTheftController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                return View("..\\Reports\\IdentityTheft\\IdentityTheftReport");
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetIdentityTheftReport(IdentityTheftModel model)
        {
            try
            {
                return Json(new { model = new IdentityTheftModel().GetIdentityTheftReport(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}