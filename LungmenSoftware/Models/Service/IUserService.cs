using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityPractice.Models;
using LungmenSoftware.Models;

namespace IdentityPractice.Service
{
    interface IUserService
    {
        List<ApplicationUser> GetApplicationUsers();
        void DeleteApplicationUser(string userEmail);
        void UpdateApplicationUser(ApplicationUser user);
        ApplicationUser FindApplicationUserById(string userId);


    }
}
