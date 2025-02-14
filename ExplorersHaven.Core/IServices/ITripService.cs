using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface ITripService
    {
        public IQueryable<Trip> GetAll();

        Task AddTripAsync(Trip entity);
        Task UpdateTripAsync(Trip entity);
        Task DeleteTripAsync(Trip entity);
        Task DeleteTripByIdAsync(int id);
        IQueryable<Trip> CombinedInclude(params Expression<Func<Trip, object>>[] includes);
        Task<Trip> GetTripByIdAsync(int id);
        Task<Trip> GetTripAsync(Expression<Func<Trip, bool>> filter);
        Task<IEnumerable<Trip>> GetAllTripAsync(Expression<Func<Trip, bool>> filter);
        Task<IEnumerable<Trip>> GetAllTripAsync();
    }
}
