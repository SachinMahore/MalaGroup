using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MalaGroupERP.Models;

namespace MalaGroupERP.Controllers
{
    [MalaGroupWebAuthorizationController]
    public class NavigationMenuController : Controller
    {
        //
        // GET: /NavigationMenu/
        public ActionResult Index()
        {
            NavigationMenuModel model = new NavigationMenuModel();
            model.GetMenuList();
            return PartialView(model);
        }
	}
}