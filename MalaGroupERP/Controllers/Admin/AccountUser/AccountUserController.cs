using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;
using System.Web.Routing;

namespace MalaGroupERP.Controllers
{
    [MalaGroupWebAuthorizationController]
    public class AccountUserController : Controller
    {
        //
        // GET: /Leads/
        public ActionResult Index()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights model = malaGroupWebSession.UserAccess("AccountUser");
            if (model.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("..\\Admin\\AccountUser\\Index");
        }
        public ActionResult AddEdit()
        {
            long id = 0;
            var model = new AccountUserModel().GetAccountsInfo(id);
            return View("..\\Admin\\AccountUser\\AddEdit", model);
        }
        public ActionResult SaveUpdateAccounts(AccountUserModel model)
        {
            try
            {

                return Json(new { AccountID = (new AccountUserModel()).SaveUpdateAccounts(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteAccountData(long AccountID)
        {
            try
            {
                string msg = new AccountUserModel().DeleteAccountData(AccountID);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Edit(long id)
        {
            
            var model = new AccountUserModel().GetAccountsInfo(id);
            return View("..\\Admin\\AccountUser\\AddEdit", model);
        }
        public ActionResult GetAccountsList(AccountUserModel model)
        {

            try
            {
                return Json((new AccountUserModel()).GetAccountsList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAccountFilterRangeList(AccountUserModel model)
        {
            try
            {
                return Json(new { PageNumber = (new AccountUserModel()).GetAccountFilterRangeList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateDecal(AccountUserModel model)
        {
            try
            {

                return Json(new { AOID = (new AccountUserModel()).UpdateDecal(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateVehicleInfo(AccountUserModel.DecalData model)
        {
            try
            {

                return Json(new { AOID = (new AccountUserModel()).UpdateVehicleInfo(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDecalInfoData(long AOID)
        {
            try
            {
                return Json((new { returnREData = new AccountUserModel().GetDecalInfoData(AOID) }), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTransactionHistoryDeatil(long AccountID)
        {

            try
            {
                return Json((new AccountUserModel()).GetTransactionHistoryDeatil(AccountID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTransactionDetails(long AccountID)
        {

            try
            {
                return Json((new AccountUserModel()).GetTransactionDetails(AccountID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTransOrderDetails(long AOID)
        {

            try
            {
                return Json((new AccountUserModel()).GetTransOrderDetails(AOID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTotalOrderDecal(long AOID)
        {

            try
            {
                return Json((new AccountUserModel()).GetTotalOrderDecal(AOID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCardCheckInfo(long AccountID)
        {

            try
            {
                return Json((new AccountUserModel()).GetCardCheckInfo(AccountID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult EditCardCheckDetail(int isDefault, long lid, string CCNUM, string CCCODE, string CCMM, string CCYY, string CardNumber, string CardSecCode, string CardMonth, string CardYear)
        {

            try
            {
                return Json(new { MSG = (new AccountUserModel()).EditCardCheckDetail(isDefault,lid, CCNUM, CCCODE, CCMM, CCYY,CardNumber ,CardSecCode ,CardMonth,CardYear ) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ExportDailyMail(string ExportDate)
        {
            try
            {
                string fileName = new AccountUserModel().ExportDailyMail(ExportDate);
                return Json(new { FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ImportToAccount(AccountUserModel formData)
        {

            try
            {
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                string msg = formData.ImportToAccount(fb);
                return Json(new { Msg = msg, ID = 1 }, JsonRequestBehavior.AllowGet);
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
                new CommonModel().SendEmail(FromEmail, ToEmail, CCEmail, BCCEmail, EmailSubject, EmailMessage, LeadID, AttachFile, 2);

                return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveToFolder(AccountUserModel formData)
        {
            try
            {
                long AccountID = formData.AccountID;
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                string msg = formData.SaveToFolder(fb, AccountID);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAttachedLeadFiles(long AccountID)
        {
            try
            {
                return Json((new { returnREData = new AccountUserModel().GetAttachedLeadFiles(AccountID) }), JsonRequestBehavior.AllowGet);

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
                string msg = new AccountUserModel().DeleteFiles(ID);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetEmailTemplates()
        {
            try
            {
                return Json((new AccountUserModel()).GetEmailTemplates(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetEmailData(long TemplateId, long AccountId)
        {
            try
            {
                string emailData = "";
                string emailSubject = "";
                string attachmentFile = "";
                (new AccountUserModel()).GetEmailData(TemplateId, AccountId, ref emailData, ref emailSubject, ref attachmentFile);
                return Json(new { html = emailData, subject = emailSubject, attachmentFile = attachmentFile }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SavePaymentConsoleTrans(TransactionModel model)
        {
            try
            {

                return Json(new { Msg = (new AccountUserModel()).SavePaymentConsoleTrans(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetRefundVoidInfo(long AccountID, int RVCType)
        {

            try
            {
                return Json((new AccountUserModel()).GetRefundVoidInfo(AccountID, RVCType), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveRefundVoidInfo(string CDID, int RVCType, string PinNumber, decimal RefundAmt)
        {
            try
            {

                return Json(new { Msg = (new AccountUserModel()).SaveRefundVoidInfo(CDID, RVCType, PinNumber, RefundAmt) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SetScheduleStatus(long CDID, int TransType)
        {
            try
            {
                return Json(new { Msg = (new AccountUserModel()).SetScheduleStatus(CDID, TransType) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateDateAmountTransaction(List<OrderTransactions> model)
        {
            try
            {
                return Json(new { Msg = (new AccountUserModel()).UpdateDateAmountTransaction(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveDecal(AccountUserModel model)
        {
            try
            {

                return Json(new { AOID = (new AccountUserModel()).SaveDecal(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetEmailHistory(long AccountID,int PageID)
        {
            try
            {
                return Json((new AccountUserModel()).GetEmailHistory(AccountID, PageID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetEmailDet(int ID)
        {
            try
            {
                return Json((new AccountUserModel()).GetEmailDet(ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAccountAdvSearch(AccountUserModel model)
        {
            try
            {
                return Json(new { PageNumber = (new AccountUserModel()).GetAccountAdvSearch(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult HistoryDetail(long AccountID)
        {
            try
            {
                return Json((new AccountUserModel()).HistoryDetail(AccountID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}