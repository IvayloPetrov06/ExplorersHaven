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
    public class TravelService:ITravelService
    {
        private readonly IRepository<Travel> _repo;

        public TravelService(IRepository<Travel> repo)
        {
            this._repo = repo;
        }
        private bool ValidateTravel(Travel travel)
        {
            if (!TravelValidator.ValidateInput(travel.Start))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Add(Travel travel)
        {
            if (!ValidateTravel(travel))
            {
                throw new ArgumentException("The travel is not valid!");
            }
            _repo.Add(travel);
            /*int ID = travel.Id;
            foreach (var trip in )
            { 
            
            }*/

        }

        public Travel GetById(int id)
        {
            return _repo.Get(id);
        }


        public void Update(Travel travel)
        {
            if (!ValidateTravel(travel))
            {
                throw new ArgumentException("The travel is not valid!");
            }
            _repo.Update(travel);
        }

        public void Delete(int id)
        {
            if (TravelValidator.TravelExists(id))
            {
                _repo.Delete(id);
            }

        }

        public List<Travel> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Travel> Find(Expression<Func<Travel, bool>> filter)
        {
            return _repo.Find(filter);
        }

        public Travel Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Travel> CheckIfExists(List<int> id)
        {
            throw new NotImplementedException();
        }
    }
}
