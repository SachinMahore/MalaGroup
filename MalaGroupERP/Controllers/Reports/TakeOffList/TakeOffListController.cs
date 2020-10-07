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
    public class TakeOffListController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                return View("..\\Reports\\TakeOffList\\TakeOffListReport");
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTakeOffListReport(TakeOffListModel model)
        {
            try
            {
                return Json(new { MSG = new TakeOffListModel().GetTakeOffListReport(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}