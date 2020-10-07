using MalaGroupERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalaGroupERP.Controllers
{
    public class RenewalOrderController : Controller
    {
        //
        // GET: /RenewalOrder/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEdit()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights modelRole = malaGroupWebSession.UserAccess("RenewalOrder_AddEdit");
            if (modelRole.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            string pin = "";
            var model = new AgentOrderModel().GetRenewalLeadInfo(pin);
            return View("..\\RenewalOrder\\AddEdit", model);
        }
        public ActionResult Edit(string PinNo)
        {
            try
            {
                AgentOrderModel model = new AgentOrderModel();
                return Json((new { leadData = model.GetRenewalLeadInfo(PinNo) }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json((new { error = ex.Message }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveRenewalTrans(TransactionModel model)
        {
            try
            {

                return Json(new { Msg = (new AccountPageModel()).SaveRenewalTrans(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}