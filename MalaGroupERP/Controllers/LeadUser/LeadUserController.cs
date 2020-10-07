using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;
using System.Net.Mail;
using System.Net;



namespace MalaGroupERP.Controllers
{
     [MalaGroupWebAuthorizationController]
    public class LeadUserController : Controller
    {
        //
        // GET: /Leads/
        public ActionResult Index()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights modelRole = malaGroupWebSession.UserAccess("LeadUser");
            if (modelRole.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("..\\LeadUser\\Index");
        }
        public ActionResult AddEdit()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights modelRole = malaGroupWebSession.UserAccess("LeadUser");
            if (modelRole.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            long id = 0;
            var model = new LeadUserModel().GetLeadInfo(id);
            return View("..\\LeadUser\\AddEdit", model);
        }
        
        public ActionResult SaveUpdateLead(LeadUserModel model)
        {
            try
            {
                
                return Json(new { LeadID = (new LeadUserModel()).SaveUpdateLead(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteLeadData(long LeadID)
        {
            try
            {
                string msg = new LeadUserModel().DeleteLeadData(LeadID);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Edit(long id)
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights modelRole = malaGroupWebSession.UserAccess("LeadUser");
            if (modelRole.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new LeadUserModel().GetLeadInfo(id);
            return View("..\\LeadUser\\AddEdit", model);
        }

        public ActionResult GetVehicleMakeList()
        {
            try
            {
                return Json(new { model = ((new LeadUserModel()).GetVehicleMakeList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetVehicleTypeList(int VehcileMake)
        {
            try
            {
                return Json(new { model = ((new LeadUserModel()).GetVehicleTypeList( VehcileMake)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult GetVehicleModelList(int VehcileMake,int VehicleType)
        //{
        //    try
        //    {
        //        return Json(new { model = ((new LeadUserModel()).GetVehicleModelList(VehcileMake, VehicleType)) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult GetVehicleLeadInfo(long LeadID)
        {
            try
            {
                LeadUserModel model = new LeadUserModel();
                return Json((new { vehicleData = model.GetVehicleLeadInfo(LeadID) }), JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json((new { error = ex.Message }), JsonRequestBehavior.AllowGet);
            }
        }


        //Index Page Code
        public ActionResult GetLeadInfoPageList(LeadUserModel model)
        {

            try
            {
                return Json((new LeadUserModel()).GetLeadInfoPageList( model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetLeadsFilterRangeList(LeadUserModel model)
        {
            try
            {
                return Json(new { PageNumber = (new LeadUserModel()).GetLeadsFilterRangeList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
       

        public ActionResult ExportToLead(LeadUserModel formData)
        {

            try
            {
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                string msg = formData.ExportToLead(fb);
                return Json(new { Msg = msg, ID = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //TempData["msg"] = ex.Message.ToString();
                //TempData["success"] = "0";
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetEmailTemplates()
        {
            try
            {
                return Json((new LeadUserModel()).GetEmailTemplates(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetEmailData(long TemplateId, long LeadId)
        {
            try
            {
                string emailData = "";
                string emailSubject = "";
                string attachmentFile = "";
                (new LeadUserModel()).GetEmailData(TemplateId, LeadId, ref emailData, ref emailSubject, ref attachmentFile);
                return Json(new { html = emailData, subject = emailSubject, attachmentFile = attachmentFile }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SendEmail(string FromEmail, string ToEmail, string CCEmail, string BCCEmail, string EmailSubject, string EmailMessage, long LeadID, string AttachFile)
        {
            try
            {
                new CommonModel().SendEmail(FromEmail, ToEmail, CCEmail, BCCEmail, EmailSubject, EmailMessage, LeadID, AttachFile,1);
                
                return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveToFolder(LeadUserModel formData)
        {
            try
            {
                long LeadID = formData.LeadID;
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                string msg = formData.SaveToFolder(fb, LeadID);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAttachedLeadFiles(long LeadID)
        {
            try
            {
                return Json((new { returnREData = new LeadUserModel().GetAttachedLeadFiles(LeadID) }), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteFiles(long ID)
        {
            try
            {
                string msg = new LeadUserModel().DeleteFiles(ID);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetLeadAdvSearch(LeadUserModel model)
        {
            try
            {
                return Json(new { PageNumber = (new LeadUserModel()).GetLeadAdvSearch(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}


