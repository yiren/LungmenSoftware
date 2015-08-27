using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models.Service;
using LungmenSoftware.Models.ViewModel;

namespace LungmenSoftware.Controllers
{
    public class ChangeRequestController : Controller
    {
        private ChangeRequestService crService;

        public ChangeRequestController()
        {
            this.crService=new ChangeRequestService();
        }
        // GET: ChangeRequest
        public ActionResult Index()
        {
            ChangeRequestView dataForView=new ChangeRequestView();
            
            dataForView.ChangeRequests=crService.GetChangeRequestList();
            return View(dataForView);
        }
    }
}