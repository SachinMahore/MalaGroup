using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        public ActionResult Index(string term)
        {
            List<AutocompleteSuggestions> model = (new AutocompleteSuggestions()).GetMainSearchList(term);
            return View(model);
            //var model = new HomeModel().GetMainSearchList(term);
            //return View("..\\Search\\Index\\", model);
        }
        //public ActionResult GetMainSearchList(string term)
        //{
        //    try
        //    {
        //        return Json((new HomeModel()).GetMainSearchList(term), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
	}
}