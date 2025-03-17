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
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<T>();

        }

        public async Task AddAsync(T entity)
        {
            if (entity != null)
            {
                await _dbSet.AddAsync(entity);
                await SaveAsync();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await (_context.SaveChangesAsync());
            }
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<T> GetAllQuery(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> list = _dbSet.Where(filter);
            return list;
        }

        public IQueryable<T> GetAllQuery()
        {
            IQueryable<T> list = _dbSet;
            return list;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            T entity = await _dbSet.FirstOrDefaultAsync(filter);//.AsNoTracking()
            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveAsync();
        }
        /*
        public T Add(T entity)
        {
            dbSet.Add(entity);
            _context.SaveChanges(); 

            return entity;
        }

        public List<T> CheckIfExists(List<int> id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            T obj = dbSet.Find(id);
            dbSet.Remove(obj);
            _context.SaveChanges();
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
            _context.SaveChanges();
        }*/
    }
}
