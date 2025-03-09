using Explorers_Haven.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Core.IServices
{
    public interface IAmenityService
    {
        public IQueryable<Amenity> GetAll();

        Task AddAmenityAsync(Amenity entity);
        Task UpdateAmenityAsync(Amenity entity);
        Task DeleteAmenityAsync(Amenity entity);
        Task DeleteAmenityByIdAsync(int id);
        IQueryable<Amenity> AllWithInclude(params Expression<Func<Amenity, object>>[] includes);
        Task<Amenity> GetAmenityByIdAsync(int id);
        Task<Amenity> GetAmenityAsync(Expression<Func<Amenity, bool>> filter);
        Task<IEnumerable<Amenity>> GetAllAmenitiesAsync(Expression<Func<Amenity, bool>> filter);
        Task<IEnumerable<Amenity>> GetAllAmenitiesAsync();
    }
}
