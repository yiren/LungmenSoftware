using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.HOS
{
    public class FDI
    {
        public int FdiId { get; set; }
        public string FdiNo { get; set; }
        public int FdiRev { get; set; }
        public string Status { get; set; }
        public string IssuedLetter { get; set; }
        public string IssuedDate { get; set; }
        public string CompletedLetter { get; set; }
        public string CompletedDate { get; set; }
        public List<FddrToFdi> FddrToFdis { get; set; }
        public float RequestedManHour { get; set; }
        public int InvoiceYear { get; set; }
        public string Quarter { get; set; }
        public string GETPLetter { get; set; }
        public string InvoiceNo { get; set; }
        public bool IsNewScope { get; set; }




    }
}