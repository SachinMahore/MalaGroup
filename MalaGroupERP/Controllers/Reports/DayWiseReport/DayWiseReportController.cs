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
    public class DayWiseReportController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                return View("..\\Reports\\DayWiseReport\\DayWiseReport");
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetExportDayWiseData(DayWiseReportModel model)
        {
            try
            {
                return Json(new { model = new DayWiseReportModel().GetExportDayWiseData(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}