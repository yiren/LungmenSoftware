using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungmenSoftware.Models;

namespace LungmenSoftware.Service
{
    interface IUserService
    {
        List<ApplicationUser> GetApplicationUsers();
        void DeleteApplicationUser(string userEmail);
        void UpdateApplicationUser(ApplicationUser user);
        ApplicationUser FindApplicationUserById(string userId);


    }
}
