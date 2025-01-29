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
    public class ActivityService: IActivityService
    {
        private readonly IRepository<Models.Activity> _repo;

        public ActivityService(IRepository<Models.Activity> repo)
        {
            this._repo = repo;
        }
        private bool ValidateActivity(Models.Activity activity)
        {
            if (!ActivityValidator.ValidateInput(activity.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        

        public Models.Activity GetById(int id)
        {
            return _repo.Get(id);
        }


        public void Update(Models.Activity activity)
        {
            if (!ValidateActivity(activity))
            {
                throw new ArgumentException("The activity is not valid!");
            }
            _repo.Update(activity);
        }

        public void Delete(int id)
        {
            if (ActivityValidator.ActivityExists(id))
            {
                _repo.Delete(id);
            }

        }

        public List<Models.Activity> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Models.Activity> Find(Expression<Func<Models.Activity, bool>> filter)
        {
            return _repo.Find(filter);
        }

        public void Add(Models.Activity activity)
        {
            if (!ValidateActivity(activity))
            {
                throw new ArgumentException("The activity is not valid!");
            }
            _repo.Add(activity);
        }

        
    }
}
