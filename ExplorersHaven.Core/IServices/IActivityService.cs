using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface IActivityService
    {
        public IQueryable<Models.Activity> GetAll();

        Task AddActivityAsync(Models.Activity entity);
        Task UpdateActivityAsync(Models.Activity entity);
        Task DeleteActivityAsync(Models.Activity entity);
        Task DeleteActivityByIdAsync(int id);
        Task DeleteAllActivitysByOffers(int Id);
        IQueryable<Models.Activity> CombinedInclude(params Expression<Func<Models.Activity, object>>[] includes);
        Task<Models.Activity> GetActivityByIdAsync(int id);
        Task<Models.Activity> GetActivityAsync(Expression<Func<Models.Activity, bool>> filter);
        Task<IEnumerable<Models.Activity>> GetAllActivityAsync(Expression<Func<Models.Activity, bool>> filter);
        Task<IEnumerable<Models.Activity>> GetAllActivityAsync();
    }
}
