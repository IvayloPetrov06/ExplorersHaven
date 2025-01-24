using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ExplorersHaven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface IStayService
    {
        void Add(Stay stay);
        void Update(Stay stay);
        void Delete(int id);
        Stay Get(int id);
        List<Stay> GetAll();
        List<Stay> Find(Expression<Func<Stay, bool>> filter);
        List<Stay> CheckIfExists(List<int> id);
    }
}
