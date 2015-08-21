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
            var roleManager=new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            if (roleManager.RoleExists(roleName)==false)
            {
                roleManager.Create(new IdentityRole(roleName));
            }
            applicationUserManager.AddToRole(userId, roleName);
        }
    }
}