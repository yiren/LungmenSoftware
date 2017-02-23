﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LungmenSoftware.Models.Service;

namespace LungmenSoftware.Controllers.WebAPI
{
    [RoutePrefix("crdata")]  
    public class ChangeRequestDataController : ApiController
    {
        private ChangeRequestService crService;

        public ChangeRequestDataController()
        {
            crService=new ChangeRequestService();
        }
        // GET api/<controller>
        
        [Route("")]
        public IEnumerable Get()
        {
            return crService.GetChangeRequestHistory();

        }
        //public string Get()
        //{
        //    return "string";

        //}

        [Route("test")]
        [HttpGet]
        public string Test()
        {
            return "test";

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