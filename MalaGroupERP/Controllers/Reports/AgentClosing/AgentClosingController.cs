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
    public class AgentClosingController : Controller
    {
        public ActionResult Index()
        {
            try
            {

                return View("..\\Reports\\AgentClosing\\AgentClosing");

            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ExportReport(AgentClosingModel model)
        {
            try
            {
                return Json(new { model = new AgentClosingModel().ExportReport(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetLastModified()
        {
            try
            {
                return Json(new { model = ((new AgentClosingModel()).GetLastModified()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult GetState()
        //{
        //    try
        //    {
        //        return Json(new { model = ((new AgentClosingModel()).GetState()) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

       

	}
}