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
    public class PayrollController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                return View("..\\Reports\\PayRoll\\PayRollReport");
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetPayRollViewData(PayrollModel model)
        {
            try
            {
                return Json(new { model = new PayrollModel().ExportReport(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult ExportReport(PayrollModel model)
        //{
        //    try
        //    {
        //        ViewBag.RT = "";
        //        ViewBag.RT = "2Payroll";

        //        string fileName = "";

        //        DataTable Dt = new DataTable();
        //        DataTable barGraph = new DataTable();
        //        Dt = new PayrollModel().ExportReport(model, ref fileName, ref barGraph);
        //        ViewBag.FileName = fileName;

        //        List<PayrollModel> labelList = new List<PayrollModel>();
        //        string labels = "";
        //        string grossdata = "";
        //        string netdata = "";
        //        string cancelvoiddata = "";
        //        string adddecaldata = "";
        //        string identitydata = "";

        //        string grossdataTotal = "";
        //        string netdataTotal = "";
        //        string cancelvoiddataTotal = "";
        //        string adddecaldataTotal = "";
        //        string identitydataTotal = "";
        //        foreach (DataRow dr in barGraph.Rows)
        //        {
        //            if (labels == "")
        //            {
        //                labels = dr["CreatedBy"].ToString();
        //            }
        //            else
        //            {
        //                labels += "," + dr["CreatedBy"].ToString();
        //            }
        //        }

        //        ViewData["Label"] = labels;
        //        List<PayrollModel> subBarList = new List<PayrollModel>();
        //        int i = 0;
        //        foreach (DataRow dr in barGraph.Rows)
        //        {
        //            if (i == 0)
        //            {
        //                grossdataTotal = dr["GrossTotal"].ToString();
        //                netdataTotal = dr["NETTotal"].ToString();
        //                cancelvoiddataTotal = dr["CancelsandVoidsTotal"].ToString();
        //                adddecaldataTotal = dr["ADDDecalTotal"].ToString();
        //                identitydataTotal = dr["IDETheftTotal"].ToString();
        //            }
        //            grossdata += (grossdata == "" ? dr["Gross"].ToString() : "," + dr["Gross"].ToString());
        //            netdata += (netdata == "" ? dr["NET"].ToString() : "," + dr["NET"].ToString());
        //            cancelvoiddata += (cancelvoiddata == "" ? dr["CancelsandVoids"].ToString() : "," + dr["CancelsandVoids"].ToString());
        //            adddecaldata += (adddecaldata == "" ? dr["ADDDecal"].ToString() : "," + dr["ADDDecal"].ToString());
        //            identitydata += (identitydata == "" ? dr["IDETheft"].ToString() : "," + dr["IDETheft"].ToString());
        //            i++;
        //        }
        //        ViewData["GrossData"] = grossdata;
        //        ViewData["NetData"] = netdata;
        //        ViewData["CVData"] = cancelvoiddata;
        //        ViewData["ADData"] = adddecaldata;
        //        ViewData["IDData"] = identitydata;


        //        ViewData["GrossDataTotal"] = grossdataTotal;
        //        ViewData["NetDataTotal"] = netdataTotal;
        //        ViewData["CVDataTotal"] = cancelvoiddataTotal;
        //        ViewData["ADDataTotal"] = adddecaldataTotal;
        //        ViewData["IDDataTotal"] = identitydataTotal;

        //        return View("..\\Reports\\PayRoll\\PayRollReport", Dt);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}


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