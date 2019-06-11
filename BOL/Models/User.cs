using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOL.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required(ErrorMessage = "Enter Email!")]
        [EmailAddress(ErrorMessage = "Enter valid Email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Password!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}
