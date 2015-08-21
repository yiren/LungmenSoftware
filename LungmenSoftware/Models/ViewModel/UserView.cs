using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LungmenSoftware.Models.ViewModel
{
    public class UserView
    {
        public ApplicationUser UserInfo { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public List<IdentityRole> Roles { get; set; }  

        public IEnumerable<SelectListItem> RolesFromUserId { get; set; }

    }
}