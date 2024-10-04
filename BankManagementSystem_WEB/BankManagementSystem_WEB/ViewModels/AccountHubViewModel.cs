using BankManagementSystem_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManagementSystem_WEB.ViewModels
{
    public class AccountHubViewModel
    {
        public Account Account { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
