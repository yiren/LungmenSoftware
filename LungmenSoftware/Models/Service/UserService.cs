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


        public List<ApplicationUser> GetApplicationUsers()
        {
            return db.Users.ToList();
        }

        public void GetUserRoles()
        {
            //var query=from users in db.Users                      
            //    join role in db.Roles on users. equals EXPR2
            
            
        }

        public void DeleteApplicationUser(string id)
        {
             
            var record=db.Users.Find(id);
            db.Users.Remove(record);
            db.SaveChanges();

        }

        public void UpdateApplicationUser(ApplicationUser user)
        {
            //var userToUpdate = db.Users.Find(user.Id);
            //Where(u=>u.UserName.Equals(user.UserName)).FirstOrDefault();
            //userToUpdate = user;
            user.LastModifiedDate = DateTime.Now;
       //     db.Entry(userToUpdate).State=EntityState.Modified;
            db.SaveChanges();

        }

        public ApplicationUser FindApplicationUserById(string userId)
        {
            return db.Users.Find(userId);
        }
    }
}