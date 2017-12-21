using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models;
using LungmenSoftware.Models.Repositories;
using LungmenSoftware.Models.Service;
using LungmenSoftware.Models.ViewModel;
using Newtonsoft.Json;

namespace LungmenSoftware.Controllers
{
    [RoutePrefix("foxworkstations")]
    public class FoxWorkStationsController : Controller
    {
        private WorkStationService wkService;
        private FoxSoftService softService;

        public FoxWorkStationsController()
        {
            this.wkService = new WorkStationService();
            this.softService=new FoxSoftService();
        }
        // GET: FoxWorkStations
        public ActionResult Index(string search)
        {
            FoxWorkStationView dataForView = new FoxWorkStationView()
            {
                WPList = wkService.GetWorkStationsByType("WP"),
                AWList = wkService.GetWorkStationsByType("AW"),
                OtherList = wkService.GetWorkStationsByType("CPU")
            };

            return View(dataForView);
        }

        public ActionResult EditWorkStation(int id)
        {
            var wk = wkService.GetWorkStationById(id);
            if (wk == null)
            {
                return HttpNotFound();
            }
            WorkStationViewModelForEdit dataForView=new WorkStationViewModelForEdit()
            {
                
                FoxWorkStation=wk,
                Types= wkService.GetWKHardareTypes().Select(
                d=> new SelectListItem()
                    {
                        Text = d.HardwareTypeName,
                        Value = d.Id.ToString()
                    }).OrderBy(o=>o.Value)
                };
            return View(dataForView);
        }

        [HttpPost]
        public ActionResult EditWorkStation(WorkStationViewModelForEdit vm)
        {
            var wk = wkService.GetWorkStationById(vm.FoxWorkStation.WorkStationId);
            if (wk == null)
            {
                return HttpNotFound();
            }
            wk.WorkStationName = vm.FoxWorkStation.WorkStationName;
            wk.WorkStationHardwareTypeId = vm.FoxWorkStation.WorkStationHardwareTypeId;
            wk.Owner = vm.FoxWorkStation.Owner;
            if (ModelState.IsValid)
            {
                wkService.UpdateWorkStationInfo(wk);
            }

            return RedirectToAction("Index");
        }

        public ActionResult GetWorkstations()
        {
            var wksFromDB=wkService.GetAllWorkstations();
            JsonSerializerSettings jsSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(wksFromDB, jsSettings);
            return new ContentResult()
            {
                Content = json,
                ContentType = "application/json"
            };
        }

        public ActionResult InstalledSoftwaresByWorkStationId(int id)
        {
            var allSoftware = softService.GetFoxSoftwareListByWorkStationId(id);
            FoxSoftView dataForView = new FoxSoftView()
            {
                APList = allSoftware.Where(s=>s.SoftwareTypeId==1).ToList(),
                OSList = allSoftware.Where(s => s.SoftwareTypeId == 2).ToList()
            };
            return View(dataForView);
        }

        //for AngularJS Only
        [HttpPost]
        public ActionResult AddNewWorkStation(FoxWorkStation wk)
        {
            //wkService.AddNewWorkStation(wk);

            JsonSerializerSettings jsSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(wk, jsSettings);
            return new ContentResult()
            {
                Content = json,
                ContentType = "application/json"
            };
            //return wk !=null;
        }
    }
}