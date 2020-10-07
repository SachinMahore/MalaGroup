using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers
{
    public class ChatterController : Controller
    {
        //
        // GET: /Chatter/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveUpdateChat(ChatterModel formData)
        {
            //try
            //{

            //    return Json(new { MSG = (new ChatterModel()).SaveUpdateChat(model) }, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            //}
            try
            {
               // long AccountID = formData.AccountID;
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                string msg = formData.SaveUpdateChat(fb, formData);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetChats(long AccountID, int PageId)
        {
            try
            {
                return Json((new ChatterModel()).GetChats(AccountID,PageId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetChatDet(int ID)
        {
            try
            {
                return Json((new ChatterModel()).GetChatDet(ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteChat(long CID)
        {
            try
            {
                return Json(new { MSG = (new ChatterModel()).DeleteChat(CID) }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new  { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}