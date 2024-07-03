using LeBarbier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeBarbier.Controllers
{
    public class ServicesController : Controller
    {
        private DBContext db = new DBContext();
        public ActionResult AllServices()
        {
            var allServices = db.OfferedServices.ToList();

            return View(allServices);
        }
    }
}