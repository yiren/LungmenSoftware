using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models;
using LungmenSoftware.Models.Repositories;
using LungmenSoftware.Models.Service;
using LungmenSoftware.Models.ViewModel;

namespace LungmenSoftware.Controllers
{
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
            ViewBag.StationTypes = wkService.GetWKHardareTypes().Select(
                d=> new SelectListItem()
                {
                    Text = d.HardwareTypeName,
                    Value = d.HardwareTypeName
                }).OrderBy(o=>o.Value);
            if (wk == null)
            {
                return HttpNotFound();
            }
            return View(wk);
        }

        [HttpPost]
        public ActionResult EditWorkStation(FoxWorkStation wkToUpdate)
        {
            var wk = wkService.GetWorkStationById(wkToUpdate.WorkStationId);
            if (wk == null)
            {
                return HttpNotFound();
            }
            wk.WorkStationName = wkToUpdate.WorkStationName;
            wk.WorkStationHardwareTypeId = wkToUpdate.WorkStationHardwareTypeId;
            wk.Owner = wkToUpdate.Owner;
            if (ModelState.IsValid)
            {
                wkService.UpdateWorkStationInfo(wk);
            }

            return RedirectToAction("Index");
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
    }
}