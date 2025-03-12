using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Models;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Core.Services
{
    public class RatingService:IRatingService
    {
        IRepository<Rating> _ratingService;
        public RatingService(IRepository<Rating>ratingService)
        {
            _ratingService = ratingService;
        }
        public async Task AddRatingAsync(Rating entity)
        {
            await _ratingService.AddAsync(entity);
        }

        public IQueryable<Rating> AllWithInclude(params Expression<Func<Rating, object>>[] includes)
        {
            IQueryable<Rating> query = _ratingService.GetAllQuery();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task DeleteRatingAsync(Rating entity)
        {
            await _ratingService.DeleteAsync(entity);
        }

        public async Task DeleteRatingByIdAsync(int id)
        {
            await _ratingService.DeleteByIdAsync(id);
        }

        public IQueryable<Rating> GetAll()
        {
            return _ratingService.GetAll();
        }

        public async Task<IEnumerable<Rating>> GetAllRatingsAsync(Expression<Func<Rating, bool>> filter)
        {
            return await _ratingService.GetAllAsync(filter);
        }

        public async Task<IEnumerable<Rating>> GetAllRatingsAsync()
        {
            return await _ratingService.GetAllAsync();
        }

        public async Task<Rating> GetRatingAsync(Expression<Func<Rating, bool>> filter)
        {
            return await _ratingService.GetAsync(filter);
        }

        public async Task<Rating> GetRatingByIdAsync(int id)
        {
            return await _ratingService.GetByIdAsync(id);
        }

        public async Task UpdateRatingAsync(Rating entity)
        {
            await _ratingService.UpdateAsync(entity);
        }
    }
}
