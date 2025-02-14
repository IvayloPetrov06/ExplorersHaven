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

        private bool ValidateActivity(Models.Activity act)
        {
            return true;
            //var validator = new ActivityValidator(_repo);
            //if (!validator.ValidateInput(act.Name))
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
        }

        public IQueryable<Models.Activity> GetAll()
        {
            return _repo.GetAll();
        }

        public Task AddActivityAsync(Models.Activity entity)
        {
            if (!ValidateActivity(entity))
            {
                throw new ArgumentException("The activity is not valid!");
            }
            return _repo.AddAsync(entity);
        }

        public async Task UpdateActivityAsync(Models.Activity entity)
        {
            if (!ValidateActivity(entity))
            {
                throw new ArgumentException("The activity is not valid!");
            }
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteActivityAsync(Models.Activity entity)
        {
            await _repo.DeleteAsync(entity);
        }

        public async Task DeleteActivityByIdAsync(int id)
        {
            //var validator = new ActivityValidator(_repo);
            if (true)//validator.ActivityExists(id))
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

        /*
        private bool ValidateActivity(Models.Activity activity)
        {
            var validator = new ActivityValidator(_repo);
            if (!validator.ValidateInput(activity.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Models.Activity GetById(int id)
        {
            return _repo.Get(id);
        }

        public Models.Activity Add(Models.Activity activity)
        {
            if (!ValidateActivity(activity))
            {
                throw new ArgumentException("The activity is not valid!");
            }
            return _repo.Add(activity);

        }


        public void Update(Models.Activity activity)
        {
            if (!ValidateActivity(activity))
            {
                throw new ArgumentException("The activity is not valid!");
            }
            _repo.Update(activity);
        }

        public void Delete(int id)
        {
            var validator = new ActivityValidator(_repo);
            if (validator.ActivityExists(id))
            {
                _repo.Delete(id);
            }

        }

        public List<Models.Activity> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Models.Activity> Find(Expression<Func<Models.Activity, bool>> filter)
        {
            return _repo.Find(filter);
        }

        public Models.Activity Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Models.Activity> CheckIfExists(List<int> id)
        {
            throw new NotImplementedException();
        }*/


    }
}
