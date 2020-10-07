using MalaGroupERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalaGroupERP.Controllers
{
     [MalaGroupWebAuthorizationController]
    public class AgentOrderController : Controller
    {
        //
        // GET: /AgentOrder/
        public ActionResult Index()
        {
            return RedirectToAction("AddEdit", "AgentOrder");
        }
        public ActionResult AddEdit()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights modelRole = malaGroupWebSession.UserAccess("AgentOrder_AddEdit");
            if (modelRole.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            string pin = "";
            var model = new AgentOrderModel().GetLeadInfo(pin);
            return View("..\\AgentOrder\\AddEdit", model);
        }
        public ActionResult Edit(string PinNo)
        {
            try
            {
                AgentOrderModel model = new AgentOrderModel();
                return Json((new { leadData = model.GetLeadInfo(PinNo) }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json((new { error = ex.Message }), JsonRequestBehavior.AllowGet);
            }
        }
       
        public ActionResult UpdateAgentLead(AgentOrderModel model)
        {
            try
            {

                return Json(new { LeadID = (new AgentOrderModel()).UpdateAgentLead(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetVehicleLeadInfo(int VehicleMake, int VehicleType)
        {
            try
            {
                AgentOrderModel model = new AgentOrderModel();
                return Json((new { vehicleData = model.GetVehicleLeadInfo(VehicleMake, VehicleType) }), JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json((new { error = ex.Message }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetVehicleLeadDetInfo(long VehicleID)
        {
            try
            {
                AgentOrderModel model = new AgentOrderModel();
                return Json((new { vehicleData = model.GetVehicleLeadDetInfo(VehicleID) }), JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json((new { error = ex.Message }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveTransaction(TransactionModel model)
        {
            try
            {

                return Json(new { Msg = (new TransactionModel()).SaveTransaction(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PayNow(string PinNo)
        {
            try
            {

                return Json(new { accountID = (new AccountPageModel()).OrderToAccounts(PinNo) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                new CommonModel().LogAgentOrder("Date : " + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "\r\n==============================\r\nERROR : \r\n" + ex.StackTrace + "\r\n==============================\r\n");
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetVehicleInfo(long ID)
        {
            try
            {
                return Json((new AgentOrderModel()).GetVehicleInfo(ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetCallHistory(long LeadID, int PageID)
        {
            try
            {
                return Json((new CallHistoryModel()).GetCallHistory(LeadID,PageID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetQuestionsList(int Type)
        {
            try
            {
                return Json((new QuestionModel()).GetQuestionsList(Type), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetVehicleAOInfo(long AOID)
        {
            try
            {
                AgentOrderModel model = new AgentOrderModel();
                return Json((new { vehicleData = model.GetVehicleAOInfo(AOID) }), JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json((new { error = ex.Message }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AuthorizeAdmin(String UserId,String Password)
        {
            try
            {
                return Json((new AgentOrderModel()).AuthorizeAdmin(UserId, Password), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckLeadOrder(string PinNo)
        {
            try
            {
                AgentOrderModel model = new AgentOrderModel();
                return Json((new { leadData = model.CheckLeadOrder(PinNo) }), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json((new { error = ex.Message }), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveLeadToAccount(LeadToAccount model)
        {
            try
            {

                return Json(new { LeadID = (new AgentOrderModel()).SaveLeadToAccount(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SendCompEmail(String EmailID, String PinNo)
        {
            try
            {
                return Json((new AgentOrderModel()).SendCompEmail(EmailID, PinNo), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
       
	}
}