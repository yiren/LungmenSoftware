using System;

namespace LungmenSoftware.Models.ViewModel
{
    public class FirmwareViewModel
    {
        public Guid ChassisId { get; set; }

        public Guid EPROMId { get; set; }

        public Guid ChassisBoardId { get; set; }

        public string ChassisName { get; set; }

        public string Panel { get; set; }


        public string ChassisBoardName { get; set; }

        public string SocketLocation { get; set; }

        public string Assembly { get; set; }

        public string SerialNumber { get; set; }

        public string Program{ get; set; }

        public int Rev { get; set; }


    }
    public class FirmwareViewModelV2
    {
        public Guid ChassisId { get; set; }

        public Guid EPROMId { get; set; }

        public Guid ChassisBoardId { get; set; }

        public string ChassisName { get; set; }

        public string Panel { get; set; }

        public string SubSystem { get; set; }

        public string ChassisBoardName { get; set; }

        public string SocketLocation { get; set; }

        public string Assembly { get; set; }

        public string SerialNumber { get; set; }

        public string Program { get; set; }

        public string Rev { get; set; }


    }
}