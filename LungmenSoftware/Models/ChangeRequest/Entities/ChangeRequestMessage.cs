using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class ChangeRequestMessage
    {
        public int ChangeRequestMessageId { get; set; }

        public string Message { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public Guid ChangeRequestId { get; set; }

        public ChangeRequest ChangeRequest { get; set; }

    }
}