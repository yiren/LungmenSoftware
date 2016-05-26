using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.HOS
{
    //JoinTable
    public class FddrToFdi
    {
        public int FddrToFdiId { get; set; }
        public int FddrId { get; set; }
        public int FdiId { get; set; }
        public FDDR Fddr { get; set; }
        public FDI fdi { get; set; }
    }
}