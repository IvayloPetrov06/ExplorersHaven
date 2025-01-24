using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Core.IServices;
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
        /*private bool ValidateProduct(Activity Product)
        {
            if (!ProductValidator.ValidateInput(Product.Name, Product.Price))
            {
                return false;
            }
            else if (!CategoryValidator.CategoryExist(Product.CategoryId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }*/

        public void Add(Activity product)
        {
            if (!ValidateActivity(product))
            {
                throw new ArgumentException("The product is not valid!");
            }
            _repo.Add(product);

        }

        public Activity GetById(int id)
        {
            return _repo.Get(id);
        }

        public List<Activity> GetProductsByCategory(int categoryId)
        {
            if (ActivityValidator.CategoryExist(categoryId))
            {
                return _repo.Find(x => x.CategoryId == categoryId);
            }
            else
            {
                throw new ArgumentException("The category is not valid!");
            }
        }

        public void Update(Activity product)
        {
            if (!ValidateActivity(product))
            {
                throw new ArgumentException("The product is not valid!");
            }
            _repo.Update(product);
        }

        public void Delete(int id)
        {
            if (ProductValidator.ProductExists(id))
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
