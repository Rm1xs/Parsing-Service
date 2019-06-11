using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.Models
{
    public class iPerf3
    {
        [Key]
        public int PerfId { get; set; }
        public string Server { get; set; }
        public int Speed { get; set; }
        public string Port { get; set; }
        public string IPVersion { get; set; }
        public string Hosting { get; set; }
        [DisplayName("Last Update")]
        public DateTime DateTime { get; set; }
        public string Site { get; set; }
    }
}
