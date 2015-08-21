using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace LungmenSoftware.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
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
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListOfRoles()
        {

            var roles = RoleManager.Roles.ToList();
            return View(roles);
        }


        public ActionResult EditRole(string id)
        {
            var role = RoleManager.FindById(id);
            return View(role);
        }

        [HttpPost]
        public ActionResult EditRole(ApplicationRole roleToUpdate)
        {
            var role = RoleManager.FindById(roleToUpdate.Id);
            if (role == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                RoleManager.Update(roleToUpdate);
            }

            return RedirectToAction("ListOfRoles");
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(IdentityRole roleToInsert)
        {
            if (ModelState.IsValid)
            {
                var role= new IdentityRole(roleToInsert.Name);
                RoleManager.Create(role);
            }
            return RedirectToAction("ListOfRoles");
        }
    }
}