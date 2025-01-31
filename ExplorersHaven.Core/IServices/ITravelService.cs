using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface ITravelService
    {
        Travel Add(Travel tr);
        void Update(Travel tr);
        void Delete(int id);
        Travel Get(int id);
        List<Travel> GetAll();
        public Travel GetById(int id);
        List<Travel> Find(Expression<Func<Travel, bool>> filter);
        List<Travel> CheckIfExists(List<int> id);
    }
}
