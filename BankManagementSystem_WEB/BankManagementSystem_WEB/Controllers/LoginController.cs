using BankManagementSystem_WEB.Interfaces;
using BankManagementSystem_WEB.Models;
using BankManagementSystem_WEB.Respository;
using BankManagementSystem_WEB.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace BankManagementSystem_WEB.Controllers
{
    public class LoginController : Controller
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;

        public LoginController()
        {
            this._customerRepository = new GenericRepository<Customer>();
            this._employeeRepository = new GenericRepository<Employee>();
        }

        // GET: Login/CustomerLogin
        public ActionResult CustomerLogin()
        {
            return View();
        }

        // POST: Login/CustomerLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerLogin(CustomerLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerRepository.GetAll().FirstOrDefault(c => c.Email == model.Email && c.Password == model.Password);
                if (customer != null)
                {
                    // Authentication successful, redirect to the customer details page
                    return RedirectToAction("Details", "Customer", new { id = customer.Id });
                }
                ModelState.AddModelError("", "Invalid email or password.");
            }
            return View(model);
        }

        // GET: Login/EmployeeLogin
        public ActionResult EmployeeLogin()
        {
            return View();
        }

        // POST: Login/EmployeeLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeLogin(EmployeeLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeRepository.GetAll().FirstOrDefault(e => e.LastName == model.LastName && e.Password == model.Password);
                if (employee != null)
                {
                    // Authentication successful, redirect to an employee-specific page
                    return RedirectToAction("EmployeeHub", "Employee", new { id = employee.Id });
                }
                ModelState.AddModelError("", "Invalid last name or password.");
            }
            return View(model);
        }

        // GET: Login/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}
