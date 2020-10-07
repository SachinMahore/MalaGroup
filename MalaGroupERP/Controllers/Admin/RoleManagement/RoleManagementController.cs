using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;
using MalaGroupERP.Data;

namespace MalaGroupERP.Controllers
{
    [MalaGroupWebAuthorizationController]
    public class RoleManagementController : Controller
    {
          public ActionResult Index()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights model = malaGroupWebSession.UserAccess("RoleManagement");
            if (model.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            new CommonModel().AddPageLoginHistory("RoleManagement");
            return View("..\\Admin\\RoleManagement\\Index", model);
        }

        public ActionResult GetRoleList()
        {
            return PartialView("..\\Admin\\RoleManagement\\_RoleList");
        }

        public ActionResult AddRole()
        {
            var usersinrole = new RoleManagementModel().GetUsersRole(0);
            var modelsinrole = new RoleManagementModel().GetModelsRole(0);

            var model = new RoleManagementModel();
            model.RoleId = 0;
            model.UsersInRole = usersinrole;
            model.ModelRoles = modelsinrole;
            return PartialView("..\\Admin\\RoleManagement\\_AddRole", model);
        }
        public ActionResult EditRole(int RoleID = 0)
        {
            var usersinrole = new RoleManagementModel().GetUsersRole(RoleID);
            var modelroles = new RoleManagementModel().GetModelsRole(RoleID);
            var model = new RoleManagementModel().GetRoleInfo(RoleID);
            model.UsersInRole = usersinrole;
            model.ModelRoles = modelroles;
            return PartialView("..\\Admin\\RoleManagement\\_EditRole", model);
        }
        public ActionResult GetList()
        {
            try
            {
                return Json((new RoleManagementModel()).GetList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetRolesToAssign(int RoleID)
        {
            try
            {
                return Json((new RoleManagementModel()).GetRoleToAssign(RoleID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteRole(string RoleID)
        {
            try
            {
                RoleManagementModel model = new RoleManagementModel();

                model.RoleId = int.Parse(RoleID);
                model.Delete();
                return Json(new { result = "1", msg = "Role deleted successfully."}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = "0", msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult AssignDeleteRole(int RoleID, int AssignRoleID)
        {
            try
            {
                RoleManagementModel model = new RoleManagementModel();
                model.AssingAndDeleteRole(RoleID, AssignRoleID);
                return Json(new { result = "1", msg = "Role deleted successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { result = "0", msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetRoleInfo(int RoleID)
        {
            try
            {
                return Json((new RoleManagementModel()).GetRoleInfo(RoleID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //[ClientErrorHandler]
        [HttpPost]
        public ActionResult Create(RoleManagementModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (IsValidInput(model))
                    {
                        model.Create();
                        return Json(new { result = "1", msg = "Role Created Successfully.", RoleID = model.RoleId }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new Exception("Invalid request");
                    }
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

        public ActionResult Update(RoleManagementModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (IsValidInput(model))
                    {
                        model.Update();
                        return Json(new { result = "1", msg = "Role Updated Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = "0", msg = "Invalid request" }, JsonRequestBehavior.AllowGet);
                    }
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
        public bool IsValidInput(RoleManagementModel model)
        {
            return true;
        }
    
	}
}