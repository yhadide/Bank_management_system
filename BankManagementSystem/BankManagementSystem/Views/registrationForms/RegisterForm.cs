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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            textpw.PasswordChar = '*';
            btnRegister.Click += new EventHandler(this.btnRegister_Click);
        }

        public bool RegisterCustomer(string firstName, string lastName, string email, string password, string phone, string address)
        {
            using (var context = new BankContext())
            {
                var customerRepository = new Repository<Customer>(context);

                if (customerRepository.GetAll().Any(c => c.Email == email))
                {
                    return false;
                }

                var customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    Phone = phone,
                    Address = address
                };

                customerRepository.Insert(customer);
            }
            return true; 
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (!RegisterCustomer(textfn.Text, textln.Text, textem.Text, textpw.Text, textph.Text, textad.Text))
                {
                    MessageBox.Show("Customer registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while registering the customer. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
