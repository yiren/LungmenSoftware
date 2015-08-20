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
        private WorkStationService wkService;

        public FoxSoftwaresController()
        {
            this.softService=new FoxSoftService();
            this.wkService=new WorkStationService();
        }
        // GET: FoxSoftwares
        public ActionResult Index()
        {
            FoxSoftView dataForView=new FoxSoftView()
            {
                APs = softService.GetFoxSoftwaresByTypeId(1),
                OSs = softService.GetFoxSoftwaresByTypeId(2)
                
            };
            
            return View(dataForView);
        }

        public ActionResult InstalledInWorkstations(int id)
        {
            FoxWorkStationView dataForView=new FoxWorkStationView()
            {
                WorkStationsBySoftwareId = wkService.GetWorkStationsBySoftwareId(id)
            };


            return View(dataForView);
        }
    }
}