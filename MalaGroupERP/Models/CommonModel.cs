using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using MalaGroupERP.Data;
using System.Net;
using System.Net.Mail;

namespace MalaGroupERP.Models
{
    public class CommonModel
    {
        public void SendEmail(string FromEmail, string ToEmail, string CCEmail, string BCCEmail, string EmailSubject, string EmailMessage, long AGID, string AttachFile, int PageID)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            var fromAddress = new MailAddress(MalaGroupWebSession.CurrentUser.SMPTUserName);
            var toAddress = new MailAddress(ToEmail);
            string fromPassword = MalaGroupWebSession.CurrentUser.SMTPPassword;
            string subject = EmailSubject;
            string body = EmailMessage;

            var smtp = new SmtpClient
            {
                Host = "mail.ntsrtracking.com",
                Port = 26,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            var message = new MailMessage();
            message.From = new MailAddress(FromEmail);
            string[] toEmailsArray=ToEmail.Split(',');
            foreach(string toEmail in toEmailsArray)
            {
                message.To.Add(toEmail);
            }

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            //// Create  the file attachment for this email message.
            //Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            //// Add time stamp information for the file.
            //ContentDisposition disposition = data.ContentDisposition;
            //disposition.CreationDate = System.IO.File.GetCreationTime(file);
            //disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            //disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            //// Add the file attachment to this email message.
            //message.Attachments.Add(data);

            if (!string.IsNullOrWhiteSpace(CCEmail))
            {
                string[] ccEmailsArray = CCEmail.Split(',');
                foreach (string ccEmail in ccEmailsArray)
                {
                    message.CC.Add(CCEmail);
                }
               
            }
            if (!string.IsNullOrWhiteSpace(BCCEmail))
            {
                string[] bccEmailsArray = BCCEmail.Split(',');
                foreach (string bccEmail in bccEmailsArray)
                {
                    message.Bcc.Add(bccEmail);
                }
            }

            if (!string.IsNullOrWhiteSpace(AttachFile))
            {
                string fileName = HttpContext.Current.Server.MapPath("~/AttachmentFiles") + "/" + AttachFile;
                message.Attachments.Add(new Attachment(fileName));
            }
            smtp.Send(message);

            var emaiData = new tbl_EMails()
            {
                PageID = PageID,
                AutoGenID = AGID,
                FromEmail = FromEmail,
                ToEmails = ToEmail,
                CCEmails = CCEmail,
                BCCEmails = BCCEmail,
                EmailSubject = EmailSubject,
                EmailHTMLBody = EmailMessage,
                EmailPlainText = "",
                CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                CreatedDate = DateTime.Now,
            };
            db.tbl_EMails.Add(emaiData);
            db.SaveChanges();
        }
        public string SendEmailAttach(HttpPostedFileBase fb, EmailModel model)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string filePath = "";
            string fileName = "";
            string sysFileName = "";


            var fromAddress = new MailAddress(MalaGroupWebSession.CurrentUser.SMPTUserName);
            var toAddress = new MailAddress(model.ToEmails);
            string fromPassword = MalaGroupWebSession.CurrentUser.SMTPPassword;
            string subject = model.EmailSubject;
            string body = model.EmailHTMLBody;

            var smtp = new SmtpClient
            {
                Host = "mail.ntsrtracking.com",
                Port = 26,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("robz@ntsrtracking.com", "Bs935418")
            };
            var message = new MailMessage();
            message.From = new MailAddress(model.FromEmail);
            string[] toEmailsArray = model.ToEmails.Split(',');
            foreach (string toEmail in toEmailsArray)
            {
                message.To.Add(toEmail);
            }

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;


            if (!string.IsNullOrWhiteSpace(model.CCEmails))
            {
                string[] ccEmailsArray = model.CCEmails.Split(',');
                foreach (string ccEmail in ccEmailsArray)
                {
                    message.CC.Add(model.CCEmails);
                }

            }
            if (!string.IsNullOrWhiteSpace(model.BCCEmails))
            {
                string[] bccEmailsArray = model.BCCEmails.Split(',');
                foreach (string bccEmail in bccEmailsArray)
                {
                    message.Bcc.Add(bccEmail);
                }
            }
            if (fb != null && fb.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/AttachmentFiles/");
                fileName = fb.FileName;
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fb.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/AttachmentFiles") + "/" + sysFileName;
                    message.Attachments.Add(new Attachment(afileName));
                }
            }
            smtp.Send(message);

            var emaiData = new tbl_EMails()
            {
                PageID = model.PageID,
                AutoGenID = model.AGID,
                FromEmail = model.FromEmail,
                ToEmails = model.ToEmails,
                CCEmails = model.CCEmails,
                BCCEmails = model.BCCEmails,
                EmailSubject = model.EmailSubject,
                EmailHTMLBody = model.EmailHTMLBody,
                EmailPlainText = "",
                CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                CreatedDate = DateTime.Now,
                AttachedFileName = sysFileName,
                OriginalFileName = fileName,
            };
            db.tbl_EMails.Add(emaiData);
            db.SaveChanges();


            return "Email Send Successfully";
        }
        public void SendEmailPayments(string ToEmail, string EmailSubject,string EmailMessage, long AGID)
        {
           
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            var toAddress = new MailAddress(ToEmail);
            string fromPassword = MalaGroupWebSession.CurrentUser.SMTPPassword;
            string subject = EmailSubject;
            string body = EmailMessage;

            var smtp = new SmtpClient
            {
                Host = "mail.ntsrtracking.com",
                Port = 26,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("robz@ntsrtracking.com", "Bs935418")
            };
            var message = new MailMessage();
            message.From = new MailAddress("admin@nationaltheftsearchandrecovery.org");
            string[] toEmailsArray = ToEmail.Split(',');
            foreach (string toEmail in toEmailsArray)
            {
                message.To.Add(toEmail);
            }

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            try
            {

                smtp.Send(message);
            }
            catch
            {

            }

            var emaiData = new tbl_EMails()
            {
                PageID = 0,
                AutoGenID = AGID,
                FromEmail = "admin@nationaltheftsearchandrecovery.org",
                ToEmails = ToEmail,
                CCEmails = "",
                BCCEmails = "",
                EmailSubject = EmailSubject,
                EmailHTMLBody = EmailMessage,
                EmailPlainText = "",
                CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                CreatedDate = DateTime.Now,
            };
            db.tbl_EMails.Add(emaiData);
            db.SaveChanges();
        }
        public void SendEmailPaymentSchedule(string EmailSubject, string EmailBody, string AttachFile)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string subject = EmailSubject;
            var smtp = new SmtpClient
            {
                Host = "mail.ntsrtracking.com",
                Port = 26,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("robz@ntsrtracking.com", "Bs935418")
            };
            var message = new MailMessage();
            message.From = new MailAddress("admin@nationaltheftsearchandrecovery.org");
            string[] toEmailsArray = "admin@nationaltheftsearchandrecovery.org,robz@ntsrtracking.com".Split(',');
            //string[] toEmailsArray = "vijayramteke@gmail.com".Split(',');
            foreach (string toEmail in toEmailsArray)
            {
                message.To.Add(toEmail);
            }

            message.Subject = subject;
            message.Body = EmailBody;
            message.IsBodyHtml = true;

            if (!string.IsNullOrWhiteSpace(AttachFile))
            {
                string fileName = HttpContext.Current.Server.MapPath("~/TempFiles") + "/" + AttachFile;
                message.Attachments.Add(new Attachment(fileName));
            }
            smtp.Send(message);

            var emaiData = new tbl_EMails()
            {
                PageID = 0,
                AutoGenID = 0,
                FromEmail = "admin@nationaltheftsearchandrecovery.org",
                ToEmails = "admin@nationaltheftsearchandrecovery.org,robz@ntsrtracking.com",
                CCEmails = "",
                BCCEmails = "",
                EmailSubject = EmailSubject,
                EmailHTMLBody = EmailBody,
                EmailPlainText = "",
                CreatedById = 1,
                CreatedDate = DateTime.Now,
            };
            db.tbl_EMails.Add(emaiData);
            db.SaveChanges();
        }
        public static string GetUserFirstName()
        {
            return MalaGroupWebSession.CurrentUser.LoggedInUser;
        }
        public static string GetUserFullName()
        {
            return MalaGroupWebSession.CurrentUser.FullName;
        }
        public static string GetUserExitension()
        {
            return MalaGroupWebSession.CurrentUser.Extension;
        }
        public void AddPageLoginHistory(string pageName)
        {
            try
            {
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_AddPageLoginHistory";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter paramUserID = cmd.CreateParameter();
                        paramUserID.ParameterName = "UserID";
                        paramUserID.Value = (MalaGroupWebSession.CurrentUser.UserID == 0 ? 0 : MalaGroupWebSession.CurrentUser.UserID);
                        cmd.Parameters.Add(paramUserID);

                        DbParameter paramSessionID = cmd.CreateParameter();
                        paramSessionID.ParameterName = "SessionID";
                        paramSessionID.Value = HttpContext.Current.Session.SessionID.ToString();
                        cmd.Parameters.Add(paramSessionID);

                        DbParameter paramPageName = cmd.CreateParameter();
                        paramPageName.ParameterName = "PageName";
                        paramPageName.Value = (pageName);
                        cmd.Parameters.Add(paramPageName);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        cmd.ExecuteNonQuery();
                        db.Database.Connection.Close();
                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Log(string message)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Log");
            FileStream fs = new FileStream(filePath + "/Log.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(message);
            sw.Flush();
            sw.Close();
        }
        public void LogAgentOrder(string message)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Log");
            FileStream fs = new FileStream(filePath + "/LogAgentOrder.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(message);
            sw.Flush();
            sw.Close();
        }
        public void DeleteFiles()
        {
            var filePath = HttpContext.Current.Server.MapPath("~/TempFiles");
            string[] files = Directory.GetFiles(filePath);

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.LastAccessTime < DateTime.Now.AddDays(-1))
                    fi.Delete();
            }
        }

        public static string AOScript(int StepID)
        {
            string script = "";
            string AOscript = "";
            try
            {               
                MalaGroupERPEntities db = new MalaGroupERPEntities();
             DataSet dsDataSet = new DataSet();
             using (var cmd = db.Database.Connection.CreateCommand())
             {
                 try
                 {
                     db.Database.Connection.Open();
                     cmd.CommandText = "usp_GetAOScript";
                     cmd.CommandType = CommandType.StoredProcedure;

                     DbParameter AID = cmd.CreateParameter();
                     AID.ParameterName = "StepID ";
                     AID.Value = StepID;
                     cmd.Parameters.Add(AID);

                     DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                     da.SelectCommand = cmd;
                     da.Fill(dsDataSet);
                     db.Database.Connection.Close();
                 }
                 catch
                 {
                     db.Database.Connection.Close();
                 }
                 if (dsDataSet.Tables[0] != null && dsDataSet.Tables[0].Rows.Count > 0)
                 {

                     script = dsDataSet.Tables[0].Rows[0]["AOScript"].ToString();
                   
                 }
             }
             //if (StepID == 1 || StepID == 12)
             //{
                 AOscript = script;
                 AOscript = AOscript.Replace("{UserName}", CommonModel.GetUserFirstName() );
                 AOscript = AOscript.Replace("{UserExt}", CommonModel.GetUserExitension() );
             //}
             //else
             //{
             //    AOscript = script;
             //}
             return AOscript;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int IsAdmin()
        {
            return MalaGroupWebSession.CurrentUser.IsAdmin;
        }
    }
    public class DropDownModel
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
    public class Enums
    {
        public enum UserRole
        {
            Administrator = 1,
            Customer = 2
        }
    }
    public class AttachedFiles
    {
        public long ID { get; set; }
        public Nullable<int> PageID { get; set; }
        public Nullable<long> AGID { get; set; }
        public string SystemFileName { get; set; }
        public string OriginalFileName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }

    }
}