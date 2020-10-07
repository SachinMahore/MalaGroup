using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers.UserManagement
{
    [MalaGroupWebAuthorizationController]
    public class UserManagementController : Controller
    {
        public ActionResult Index()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights model = malaGroupWebSession.UserAccess("UserManagement");
            if (model.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            new CommonModel().AddPageLoginHistory("UserManagement");
            return View("..\\Admin\\UserManagement\\Index");
        }
        public ActionResult AddEdit()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights modelRole = malaGroupWebSession.UserAccess("UserManagement");
            if (modelRole.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            int id = 0;
            var model = new UserManagementModel().GetUserInfo(id);
            var roles = new UserManagementModel().GetRoleList(0);
            model.UserRole = roles;
            return View("..\\Admin\\UserManagement\\AddEdit", model);
        }
        
       
        public ActionResult AddUser(UserManagementModel model)
        {
            try
            {
                return Json(new { UserID = (new UserManagementModel()).Create(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
       
        public ActionResult Edit(int id)
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights modelRole = malaGroupWebSession.UserAccess("UserManagement");
            if (modelRole.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new UserManagementModel().GetUserInfo(id);
            var roles = new UserManagementModel().GetRoleList(id);
            model.UserRole = roles;
            return View("..\\Admin\\UserManagement\\AddEdit", model);
        }
        
     
        public ActionResult GetUserInfo(int UserID)
        {
            try
            {
                return Json((new UserManagementModel()).GetUserInfo(UserID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetOwnerList(string UserName)
        {
            try
            {
                return Json(new { model = ((new UserManagementModel()).GetOwnerList(UserName)) }, JsonRequestBehavior.AllowGet);
                  
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetUserList(UserManagementModel model)
        {
            try
            {
                return Json((new UserManagementModel()).GetUserList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetUserFilterRangeList(UserManagementModel model)
        {
            try
            {
                return Json(new { PageNumber = (new UserManagementModel()).GetUserFilterRangeList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}