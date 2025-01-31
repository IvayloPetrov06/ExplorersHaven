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
    public class StayService:IStayService
    {

        private readonly IRepository<Stay> _repo;

        public StayService(IRepository<Stay> repo)
        {
            this._repo = repo;
        }


        private bool ValidateStay(Stay stay)
        {
            var validator = new StayValidator(_repo);
            if (!validator.ValidateInput(stay.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Stay GetById(int id)
        {
            return _repo.Get(id);
        }

        public Stay Add(Stay stay)
        {
            if (!ValidateStay(stay))
            {
                throw new ArgumentException("The stay is not valid!");
            }
            return _repo.Add(stay);

        }


        public void Update(Stay stay)
        {
            if (!ValidateStay(stay))
            {
                throw new ArgumentException("The stay is not valid!");
            }
            _repo.Update(stay);
        }

        public void Delete(int id)
        {
            var validator = new StayValidator(_repo);
            if (validator.StayExists(id))
            {
                _repo.Delete(id);
            }

        }

        public List<Stay> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Stay> Find(Expression<Func<Stay, bool>> filter)
        {
            return _repo.Find(filter);
        }

        public Stay Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Stay> CheckIfExists(List<int> id)
        {
            throw new NotImplementedException();
        }
    }
}
