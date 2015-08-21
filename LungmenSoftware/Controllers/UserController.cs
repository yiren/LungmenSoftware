using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentityPractice.Models;
using IdentityPractice.Service;
using LungmenSoftware;
using LungmenSoftware.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebGrease.Css.Extensions;

namespace IdentityPractice.Controllers
{
    [Authorize]
    public class UserController : Controller
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

        private IUserService userService;

        public UserController()
        {
            this.userService=new UserService();
        }
        // GET: User
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                return RedirectToAction("ListOfUsers");
            }
            else if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserDetail");
            }
            else
            {
                return RedirectToAction("Register", "Account");
            }
        }

        
        public ActionResult ListOfUsers()
        {
            UserView dataForview=new UserView();
            dataForview.Users = UserManager.Users.ToList();
            return View(dataForview);
        }

        
        public ActionResult UserDetail()
        {
            throw new NotImplementedException();
        }

        
        public ActionResult EditUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var record = userService.FindApplicationUserById(id);
            if (record==null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

       [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult PostToUpdate(//注意傳遞的Model物件是否有包含需驗證欄位
            ApplicationUser userToUpdate)
        {
            var user = userService.FindApplicationUserById(userToUpdate.Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //要Update的屬性
            user.Department = userToUpdate.Department;
            user.EmployeeName = userToUpdate.EmployeeName;
           user.IsDisabled = userToUpdate.IsDisabled;
            
            //userService.UpdateApplicationUser(userToUpdate);
            
            if (ModelState.IsValid)
            {
                //userService.UpdateApplicationUser(userToUpdate);
                
                userService.UpdateApplicationUser(user);

            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                    .Where(y => y.Count > 0)
                    .ToList();

                return Content(errors.ToString());
            }
            
            return RedirectToAction("ListOfUsers");
        }

        public ActionResult DeleteUser(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            userService.DeleteApplicationUser(id);
            return RedirectToAction("ListOfUsers");
        }
    }
}