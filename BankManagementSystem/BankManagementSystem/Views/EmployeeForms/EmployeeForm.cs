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

namespace BankManagementSystem.Views
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            BindingList<Account> accountsBindingList;
            BindingList<Customer> customersBindingList;

            using (var context = new BankContext())
            {
                var accRepository = new Repository<Account>(context);
                var custsRepository = new Repository<Customer>(context);
                var accounts = accRepository.GetAllIncluding(a => a.Customer).ToList();
                var customers = custsRepository.GetAllIncluding(c => c.Accounts).ToList();
                accountsBindingList = new BindingList<Account>(accounts);
                customersBindingList = new BindingList<Customer>(customers);
            }

            // Account details
            AccDataGridView.AutoGenerateColumns = false;
            AccDataGridView.Columns.Clear();
            AccDataGridView.Rows.Clear();
            AccDataGridView.ReadOnly = true;
            AccDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AccountId",
                Name = "AccountId",
                HeaderText = "Account ID"
            });
            AccDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AccountType",
                Name = "AccountType",
                HeaderText = "Account Type"
            });
            AccDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Balance",
                Name = "Balance",
                HeaderText = "Balance"
            });
            // Create a new DataGridViewButtonColumn
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
            {
                Name = "DeleteButtonColumn",
                HeaderText = "",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            };

            // Add the DataGridViewButtonColumn to the DataGridView
            AccDataGridView.Columns.Add(deleteButtonColumn);

            

            //Customer details
            custsDataGridView.AutoGenerateColumns = false;
            custsDataGridView.Rows.Clear();
            custsDataGridView.Columns.Clear();
            custsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                Name = "Id",
                HeaderText = "Customer ID"
            });
            custsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FirstName",
                Name = "FirstName",
                HeaderText = "First Name"
            });
            custsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LastName",
                Name = "LastName",
                HeaderText = "Last Name"
            });
            custsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AccountIds",
                Name = "AccountIds",
                HeaderText = "Account IDs",
                DefaultCellStyle = new DataGridViewCellStyle { NullValue = null },
                ValueType = typeof(string),
                CellTemplate = new DataGridViewTextBoxCell { ValueType = typeof(string) }
            });

            AccDataGridView.DataSource = accountsBindingList;
            custsDataGridView.DataSource = customersBindingList;

        }

        private void AccDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (AccDataGridView.Columns[e.ColumnIndex].Name == "DeleteButtonColumn")
                {
                    int accountId = (int)AccDataGridView.Rows[e.RowIndex].Cells["AccountId"].Value;
                    using (var context = new BankContext())
                    {
                        var accountRepository = new Repository<Account>(context);
                        accountRepository.Delete(accountId);
                    }
                    RefreshAccountDetails();
                }
                else
                {
                    var accountTypes = new List<string> { "Savings", "Checking", "Business" };
                    comboBoxAccType.DataSource = accountTypes;
                    DataGridViewRow row = this.AccDataGridView.Rows[e.RowIndex];
                    accountIdTextBox.Text = row.Cells["AccountId"].Value.ToString();
                    comboBoxAccType.Text = row.Cells["AccountType"].Value.ToString();
                    balanceTextBox.ReadOnly = true;
                    balanceTextBox.Text = row.Cells["Balance"].Value.ToString();
                }
            }
        }
        private void RefreshAccountDetails()
        {
            using (var context = new BankContext())
            {
                var accounts = context.Accounts.ToList();
                var customers = context.Customers.ToList();
                AccDataGridView.DataSource = accounts;
                custsDataGridView.DataSource = customers;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            accountIdTextBox.Text = string.Empty;
            comboBoxAccType.Text = string.Empty;
            balanceTextBox.Text = string.Empty;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (var context = new BankContext())
            {
                var accountRepository = new Repository<Account>(context);

                int accountId = int.Parse(accountIdTextBox.Text);
                var account = accountRepository.GetById(accountId);
                if (account != null)
                {
                    account.AccountType = comboBoxAccType.Text;
                    account.Balance = decimal.Parse(balanceTextBox.Text);
                    accountRepository.Update(account);
                    RefreshAccountDetails();
                }
                else
                {
                    MessageBox.Show("Account not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNewAcc_Click(object sender, EventArgs e)
        {
            var newAccountForm = new RegisterForm();
            newAccountForm.ShowDialog();

            RefreshAccountDetails();
        }

        private void btnNewAccBk_Click(object sender, EventArgs e)
        {
            var newAccountRegisterForm = new newAccountRegisterForm();
            newAccountRegisterForm.ShowDialog();
            RefreshAccountDetails();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            var login = new Login();
            login.Show();
            this.Hide();
        }

    }
}
