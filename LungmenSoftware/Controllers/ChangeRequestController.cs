using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Helper;
using LungmenSoftware.Models;
using LungmenSoftware.Models.CodeFirst.Entities;
using LungmenSoftware.Models.Service;
using LungmenSoftware.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace LungmenSoftware.Controllers
{
    
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

        //public ActionResult AddChangeRequest()
        //{
        //    ChangeRequest cr=new ChangeRequest();
        //    cr.ChangeRequestId = Guid.NewGuid();
           
        //    cr.SerialNumber = string.Format("{0:yyyyMMdd}", DateTime.Today) +"E0"+ new Random().Next(100, 999);

        //    //這邊以後可能要修掉
            
        //    //ViewBag.   
        //    return View(cr);
        //}

        public ActionResult GetInvChangeForm()
        {

            return View();
        }

        public ActionResult GetNumacChangeForm()
        {
            return View();
        }


        public ActionResult GetDrsChangeForm()
        {
           return View();
        }

        //For AngularJS
        public ContentResult InitChangeRequest()
        {
           
            ChangeRequest crToJson = crService.InitNewChangeRequestRecord(User.Identity.Name);
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

        //For AngularJS
        [HttpPost]
        public ContentResult AddNewChangeRequestRecord(ChangeRequest crEntry)
        {
            if (crEntry.CreatedBy == null)
            {
                crEntry.CreatedBy = "test2@taipower.com.tw";
            }
            crEntry.ReviewBy = crService.GetReviewer();
            crEntry.Owner = crEntry.ReviewBy;
            crEntry.IsActive = true;

            var newRecord=crService.AddChangeRequestRecord(crEntry);
            string json = JsonConvert.SerializeObject(newRecord, new JsonSerializerSettings()
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
        public ContentResult AddNumacChangeRequestRecord(ChangeRequest crEntry)
        {
            crEntry.ReviewBy = crService.GetReviewer();
            crEntry.Owner = crEntry.ReviewBy;
            crEntry.IsActive = true;
            if (crEntry.CreatedBy == null) crEntry.CreatedBy = "test2@taipower.com.tw";

            var newRecord = crService.AddNumacChangeRequestEntry(crEntry);
            string json = JsonConvert.SerializeObject(newRecord, new JsonSerializerSettings()
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

            crEntry.Owner = "test2@taipower.com.tw"; //crService.GetReviewer();
            crEntry.ReviewBy = "test2@taipower.com.tw";
            if (ModelState.IsValid)
            {
                
                crEntry.Description = crEntry.Description;
                crService.AddChangeRequestEntry(crEntry);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ChangeRequestDetail(Guid id)
        {
            var crEntry = crService.FindDetailCRByChangeRequestId(id);
            if (crEntry == null)
            {
                return HttpNotFound();
            }
            ChangeRequestViewModelForDetail dataForView=new ChangeRequestViewModelForDetail()
            {
                ChangeRequest = crEntry,
                ChangeDeltas = crEntry.ChangeDeltas,
                ChangeRequestMessages = crEntry.ChangeRequestMessages,
                ChangeRequestStatuses = crEntry.ChangeRequestStatuses,
                NumacChangeDeltas=crEntry.NumacChangeDeltas
            };
            return View(dataForView);
        }

        public ActionResult EditChangeRequest(Guid id)
        {
            var crEntry = crService.FindDetailCRByChangeRequestId(id);
            if (crEntry == null)
            {
                return HttpNotFound();
            }
            ChangeRequestViewModelForDetail dataForView=new ChangeRequestViewModelForDetail()
            {
                ChangeRequest = crEntry,
                ChangeDeltas = crEntry.ChangeDeltas,
                NumacChangeDeltas=crEntry.NumacChangeDeltas,
                ChangeRequestMessages = crEntry.ChangeRequestMessages
            };


            return View(dataForView);
        }

        [HttpPost]
        public ActionResult EditChangeRequest(ChangeRequestViewModelForDetail crEntry)
        {
            var crToUpdate = crService.FindByChangeRequestId(crEntry.ChangeRequest.ChangeRequestId);
            if (crToUpdate == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                ChangeRequestMessage crm = new ChangeRequestMessage()
                {
                    CreateBy = User.Identity.Name,
                    ChangeRequestId = crEntry.ChangeRequest.ChangeRequestId,
                    ChangeRequest = crToUpdate,
                    CreateTime = DateTime.Now,
                    Message = crEntry.ChangeRequestMessage.Message
                };
                crService.AddChangeRequestMessage(crm);

                crToUpdate.Description = crEntry.ChangeRequest.Description;
                crToUpdate.DesignDoc = crEntry.ChangeRequest.DesignDoc;
                crToUpdate.Note = crEntry.ChangeRequest.Note;
                crToUpdate.Owner = crToUpdate.ReviewBy;

                //crToUpdate.ChangeDeltas = crEntry.ChangeDeltas;
                if (crEntry.ChangeDeltas !=null)
                {
                    crService.UpdateChangeDeltas(crEntry.ChangeRequest.ChangeRequestId, crEntry.ChangeDeltas);
                }
                if (crEntry.NumacChangeDeltas != null)
                {
                    crService.UpdateNumacChangeDeltas(crEntry.ChangeRequest.ChangeRequestId, crEntry.NumacChangeDeltas);
                }

                crService.StatusUpdateForClarification(crToUpdate);
            }
            return RedirectToAction("Index");
        }


        public ActionResult CancelChangeRequest(Guid id)
        {
            var crEntry = crService.FindByChangeRequestId(id);
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
            var crEntry = crService.FindDetailCRByChangeRequestId(id);
            if (crEntry == null)
            {
                return HttpNotFound();
            }

            ChangeRequestViewModelForDetail vm=new ChangeRequestViewModelForDetail()
            {
                ChangeRequest = crEntry,
                ChangeDeltas = crEntry.ChangeDeltas,
                ChangeRequestMessages = crEntry.ChangeRequestMessages,
                NumacChangeDeltas=crEntry.NumacChangeDeltas
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult ReviewChangeRequest(ChangeRequestViewModelForDetail vm, 
                                    params string[] reviewOptions)
        {
            var crEntry = crService.FindDetailCRByChangeRequestId(vm.ChangeRequest.ChangeRequestId);

            if (crEntry == null)
            {
                return HttpNotFound();
            }
            
            var option = reviewOptions[0];
            if (option.Equals(GeneralData.GetReviewOptions[1]))
            {
                string approver = User.Identity.Name;
                ChangeRequestMessage crm=new ChangeRequestMessage()
                {
                    CreateBy = User.Identity.Name,
                    ChangeRequestId = vm.ChangeRequest.ChangeRequestId,
                    ChangeRequest = crEntry,
                    CreateTime = DateTime.Now,
                    Message = vm.ChangeRequestMessage.Message
                };
                crService.AddChangeRequestMessage(crm);
                bool isUpdated = crService.UpdateRevPerChangeRequest(crEntry);
                if (isUpdated)
                {
                    crService.StatusUpdateForApproval(crEntry, approver);
                }

                return RedirectToAction("Index");
            }
            if (option.Equals(GeneralData.GetReviewOptions[2]))
            {
                ChangeRequestMessage crm = new ChangeRequestMessage()
                {
                    CreateBy = User.Identity.Name,
                    ChangeRequestId = vm.ChangeRequest.ChangeRequestId,
                    ChangeRequest = crEntry,
                    CreateTime = DateTime.Now,
                    Message = vm.ChangeRequestMessage.Message
                };
                crService.AddChangeRequestMessage(crm);
                crEntry.Owner = crEntry.CreatedBy;
                crService.StatusUpdateForComment(crEntry);

                return RedirectToAction("Index");
            }

            ChangeRequestViewModelForDetail oldData = new ChangeRequestViewModelForDetail()
            {
                ChangeDeltas = crEntry.ChangeDeltas,
                ChangeRequest = crEntry,
                ChangeRequestMessages = crEntry.ChangeRequestMessages,
                NumacChangeDeltas=crEntry.NumacChangeDeltas
            };

            ViewBag.Error = "沒有選取審查選項";
            return View(oldData);
           
        }


        //public void CommentChangeRequest(ChangeRequest crEntry)
        //{
           
        //}

        //public void ApproveChangeRequest(ChangeRequest crEntry)
        //{
            
        //}
        public ActionResult SearchChangeRequest()
        {
            var statuses = GetStatusCheckBoxList();

            ChangeRequestSearchViewModel vm=new ChangeRequestSearchViewModel()
            {
                Status = statuses
                
            };

            return View(vm);
        }

        private List<CheckBoxListModel> GetStatusCheckBoxList()
        {

            List<CheckBoxListModel> result = crService.GetChangeRequestStatusTypessForCheckBox();
            return result;
        }

        [HttpPost]
        public ActionResult SearchChangeRequest(ChangeRequestSearchViewModel vm)
        {

            List<ChangeRequestInfo> results= crService.SearchChangeRequestsByForm(vm);

            ChangeRequestSearchViewModel dataForView = new ChangeRequestSearchViewModel()
            {
                Status = GetStatusCheckBoxList(),
                SearchResult = results
            };

            return View(dataForView);
        }


        //For AngularJS Form
        public PartialViewResult GetAngularForm()
        {

            return PartialView("_GetAngularForm");
        }

        //For AngularJS Form
        public PartialViewResult ConfirmFormData()
        {
            return PartialView("_ConfirmFormData");
        }

        public ActionResult DeleteChangeRequest(Guid id)
        {
            var cr = crService.FindByChangeRequestId(id);
            if (cr == null)
            {
                return HttpNotFound();
            }
            return View(cr);
        }
        [HttpPost]
        public ActionResult DeleteChangeRequest(ChangeRequest cr)
        {
            var r = crService.FindByChangeRequestId(cr.ChangeRequestId);
            if (r == null)
            {
                return HttpNotFound();
            }
            crService.DeleteChangeRequest(r);
            return RedirectToAction("Index");
        }

        
    }
}