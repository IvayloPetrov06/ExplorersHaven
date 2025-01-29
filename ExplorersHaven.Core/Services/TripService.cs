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
    public class TripService:ITripService
    {

        private readonly IRepository<Trip> _repo;

        public TripService(IRepository<Trip> repo)
        {
            this._repo = repo;
        }
        private bool ValidateTrip(Trip trip)
        {
            if (!TripValidator.ValidateInput(trip.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Add(Trip trip)
        {
            if (!ValidateTrip(trip))
            {
                throw new ArgumentException("The trip is not valid!");
            }
            _repo.Add(trip);

        }

        public Trip GetById(int id)
        {
            return _repo.Get(id);
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
            if (TripValidator.TripExists(id))
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
        }
    }
}
