using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers
{
    public class QuestionController : Controller
    {
        //
        // GET: /Question/
        public ActionResult Index()
        {
            //var model = new QuestionModel().GetAOScriptDet(81);
            return View("..\\Admin\\Question\\Index");
        }
        public ActionResult GetAOScriptData(int step)
        {
            try
            {
                string emailData = "";
              
                (new QuestionModel()).GetAOScriptData( ref emailData,  step);
                return Json(new { html = emailData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveAOScript(string AOSrcript, int Steps)
        {
            try
            {
                new QuestionModel().SaveAOScript(AOSrcript, Steps);

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
                return Json((new QuestionModel()).GetSnippetList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}