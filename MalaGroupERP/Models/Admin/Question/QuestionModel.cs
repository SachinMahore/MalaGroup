using MalaGroupERP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MalaGroupERP.Models
{
    public class QuestionModel
    {
        public int QID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> Type { get; set; }
        public List<QuestionModel> GetQuestionsList(int Type)
        {
            try
            {
                int UserID = MalaGroupWebSession.CurrentUser.UserID;
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                List<QuestionModel> model = new List<QuestionModel>();
                var ques = db.tbl_Question.Where(p => p.Status == 1 && p.Type==Type).ToList();
                foreach (var que in ques)
                {
                    model.Add(new QuestionModel()
                    {


                        Question =que.Question,
                       Answer=que.Answer
                    });
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public QuestionModel GetAOScriptDet(int step)
        {
            try
            {
                int UserID = MalaGroupWebSession.CurrentUser.UserID;
                MalaGroupERPEntities db = new MalaGroupERPEntities();
                QuestionModel model = new QuestionModel();
                var ques = db.tbl_AOScript.Where(p => p.Step == step).FirstOrDefault();

                model.Answer = ques.AOScript.ToString();

                db.Dispose();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetAOScriptData(ref string EmailData,int step)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            QuestionModel model = new QuestionModel();
            var ques = db.tbl_AOScript.Where(p => p.Step == step).FirstOrDefault();

            EmailData = ques.AOScript;

        }
        public void SaveAOScript( string AOSrcript,  int Steps)
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            var aoScript = db.tbl_AOScript.Where(p => p.Step == Steps).FirstOrDefault();
            if (aoScript != null)
            {
                aoScript.AOScript = AOSrcript;
                db.SaveChanges();
            }
            else
            {
                var aosData = new tbl_AOScript()
                {
                    AOScript = AOSrcript,
                    Step = Steps,
                    Status = 1
                };
                db.tbl_AOScript.Add(aosData);
                db.SaveChanges();
            }
        }
        public List<DropDownModel> GetSnippetList()
        {
            MalaGroupERPEntities db = new MalaGroupERPEntities();
            List<DropDownModel> model = new List<DropDownModel>();
            var emailTemplate = db.tbl_Snippet.Where(p=>p.SnippetFor==3).ToList();
            foreach (var et in emailTemplate)
            {
                model.Add(new DropDownModel() { Value = et.Snippet, Text = et.SnippetTitle });
            }
            return model;
        }
    }
}