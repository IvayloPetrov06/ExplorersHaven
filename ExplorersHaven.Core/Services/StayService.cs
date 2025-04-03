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

        IRepository<Stay> _repo;

        public StayService(IRepository<Stay> repo)
        {
            _repo = repo;
        }

        private bool ValidateStay(Stay stay)
        {
            return true;
        }

        public IQueryable<Stay> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task AddStayAsync(Stay entity)
        {
            if (!ValidateStay(entity))
            {
                throw new ArgumentException("The stay is not valid!");
            }
            await _repo.AddAsync(entity);
        }

        public async Task UpdateStayAsync(Stay entity)
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
            //var validator = new StayValidator(_repo);
            if (true)//validator.StayExists(id)
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

    }
}
