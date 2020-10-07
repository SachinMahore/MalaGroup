using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MalaGroupERP.Data;
using System.Data;
using System.Data.Common;

namespace MalaGroupERP.Models
{
    public class NotesModel
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public string Photo { get; set; }
        public String NotesDate { get; set; }
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string ViewedCount { get; set; }
        public string ViewedBy { get; set; }
        public string SaveUpdateNotes(NotesModel model)
        {
            string msg = "";
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            string ID = "0";
            if (model.ID == 0)
            {

                var notesSave = new tbl_Notes()
                {
                   
                    Title = model.Title,
                    Notes=model.Notes,
                    NotesDate =Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")),
                    UserID = MalaGroupWebSession.CurrentUser.UserID,
                    CreatedByID = MalaGroupWebSession.CurrentUser.UserID,
                };
                db.tbl_Notes.Add(notesSave);
                db.SaveChanges();
                model.ID = notesSave.ID;
                ID = notesSave.ID.ToString();
                db.Dispose();
                msg = "Notes Added Successfully";
            }

            else
            {
                var notesUpdate = db.tbl_Notes.Where(p => p.ID == model.ID).FirstOrDefault();
                notesUpdate.Title = model.Title;
                notesUpdate.Notes = model.Notes;
                notesUpdate.LastModifiedById = Convert.ToInt32(MalaGroupWebSession.CurrentUser.UserID);
                notesUpdate.LastModifiedDate=Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));

                db.SaveChanges();
                ID = model.ID.ToString();
                msg = "Notes Updated Successfully";
            }

            db.Dispose();
            return msg;
        }
        public List<NotesModel> GetNotes()
        {
            try
            {
                List<NotesModel> model = new List<NotesModel>();
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    try
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetNotesList";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            DateTime? createdDate = null;
                            try
                            {
                                createdDate = Convert.ToDateTime(dr["NotesDate"].ToString());
                            }
                            catch
                            {

                            }
                            model.Add(new NotesModel()
                            {
                                ID = Convert.ToInt64(dr["ID"].ToString()),
                                //Notes = dr["Notes"].ToString(),
                                Title = dr["Title"].ToString(),
                                NotesDate = (createdDate.HasValue ? createdDate.Value.ToString("MM/dd/yyyy") : ""),
                                UserName = dr["Username"].ToString(),
                                ViewedCount = dr["ViewedCount"].ToString(),
                                ViewedBy =dr["ViewedByNames"].ToString(),
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
        public NotesModel GetNotesDet(int ID)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            NotesModel model = new NotesModel();
            var notesDet = db.tbl_Notes.Where(p => p.ID == ID).FirstOrDefault();
            model.Title = notesDet.Title;
            model.Notes = notesDet.Notes;
            model.NotesDate = notesDet.NotesDate.ToString();

            int uid=Convert.ToInt32(MalaGroupWebSession.CurrentUser.UserID);
            var viewedNotes = db.tbl_ViewedBy.Where(p => p.NID == ID && p.ViewedById == uid).FirstOrDefault();
            if(viewedNotes==null)
            {
                var saveViewdBy = new tbl_ViewedBy()
                {
                    NID=ID,
                    ViewedById=Convert.ToInt32(MalaGroupWebSession.CurrentUser.UserID),
                    ViewedDate=DateTime.Now,
                    
                };
                db.tbl_ViewedBy.Add(saveViewdBy);
                db.SaveChanges();
            }

            return model;
        }
    }
}