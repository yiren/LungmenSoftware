using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LungmenSoftware.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityPractice.Models
{
    public class RoleView
    {
        public ApplicationRole RoleInfo { get; set; }

        public List<IdentityRole> Roles { get; set; }
    }
}