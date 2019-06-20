using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BOL.Models
{
    public class iPerf3
    {
        [Key]
        public int PerfId { get; set; }
        [Required]
        public string Server { get; set; }
        [Required]
        public int Speed { get; set; }
        [Required]
        public string IPVersion { get; set; }
        [Required]
        public string Hosting { get; set; }
        [DisplayName("Last Update")]
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string Site { get; set; }
        [Required]
        public string[] Port { get; set; }
    }
}
