using MalaGroupERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MalaGroupERP.Controllers
{
     [MalaGroupWebAuthorizationController]
    public class CustomReportController : Controller
    {
        //
        // GET: /AgentOrder/
        public ActionResult Index()
        {
            //return RedirectToAction("AddEdit", "CustomReport");
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights modelRole = malaGroupWebSession.UserAccess("CustomReport");
            if (modelRole.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("..\\CustomReports\\AddEdit");
        }
        public ActionResult ReportList()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights model = malaGroupWebSession.UserAccess("CustomReport");
            if (model.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("..\\CustomReports\\ReportList");
        }

        public ActionResult Step1(CustomReportModel model)
        {
            try
            {

                return Json(new { ID = (new CustomReportModel()).Step1(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Step2(string CIDs, long CusReportID)
        {
            try
            {

                return Json(new { CID = (new CustomReportModel()).Step2(CIDs, CusReportID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCustomReportList(long TableID)
        {
            try
            {
                return Json((new CustomReportModel()).GetCustomReportList(TableID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json((new { error = ex.Message }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LastStep(CustomReportModel model)
        {
            try
            {
                return Json((new CustomReportModel()).LastStep(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json((new { error = ex.Message }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCusReportFieldLabelList(long CusID,int Type)
        {
            try
            {
                return Json((new CustomReportModel()).GetCusReportFieldLabelList(CusID, Type), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json((new { error = ex.Message }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDmFilterField(long CusID)
        {
            try
            {
                return Json(new { model = ((new CustomReportModel()).GetDmFilterField(CusID)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    
        public ActionResult DeleteCustomReportFilters(long ID)
        {
            try
            {
                string msg = new CustomReportModel().DeleteCustomReportFilters(ID);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveCusReFilterTxt(CustomReportModel model)
        {
            try
            {

                return Json(new { Msg = (new CustomReportModel()).SaveCusReFilterTxt(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCreatedBy()
        {
            try
            {
                return Json(new { model = ((new CustomReportModel()).GetCreatedBy()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ExportReport(long ReportID)
        {
            try
            {
                return Json(new { FileName = new CustomReportModel().ExportReport(ReportID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCustomReportListRange(CustomReportModel model)
        {
            try
            {
                return Json(new { PageNumber = (new CustomReportModel()).GetCustomReportListRange(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCustomReportPageList(CustomReportModel model)
        {

            try
            {
                return Json((new CustomReportModel()).GetCustomReportPageList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetVehicleMakeList()
        {
            try
            {
                return Json(new { model = ((new CustomReportModel()).GetVehicleMakeList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
       
	}
}