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
using static System.Net.Mime.MediaTypeNames;

namespace BankManagementSystem.Views
{
    public partial class Login : Form
    {
        private readonly Repository<Customer> customerRepository;
        public Login()
        {
            InitializeComponent();
            textBoxPassword.PasswordChar = '*';
            textBoxPassEmp.PasswordChar = '*';
            var context = new BankContext();
            customerRepository = new Repository<Customer>(context);
        }
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            var email = textBoxEmail.Text;
            var password = textBoxPassword.Text;

            var customer = customerRepository.GetAll()
                .FirstOrDefault(c => c.Email == email && c.Password == password);

            if (customer != null)
            {
                // Login successful
                MessageBox.Show("Login successful!");
                var customerHub = new CustomerHub(customer.Id);
                customerHub.Show();
                this.Hide();
            }
            else
            {
                // Login failed
                MessageBox.Show("Invalid email or password.");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            EmployeePanel.Visible = false;
            CustomerPanel.Visible = false;
        }

        private void btnLoginEmp_Click(object sender, EventArgs e)
        {
            var lastName = textBoxLastNameEmp.Text;
            var password = textBoxPassEmp.Text;

            using (var context = new BankContext())
            {
                var employeeRepository = new Repository<Employee>(context);
                var employee = employeeRepository.GetAll()
                    .FirstOrDefault(e1 => e1.LastName == lastName && e1.Password == password);

                if (employee != null)
                {
                    MessageBox.Show("Login successful!");
                    var employeeForm = new EmployeeForm();
                    employeeForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid last name or password.");
                }
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            EmployeePanel.Visible = false;
            CustomerPanel.Visible = true;
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            EmployeePanel.Visible = true;
            CustomerPanel.Visible = false;
        }
    }
}
