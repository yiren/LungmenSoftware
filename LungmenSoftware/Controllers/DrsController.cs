using LungmenSoftware.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LungmenSoftware.Controllers
{
    [RoutePrefix("drs")]
    public class DrsController : Controller
    {
        private ChangeRequestService crSerice = new ChangeRequestService();
        // GET: Drs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DrsChangeRequestById(string fidId)
        {
            var data = crSerice.GetDrsChangeRequestRecordById(fidId);

            return View(data);
        }
    }
}