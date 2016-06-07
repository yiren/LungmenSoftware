﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungmenSoftware.Models.NUMACFirmware
{
    public class Chassis
    {
        [Key]
        public Guid ChassisId { get; set; }

        public string ChassisName { get; set; }

        public string ChasisSerialNumber{ get; set; }

        public string Equipment { get; set; }

        public string Panel { get; set; }

        public IEnumerable<ChassisBoard> ChassBoards { get; set; }

    }
}
