using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers.Admin
{
     [MalaGroupWebAuthorizationController]
    public class FixAuthorizeTransactionController : Controller
    {
        //
        // GET: /FixAuthorizeTransaction/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FixTransaction(DateTime ChargeDate, int SendApproved, int SendDecline)
        {
            try
            {
                DateTime dtChargeDate = Convert.ToDateTime(ChargeDate.ToString("MM/dd/yyyy"));
                new CardScheduleModel().CheckAuthorizeSattelment(dtChargeDate, SendApproved, SendDecline);
                return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}