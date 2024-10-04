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
    public partial class WithdrawForm : Form
    {
        private int accountId;
        public WithdrawForm(int accountId)
        {
            InitializeComponent();
            panel1.Anchor = AnchorStyles.None;
            this.Size = new Size(312, 312);
            this.accountId = accountId;
        }

        private void btnSubmitWithdraw_Click(object sender, EventArgs e)
        {
            decimal withdrawalAmount;
            if (decimal.TryParse(txtWithdrawAmount.Text, out withdrawalAmount) && withdrawalAmount > 0)
            {
                using (var context = new BankContext())
                {
                    var accountRepository = new Repository<Account>(context);
                    var transactionRepository = new Repository<Transaction>(context);

                    var account = accountRepository.GetById(accountId);
                    if (account != null)
                    {
                        if (account.Balance >= withdrawalAmount)
                        {
                            account.Balance -= withdrawalAmount;

                            var transaction = new Transaction
                            {
                                AccountId = accountId,
                                Amount = withdrawalAmount,
                                TransactionType = "Withdrawal",
                                TransactionDate = DateTime.Now
                            };
                            transactionRepository.Insert(transaction);

                            MessageBox.Show("Withdrawal successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Insufficient balance.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid withdrawal amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }
