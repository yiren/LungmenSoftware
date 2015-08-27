using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware;
using LungmenSoftware.Models;
using LungmenSoftware.Models.ViewModel;
using LungmenSoftware.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebGrease.Css.Extensions;

namespace LungmenSoftware.Controllers
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
            if (User.Identity.IsAuthenticated && (User.IsInRole("Admin")||User.IsInRole("Reviewer")))
            {
                return RedirectToAction("ListOfUsers");
            }
            else if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserDetail",new {Id=User.Identity.GetUserId()});
            }
            else
            {
                return RedirectToAction("Register", "Account");
            }
        }

        
        public ActionResult ListOfUsers()
        {
            UserView dataForview=new UserView();
            
            var users = UserManager.Users.ToList();

            dataForview.Users = users;

            foreach (var user in users)
            {
                foreach (var role in user.Roles) //拿到所有role id
                {
                    string roleName=RoleManager.FindById(role.RoleId).Name;
                }    
            }
            //var query = from users in UserManager.Users
            //            join roles in RoleManager.Roles
            //             on users.Roles
            

            return View(dataForview);
        }

        
        public ActionResult UserDetail(string id)
        {
            return View(UserManager.FindById(id));
        }

        
        public ActionResult EditUser(string id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var roles = GetRolesFromUserId(id);
            ViewBag.RoleListForUser= RoleManager.Roles.ToList().Select(r => new SelectListItem()
                    {
                        Selected = roles.Contains(r.Name),
                        Text = r.Name,
                        Value = r.Name
                    }
                );
            
           
            return View(user);
        }

        private List<String> GetRolesFromUserId(string id)
        {
            return UserManager.GetRoles(id).ToList();
        }

        [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult EditUser(//注意傳遞的Model物件是否有包含需驗證欄位
            ApplicationUser userToUpdate, params string[] selectedRole)
        {
            var user = UserManager.FindById(userToUpdate.Id);
            var userRoles = UserManager.GetRoles(userToUpdate.Id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //要Update的屬性

            //userService.UpdateApplicationUser(userToUpdate);

            selectedRole = selectedRole ?? new string[] {};
            //var role=RoleManager.FindByName(selectedRole);

            

            if (ModelState.IsValid)
            {
                //userService.UpdateApplicationUser(userToUpdate);
                user.Department = userToUpdate.Department;
                user.EmployeeName = userToUpdate.EmployeeName;
                user.IsDisabled = userToUpdate.IsDisabled;
                foreach (var role in selectedRole.Except(userRoles))
                {
                    var result = UserManager.AddToRole(user.Id, role);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                    }
                }

                foreach (var role in userRoles.Except(selectedRole))
                {
                    var result = UserManager.RemoveFromRole(user.Id, role);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                    }
                }

                var Valid = UserManager.Update(user);
                if (Valid.Succeeded)
                {
                    return RedirectToAction("ListOfUsers");
                }
                else
                {
                    foreach (var item in Valid.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }
                }

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