using MalaGroupERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalaGroupERP.Controllers
{
    [MalaGroupWebAuthorizationController]
    public class PackageController : Controller
    {
        //
        // GET: /Package/
        public ActionResult Index()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights model = malaGroupWebSession.UserAccess("Package");
            if (model.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("..\\Admin\\Package\\Index");
        }
        public ActionResult AddEdit()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights modelRole = malaGroupWebSession.UserAccess("Package");
            if (modelRole.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            int id = 0;
            var model = new PackageModel().GetPackageInfo(id);
            return View("..\\Admin\\Package\\AddEdit", model);
        }

        public ActionResult SaveUpdatePackage(PackageModel model)
        {
            try
            {

                return Json(new { PackageID = (new PackageModel()).SaveUpdatePackage(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeletePackageData(int PackageID)
        {
            try
            {
                string msg = new PackageModel().DeletePackageData(PackageID);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id)
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights modelRole = malaGroupWebSession.UserAccess("Package");
            if (modelRole.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new PackageModel().GetPackageInfo(id);
            return View("..\\Admin\\Package\\AddEdit", model);
        }
     
        public ActionResult GetPackageList()
        {
            try
            {
                return Json(new { model = ((new PackageModel()).GetPackageList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPackageListDet(string AdditionalPackage)
        {
            try
            {
                return Json(new { model = ((new PackageModel()).GetPackageListDet(AdditionalPackage)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAddPackageList()
        {
            try
            {
                return Json(new { model = ((new PackageModel()).GetAddPackageList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetFullPackageList(PackageModel model)
        {

            try
            {
                return Json((new PackageModel()).GetFullPackageList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPackageFilterRangeList(PackageModel model)
        {
            try
            {
                return Json(new { PageNumber = (new PackageModel()).GetPackageFilterRangeList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllPackageList()
        {
            try
            {
                return Json(new { model = ((new PackageModel()).GetAllPackageList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}