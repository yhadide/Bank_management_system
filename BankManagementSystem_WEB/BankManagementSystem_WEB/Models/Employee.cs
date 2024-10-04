using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankManagementSystem_WEB.Models
{
    [Table("Employees")]
    public class Employee : Person
    {
        [StringLength(50)]
        public string Position { get; set; }

        [Required]
        [StringLength(50)]  
        public string Password { get; set; }

    }
}