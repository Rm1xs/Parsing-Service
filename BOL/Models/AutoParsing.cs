using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOL.Models
{
    public class AutoParsing
    {
        [Key]
        public int AutoId { get; set; }
        public string CustomeURL { get; set; }
        public string ParamServer { get; set; }
        public int ParamServerId { get; set; }
        public int ParamIp { get; set; }
        public int ParamSpeed { get; set; }
        public int ParamHosting { get; set; }
        public int ParamPort { get; set; }
        public DateTime DateTime { get; set; }
    }
}
