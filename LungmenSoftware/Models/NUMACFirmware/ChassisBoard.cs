using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LungmenSoftware.Models.NUMACFirmware
{
    [Bind(Exclude = "ChassisBoardId, LastModifiedBy, LastModifiedDate")]
    public class ChassisBoard
    {
        [Key]
        public Guid ChassisBoardId { get; set; }

        public string ChassBoardName { get; set; }

        public string ChassisName { get; set; }

        public Guid ChassisId { get; set; }

        public Chassis Chassis { get; set; }
        public string LastModifiedBy { get; set; }

        public string LastModifiedDate { get; set; }

        public IEnumerable<EPROM> EPROMs { get; set; }

    }
}