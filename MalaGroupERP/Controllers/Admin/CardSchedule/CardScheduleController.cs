using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers
{
    [MalaGroupWebAuthorizationController]
    public class CardScheduleController : Controller
    {
        
        public ActionResult Index()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights model = malaGroupWebSession.UserAccess("CardSchedule");
            if (model.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("..\\Admin\\CardSchedule\\Index");
        }
        public ActionResult ImportCardSchedule()
        
        {

            try
            {
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                string msg =new CardScheduleModel(). ImportCardSchedule(fb);
                return Json(new { Msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCaredScheduleDetails(string ChargeDate)
        {

            try
            {
                return Json((new CardScheduleModel()).GetCaredScheduleDetails(ChargeDate), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ChargeCreditCards(string CardIDs)
        {

            try
            {
                string result= (new CardScheduleModel()).ChargeCreditCards(CardIDs);
                return Json(new { result = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DatewiseSchedule()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights model = malaGroupWebSession.UserAccess("CardSchedule");
            if (model.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("..\\Admin\\CardSchedule\\DatewiseSchedule");
        }
        public ActionResult GetCardScheduleDatewise(string ChargeDate, int PaymentStatus)
        {

            try
            {
                string fname = "";
                var model = new CardScheduleModel().GetCardScheduleDatewise(ChargeDate, PaymentStatus, ref fname);
                return Json(new { Model = model, Filename = fname }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}