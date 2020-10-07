using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Data;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers
{
    [MalaGroupWebAuthorizationController]
    public class NotesController : Controller
    {
        //
        // GET: /Notes/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveUpdateNotes(NotesModel model)
        {
            try
            {

                return Json(new { MSG = (new NotesModel()).SaveUpdateNotes(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetNotes()
        {
            try
            {
                return Json((new NotesModel()).GetNotes(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetNotesDet(int ID)
        {
            try
            {
                return Json((new NotesModel()).GetNotesDet(ID), JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}