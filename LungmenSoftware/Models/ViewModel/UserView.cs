using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LungmenSoftware.Models;

namespace IdentityPractice.Models
{
    public class UserView
    {
        public ApplicationUser UserInfo { get; set; }

        public List<ApplicationUser> Users { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必須至少 {2} 六位英文與數字的組合.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "兩次密碼輸入不一致")]
        public string ConfirmPassword { get; set; }

    }
}