using Explorers_Haven.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Explorers_Haven.Core.IServices
{
    public interface ITransportService
    {
        public IQueryable<Transport> GetAll();

        Task AddTransportAsync(Transport entity);
        Task UpdateTransportAsync(Transport entity);
        Task DeleteTransportAsync(Transport entity);
        Task DeleteTransportByIdAsync(int id);
        IQueryable<Transport> AllWithInclude(params Expression<Func<Transport, object>>[] includes);
        Task<Transport> GetTransportByIdAsync(int id);
        Task<Transport> GetTransportAsync(Expression<Func<Transport, bool>> filter);
        Task<IEnumerable<Transport>> GetAllTransportsAsync(Expression<Func<Transport, bool>> filter);
        Task<IEnumerable<Transport>> GetAllTransportsAsync();
    }
}
