using BankManagementSystem_WEB.Models;
using System.Collections.Generic;

namespace BankManagementSystem_WEB.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
