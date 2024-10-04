using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankManagementSystem_WEB.Models
{
    [Table("Transactions", Schema = "dbo")]
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}