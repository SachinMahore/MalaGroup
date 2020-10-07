using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers.Admin
{
    [MalaGroupWebAuthorizationController]
    public class AssignRoleController : Controller
    {
        // GET: /AssignRole/
        public ActionResult Index()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights model = malaGroupWebSession.UserAccess("AssignRole");
            if (model.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            new CommonModel().AddPageLoginHistory("AssignRole");
            return View("..\\Admin\\AssignRole\\Index", model);
        }
        public ActionResult GetRoles()
        {

            try
            {
                return Json((new AssignRoleModel()).GetRoles(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetModules(int RoleID)
        {

            try
            {
                return Json((new AssignRoleModel()).GetModules(RoleID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetSubModules(int ModelID)
        {

            try
            {
                return Json((new AssignRoleModel()).GetSubModules(ModelID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RoleModuleRightsList(int RoleID, int ModuleID)
        {

            try
            {
                return Json((new AssignRoleModel()).RoleModuleRightsList(RoleID, ModuleID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //[ClientErrorHandler]
        //[HttpPost]
        public ActionResult Create(int RoleID, int ModuleID, List<AssignRoleModel.ARMData> rolemodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AssignRoleModel model = new AssignRoleModel();
                    model.RoleId = RoleID;
                    model.ModuleId = ModuleID;
                    model.RoleModels = rolemodel;
                    model.Create();
                    return Json(new { result = "1", msg = "Assign Role Successfully." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { result = "0", msg = "Invalid request" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "0", msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public class ARMData
        {
            public int ID { get; set; }
            public int RoleID { get; set; }
            public int ResourceID { get; set; }
            public int AccessLevel { get; set; }
            public int HasSpecial { get; set; }

        }
	}
}