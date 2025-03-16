using Explorers_Haven.Core.IServices;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Core.Services
{
    public class StayAmenityService:IStayAmenityService
    {
        IRepository<StayAmenity> _StayAmenityService;
        public StayAmenityService(IRepository<StayAmenity> StayAmenityService)
        {
            _StayAmenityService = StayAmenityService;
        }
        public async Task AddStayAmenityAsync(StayAmenity entity)
        {
            await _StayAmenityService.AddAsync(entity);
        }

        public IQueryable<StayAmenity> AllWithInclude(params Expression<Func<StayAmenity, object>>[] includes)
        {
            IQueryable<StayAmenity> query = _StayAmenityService.GetAllQuery();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task DeleteStayAmenityAsync(StayAmenity entity)
        {
            await _StayAmenityService.DeleteAsync(entity);
        }

        public async Task DeleteStayAmenityByIdAsync(int id)
        {
            await _StayAmenityService.DeleteByIdAsync(id);
        }

        public IQueryable<StayAmenity> GetAll()
        {
            return _StayAmenityService.GetAll();
        }

        public async Task<IEnumerable<StayAmenity>> GetAllStayAmenitysAsync(Expression<Func<StayAmenity, bool>> filter)
        {
            return await _StayAmenityService.GetAllAsync(filter);
        }

        public async Task<IEnumerable<StayAmenity>> GetAllStayAmenitysAsync()
        {
            return await _StayAmenityService.GetAllAsync();
        }

        public async Task<StayAmenity> GetStayAmenityAsync(Expression<Func<StayAmenity, bool>> filter)
        {
            return await _StayAmenityService.GetAsync(filter);
        }

        public async Task<StayAmenity> GetStayAmenityByIdAsync(int id)
        {
            return await _StayAmenityService.GetByIdAsync(id);
        }

        public async Task UpdateStayAmenityAsync(StayAmenity entity)
        {
            await _StayAmenityService.UpdateAsync(entity);
        }
    }
}
