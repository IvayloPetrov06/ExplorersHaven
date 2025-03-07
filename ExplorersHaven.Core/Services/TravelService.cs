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
    public class TravelService:ITravelService
    {
        private readonly IRepository<Travel> _repo;

        public TravelService(IRepository<Travel> repo)
        {
            this._repo = repo;
        }

        private bool ValidateTravel(Travel tr)
        {
            return true;
            //var validator = new TravelValidator(_repo);
            //if (!validator.ValidateInput(tr.Start) && !validator.ValidateInput(tr.Finish))
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
        }

        public IQueryable<Travel> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task AddTravelAsync(Travel entity)
        {
            if (!ValidateTravel(entity))
            {
                throw new ArgumentException("The travel is not valid!");
            }
            await _repo.AddAsync(entity);
        }

        public async Task UpdateTravelAsync(Travel entity)
        {
            if (!ValidateTravel(entity))
            {
                throw new ArgumentException("The travel is not valid!");
            }
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteTravelAsync(Travel entity)
        {
            await _repo.DeleteAsync(entity);
        }

        public async Task DeleteTravelByIdAsync(int id)
        {
            //var validator = new TravelValidator(_repo);
            if (true)//validator.TravelExists(id))
            {
                await _repo.DeleteByIdAsync(id);
            }
        }

        public IQueryable<Travel> CombinedInclude(params Expression<Func<Travel, object>>[] includes)
        {
            IQueryable<Travel> query = _repo.GetAllQuery();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task<Travel> GetTravelByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Travel> GetTravelAsync(Expression<Func<Travel, bool>> filter)
        {
            return await _repo.GetAsync(filter);
        }

        public async Task<IEnumerable<Travel>> GetAllTravelAsync(Expression<Func<Travel, bool>> filter)
        {
            return await _repo.GetAllAsync(filter);
        }

        public async Task<IEnumerable<Travel>> GetAllTravelAsync()
        {
            return await _repo.GetAllAsync();
        }

        /*private bool ValidateTravel(Travel tr)
        {
            var validator = new TravelValidator(_repo);
            if (!validator.ValidateInput(tr.Start)&&!validator.ValidateInput(tr.Finish))
            { 
              return false;
            }
            else
            {
                return true;
            }
        }
        public Travel GetById(int id)
        {
            return _repo.Get(id);
        }

        public Travel Add(Travel tr)
        {
            if (!ValidateTravel(tr))
            {
                throw new ArgumentException("The travel is not valid!");
            }
            return _repo.Add(tr);

        }


        public void Update(Travel tr)
        {
            if (!ValidateTravel(tr))
            {
                throw new ArgumentException("The travel is not valid!");
            }
            _repo.Update(tr);
        }

        public void Delete(int id)
        {
            var validator = new TravelValidator(_repo);
            if (validator.TravelExists(id))
            {
                _repo.Delete(id);
            }
        }

        public List<Travel> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Travel> Find(Expression<Func<Travel, bool>> filter)
        {
            return _repo.Find(filter);
        }

        public Travel Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Travel> CheckIfExists(List<int> id)
        {
            throw new NotImplementedException();
        }*/
    }
}
