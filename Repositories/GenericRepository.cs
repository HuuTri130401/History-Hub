using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly HistoryHubContext _historyHubContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository()
        {
            _historyHubContext = new HistoryHubContext();
            _dbSet = _historyHubContext.Set<T>();
        }

        public GenericRepository(HistoryHubContext historyHubContext, DbSet<T> dbSet)
        {
            _historyHubContext = historyHubContext;
            _dbSet = _historyHubContext.Set<T>();
        }

        public void Delete(object id)
        {
            T exist = _dbSet.Find(id);
            if (exist != null)
            {
                _dbSet.Remove(exist);
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id);

        }

        public void Insert(T obj)
        {
            _dbSet.Add(obj);
            _historyHubContext.SaveChanges();
        }

        public void Save()
        {
            _historyHubContext.SaveChanges();
        }
        public void Update(T obj)
        {
            _dbSet.Attach(obj);
            _historyHubContext.Entry(obj).State = EntityState.Modified;
        }
    }
}
