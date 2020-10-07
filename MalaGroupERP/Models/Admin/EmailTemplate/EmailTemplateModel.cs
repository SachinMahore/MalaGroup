using MalaGroupERP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace MalaGroupERP.Models
{
    public class EmailTemplateModel
    {
        public long TemplateID { get; set; }
        public string TemplateName { get; set; }
        public string TemplateSubject { get; set; }
        public string TemplateHTML { get; set; }
        public string TemplatePlainText { get; set; }
        public string AttachmentFileName { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<int> LastModifiedById { get; set; }
        public Nullable<int> TemplateFor { get; set; }
        public List<EmailTemplateModel> GetEmailTemplates()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<EmailTemplateModel> model = new List<EmailTemplateModel>();
            var emailTemplate = db.tbl_EmailTemplates.ToList().OrderBy(p => p.TemplateName);
            foreach (var et in emailTemplate)
            {
                model.Add(new EmailTemplateModel() { TemplateID = et.TemplateID, TemplateName = et.TemplateName ,TemplateFor=et.TemplateFor});
            }
            return model;
        }
        public void GetEmailTemplateData(long TemplateId, long AccountId, ref string EmailData, ref string EmailSubject, ref string TemplateName, ref string TemplateFor, ref string AttachmentFile)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            var emailTemplate = db.tbl_EmailTemplates.Where(p => p.TemplateID == TemplateId).FirstOrDefault();
            EmailData = emailTemplate.TemplateHTML;
            EmailSubject = emailTemplate.TemplateSubject;
            AttachmentFile = emailTemplate.AttachmentFileName;
            TemplateName = emailTemplate.TemplateName;
            TemplateFor = emailTemplate.TemplateFor.ToString();
        }
        public void SaveEmailTemplate(long TemplateId, string EmailData, string EmailSubject, string TemplateName, string TemplateFor)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            var aoEmailTemp = db.tbl_EmailTemplates.Where(p => p.TemplateID == TemplateId).FirstOrDefault();
            if (aoEmailTemp != null)
            {
                aoEmailTemp.TemplateName = TemplateName;
                aoEmailTemp.TemplateSubject = EmailSubject;
                aoEmailTemp.TemplateHTML = EmailData;
                aoEmailTemp. LastModifiedDate = DateTime.Now;
                aoEmailTemp.LastModifiedById = MalaGroupWebSession.CurrentUser.UserID;
                aoEmailTemp.TemplateFor =Convert.ToInt32(TemplateFor);
                db.SaveChanges();
            }
            else
            {
                var aosData = new tbl_EmailTemplates()
                {
                    TemplateName = TemplateName,
                    TemplateSubject = EmailSubject,
                    TemplateHTML = EmailData,
                    IsDeleted=0,
                    TemplateFor=2,
                    CreatedDate = DateTime.Now,
                    CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedById = MalaGroupWebSession.CurrentUser.UserID,
                };
                db.tbl_EmailTemplates.Add(aosData);
                db.SaveChanges();
            }
        }

        public List<DropDownModel> GetSnippetList()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<DropDownModel> model = new List<DropDownModel>();
            var emailTemplate = db.tbl_Snippet.Where(p => p.SnippetFor == 1 || p.SnippetFor == 2).ToList();
            foreach (var et in emailTemplate)
            {
                model.Add(new DropDownModel() { Value = et.Snippet, Text = et.Snippet });
            }
            return model;
        }
    }
}