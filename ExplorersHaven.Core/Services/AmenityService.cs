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
    public class AmenityService:IAmenityService
    {
        IRepository<Amenity> _amenityService;
        public AmenityService(IRepository<Amenity> amenityService)
        {
            _amenityService = amenityService;
        }
        public async Task AddAmenityAsync(Amenity entity)
        {
            await _amenityService.AddAsync(entity);
        }

        public IQueryable<Amenity> AllWithInclude(params Expression<Func<Amenity, object>>[] includes)
        {
            IQueryable<Amenity> query = _amenityService.GetAllQuery();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task DeleteAmenityAsync(Amenity entity)
        {
            await _amenityService.DeleteAsync(entity);
        }

        public async Task DeleteAmenityByIdAsync(int id)
        {
            await _amenityService.DeleteByIdAsync(id);
        }

        public IQueryable<Amenity> GetAll()
        {
            return _amenityService.GetAll();
        }

        public async Task<IEnumerable<Amenity>> GetAllAmenitiesAsync(Expression<Func<Amenity, bool>> filter)
        {
            return await _amenityService.GetAllAsync(filter);
        }

        public async Task<IEnumerable<Amenity>> GetAllAmenitiesAsync()
        {
            return await _amenityService.GetAllAsync();
        }

        public async Task<Amenity> GetAmenityAsync(Expression<Func<Amenity, bool>> filter)
        {
            return await _amenityService.GetAsync(filter);
        }

        public async Task<Amenity> GetAmenityByIdAsync(int id)
        {
            return await _amenityService.GetByIdAsync(id);
        }

        public async Task UpdateAmenityAsync(Amenity entity)
        {
            await _amenityService.UpdateAsync(entity);
        }
    }
}
