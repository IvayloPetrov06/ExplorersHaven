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
    public class OfferService : IOfferService
    {
        IRepository<Offer> _repo;

        public OfferService(IRepository<Offer> repo)
        {
            _repo = repo;
        }

        public IQueryable<Offer> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task AddOfferAsync(Offer entity)
        {
            await _repo.AddAsync(entity);
        }

        public async Task UpdateOfferAsync(Offer entity)
        {
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteOfferAsync(Offer entity)
        {
            await _repo.DeleteAsync(entity);

        }

        public async Task DeleteOfferByIdAsync(int id)
        {
            if (true)
            {
                await _repo.DeleteByIdAsync(id);
            }
        }

        public async Task DeleteAllOffersByStays(int stayId)
        {
            var likes = await _repo.GetAllAsync(x => x.StayId == stayId);
            foreach (var like in likes)
            {
                await _repo.DeleteAsync(like);
            }
        }

        public IQueryable<Offer> CombinedInclude(params Expression<Func<Offer, object>>[] includes)
        {
            IQueryable<Offer> query = _repo.GetAllQuery();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task<Offer> GetOfferByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Offer> GetOfferAsync(Expression<Func<Offer, bool>> filter)
        {
            return await _repo.GetAsync(filter);
        }

        public async Task<IEnumerable<Offer>> GetAllOfferAsync(Expression<Func<Offer, bool>> filter)
        {
            return await _repo.GetAllAsync(filter);
        }

        public async Task<IEnumerable<Offer>> GetAllOfferAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<IEnumerable<string>> GetAllOfferNamesAsync()
        {
            List<string> names = new List<string>();

            var albums = GetAll().ToList();

            albums.ForEach(x => names.Add(x.Name));

            return names;
        }
    }
}
