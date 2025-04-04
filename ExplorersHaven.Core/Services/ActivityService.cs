using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Validators;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Models;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Core.Services
{
    public class ActivityService: IActivityService
    {
        private readonly IRepository<Models.Activity> _repo;

        public ActivityService(IRepository<Models.Activity> repo)
        {
            this._repo = repo;
        }

        public IQueryable<Models.Activity> GetAll()
        {
            return _repo.GetAll();
        }

        public Task AddActivityAsync(Models.Activity entity)
        {
            return _repo.AddAsync(entity);
        }

        public async Task UpdateActivityAsync(Models.Activity entity)
        {
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteActivityAsync(Models.Activity entity)
        {
            await _repo.DeleteAsync(entity);
        }

        public async Task DeleteActivityByIdAsync(int id)
        {
            if (true)
            {
                await _repo.DeleteByIdAsync(id);
            }
        }

        public IQueryable<Models.Activity> CombinedInclude(params Expression<Func<Models.Activity, object>>[] includes)
        {
            IQueryable<Models.Activity> query = _repo.GetAllQuery();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task DeleteAllActivitysByOffers(int Id)
        {
            var likes = await _repo.GetAllAsync(x => x.OfferId == Id);
            foreach (var like in likes)
            {
                await _repo.DeleteAsync(like);
            }
        }

        public async Task<Models.Activity> GetActivityByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Models.Activity> GetActivityAsync(Expression<Func<Models.Activity, bool>> filter)
        {
            return await _repo.GetAsync(filter);
        }

        public async Task<IEnumerable<Models.Activity>> GetAllActivityAsync(Expression<Func<Models.Activity, bool>> filter)
        {
            return await _repo.GetAllAsync(filter);
        }

        public async Task<IEnumerable<Models.Activity>> GetAllActivityAsync()
        {
            return await _repo.GetAllAsync();
        }

    }
}
