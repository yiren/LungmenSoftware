using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models.Service;

namespace LungmenSoftware.Controllers
{
    public class AngularController : Controller
    {
        private FoxSoftService softService;
        private WorkStationService wkService;

        public AngularController()
        {
            this.softService=new FoxSoftService();
            this.wkService=new WorkStationService();
        }
        // GET: Angular
        public ActionResult Index()
        {
            
            return View(wkService.GetAllWorkstations().OrderBy(p=>p.WorkStationName).ToList());
        }

        public ActionResult FormTest()
        {
            return View();
        }



        public PartialViewResult Home()
        {
            return PartialView();
        }

        public PartialViewResult Second()
        {
            return PartialView();
        }

        public PartialViewResult Third()
        {
            return PartialView();
        }

    }
}