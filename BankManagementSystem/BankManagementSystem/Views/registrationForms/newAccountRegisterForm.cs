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
    public partial class newAccountRegisterForm : Form
    {
        public newAccountRegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (var context = new BankContext())
            {
                var accountRepository = new Repository<Account>(context);

                var account = new Account
                {
                    CustomerId = int.Parse(comboBoxCustId.Text),
                    AccountType = comboBoxAccType.Text,
                    Balance = 0
                };

                accountRepository.Insert(account);
                MessageBox.Show("Account created successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();
        }

        private void newAccountRegisterForm_Load(object sender, EventArgs e)
        {
            using (var context = new BankContext())
            {
                var custsRepository = new Repository<Customer>(context);
                var customers = custsRepository.GetAll().ToList();

                comboBoxCustId.DataSource = customers;
                comboBoxCustId.DisplayMember = "Id";
                comboBoxCustId.ValueMember = "Id";
            }
            var accountTypes = new List<string> { "Savings", "Checking", "Business" };
            comboBoxAccType.DataSource = accountTypes;
        }
    }
}
