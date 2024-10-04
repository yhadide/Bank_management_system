using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Models
{
    [Table("Accounts", Schema = "dbo")]
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public decimal Balance { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountType { get; set; } 

    }
}
