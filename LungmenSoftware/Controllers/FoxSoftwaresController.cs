using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using LungmenSoftware.Helper;
using LungmenSoftware.Models;
using LungmenSoftware.Models.CodeFirst.Entities;
using LungmenSoftware.Models.Service;
using LungmenSoftware.Models.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LungmenSoftware.Controllers
{
    public class FoxSoftwaresController : Controller
    {
        private FoxSoftService softService;
        private WorkStationService wkService;
        private ChangeRequestService crService;

        public FoxSoftwaresController()
        {
            this.softService=new FoxSoftService();
            this.wkService=new WorkStationService();
            this.crService=new ChangeRequestService();
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

        public ActionResult ChangeRequestHistory(long k)
        {
            
            WKAndFoxJoinTable record = softService.GetJoinTableDataById(k);
            if (record == null)
            {
                return HttpNotFound();
            }
            List<ChangeRequestInfo> data = crService.GetChangeRequestHistoryByJoinTable(record);

            return View(data);
        }

        public class AngularData
        {
            public string Rev { get; set; }
            public List<WKAndFoxJoinTable> JoinTableData { get; set; }

            //使用StringBuilder組字串
            //public String ToJSon()
            //{
            //    StringBuilder sb = new StringBuilder();
            //    JsonWriter jw = new JsonTextWriter(new StringWriter(sb));

            //    jw.Formatting=Formatting.Indented;
            //    ;
            //    jw.WriteStartObject();
            //    jw.WritePropertyName("rev");
            //    jw.WriteValue(Rev);
            //    jw.WritePropertyName("JoinTableData");
            //    jw.WriteStartArray();

            //    foreach (WKAndFoxJoinTable jt in JoinTableData)
            //    {
            //        jw.WriteStartObject();
            //        jw.WritePropertyName("FoxWorkStationId");
            //        jw.WriteValue(jt.FoxWorkStationId);
            //        jw.WriteEndObject();
            //    }

            //    jw.WriteEndArray();
            //    jw.WriteEndObject();

            //    return sb.ToString();
            //}

            //使用JObject
            public JObject ConvertToJObject()
            {
                JObject obj=new JObject();
                List<JProperty> children=new List<JProperty>();
                obj.Add(new JProperty("Rev", Rev));

                foreach (var item in JoinTableData)
                {
                   children.Add(new JProperty("FoxWorkStationId", item.FoxWorkStationId));      
                }
                obj.Add(new JProperty("Workstations", children));

                return obj;
            }

        }

        //For AngularJS Form
        public ActionResult GetRevListBySoftId(int id)
        {
            var wkListById = wkService.AjaxRequestForWorkstationsBySoftId(id);
            //List<AngularData> dataToJson=new List<AngularData>();
            //List<WKAndFoxJoinTable> jts=new List<WKAndFoxJoinTable>();
            //StringBuilder json=new StringBuilder();
            //JObject obj=new JObject();
            //JObject children = new JObject();
            //foreach (var perRev in wkListById)
            //{
            //    jts.AddRange(perRev);
            //    //obj.Add("Rev", perRev.Key);
            //    //foreach (var item in jts)
            //    //{
            //    //    children.Add(new JProperty("FoxWorkStationId", item.FoxWorkStationId));
            //    //}
            //    //obj.Add(new JProperty("FoxWorkstations", jts));
            //    AngularData record = new AngularData()
            //    {
            //        Rev = perRev.Key,
            //        JoinTableData = jts
            //    };

            //    dataToJson.Add(record);
            //}
            //string json2 = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            //{
            //    Formatting = Formatting.Indented
            //});

            //Use SerializeObject Method
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

        //Legacy Code
        ////For AngualrJS
        //public ActionResult GetWorkstationsByRev(string rev)
        //{
        //    List<FoxWorkStation> wkList = wkService.GetWorkstationsByRev(rev);

        //    string json = JsonConvert.SerializeObject(wkList, new JsonSerializerSettings()
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //        Formatting = Formatting.Indented
        //    });

        //    return new ContentResult()
        //    {
        //        Content = json,
        //        ContentType = "application/json"
        //    };
        //}

        //For AngularJS Form

        //For AngualrJS
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
        public ActionResult UpdateSoftwareRev(List<ChangeDelta> data)
        {
            bool isUpdate = true;//softService.UpdateSoftwareRev(data);

            if (isUpdate)
            {
                string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings()
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
            //Legacy Code
            //var softFromDb = softService.GetFoxSoftwareById(record.FoxSoftwareId);
            //if (softFromDb == null)
            //{
            //    return new ContentResult()
            //    {
            //        Content = "The record is not Found.",
            //        ContentType = "application/json"
            //    };
            //}
        }

        //For Angular Grid UI Test
        public ActionResult GetJsonSoftwareList()
        {
            var sysSoftList = softService.GetFoxSoftwaresByTypeId(2);

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