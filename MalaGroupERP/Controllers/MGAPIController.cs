using MalaGroupERP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO.Compression;
using System.Globalization;
using System.Data.Common;
using System.Collections.Specialized;
using System.Text;


namespace MalaGroupERP.Controllers
{
    public class AllowCrossJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            var headers = Enumerable.ToList(HttpContext.Current.Request.Headers.AllKeys);
            headers.Add("X-HTTP-Method-Override");
            filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Headers", string.Join(", ", headers));
            filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }
    }
    [AllowCrossJson]
    public class MGAPIController : Controller
    {
        public ActionResult Index()
        {
            return null;
        }
        [AllowCrossJson]
        public ActionResult ChargeScheduleCard()
        {
            try
            {
                new CardScheduleModel().ChargeScheduleCards();
                return Json(new { result = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n" + ex.StackTrace + "\r\n==============================\r\n");
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ChargeScheduleCardsWithDate(string ScheduleDate)
        {
            try
            {
                ScheduleDate = ScheduleDate.Replace("-", "/");

                new CardScheduleModel().ChargeScheduleCardsWithDate(ScheduleDate);
                return Json(new { result = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n" + ex.StackTrace + "\r\n==============================\r\n");
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SendEmailBeforeScheduleRun()
        {
            try
            {
                string ChargeDate = DateTime.Now.ToString("MM/dd/yyyy");
                int PaymentStatus = 2;
                string FileName = "";
                var model = new CardScheduleModel().GetToDaysSchedules(ChargeDate, PaymentStatus, ref FileName);
                if (model.Count > 0)
                {
                    new CommonModel().SendEmailPaymentSchedule("List Of Payment Schedule Charge Today " + DateTime.Now.ToString("MMM, dd yyyy"), "List Of Payment Schedule Charge Today " + DateTime.Now.ToString("MMM, dd yyyy") + "<br/>Please see the attachment.", FileName);
                }
                return Json(new { result = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n" + ex.StackTrace + "\r\n==============================\r\n");
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SendEmailAfterScheduleRun()
        {
            try
            {
                string ChargeDate = DateTime.Now.ToString("MM/dd/yyyy");
                int PaymentStatus = 2;
                string FileName = "";
                var model = new CardScheduleModel().GetToDaysSchedules(ChargeDate, PaymentStatus, ref FileName);
                if (model.Count > 0)
                {
                    new CommonModel().SendEmailPaymentSchedule("List Of Payment Schedule Not Send Today " + DateTime.Now.ToString("MMM, dd yyyy"), "List Of Payment Schedule Not Send Today " + DateTime.Now.ToString("MMM, dd yyyy") + "<br/>Please see the attachment.", FileName);
                }
                return Json(new { result = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n" + ex.StackTrace + "\r\n==============================\r\n");
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [AllowCrossJson]
        public ActionResult CheckAuthorizeSattelment()
        {
            try
            {
                DateTime chargeDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy") + " 18:55:00");
                //DateTime chargeDate = Convert.ToDateTime("09/10/2019 18:55:00");
                new CardScheduleModel().CheckAuthorizeSattelment(chargeDate, 1, 1);
                return Json(new { result = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                new CommonModel().Log("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n" + ex.StackTrace + "\r\n==============================\r\n");
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}