using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LungmenSoftware.Models.DRS;
using LungmenSoftware.Models.Service;

namespace LungmenSoftware.Controllers.WebAPI
{
    [RoutePrefix("drsData")]
    public class DrsDataController : ApiController
    {
        // GET api/<controller>
        private DrsDataService dataService=new DrsDataService();
                    
        [Route("")]
        public IEnumerable<DrsListViewModel> Get()
        {
            return dataService.GetAllDrsDataList();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}