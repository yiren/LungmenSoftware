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

        
        //For AngularJS Form
        public ActionResult GetWorkStationsBySoftId(int id)
        {
            var wkListById = wkService.AjaxRequestForWorkstationsBySoftId(id);
            string json = JsonConvert.SerializeObject(wkListById, new JsonSerializerSettings()
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

        //For AngularJS Form
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

        //For AngularJS Form
        public ActionResult UpdateSoftwareRev(SoftwareToUpdate record)
        {
            var softFromDb = softService.GetFoxSoftwareById(record.FoxSoftwareId);
            if (softFromDb == null)
            {
                return new ContentResult()
                {
                    Content = "The record is not Found.",
                    ContentType = "application/json"
                };
            }
            bool isUpdate=softService.UpdateSoftwareRev(softFromDb, record.Rev);

            if (isUpdate)
            {
                string json = JsonConvert.SerializeObject(record, new JsonSerializerSettings()
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
            else
            {
                return new ContentResult()
                {
                    Content="The record is not Updated.",
                    ContentType = "application/json"
                };
            }
            
        }
    }
}