using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LungmenSoftware.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace LungmenSoftware.Service
{
    public class UserService:Controller, IUserService
    {
        private ApplicationDbContext db=new ApplicationDbContext();


       
    }
}