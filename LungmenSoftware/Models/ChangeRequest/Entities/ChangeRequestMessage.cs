using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LungmenSoftware.Models.CodeFirst.Entities
{
    public class ChangeRequestMessage
    {
        public int ChangeRequestMessageId { get; set; }

        [DisplayName("意見輸入")]
        public string Message { get; set; }

        [DisplayName("回覆人員")]
        public string CreateBy { get; set; }

        [DisplayName("時間")]
        public DateTime CreateTime { get; set; }

        public Guid ChangeRequestId { get; set; }

        public ChangeRequest ChangeRequest { get; set; }

    }
}