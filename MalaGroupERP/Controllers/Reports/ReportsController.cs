using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;
using System.Web.Routing;
using System.Data;

namespace MalaGroupERP.Controllers
{
    [MalaGroupWebAuthorizationController]
    public class ReportsController : Controller
    {
        //
        // GET: /Leads/

        public ActionResult ExportReport(string pid, string ed, string to)
        {
            try
            {
                ViewBag.RT = "";
                if (pid == "1")
                {
                    ViewBag.RT = "Day Wise Report";
                }
                else if (pid == "2")
                {
                    ViewBag.RT = "New Agent ReportDaily Deals";

                }
                else if (pid == "3")
                {
                    ViewBag.RT = "AGENT CLOSING";
                }
                else if (pid == "4")
                {
                    ViewBag.RT = "2Payroll";
                }
                string fileName = "";
                string dtStratDate = ed.Replace("-", "/");
                string dtStratDateTo = "";
                if (string.IsNullOrEmpty(to))
                {
                    dtStratDateTo = "";
                }else
                {
                    dtStratDateTo = to.Replace("-", "/");
                }
                 DataTable Dt = new DataTable();
                 Dt = new ReportsModel().ExportReport(dtStratDate,dtStratDateTo, pid, ref fileName);
                 ViewBag.FileName = fileName;
                 return View(Dt);
                 
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ExportReportPayRoll(string pid, string clDateFrom, string clDateto, string accStatus, string lstDateFrom, string lstDateto, string chargeatm, string product)
        {
            try
            {
                ViewBag.RT = "";
                ViewBag.RT = "2Payroll";
               
                string fileName = "";
                string dtClDatefrom = null;
                string dtClDateTo = null;
                string dtLTDatefrom = null;
                string dtLTDateTo = null;
                if (!string.IsNullOrEmpty(clDateFrom))
                {
                    dtClDatefrom = clDateFrom.Replace("-", "/");
                    dtClDateTo = clDateto.Replace("-", "/");
                }
                if (!string.IsNullOrEmpty(lstDateFrom))
                {
                    dtLTDatefrom = lstDateFrom.Replace("-", "/");
                    dtLTDateTo = lstDateto.Replace("-", "/");
                }
                
                DataTable Dt = new DataTable();
                DataTable barGraph = new DataTable();
                Dt = new ReportsModel().ExportReportPayRoll(dtClDatefrom, dtClDateTo, pid, dtLTDatefrom, dtLTDateTo, accStatus, chargeatm, product, ref fileName, ref barGraph);
                ViewBag.FileName = fileName;

                List<ReportsModel> labelList = new List<ReportsModel>();
                string labels = "";
                string grossdata = "";
                string netdata = "";
                string cancelvoiddata = "";
                string adddecaldata = "";
                string identitydata = "";

                string grossdataTotal = "";
                string netdataTotal = "";
                string cancelvoiddataTotal = "";
                string adddecaldataTotal = "";
                string identitydataTotal = "";
                foreach (DataRow dr in barGraph.Rows)
                {
                    if(labels=="")
                    {
                        labels =  dr["CreatedBy"].ToString() ;
                    }
                    else
                    {
                        labels += "," + dr["CreatedBy"].ToString();
                    }
                    
                    //labelList.Add(new ReportsModel()
                    //{
                    //    CreatedBy = dr["CreatedBy"].ToString(),
                    //});
                }

                ViewData["Label"] = labels;
                List<ReportsModel> subBarList = new List<ReportsModel>();
                int i = 0;
                foreach (DataRow dr in barGraph.Rows)
                {
                    if(i==0)
                    {
                        grossdataTotal      = dr["GrossTotal"].ToString();
                        netdataTotal        = dr["NETTotal"].ToString();
                        cancelvoiddataTotal = dr["CancelsandVoidsTotal"].ToString();
                        adddecaldataTotal   = dr["ADDDecalTotal"].ToString();
                        identitydataTotal   = dr["IDETheftTotal"].ToString();
                    }
                    grossdata += (grossdata == "" ? dr["Gross"].ToString() : "," + dr["Gross"].ToString());
                    netdata += (netdata == "" ? dr["NET"].ToString() : "," + dr["NET"].ToString());
                    cancelvoiddata += (cancelvoiddata == "" ? dr["CancelsandVoids"].ToString() : "," + dr["CancelsandVoids"].ToString());
                    adddecaldata += (adddecaldata == "" ? dr["ADDDecal"].ToString() : "," + dr["ADDDecal"].ToString());
                    identitydata += (identitydata == "" ? dr["IDETheft"].ToString() : "," + dr["IDETheft"].ToString());
                    i++;
                }
                ViewData["GrossData"] = grossdata;
                ViewData["NetData"] = netdata;
                ViewData["CVData"] = cancelvoiddata;
                ViewData["ADData"] = adddecaldata;
                ViewData["IDData"] = identitydata;


                ViewData["GrossDataTotal"] = grossdataTotal ;     
                ViewData["NetDataTotal"]   = netdataTotal  ;      
                ViewData["CVDataTotal"]    = cancelvoiddataTotal ;
                ViewData["ADDataTotal"]    = adddecaldataTotal ;
                ViewData["IDDataTotal"] = identitydataTotal;  

                return View("..\\Reports\\PayRollReport", Dt);

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
                return Json(new { model = ((new ReportsModel()).GetProductList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}