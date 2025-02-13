using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface IStayService
    {
        Task AddStayAsync(Stay entity);
        Task UpdateStayAsync(Stay entity);
        Task DeleteStayAsync(Stay entity);
        Task DeleteStayByIdAsync(int id);
        IQueryable<Stay> CombinedInclude(params Expression<Func<Stay, object>>[] includes);
        Task<Stay> GetStayByIdAsync(int id);
        Task<Stay> GetStayAsync(Expression<Func<Stay, bool>> filter);
        Task<IEnumerable<Stay>> GetAllStayAsync(Expression<Func<Stay, bool>> filter);
        Task<IEnumerable<Stay>> GetAllStayAsync();
    }
}
