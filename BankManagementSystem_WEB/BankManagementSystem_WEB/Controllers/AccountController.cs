using BankManagementSystem_WEB.Interfaces;
using BankManagementSystem_WEB.Models;
using BankManagementSystem_WEB.Respository;
using BankManagementSystem_WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BankManagementSystem_WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenericRepository<Account> _accountRepository;
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IGenericRepository<Customer> _customerRepository; // Added Customer repository

        public AccountController()
        {
            this._accountRepository = new GenericRepository<Account>();
            this._transactionRepository = new GenericRepository<Transaction>();
            this._customerRepository = new GenericRepository<Customer>(); // Initialize Customer repository
        }

        public AccountController(IGenericRepository<Account> accountRepository, IGenericRepository<Transaction> transactionRepository, IGenericRepository<Customer> customerRepository)
        {
            this._accountRepository = accountRepository;
            this._transactionRepository = transactionRepository;
            this._customerRepository = customerRepository; // Inject Customer repository
        }

        // GET: Account/Hub/5
        public ActionResult Hub(int id)
        {
            var account = _accountRepository.GetById(id);
            if (account == null)
            {
                return HttpNotFound();
            }

            var transactions = _transactionRepository.GetAll().Where(t => t.AccountId == id).ToList();

            var viewModel = new AccountHubViewModel
            {
                Account = account,
                Transactions = transactions
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit(TransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var transaction = new Transaction
                {
                    AccountId = model.AccountId,
                    Amount = model.Amount,
                    TransactionType = "Deposit",
                    TransactionDate = DateTime.Now
                };

                _transactionRepository.Insert(transaction);

                var account = _accountRepository.GetById(model.AccountId);
                account.Balance += model.Amount;
                _accountRepository.Update(account);
                _accountRepository.Save();
                _transactionRepository.Save();

                return RedirectToAction("Hub", new { id = model.AccountId });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw(TransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var transaction = new Transaction
                {
                    AccountId = model.AccountId,
                    Amount = model.Amount,
                    TransactionType = "Withdraw",
                    TransactionDate = DateTime.Now
                };

                _transactionRepository.Insert(transaction);

                var account = _accountRepository.GetById(model.AccountId);
                account.Balance -= model.Amount;
                _accountRepository.Update(account);
                _accountRepository.Save();
                _transactionRepository.Save();

                return RedirectToAction("Hub", new { id = model.AccountId });
            }

            return View(model);
        }

        public ActionResult CreateAccountForm()
        {
            PopulateCustomersDropDownList();
            ViewBag.AccountTypes = new List<string> { "Savings", "Checking", "Business", "Student" };
            var newAccount = new Account();
            return PartialView("CreateAccountForm", newAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                _accountRepository.Insert(account);
                _accountRepository.Save();
                return RedirectToAction("AccList");
            }

            PopulateCustomersDropDownList(account.CustomerId);
            ViewBag.AccountTypes = new List<string> { "Savings", "Checking", "Business", "Student" };
            return View(account);
        }



        // GET: Account/EditAccountForm/5
        public ActionResult EditAccountForm(int id)
        {
            var account = _accountRepository.GetById(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            PopulateCustomersDropDownList(account.CustomerId);
            ViewBag.AccountTypes = new List<string> { "Savings", "Checking", "Business", "Student" };
            return PartialView("EditAccountForm", account);
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                _accountRepository.Update(account);
                _accountRepository.Save();
                return RedirectToAction("AccList");
            }
            PopulateCustomersDropDownList(account.CustomerId);
            return View(account);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            var account = _accountRepository.GetById(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View("AccDelete", account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _accountRepository.Delete(id);
            _accountRepository.Save();
            return RedirectToAction("AccList");
        }

        // GET: Account
        public ActionResult AccList()
        {
            var accounts = _accountRepository.GetAll().ToList();
            return View("AccList", accounts);
        }

        private void PopulateCustomersDropDownList(object selectedCustomer = null)
        {
            var customers = _customerRepository.GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FirstName + " " + c.LastName
                }).ToList();

            ViewBag.CustomerId = new SelectList(customers, "Value", "Text", selectedCustomer);
        }
    }
}
