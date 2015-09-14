using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LungmenSoftware.Models.ViewModel;
using Newtonsoft.Json.Linq;


namespace LungmenSoftware.Models.Service
{
    public class WorkStationService : IWorkStationsService
    {
        private LungmenSoftwareDataEntities ldb;
     
        public WorkStationService()
        {
            ldb = new LungmenSoftwareDataEntities();
        }

        public void UpdateWorkStationInfo(FoxWorkStation wk)
        {
            wk.LastModifiedDate=DateTime.Now;
            ldb.SaveChanges();
        }

        public List<FoxWorkStation> GetAllWorkstations()
        {
            return ldb.FoxWorkStations.ToList(); ;
        }

        public List<FoxWorkStation> GetWorkStationsByType(string typeName)
        {
            return ldb.FoxWorkStations.Where(w => w.WorkStationName.Contains(typeName)).ToList();
        }

        public FoxWorkStation GetWorkStationByName(String name)
        {
            var wkquery = ldb.FoxWorkStations.Where(k => k.WorkStationName.Equals(name));
            int queryCount = wkquery.Count();
            if (queryCount == 0)
            {
                Console.WriteLine("no workstation is found");
                return null;
            }
            return null;
        }

        public FoxWorkStation GetWorkStationById(int id)
        {
            return ldb.FoxWorkStations.Find(id);
        }

        public List<WorkStationHardwareType> GetWKHardareTypes()
        {
            return ldb.WorkStationHardwareTypes.ToList();
        }

        public List<FoxWorkStationAndSoftwareInfo> GetWorkStationsBySoftwareId(int softId)
        {
            var query = from soft in ldb.FoxSoftwares.Where(s => s.FoxSoftwareId == softId)
                join wkJoinTable in ldb.WKAndFoxJoinTables 
                    on soft.FoxSoftwareId equals wkJoinTable.FoxSoftwareId
                join wk in ldb.FoxWorkStations
                    on wkJoinTable.FoxWorkStationId equals wk.WorkStationId
                        //where soft.FoxSoftwareId==1 && soft.FoxSoftwareTypeId==1
                select new FoxWorkStationAndSoftwareInfo()
                {
                    FoxWorkStationId= wk.WorkStationId,
                    WorkStationName = wk.WorkStationName,
                    SoftwareName=soft.SoftwareName,
                    Rev=wkJoinTable.Rev,
                    Procedure=soft.Procedure,
                    Note=wkJoinTable.Note
                };
            
        

            return query.ToList();
        }

        public void AddNewWorkStation(FoxWorkStation wk)
        {
            
            ldb.FoxWorkStations.Add(wk);
            ldb.SaveChanges();
        }


        public class AngularData
        {
            public string Rev { get; set; }
            public List<FoxWorkStationAndSoftwareInfo> JoinTableData { get; set; }

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
                JObject obj = new JObject();
                List<JProperty> children = new List<JProperty>();
                obj.Add(new JProperty("Rev", Rev));

                foreach (var item in JoinTableData)
                {
                    children.Add(new JProperty("FoxWorkStationId", item.FoxWorkStationId));
                }
                obj.Add(new JProperty("Workstations", children));

                return obj;
            }

        }
        //For AngularJS
        public List<AngularData> AjaxRequestForWorkstationsBySoftId(int id)
        {
            var query = from soft in ldb.FoxSoftwares.Where(s => s.FoxSoftwareId == id)
                join wkJoinTable in ldb.WKAndFoxJoinTables
                    on soft.FoxSoftwareId equals wkJoinTable.FoxSoftwareId
                join wk in ldb.FoxWorkStations
                    on wkJoinTable.FoxWorkStationId equals wk.WorkStationId
                        //where soft.FoxSoftwareId==1 && soft.FoxSoftwareTypeId==1
                       select new FoxWorkStationAndSoftwareInfo()
                        {
                            FoxWorkStationId = wk.WorkStationId,
                            WorkStationName = wk.WorkStationName,
                            SoftwareName = soft.SoftwareName,
                            FoxSoftwareId = soft.FoxSoftwareId,
                            Rev = wkJoinTable.Rev 
                        };


            var query2 = query.GroupBy(g => g.Rev).ToList();
                

            List<AngularData> dataToJson = new List<AngularData>();
            
            //StringBuilder json=new StringBuilder();
            //JObject obj=new JObject();
            //JObject children = new JObject();
            foreach (var perRev in query2)
            {
                List<FoxWorkStationAndSoftwareInfo> jts = new List<FoxWorkStationAndSoftwareInfo>();
                jts.AddRange(perRev);
                //obj.Add("Rev", perRev.Key);
                //foreach (var item in jts)
                //{
                //    children.Add(new JProperty("FoxWorkStationId", item.FoxWorkStationId));
                //}
                //obj.Add(new JProperty("FoxWorkstations", jts));
                AngularData record = new AngularData()
                {
                    Rev = perRev.Key,
                    JoinTableData = jts
                };

                dataToJson.Add(record);
                
            }

            return dataToJson;
        }

        public List<FoxWorkStation> GetWorkstationsByRev(string rev)
        {
            var query = from jt in ldb.WKAndFoxJoinTables.Where(j => j.Rev.Equals(rev))
                join wk in ldb.FoxWorkStations on jt.FoxWorkStationId equals wk.WorkStationId
                select wk;
            return query.ToList();
        }
    }

    //public class SoftAndWks
    //{
    //    public int? FoxWorkStationId { get; set; }
    //    public int FoxSoftwareId { get; set; }
    //    public string WorkStationName { get; set; }
    //    public string Rev { get; set; }
    //}
}