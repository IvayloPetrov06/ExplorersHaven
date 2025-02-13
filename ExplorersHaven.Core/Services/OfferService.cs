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
            var validator = new OfferValidator(_repo);
            if (!validator.ValidateInput(entity.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Travelogue GetById(int id)
        {
            return _repo.Get(id);
        }

        /* public Travelogue Add(Travelogue travelogue)
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
         }*/

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

        ///here

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
            
        }

        public async Task DeleteOfferByIdAsync(int id)
        {
            var validator = new OfferValidator(_repo);
            if (validator.OfferExists(id))
            {
                await _repo.DeleteByIdAsync(id);
            }
        }

        public IQueryable<Offer> AllWithInclude(params Expression<Func<Offer, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<Offer> GetOfferByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Offer> GetOfferAsync(Expression<Func<Offer, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Offer>> GetAllOfferAsync(Expression<Func<Offer, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Offer>> GetAllOfferAsync()
        {
            throw new NotImplementedException();
        }
    }
}
