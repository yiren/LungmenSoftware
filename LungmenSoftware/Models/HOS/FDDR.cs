using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.HOS
{
    public class FDDR
    {
        public int FddrId { get; set; }
        public string FddrNo { get; set; }
        public int FddrRev { get; set; }
        public List<FddrToFdi> FddrToFdis { get; set; }
    }
}