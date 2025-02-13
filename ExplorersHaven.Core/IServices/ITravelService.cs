using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface ITravelService
    {
        public IQueryable<Travel> GetAll();
        Task AddTravelAsync(Travel entity);
        Task UpdateTravelAsync(Travel entity);
        Task DeleteTravelAsync(Travel entity);
        Task DeleteTravelByIdAsync(int id);
        IQueryable<Travel> CombinedInclude(params Expression<Func<Travel, object>>[] includes);
        Task<Travel> GetTravelByIdAsync(int id);
        Task<Travel> GetTravelAsync(Expression<Func<Travel, bool>> filter);
        Task<IEnumerable<Travel>> GetAllTravelAsync(Expression<Func<Travel, bool>> filter);
        Task<IEnumerable<Travel>> GetAllTravelAsync();
    }
}
