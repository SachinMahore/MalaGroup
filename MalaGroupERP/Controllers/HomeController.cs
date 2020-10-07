using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers
{
   [MalaGroupWebAuthorizationController]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //AgentOrderModel model = new AgentOrderModel();
            //model.FirstName = "CARLOS";
            //model.LastName = "MARTINEZ";
            //model.IsDiffBillingAdd = 0;
            //model.Street = "2746 Silverspur Dr";
            //model.City = "Corpus Christi";
            //model.State = "TX";
            //model.Zip = "78410-2116";
            //model.Country = "US";
            //model.LeadEmail = "robz@ntsrtracking.com";
            //model.OrderID = 0;
            //model.PinNo = "GV10521335";
            //CardModel card = new CardModel() { CardNumber = "5333646595482222", ExpirationDate = "1220", CardCode = "", Amount = 36.50M, AOModel = model };
            //createTransactionResponse response = new PaymentTransactionModel().ChargeCreditCard(card);

            //CardModel card = new CardModel() { CardNumber = "5333646595482220", ExpirationDate = "122020", CardCode = "", Amount = 1.00M };
            //createTransactionResponse response = new PaymentTransactionModel().AuthorizeCreditCard(card);

            //DateTime chargeDate = Convert.ToDateTime("07/24/2019");
            //new CardScheduleModel().CheckAuthorizeSattelment(chargeDate);

            //DateTime firstDate = Convert.ToDateTime("04/02/2019");
            //DateTime lastDate = Convert.ToDateTime("04/02/2019 23:59:59");
            //getSettledBatchListResponse sbr = new TransactionReportingModel().GetSettledBatchList(firstDate, lastDate);

            //if (sbr.batchList != null)
            //{
            //    foreach (var bi in sbr.batchList)
            //    {
            //        getTransactionListResponse tlr = new TransactionReportingModel().GetTransactionList(bi.batchId, 1000, 1);
            //        if (tlr.transactions != null)
            //        {
            //            var tlrList = tlr.transactions.ToList();


            //        }
            //    }
            //}

            return View();
        }
        public ActionResult GetExportHistory()
        {

            try
            {
                return Json((new HomeModel()).GetExportHistory(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult MarkViewedExportHistory(int ID)
        {

            try
            {
                (new HomeModel()).MarkViewedExportHistory(ID);
                return Json(new { msg = "Marked Viewed Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAutocompleteSuggestions(string term)
        {
            try
            {
                return Json((new HomeModel()).GetAutocompleteSuggestions(term), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GoToController(string DValue)
        {
            try
            {
                string[] goToValue = DValue.Split(':');
                string controller = "";
                if (goToValue[0] == "LI")
                {
                    Session["LeadID"] = goToValue[1];
                    controller = "/Leads/Edit/" + Session["LeadID"].ToString();
                }
                else if (goToValue[0]== "AI")
                {
                    Session["AccID"] = goToValue[1];

                    controller = "/AccountPage/Edit/" + Session["AccID"].ToString();
                }
                else if (goToValue[0] == "CI")
                {
                    Session["AccID"] = goToValue[1];

                    controller = "/AccountPage/Edit/" + Session["AccID"].ToString();
                }
                else if (goToValue[0] == "")
                {
                    Session["term"] = goToValue[1];
                    controller = "/Search/Index/?term=" + Session["term"].ToString();
                }
                return Json(new { IsSet = "1", GoToURL = controller }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Session["LeadID"] = null;
                Session["CID"] = null;
              
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTakeOffListReport()
        {

            try
            {
                return Json((new HomeModel()).GetTakeOffListReport(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTakeOffListReportByReportID(long ID)
        {
            try
            {
                return Json((new TakeOffListModel()).GetTakeOffListReportByReportID(ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteTakeOffListReport(long ID)
        {
            try
            {
                (new HomeModel()).DeleteTakeOffListReport(ID);
                return Json(new { msg = "Deleted Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDashGraphData(HomeModel model)
        {
            try
            {
                return Json(new { model = new HomeModel().GetDashGraphData(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}