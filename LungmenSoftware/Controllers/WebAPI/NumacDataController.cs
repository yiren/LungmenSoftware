using LungmenSoftware.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LungmenSoftware.Controllers.WebAPI
{
    [RoutePrefix("nuamcData")]
    public class NumacDataController : ApiController
    {
        private NumacDataService dataService;
        public NumacDataController()
        {
            this.dataService = new NumacDataService();
        }
        public IHttpActionResult GetNumacList()
        {
            var data = dataService.GetFirmwareListV2();

            return Ok(data);
        }

        
    }
}
