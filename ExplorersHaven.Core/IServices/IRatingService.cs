using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface IRatingService
    {
        public IQueryable<Rating> GetAll();

        Task AddRatingAsync(Rating entity);
        Task UpdateRatingAsync(Rating entity);
        Task DeleteRatingAsync(Rating entity);
        Task DeleteRatingByIdAsync(int id);
        IQueryable<Rating> AllWithInclude(params Expression<Func<Rating, object>>[] includes);
        Task<Rating> GetRatingByIdAsync(int id);
        Task<Rating> GetRatingAsync(Expression<Func<Rating, bool>> filter);
        Task<IEnumerable<Rating>> GetAllRatingsAsync(Expression<Func<Rating, bool>> filter);
        Task<IEnumerable<Rating>> GetAllRatingsAsync();
    }
}
