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
    public class StayService:IStayService
    {

        private readonly IRepository<Stay> _repo;

        public StayService(IRepository<Stay> repo)
        {
            this._repo = repo;
        }

        private bool ValidateStay(Stay stay)
        {
            var validator = new StayValidator(_repo);
            if (!validator.ValidateInput(stay.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Task AddStayAsync(Stay entity)
        {
            if (!ValidateStay(entity))
            {
                throw new ArgumentException("The stay is not valid!");
            }
            return _repo.AddAsync(entity);
        }

        public async Task UpdateOfferAsync(Stay entity)
        {
            if (!ValidateStay(entity))
            {
                throw new ArgumentException("The stay is not valid!");
            }
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteStayAsync(Stay entity)
        {
            await _repo.DeleteAsync(entity);
        }

        public async Task DeleteStayByIdAsync(int id)
        {
            var validator = new StayValidator(_repo);
            if (validator.StayExists(id))
            {
                await _repo.DeleteByIdAsync(id);
            }
        }

        public IQueryable<Stay> CombinedInclude(params Expression<Func<Stay, object>>[] includes)
        {
            IQueryable<Stay> query = _repo.GetAllQuery();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task<Stay> GetStayByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Stay> GetStayAsync(Expression<Func<Stay, bool>> filter)
        {
            return await _repo.GetAsync(filter);
        }

        public async Task<IEnumerable<Stay>> GetAllStayAsync(Expression<Func<Stay, bool>> filter)
        {
            return await _repo.GetAllAsync(filter);
        }

        public async Task<IEnumerable<Stay>> GetAllStayAsync()
        {
            return await _repo.GetAllAsync();
        }

        public Task UpdateStayAsync(Stay entity)
        {
            throw new NotImplementedException();
        }

        /*private bool ValidateStay(Stay stay)
        {
            var validator = new StayValidator(_repo);
            if (!validator.ValidateInput(stay.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Stay GetById(int id)
        {
            return _repo.Get(id);
        }

        public Stay Add(Stay stay)
        {
            if (!ValidateStay(stay))
            {
                throw new ArgumentException("The stay is not valid!");
            }
            return _repo.Add(stay);

        }


        public void Update(Stay stay)
        {
            if (!ValidateStay(stay))
            {
                throw new ArgumentException("The stay is not valid!");
            }
            _repo.Update(stay);
        }

        public void Delete(int id)
        {
            var validator = new StayValidator(_repo);
            if (validator.StayExists(id))
            {
                _repo.Delete(id);
            }

        }

        public List<Stay> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Stay> Find(Expression<Func<Stay, bool>> filter)
        {
            return _repo.Find(filter);
        }

        public Stay Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Stay> CheckIfExists(List<int> id)
        {
            throw new NotImplementedException();
        }*/
    }
}
