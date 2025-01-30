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
    public class TravelogueService : ITravelogueService
    {
        private readonly IRepository<Travelogue> _repo;

        public TravelogueService(IRepository<Travelogue> repo)
        {
            this._repo = repo;
        }


        private bool ValidateTravelogue(Travelogue travelogue)
        {
            var validator = new TravelogueValidator(_repo);
            if (!validator.ValidateInput(travelogue.Name))
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
        }

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
    }
}
