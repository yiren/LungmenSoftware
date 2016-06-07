﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LungmenSoftware.Models.NUMACFirmware
{
    public class ChassisBoard
    {
        [Key]
        public Guid ChassisBoardId { get; set; }

        public string ChassBoardName { get; set; }

        public Guid ChassisId { get; set; }

        public Chassis Chassis { get; set; }

        public IEnumerable<EPROM> EPROMs { get; set; }

    }
}