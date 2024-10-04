using BankManagementSystem.Models;
using BankManagementSystem.Repositories;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BankManagementSystem.Views
{
    public partial class CustomerHub : Form
    {
        private int customerId;
        private Repository<Customer> customerRepository;
        private Repository<Account> accountRepository;
        public CustomerHub(int customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
            var context = new BankContext();
            customerRepository = new Repository<Customer>(context);
            accountRepository = new Repository<Account>(context);
        }

        private void CustomerHub_Load(object sender, EventArgs e)
        {
            DisplayCustomerInfo(customerId);
        }
        public void DisplayCustomerInfo(int customerId)
        {
            var customer = customerRepository.GetById(customerId);

            if (customer == null)
            {
                idLbl.Text = "Customer not found.";
                return;
            }

            idLbl.Text = $"{customer.Id}";
            fnLbl.Text = $"{customer.FirstName}";
            lnLbl.Text = $"{customer.LastName}";
            adLbl.Text = $"{customer.Address}";
            emLbl.Text = $"{customer.Email}";
            phLbl.Text = $"{customer.Phone}";

            var accounts = accountRepository.GetAllIncluding(a => a.Customer)
                .Where(a => a.Customer.Id == customerId)
                .ToList();

            if (accounts.Any())
            {

                linkLabelAcc.Text = "";
                foreach (var account in accounts)
                {
                    string linkText = $"Account ID: {account.AccountId}, Balance: {account.Balance}\n";
                    linkLabelAcc.Text += linkText;
                    linkLabelAcc.Links.Add(linkLabelAcc.Text.Length - linkText.Length, linkText.Length, account.AccountId);
                }

                linkLabelAcc.LinkClicked += (sender, e) =>
                {
                    var accountForm = new AccountForm((int)e.Link.LinkData);
                    accountForm.Show();
                    this.Hide();
                };
            }
            else
            {
                linkLabelAcc.Text = "You don't have an account yet. Please contact an employee to make you one.";
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            var login = new Login();
            login.Show();
            this.Hide();
            this.Dispose();
        }
    }
}
