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

        public IQueryable<Travel> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task AddTravelAsync(Travel entity)
        { 
            await _repo.AddAsync(entity);
        }

        public async Task UpdateTravelAsync(Travel entity)
        {
            await _repo.UpdateAsync(entity);
        }
        public async Task DeleteAllTravelsByOffers(int Id)
        {
            var likes = await _repo.GetAllAsync(x => x.OfferId == Id);
            foreach (var like in likes)
            {
                await _repo.DeleteAsync(like);
            }
        }
        public async Task DeleteTravelAsync(Travel entity)
        {
            await _repo.DeleteAsync(entity);
        }

        public async Task DeleteTravelByIdAsync(int id)
        {
            if (true)
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
    }
}
