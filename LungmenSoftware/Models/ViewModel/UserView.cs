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

        public List<UserAttriForList> UsersForList { get; set; }

        public IEnumerable<SelectListItem> RolesFromUserId { get; set; }

    }

    public class UserAttriForList
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public string TPCId { get; set; }

        public string Department { get; set; }

        public string EmployeeName { get; set; }

        public bool IsDisabled { get; set; }
    }
}