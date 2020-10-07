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
    public class AccountPageController : Controller
    {
        //
        // GET: /Leads/
        public ActionResult Index()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights model = malaGroupWebSession.UserAccess("AccountPage");
            if (model.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("..\\Admin\\AccountPage\\Index");
        }
        public ActionResult AddEdit()
        {
            long id = 0;
            var model = new AccountPageModel().GetAccountsInfo(id);
            return View("..\\Admin\\AccountPage\\AddEdit",model);
        }
        public ActionResult SaveUpdateAccounts(AccountPageModel model)
        {
            try
            {

                return Json(new { AccountID = (new AccountPageModel()).SaveUpdateAccounts(model) }, JsonRequestBehavior.AllowGet);
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
                string msg = new AccountPageModel().DeleteAccountData(AccountID);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(long id)
        {
            
            var model = new AccountPageModel().GetAccountsInfo(id);
            return View("..\\Admin\\AccountPage\\AddEdit", model);
        }
        public ActionResult GetAccountsList(AccountPageModel model)
        {

            try
            {
                return Json((new AccountPageModel()).GetAccountsList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAccountFilterRangeList(AccountPageModel model)
        {
            try
            {
                return Json(new { PageNumber = (new AccountPageModel()).GetAccountFilterRangeList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateDecal(AccountPageModel model)
        {
            try
            {

                return Json(new { AOID = (new AccountPageModel()).UpdateDecal(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteDecal(AccountPageModel model)
        {
            try
            {

                return Json(new { AOID = (new AccountPageModel()).DeleteDecal(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateVehicleInfo(AccountPageModel.DecalData model)
        {
            try
            {

                return Json(new { AOID = (new AccountPageModel()).UpdateVehicleInfo(model) }, JsonRequestBehavior.AllowGet);
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
                return Json((new { returnREData = new AccountPageModel().GetDecalInfoData(AOID) }), JsonRequestBehavior.AllowGet);

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
                return Json((new AccountPageModel()).GetTransactionHistoryDeatil(AccountID), JsonRequestBehavior.AllowGet);
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
                return Json((new AccountPageModel()).GetTransactionDetails(AccountID), JsonRequestBehavior.AllowGet);
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
                return Json((new AccountPageModel()).GetTransOrderDetails(AOID), JsonRequestBehavior.AllowGet);
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
                return Json((new AccountPageModel()).GetTotalOrderDecal(AOID), JsonRequestBehavior.AllowGet);
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
                return Json((new AccountPageModel()).GetCardCheckInfo(AccountID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditCardCheckDetail(int isDefault, long lid, string CCNUM, string CCCODE, string CCMM, string CCYY, string OldCardNumber, string OldCardSecCode, string OldCardMonth, string OldCardYear)
        {

            try
            {
                return Json(new { MSG = (new AccountPageModel()).EditCardCheckDetail(isDefault, lid, CCNUM, CCCODE, CCMM, CCYY, OldCardNumber, OldCardSecCode, OldCardMonth, OldCardYear) }, JsonRequestBehavior.AllowGet);
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
                string fileName = new AccountPageModel().ExportDailyMail(ExportDate);
                return Json(new { FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ImportToAccount(AccountPageModel formData)
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
        //public ActionResult SendEmail(string FromEmail, string ToEmail, string CCEmail, string BCCEmail, string EmailSubject, string EmailMessage, long LeadID, string AttachFile)
        //{
        //    try
        //    {
        //        new CommonModel().SendEmail(FromEmail, ToEmail, CCEmail, BCCEmail, EmailSubject, EmailMessage, LeadID, AttachFile, 2);

        //        return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public ActionResult SendEmailAttach(EmailModel formData)
        {

            try
            {
                // long AccountID = formData.AccountID;
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                string msg = new CommonModel().SendEmailAttach(fb, formData);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveToFolder(AccountPageModel formData)
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
                return Json((new { returnREData = new AccountPageModel().GetAttachedLeadFiles(AccountID) }), JsonRequestBehavior.AllowGet);

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
                string msg = new AccountPageModel().DeleteFiles(ID);
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
                return Json((new AccountPageModel()).GetEmailTemplates(), JsonRequestBehavior.AllowGet);
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
                (new AccountPageModel()).GetEmailData(TemplateId, AccountId, ref emailData, ref emailSubject, ref attachmentFile);
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

                return Json(new { Msg = (new AccountPageModel()).SavePaymentConsoleTrans(model) }, JsonRequestBehavior.AllowGet);
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
                return Json((new AccountPageModel()).GetRefundVoidInfo(AccountID, RVCType), JsonRequestBehavior.AllowGet);
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

                return Json(new { Msg = (new AccountPageModel()).SaveRefundVoidInfo(CDID, RVCType, PinNumber, RefundAmt) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveChargeInfo(string CDID, int RVCType, string PinNumber, decimal RefundAmt)
        {
            try
            {

                return Json(new { Msg = (new AccountPageModel()).SaveChargeInfo(CDID, RVCType, PinNumber, RefundAmt) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ReenableRefund(long AOID)
        {
            try
            {

                return Json(new { Msg = (new AccountPageModel()).ReenableRefund(AOID) }, JsonRequestBehavior.AllowGet);
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
                return Json(new { Msg = (new AccountPageModel()).SetScheduleStatus(CDID, TransType) }, JsonRequestBehavior.AllowGet);
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
                return Json(new { Msg = (new AccountPageModel()).UpdateDateAmountTransaction(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveDecal(AccountPageModel model)
        {
            try
            {

                return Json(new { AOID = (new AccountPageModel()).SaveDecal(model) }, JsonRequestBehavior.AllowGet);
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
                return Json((new AccountPageModel()).GetEmailHistory(AccountID, PageID), JsonRequestBehavior.AllowGet);
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
                return Json((new AccountPageModel()).GetEmailDet(ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAccountAdvSearch(AccountPageModel model)
        {
            try
            {
                return Json(new { PageNumber = (new AccountPageModel()).GetAccountAdvSearch(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult HistoryDetail(long AccountID,long LeadID)
        {
            try
            {
                return Json((new AccountPageModel()).HistoryDetail(AccountID, LeadID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveNewTrans(AccountPageModel.TransactionHistory model)
        {
            try
            {

                return Json(new { AOID = (new AccountPageModel()).SaveNewTrans(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateDecalTheft(long AOID,int DecTheft,int DecTheftValue)
        {
            try
            {

                return Json(new { Msg = (new AccountPageModel()).UpdateDecalTheft(AOID,DecTheft, DecTheftValue) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}