using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers
{
    public class EmailTemplateController : Controller
    {
        //
        // GET: /EmailTemplate/
        public ActionResult Index()
        {
            //var model = new QuestionModel().GetAOScriptDet(81);
            return View("..\\Admin\\EmailTemplate\\Index");
        }
        public ActionResult GetEmailTemplates()
        {
            try
            {
                return Json((new EmailTemplateModel()).GetEmailTemplates(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetEmailTemplateData(long TemplateId, long AccountId)
        {
            try
            {
                string emailData = "";
                string emailSubject = "";
                string tempName = "";
                string tempFor = "";
                string attachmentFile = "";
                (new EmailTemplateModel()).GetEmailTemplateData(TemplateId, AccountId, ref emailData, ref emailSubject, ref tempName, ref tempFor, ref attachmentFile);
                return Json(new { html = emailData, subject = emailSubject,name=tempName,templateFor=tempFor, attachmentFile = attachmentFile }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveEmailTemplate(long TemplateId, string EmailData, string EmailSubject, string TemplateName, string TemplateFor)
        {
            try
            {
                new EmailTemplateModel().SaveEmailTemplate(TemplateId, EmailData, EmailSubject, TemplateName, TemplateFor);

                return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetSnippetList()
        {
            try
            {
                return Json((new EmailTemplateModel()).GetSnippetList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}