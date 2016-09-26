using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LungmenSoftware.Models;

namespace LungmenSoftware.Helper
{
    public static class GeneralData
    {
        public static ApplicationUser CurrentUser { get; set; }
           
        public static List<string> GetDeparments = new List<string>()
        {
            "核技處",
            "龍門電廠",
            "核發處",
            "核安處"
        };

        public static List<string> GetReviewOptions =new List<string>()
        {
            "請選擇審查選項",
            "同意",
            "審查有意見，退回修改"
        }; 
    }
}