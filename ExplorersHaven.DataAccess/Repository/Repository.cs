using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            this.dbSet = _context.Set<T>();

        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public List<T> CheckIfExists(List<int> id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            T obj = dbSet.Find(id);
            dbSet.Remove(obj);
        }

        public List<T> Find(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).ToList();

        }

        public T Get(int id)
        {
            T obj = dbSet.Find(id);
            return obj;
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
