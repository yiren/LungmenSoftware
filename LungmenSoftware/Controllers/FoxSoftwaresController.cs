using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LungmenSoftware.Helper;
using LungmenSoftware.Models;
using LungmenSoftware.Models.Service;
using LungmenSoftware.Models.ViewModel;
using Newtonsoft.Json;

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

        public ActionResult GetWorkStationsBySoftId(int id)
        {
            var softListById = wkService.GetWorkStationsBySoftwareId(id);
            string json = JsonConvert.SerializeObject(softListById, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            });

            return new ContentResult()
            {
                Content = json,
                ContentType = "application/json"
            };
        }


        public ActionResult GetSystemSoftwares()
        {
            var sysSoftList = softService.GetSystemSoftwareList();

            //JavaScriptSerializer jsonSerializer=new JavaScriptSerializer();
            //jsonSerializer.RegisterConverters(new List<EFToJSONConverter>()
            //{
            //    new EFToJSONConverter()
            //});

            //string jsonContent = jsonSerializer.Serialize(sysSoftList);

            string json = JsonConvert.SerializeObject(sysSoftList, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            });

            return new ContentResult()
            {
                Content = json,
                ContentType = "application/json"
            };
        }
    }
}