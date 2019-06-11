using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOL.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [DisplayName("Role")]
        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
