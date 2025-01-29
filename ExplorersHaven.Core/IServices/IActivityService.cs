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
        // get by id;update;delete;getall;find;add;
        void Add(Models.Activity activity);
        void Update(Models.Activity activity);
        void Delete(int id);
        public Models.Activity GetById(int id);
        List<Models.Activity> GetAll();
        List<Models.Activity> Find(Expression<Func<Models.Activity, bool>> filter);
    }
}
