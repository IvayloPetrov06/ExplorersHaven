using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Validators;
using Explorers_Haven.DataAccess.Repository;
using ExplorersHaven.Models;

namespace Explorers_Haven.Core.Services
{
    public class ActivityService: Core.IServices.IActivityService
    {
        private readonly IRepository<Activity> _repo;

        public ActivityService(IRepository<Activity> repo)
        {
            this._repo = repo;
        }
        private bool ValidateActivity(Activity activity)
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

        public void Add(Activity activity)
        {
            if (!ValidateActivity(activity))
            {
                throw new ArgumentException("The activity is not valid!");
            }
            _repo.Add(activity);

        }

        public Activity GetById(int id)
        {
            return _repo.Get(id);
        }


        public void Update(Activity activity)
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

        public List<Activity> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Activity> Find(Expression<Func<Activity, bool>> filter)
        {
            return _repo.Find(filter);
        }
    }
}
