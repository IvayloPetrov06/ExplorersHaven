using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Models;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Core.Services
{
    public class FavoriteService:IFavoriteService
    {
        IRepository<Favorite> _favoriteService;
        public FavoriteService(IRepository<Favorite> FavoriteService)
        {
            _favoriteService = FavoriteService;
        }
        public async Task AddFavoriteAsync(Favorite entity)
        {
            await _favoriteService.AddAsync(entity);
        }

        public IQueryable<Favorite> AllWithInclude(params Expression<Func<Favorite, object>>[] includes)
        {
            IQueryable<Favorite> query = _favoriteService.GetAllQuery();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task DeleteFavoriteAsync(Favorite entity)
        {
            await _favoriteService.DeleteAsync(entity);
        }

        public async Task DeleteFavoriteByIdAsync(int id)
        {
            await _favoriteService.DeleteByIdAsync(id);
        }

        public IQueryable<Favorite> GetAll()
        {
            return _favoriteService.GetAll();
        }

        public async Task<IEnumerable<Favorite>> GetAllFavoritesAsync(Expression<Func<Favorite, bool>> filter)
        {
            return await _favoriteService.GetAllAsync(filter);
        }

        public async Task<IEnumerable<Favorite>> GetAllFavoritesAsync()
        {
            return await _favoriteService.GetAllAsync();
        }

        public async Task<Favorite> GetFavoriteAsync(Expression<Func<Favorite, bool>> filter)
        {
            return await _favoriteService.GetAsync(filter);
        }

        public async Task<Favorite> GetFavoriteByIdAsync(int id)
        {
            return await _favoriteService.GetByIdAsync(id);
        }

        public async Task UpdateFavoriteAsync(Favorite entity)
        {
            await _favoriteService.UpdateAsync(entity);
        }
    }
}
