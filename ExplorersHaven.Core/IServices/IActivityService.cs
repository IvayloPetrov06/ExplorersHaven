using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface IActivityService
    {
        Models.Activity Add(Models.Activity trip);
        void Update(Models.Activity trip);
        void Delete(int id);
        Models.Activity Get(int id);
        List<Models.Activity> GetAll();
        public Models.Activity GetById(int id);
        List<Models.Activity> Find(Expression<Func<Models.Activity, bool>> filter);
        List<Models.Activity> CheckIfExists(List<int> id);
    }
}
