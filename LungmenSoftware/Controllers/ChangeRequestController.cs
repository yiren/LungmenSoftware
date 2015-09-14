using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models;
using LungmenSoftware.Models.CodeFirst.Entities;
using LungmenSoftware.Models.Service;
using LungmenSoftware.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace LungmenSoftware.Controllers
{
    [Authorize]
    public class ChangeRequestController : Controller
    {

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

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

        public ActionResult ChangeRequestHistorialData()
        {
            ChangeRequestView dataForView = new ChangeRequestView()
            {
                ChangeRequests = crService.GetChangeRequestHistory()
            };

            return View(dataForView);
        }

        public ActionResult AddChangeRequest()
        {
            ChangeRequest cr=new ChangeRequest();
            cr.ChangeRequestId = Guid.NewGuid();
           
            cr.SerialNumber = string.Format("{0:yyyyMMdd}", DateTime.Today) +"E"+ new Random().Next(1000, 9999);

            //這邊以後可能要修掉
            
            //ViewBag.   
            return View(cr);
        }

        public ActionResult AngularForm()
        {

            return View();
        }
        //For AngularJS
        [HttpPost]
        public ContentResult AddNewChangeRequestRecord(ChangeRequest crEntry)
        {

            crEntry.ReviewBy = crService.GetReviewer();
            crEntry.Owner = crEntry.ReviewBy;
            crEntry.IsActive = true;
            bool isSuccess=crService.AddChangeRequestRecord(crEntry);
            string json = JsonConvert.SerializeObject(isSuccess, new JsonSerializerSettings()
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
        //For AngularJS
        public ContentResult InitChangeRequest()
        {
            

            ChangeRequest crToJson= crService.InitNewChangeRequestRecord(User.Identity.Name);
            string json = JsonConvert.SerializeObject(crToJson, new JsonSerializerSettings()
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

        [HttpPost]
        public ActionResult AddChangeRequest(ChangeRequest crEntry)
        {
            crService.EntityToModifiedState(crEntry);
            crEntry.CreatedBy = User.Identity.Name;
            crEntry.CreateDate = DateTime.Now;
            crEntry.LastModifiedDate = DateTime.Now;

            crEntry.Owner = "test2@taipower.com.tw";//crService.GetReviewer();
            crEntry.ReviewBy = "test2@taipower.com.tw";
            if (ModelState.IsValid)
            {
                
                crEntry.Description = crEntry.Description;
                crService.AddChangeRequestEntry(crEntry);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditChangeRequest(Guid id)
        {
            var crEntry = crService.FindByChangeRequestId(id);
            if (crEntry == null)
            {
                return HttpNotFound();
            }

            return View(crEntry);
        }

        [HttpPost]
        public ActionResult EditChangeRequest(ChangeRequest crEntry)
        {
            var crToUpdate = crService.FindByChangeRequestId(crEntry.ChangeRequestId);
            if (crToUpdate == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                crToUpdate.Owner = crToUpdate.ReviewBy;
                crService.StatusUpdateForClarification(crToUpdate);
            }
            return RedirectToAction("Index");
        }


        public ActionResult CancelChangeRequest(Guid id)
        {
            var crEntry= crService.FindByChangeRequestId(id);
            if (crEntry == null)
            {
                return HttpNotFound();
            }

            return View(crEntry);

        }


        [HttpPost]
        public ActionResult CancelChangeRequest(ChangeRequest cr)
        {
            var crEntry = crService.FindByChangeRequestId(cr.ChangeRequestId);
            if (crEntry == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                crService.StatusUpdateForCancellation(crEntry);
            }

            return RedirectToAction("Index");

        }

        public ActionResult ReviewChangeRequest(Guid id)
        {
            var crEntry = crService.FindByChangeRequestId(id);
            if (crEntry == null)
            {
                return HttpNotFound();
            }

            return View(crEntry);
        }

        public ActionResult ApproveChangeRequest(Guid id)
        {
            var crEntry = crService.FindByChangeRequestId(id);
            if (crEntry == null)
            {
                return HttpNotFound();
            }
            
            crService.StatusUpdateForApproval(crEntry);
            return RedirectToAction("Index");
        }

        public ActionResult CommentChangeRequest(Guid id)
        {
            var crEntry = crService.FindByChangeRequestId(id);
            if (crEntry == null)
            {
                return HttpNotFound();
            }

            crEntry.Owner = crEntry.CreatedBy;
            crService.StatusUpdateForComment(crEntry);
            return RedirectToAction("Index");
        }
    }
}