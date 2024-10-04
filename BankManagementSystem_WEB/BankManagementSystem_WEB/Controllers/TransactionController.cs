using System.Linq;
using System.Web.Mvc;
using BankManagementSystem_WEB.Models;
using BankManagementSystem_WEB.Interfaces;
using BankManagementSystem_WEB.Respository;

namespace BankManagementSystem_WEB.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IGenericRepository<Transaction> repository;

        public TransactionController()
        {
            this.repository = new GenericRepository<Transaction>();
        }

        public ActionResult Index()
        {
            var transactions = repository.GetAll();
            return View(transactions);
        }

        public ActionResult Details(int id)
        {
            var transaction = repository.GetById(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(new GenericRepository<Account>().GetAll(), "AccountId", "AccountNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                repository.Insert(transaction);
                repository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(new GenericRepository<Account>().GetAll(), "AccountId", "AccountNumber", transaction.AccountId);
            return View(transaction);
        }

        public ActionResult Edit(int id)
        {
            var transaction = repository.GetById(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(new GenericRepository<Account>().GetAll(), "AccountId", "AccountNumber", transaction.AccountId);
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                repository.Update(transaction);
                repository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(new GenericRepository<Account>().GetAll(), "AccountId", "AccountNumber", transaction.AccountId);
            return View(transaction);
        }

        public ActionResult Delete(int id)
        {
            var transaction = repository.GetById(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            repository.Save();
            return RedirectToAction("Index");
        }
    }
}