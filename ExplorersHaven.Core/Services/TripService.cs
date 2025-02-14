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
    public class TripService : ITripService
    {
        private readonly IRepository<Trip> _repo;

        public TripService(IRepository<Trip> repo)
        {
            this._repo = repo;
        }

        private bool ValidateTrip(Trip trip)
        {
            var validator = new TripValidator(_repo);
            if (!validator.ValidateInput(trip.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IQueryable<Trip> GetAll()
        {
            return _repo.GetAll();
        }

        public Task AddTripAsync(Trip entity)
        {
            if (!ValidateTrip(entity))
            {
                throw new ArgumentException("The trip is not valid!");
            }
            return _repo.AddAsync(entity);

        }

        public async Task UpdateTripAsync(Trip entity)
        {
            if (!ValidateTrip(entity))
            {
                throw new ArgumentException("The trip is not valid!");
            }
            await _repo.UpdateAsync(entity);

        }

        public async Task DeleteTripAsync(Trip entity)
        {
            await _repo.DeleteAsync(entity);
        }

        public async Task DeleteTripByIdAsync(int id)
        {
            var validator = new TripValidator(_repo);
            if (validator.TripExists(id))
            {
                await _repo.DeleteByIdAsync(id);
            }
        }

        public IQueryable<Trip> CombinedInclude(params Expression<Func<Trip, object>>[] includes)
        {
            IQueryable<Trip> query = _repo.GetAllQuery();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task<Trip> GetTripByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Trip> GetTripAsync(Expression<Func<Trip, bool>> filter)
        {
            return await _repo.GetAsync(filter);
        }

        public async Task<IEnumerable<Trip>> GetAllTripAsync(Expression<Func<Trip, bool>> filter)
        {
            return await _repo.GetAllAsync(filter);
        }

        public async Task<IEnumerable<Trip>> GetAllTripAsync()
        {
            return await _repo.GetAllAsync();
        }

        /* private bool ValidateTrip(Trip trip)
         {
             var validator = new TripValidator(_repo);
             if (!validator.ValidateInput(trip.Name))
             {
                 return false;
             }
             else
             {
                 return true;
             }
         }
         public Trip GetById(int id)
         {
             return _repo.Get(id);
         }

         public Trip Add(Trip trip)
         {
             if (!ValidateTrip(trip))
             {
                 throw new ArgumentException("The trip is not valid!");
             }
             return _repo.Add(trip);

         }


         public void Update(Trip trip)
         {
             if (!ValidateTrip(trip))
             {
                 throw new ArgumentException("The trip is not valid!");
             }
             _repo.Update(trip);
         }

         public void Delete(int id)
         {
             var validator = new TripValidator(_repo);
             if (validator.TripExists(id))
             {
                 _repo.Delete(id);
             }

         }

         public List<Trip> GetAll()
         {
             return _repo.GetAll();
         }

         public List<Trip> Find(Expression<Func<Trip, bool>> filter)
         {
             return _repo.Find(filter);
         }

         public Trip Get(int id)
         {
             throw new NotImplementedException();
         }

         public List<Trip> CheckIfExists(List<int> id)
         {
             throw new NotImplementedException();
         }*/
    }
}
