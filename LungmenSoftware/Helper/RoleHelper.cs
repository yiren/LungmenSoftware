using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LungmenSoftware;
using LungmenSoftware.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LungmenSoftware.Helper
{
    public static class RoleHelper
    {
        public static void AddToUserRole
            (this ApplicationUserManager applicationUserManager,
                string userId,
                string roleName)
        {
            var roleManager=new RoleManager<ApplicationRole>(
                new RoleStore<ApplicationRole>(new ApplicationDbContext()));
            if (roleManager.RoleExists(roleName)==false)
            {
                roleManager.Create(new ApplicationRole(roleName));
            }
            applicationUserManager.AddToRole(userId, roleName);
        }
    }
}