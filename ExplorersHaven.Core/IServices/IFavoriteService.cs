using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface IFavoriteService
    {
        public IQueryable<Favorite> GetAll();

        Task AddFavoriteAsync(Favorite entity);
        Task UpdateFavoriteAsync(Favorite entity);
        Task DeleteFavoriteAsync(Favorite entity);
        Task DeleteFavoriteByIdAsync(int id);
        IQueryable<Favorite> AllWithInclude(params Expression<Func<Favorite, object>>[] includes);
        Task<Favorite> GetFavoriteByIdAsync(int id);
        Task<Favorite> GetFavoriteAsync(Expression<Func<Favorite, bool>> filter);
        Task<IEnumerable<Favorite>> GetAllFavoritesAsync(Expression<Func<Favorite, bool>> filter);
        Task<IEnumerable<Favorite>> GetAllFavoritesAsync();
    }
}
