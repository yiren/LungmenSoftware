using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class RevInfo
    {
        public int RevInfoId { get; set; }

        public long? JoinTableId { get; set; }

        public int? FoxWorkStationId { get; set; }

        public int? FoxSoftwareId { get; set; }

        public string WorkStationName { get; set; }

        public string SoftwareName { get; set; }

        public string Rev { get; set; }

        public string Procedure { get; set; }

        public string Software_Library_Identification { get; set; }

        public string Media_Identification { get; set; }

        public string Note { get; set; }

        public ChangeDelta ChangeDelta { get; set; }

        public Guid ChangeDeltaId { get; set; }
        
    }
}