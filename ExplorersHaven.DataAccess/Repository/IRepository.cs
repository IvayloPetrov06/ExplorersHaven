using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task SaveAsync();
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        IQueryable<T> GetAllQuery(Expression<Func<T, bool>> filter);
        IQueryable<T> GetAllQuery();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAllAsync();
        /*
        T Add(T entity);
        void Update(T entity);
        void Delete(int id);
        T Get(int id);
        List<T> GetAll();
        List<T> Find(Expression<Func<T, bool>> filter);
        List<T> CheckIfExists(List<int> id);*/
    }
}
