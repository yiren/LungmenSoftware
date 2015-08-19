using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models.Service;
using LungmenSoftware.Models.ViewModel;

namespace LungmenSoftware.Controllers
{
    public class FoxSoftwaresController : Controller
    {
        private FoxSoftService softService;

        public FoxSoftwaresController()
        {
            this.softService=new FoxSoftService();
        }
        // GET: FoxSoftwares
        public ActionResult Index()
        {

            return View();
        }

    }
}