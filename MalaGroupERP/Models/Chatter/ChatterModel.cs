using MalaGroupERP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;

namespace MalaGroupERP.Models
{
    public class ChatterModel
    {
        public long CID { get; set; }
        public string FeedItemId { get; set; }
        public string AccountId { get; set; }
        public long LeadID { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public string CreatedDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string LinkUrl { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> InsertedById { get; set; }
        public Nullable<int> IsRichText { get; set; }
        public string ViewedCount { get; set; }
        public string ViewedBy { get; set; }
        public string UserName { get; set; }
        public string SystemFileName { get; set; }
        public string OriginalFileName { get; set; }
        public string SaveUpdateChat(HttpPostedFileBase fb,ChatterModel model)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string ID = "0";
            if (model.CID == 0)
            {
                if (fb != null && fb.ContentLength > 0)
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/FileAttachments/");
                    string fileName = fb.FileName;
                    string sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb.FileName);
                    fb.SaveAs(filePath + "//" + sysFileName);

                    var chatSave = new tbl_Chattter()
                    {

                        Title = model.Title,
                        Body = model.Body,
                        Type = model.Type,
                        LinkUrl = model.LinkUrl,
                        AccountId = model.AccountId,
                        IsRichText = 0,
                        IsDeleted = 0,
                        CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                        InsertedById = MalaGroupWebSession.CurrentUser.UserID,
                        CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                        SystemFileName = sysFileName,
                        OriginalFileName = fileName,
                        LeadID = model.LeadID,
                    };
                    db.tbl_Chattter.Add(chatSave);
                    db.SaveChanges();
                    model.CID = chatSave.CID;
                    ID = chatSave.CID.ToString();
                    db.Dispose();
                    msg = "Chat Added Successfully";
                }
                else
                {
                    var chatSave = new tbl_Chattter()
                    {

                        Title = model.Title,
                        Body = model.Body,
                        Type = model.Type,
                        LinkUrl = model.LinkUrl,
                        AccountId = model.AccountId,
                        IsRichText = 0,
                        IsDeleted = 0,
                        CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                        InsertedById = MalaGroupWebSession.CurrentUser.UserID,
                        CreatedById = MalaGroupWebSession.CurrentUser.UserID,
                        SystemFileName = "",
                        OriginalFileName = "",
                        LeadID = model.LeadID,
                    };
                    db.tbl_Chattter.Add(chatSave);
                    db.SaveChanges();
                    model.CID = chatSave.CID;
                    ID = chatSave.CID.ToString();
                    db.Dispose();
                    msg = "Chat Added Successfully";
                }
            }
            else
            {
               
                var chatSave = db.tbl_Chattter.Where(p => p.CID == model.CID).FirstOrDefault();
                if (chatSave.CreatedById == MalaGroupWebSession.CurrentUser.UserID)
                {
                    chatSave.Title = model.Title;
                    chatSave.Body = model.Body;
                    chatSave.Type = model.Type;
                    chatSave.LinkUrl = model.LinkUrl;

                    db.SaveChanges();
                    msg = "Chat Updated Successfully";
                }
                else
                {
                    msg = "Sorry ! Unable to Edit Chat Created by Other";
                }
            }
            db.Dispose();
            return msg;
        }
        public List<ChatterModel> GetChats(long AccountID,int PageId)
        {
            try
            {
                List<ChatterModel> model = new List<ChatterModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetChatsList";
                        cmd.CommandType = CommandType.StoredProcedure;
                       
                        DbParameter AID = cmd.CreateParameter();
                        AID.ParameterName = "AccountID";
                        AID.Value = AccountID;
                        cmd.Parameters.Add(AID);

                        DbParameter PID = cmd.CreateParameter();
                        PID.ParameterName = "PageId";
                        PID.Value = PageId;
                        cmd.Parameters.Add(PID);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? createdDate = null;
                            try
                            {
                                createdDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                            }
                            catch
                            {

                            }
                            model.Add(new ChatterModel()
                            {
                                CID = Convert.ToInt64(dr["CID"].ToString()),
                                Body = dr["Body"].ToString(),
                                Title = dr["Title"].ToString(),
                                CreatedDate = (createdDate.HasValue ? createdDate.Value.ToString("MM/dd/yyyy hh:mm tt") : ""),
                                UserName = dr["Username"].ToString(),
                                ViewedCount = dr["ViewedCount"].ToString(),
                                ViewedBy = dr["ViewedByNames"].ToString(),
                            });
                        }


                    }
                    catch
                    {
                        db.Database.Connection.Close();
                    }
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ChatterModel GetChatDet(int ID)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            ChatterModel model = new ChatterModel();
            var chatDet = db.tbl_Chattter.Where(p => p.CID == ID).FirstOrDefault();
            model.Title = chatDet.Title;
            model.Body = chatDet.Body;
            model.CreatedDate = chatDet.CreatedDate.ToString();
            model.Type = chatDet.Type;
            model.IsRichText = chatDet.IsRichText;
            model.LinkUrl = chatDet.LinkUrl;
            model.SystemFileName = chatDet.SystemFileName;
            model.OriginalFileName = chatDet.OriginalFileName;
            model.CID = ID;

            int uid = Convert.ToInt32(MalaGroupWebSession.CurrentUser.UserID);
            var viewedChat = db.tbl_ChatViewedBy.Where(p => p.CID == ID && p.ViewedById == uid).FirstOrDefault();
            if (viewedChat == null)
            {
                var saveViewdBy = new tbl_ChatViewedBy()
                {
                    CID = ID,
                    ViewedById = Convert.ToInt32(MalaGroupWebSession.CurrentUser.UserID),
                    ViewedDate = DateTime.Now,

                };
                db.tbl_ChatViewedBy.Add(saveViewdBy);
                db.SaveChanges();
            }

            return model;
        }
        public string DeleteChat(long CID)
        {
            string MSG="";
            MalaGroupERPEntities db=new MalaGroupERPEntities ();
            var chatDet = db.tbl_Chattter.Where(p => p.CID == CID).FirstOrDefault();
            if(chatDet!=null)
            {
                if (chatDet.CreatedById == MalaGroupWebSession.CurrentUser.UserID)
                {
                    db.tbl_Chattter.Remove(chatDet);
                    db.SaveChanges();
                    MSG = "Chat deleted Successfully";
                }
                else
                {
                    MSG = "Sorry ! Unable to Delete Chat Created by Other";
                }
            }
            return MSG;
        }
    }
}