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
        private readonly IRepository<Offer> _repo;

        public OfferService(IRepository<Offer> repo)
        {
            this._repo = repo;
        }
        private bool ValidateOffer(Offer entity)
        {
            return true;
            //var validator = new OfferValidator(_repo);
            //if (!validator.ValidateInput(entity.Name))
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
        }

        public IQueryable<Offer> GetAll()
        {
            return _repo.GetAll();
        }

        public Task AddOfferAsync(Offer entity)
        {
            if (!ValidateOffer(entity))
            {
                throw new ArgumentException("The offer is not valid!");
            }
            return _repo.AddAsync(entity);
        }

        public async Task UpdateOfferAsync(Offer entity)
        {
            if (!ValidateOffer(entity))
            {
                throw new ArgumentException("The offer is not valid!");
            }
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteOfferAsync(Offer entity)
        {
            await _repo.DeleteAsync(entity);
        }

        public async Task DeleteOfferByIdAsync(int id)
        {
            //var validator = new OfferValidator(_repo);
            //bool exists = OfferValidator.OfferExists(id, _repo);
            if (true)
            {
                await _repo.DeleteByIdAsync(id);
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

        /*public Travelogue GetById(int id)
        {
            return _repo.Get(id);
        }

         public Travelogue Add(Travelogue travelogue)
         {
             if (!ValidateTravelogue(travelogue))
             {
                 throw new ArgumentException("The travelogue is not valid!");
             }
             return _repo.Add(travelogue);

         }


         public void Update(Travelogue travelogue)
         {
             if (!ValidateTravelogue(travelogue))
             {
                 throw new ArgumentException("The travelogue is not valid!");
             }
             _repo.Update(travelogue);
         

        public void Delete(int id)
        {
            var validator = new TravelogueValidator(_repo);
            if (validator.TravelogueExists(id))
            {
                _repo.Delete(id);
            }

        } 

        public List<Travelogue> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Travelogue> Find(Expression<Func<Travelogue, bool>> filter)
        {
            return _repo.Find(filter);
        }

        public Travelogue Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Travelogue> CheckIfExists(List<int> id)
        {
            throw new NotImplementedException();
        }
        */
    }
}
