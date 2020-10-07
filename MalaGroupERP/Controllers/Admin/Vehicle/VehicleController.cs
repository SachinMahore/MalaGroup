using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers
{
    [MalaGroupWebAuthorizationController]
    public class VehicleController : Controller
    {
        //
        // GET: /Vehicle/
        public ActionResult Index()
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights model = malaGroupWebSession.UserAccess("Vehicle");
            if (model.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("../Admin/Vehicle/Index");
        }
        public ActionResult GetVehicleMakeList()
        {
            try
            {
                return Json(new { model = ((new VehicleModel()).GetVehicleMakeList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetVehicleTypeList(int VehcileMake)
        {
            try
            {
                return Json(new { model = ((new VehicleModel()).GetVehicleTypeList(VehcileMake)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetVTypeGrid(string VehicleType)
        {
            try
            {
                return Json(new { model = ((new VehicleModel()).GetVTypeGrid(VehicleType)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult GetVModelGrid(string VehicleModal)
        //{
        //    try
        //    {
        //        return Json(new { model = ((new VehicleModel()).GetVModelGrid(VehicleModal)) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public ActionResult GetVehicleModelList(int VehcileMake, int VehicleType)
        //{
        //    try
        //    {
        //        return Json(new { model = ((new VehicleModel()).GetVehicleModelList(VehcileMake, VehicleType)) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult SaveUpdateVMake(VehicleModel model)
        {
            try
            {

                return Json(new { MSG = (new VehicleModel()).SaveUpdateVMake(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
 

        public ActionResult SaveUpdateVType(VehicleModel model)
        {
            try
            {

                return Json(new { MSG = (new VehicleModel()).SaveUpdateVType(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult SaveUpdateVModel(VehicleModel model)
        //{
        //    try
        //    {

        //        return Json(new { MSG = (new VehicleModel()).SaveUpdateVModel(model) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public ActionResult GetVehicleMakeInfo(int VID)
        {
            try
            {

                return Json((new VehicleModel()).GetVehicleMakeInfo(VID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetVehicleTypeInfo(int VTID)
        {
            try
            {

                return Json((new VehicleModel()).GetVehicleTypeInfo(VTID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult GetVehicleModelInfo(int VMID)
        //{
        //    try
        //    {

        //        return Json((new VehicleModel()).GetVehicleModelInfo(VMID), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public ActionResult Edit(long id)
        {
            MalaGroupWebSession malaGroupWebSession = new MalaGroupWebSession();
            UserAccessRights modelRight = malaGroupWebSession.UserAccess("Vehicle");
            if (modelRight.HasRight == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new LeadsModel().GetLeadInfo(id);
            return View("..\\Vehicle\\Index", model);
        }

        public ActionResult GetMakeInfoPageList(VehicleModel model)
        {

            try
            {
                return Json((new VehicleModel()).GetMakeInfoPageList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetMakeFilterRangeList(VehicleModel model)
        {
            try
            {
                return Json(new { PageNumber = (new VehicleModel()).GetMakeFilterRangeList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTypeInfoPageList(VehicleModel model)
        {

            try
            {
                return Json((new VehicleModel()).GetTypeInfoPageList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTypeFilterRangeList(VehicleModel model)
        {
            try
            {
                return Json(new { PageNumberType = (new VehicleModel()).GetTypeFilterRangeList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteVMake(VehicleModel model)
        {
            try
            {

                return Json(new { MSG = (new VehicleModel()).DeleteVMake(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}