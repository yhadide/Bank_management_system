using BankManagementSystem.Models;
using BankManagementSystem.Repositories;
using BankManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagementSystem
{
    public partial class AccountForm : Form
    {
        private int accountId;

        public AccountForm(int accountId)
        {
            InitializeComponent();
            this.accountId = accountId;
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            RefreshAccountDetails();
        }

        private void RefreshAccountDetails()
        {
            try
            {
                using (var context = new BankContext())
                {
                    var accountRepository = new Repository<Account>(context);

                    var account = accountRepository.GetById(accountId);
                    if (account != null)
                    {
                        welcomeLbl.Text = $"Welcome, {account.Customer.FirstName} {account.Customer.LastName}!";
                        lblBalance.Text = account.Balance.ToString();
                        lblAccountType.Text = account.AccountType;
                    }
                var transactions = context.Transactions
                        .Include(t => t.Account)
                        .Where(t => t.AccountId == accountId)
                        .OrderByDescending(t => t.TransactionDate)
                        .ToList();

                    var bindingList = new BindingList<Transaction>(transactions);
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.Columns.Clear();

                    var transactionIdColumn = new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "TransactionId",
                        Name = "TransactionId",
                        HeaderText = "Transaction ID"
                    };
                    dataGridView1.Columns.Add(transactionIdColumn);

                    var amountColumn = new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Amount",
                        Name = "Amount",
                        HeaderText = "Amount"
                    };
                    dataGridView1.Columns.Add(amountColumn);

                    var transactionTypeColumn = new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "TransactionType",
                        Name = "TransactionType",
                        HeaderText = "Transaction Type"
                    };
                    dataGridView1.Columns.Add(transactionTypeColumn);

                    var transactionDateColumn = new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "TransactionDate",
                        Name = "TransactionDate",
                        HeaderText = "Transaction Date"
                    };
                    dataGridView1.Columns.Add(transactionDateColumn);

                    dataGridView1.DataSource = bindingList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeposit_Click_1(object sender, EventArgs e)
        {
            using (var depositForm = new DepositForm(accountId))
            {
                depositForm.ShowDialog();
            }
            RefreshAccountDetails();
        }

        private void btnWithdraw_Click_1(object sender, EventArgs e)
        {
            using (var withdrawalForm = new WithdrawForm(accountId))
            {
                withdrawalForm.ShowDialog();
            }
            RefreshAccountDetails();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            using (var context = new BankContext())
            {
                var accountRepository = new Repository<Account>(context);
                var account = accountRepository.GetById(accountId);
                var customerhub = new CustomerHub(account.CustomerId);
                customerhub.Show();
                this.Hide();
            }
        }
    }
}
