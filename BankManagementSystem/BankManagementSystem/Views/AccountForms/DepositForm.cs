using BankManagementSystem.Models;
using BankManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagementSystem
{
    public partial class DepositForm : Form
    {
        private int accountId;

        public DepositForm(int accountId)
        {
            InitializeComponent();
            panel1.Anchor = AnchorStyles.None;
            this.Size = new Size(312, 312);
            this.accountId = accountId;
        }
        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            decimal depositAmount;
            if (decimal.TryParse(txtDepositAmount.Text, out depositAmount) && depositAmount > 0)
            {
                using (var context = new BankContext())
                {
                    var accountRepository = new Repository<Account>(context);
                    var transactionRepository = new Repository<Transaction>(context);

                    var account = accountRepository.GetById(accountId);
                    if (account != null)
                    {
                        account.Balance += depositAmount;
                        var transaction = new Transaction
                        {
                            AccountId = accountId,
                            Amount = depositAmount,
                            TransactionType = "Deposit",
                            TransactionDate = DateTime.Now
                        };
                        transactionRepository.Insert(transaction);
                        MessageBox.Show("Deposit successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid deposit amount.");
            }
        }

    }
}
