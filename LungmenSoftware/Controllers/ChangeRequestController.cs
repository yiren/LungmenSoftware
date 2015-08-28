using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models.CodeFirst.Entities;
using LungmenSoftware.Models.Service;
using LungmenSoftware.Models.ViewModel;

namespace LungmenSoftware.Controllers
{
    [Authorize]
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

        public ActionResult AddChangeRequest()
        {
            ChangeRequest cr=new ChangeRequest();
            cr.ChangeRequestId = Guid.NewGuid();
            cr.CreatedBy = User.Identity.Name;
            cr.CreateDate=DateTime.Now;
            cr.LastModifiedDate=DateTime.Now;
            cr.SerialNumber = string.Format("{0:yyyyMMdd}", DateTime.Today) +"E"+ new Random().Next(10, 99);
            crService.SaveChangeRequestTemp(cr);
            //ViewBag.   
            return View(cr);
        }

        [HttpPost]
        public ActionResult AddChangeRequest(ChangeRequest crEntry)
        {
            var crToUpdate = crService.FindChangeRequestById(crEntry.ChangeRequestId);
            if (ModelState.IsValid)
            {
                crToUpdate.Description = crEntry.Description;
                crService.AddChangeRequestEntry(crToUpdate);
            }

            return RedirectToAction("Index");
        }


        public ActionResult CancelChangeRequest(Guid id)
        {
            var crEntry= crService.FindChangeRequestById(id);
            if (crEntry == null)
            {
                return HttpNotFound();
            }

            return View(crEntry);

        }
    }
}