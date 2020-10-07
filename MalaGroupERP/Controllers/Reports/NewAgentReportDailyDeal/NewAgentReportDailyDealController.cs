using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;
using System.Web.Routing;
using System.Data;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers
{
    [MalaGroupWebAuthorizationController]
    public class NewAgentReportDailyDealController : Controller
    {
        public ActionResult Index()
        {
            try
            {

                return View("..\\Reports\\NewAgentReportDailyDeal\\NewAgentReportDailyDeal");

            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ExportReport(NewAgentReportDailyDealModel model)
        {
            try
            {
                return Json(new { model = new NewAgentReportDailyDealModel().ExportReport(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetProductList()
        {
            try
            {
                return Json(new { model = ((new PayrollModel()).GetProductList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


       

	}
}