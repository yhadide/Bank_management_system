using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankManagementSystem_WEB.Models
{
    [Table("Customers")]
    public class Customer : Person
    {
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }
        public virtual List<Account> Accounts { get; set; }
        public string AccountIds
        {
            get
            {
                return Accounts == null ? string.Empty : string.Join(", ", Accounts.Select(a => a.AccountId));
            }
        }
    }

}