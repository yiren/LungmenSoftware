using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LungmenSoftware.Models
{
    //IdentityUser對應AspNetUsers
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        //[Display(Name="帳號")]
        //public string UserName { get; set; }

        [Display(Name = "帳戶是否已停用")]
        public bool IsDisabled { get; set; }

        [Required]
        [StringLength(9, ErrorMessage = "姓名代號長度錯誤，請輸入正確姓名代號", MinimumLength = 2)]
        [Display(Name = "姓名代號")]
        public string TPCId { get; set; }

        [Required]
        [Display(Name = "員工姓名")]
        [StringLength(100)]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "單位")]
        [StringLength(50)]
        public string Department { get; set; }

        [Required]
        [Display(Name="建立時間")]
        public DateTime CreateDate { get; set; }

        [Required]
        [Display(Name = "最後修改時間")]
        public DateTime LastModifiedDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            
            return userIdentity;
        }
    }

    //IdentityUserRole對應AspNetRoleUsers JoinTable
    public class ApplicationUserRole : IdentityUserRole
    {
           
    }

    //IdentityRole對應AspNetRoles
    public class ApplicationRole : IdentityRole
    {
        [StringLength(100)]
        public string Description { get; set; }
        public ApplicationRole()
            :base()
        {
            
        }

        public ApplicationRole(string roleName)
            :base(roleName)
        {
            
        }
    }

    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ASPIdentity", throwIfV1Schema: false)
        {

        }



        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<LungmenSoftware.Models.ApplicationRole> IdentityRoles { get; set; }

        //public System.Data.Entity.DbSet<LungmenSoftware.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<IdentityPractice.Models.ApplicationUser> ApplicationUsers { get; }
    }
}