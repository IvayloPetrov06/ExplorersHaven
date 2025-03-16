using Explorers_Haven.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Core.IServices
{
    public interface IStayAmenityService
    {
        public IQueryable<StayAmenity> GetAll();

        Task AddStayAmenityAsync(StayAmenity entity);
        Task UpdateStayAmenityAsync(StayAmenity entity);
        Task DeleteStayAmenityAsync(StayAmenity entity);
        Task DeleteStayAmenityByIdAsync(int id);
        IQueryable<StayAmenity> AllWithInclude(params Expression<Func<StayAmenity, object>>[] includes);
        Task<StayAmenity> GetStayAmenityByIdAsync(int id);
        Task<StayAmenity> GetStayAmenityAsync(Expression<Func<StayAmenity, bool>> filter);
        Task<IEnumerable<StayAmenity>> GetAllStayAmenitysAsync(Expression<Func<StayAmenity, bool>> filter);
        Task<IEnumerable<StayAmenity>> GetAllStayAmenitysAsync();
    }
}
