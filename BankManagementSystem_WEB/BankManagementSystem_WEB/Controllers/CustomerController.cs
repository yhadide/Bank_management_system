using BankManagementSystem_WEB.Interfaces;
using BankManagementSystem_WEB.Models;
using BankManagementSystem_WEB.Respository;
using BankManagementSystem_WEB.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace BankManagementSystem_WEB.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IGenericRepository<Account> _accountRepository;

        public CustomerController()
        {
            this._customerRepository = new GenericRepository<Customer>();
            this._accountRepository = new GenericRepository<Account>();
        }

        public CustomerController(IGenericRepository<Customer> customerRepository, IGenericRepository<Account> accountRepository)
        {
            this._customerRepository = customerRepository;
            this._accountRepository = accountRepository;
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var accounts = _accountRepository.GetAll().Where(a => a.CustomerId == id).ToList();

            var viewModel = new CustomerDetailsViewModel
            {
                Customer = customer,
                Accounts = accounts
            };

            return View("Details", viewModel);
        }

        // GET: Customer/Create
        public ActionResult CusCreate()
        {
            return PartialView("CusCreate");
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.Insert(customer);
                _customerRepository.Save();
                return RedirectToAction("CusList");
            }

            return PartialView("CusCreate", customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View("CusEdit", customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.Update(customer);
                _customerRepository.Save();
                return RedirectToAction("CusList");
            }
            return View("CusEdit", customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View("CusDelete", customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _customerRepository.Delete(id);
            _customerRepository.Save();
            return RedirectToAction("CusList");
        }

        // GET: Customer/CusList
        public ActionResult CusList()
        {
            var customers = _customerRepository.GetAll().ToList();
            return View("CusList", customers);
        }
    }
}
