using System.Linq;
using System.Web.Mvc;
using BankManagementSystem_WEB.Models;
using BankManagementSystem_WEB.Interfaces;
using BankManagementSystem_WEB.Respository;

namespace BankManagementSystem_WEB.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IGenericRepository<Account> _accountRepository;

        public EmployeeController()
        {
            this._employeeRepository = new GenericRepository<Employee>();
            this._customerRepository = new GenericRepository<Customer>();
            this._accountRepository = new GenericRepository<Account>();
        }

        public ActionResult EmployeeHub()
        {
            var customerCount = _customerRepository.GetAll().Count();
            var accountCount = _accountRepository.GetAll().Count();

            ViewBag.CustomerCount = customerCount;
            ViewBag.AccountCount = accountCount;

            return View();
        }
        public ActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }

        public ActionResult Details(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Insert(employee);
                _employeeRepository.Save();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult Edit(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Update(employee);
                _employeeRepository.Save();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _employeeRepository.Delete(id);
            _employeeRepository.Save();
            return RedirectToAction("Index");
        }
    }
}