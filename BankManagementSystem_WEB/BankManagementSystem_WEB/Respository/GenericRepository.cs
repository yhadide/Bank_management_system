using BankManagementSystem_WEB.Interfaces;
using BankManagementSystem_WEB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BankManagementSystem_WEB.Respository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private BankContext db;
        private DbSet<T> table = null;

        public GenericRepository()
        {
            this.db = new BankContext();
            table = db.Set<T>();
        }

        public GenericRepository(BankContext db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}