using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Core.IServices
{
    public interface IActivityService
    {
        void Add(Activity activity);
        void Update(Activity activity);
        void Delete(int id);
        Activity Get(int id);
        List<Activity> GetAll();
        List<Activity> Find(Expression<Func<Activity, bool>> filter);
        List<Activity> CheckIfExists(List<int> id);
    }
}
