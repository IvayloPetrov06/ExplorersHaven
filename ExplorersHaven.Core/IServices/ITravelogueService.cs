using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface ITravelogueService
    {
        Travelogue Add(Travelogue travelogue);
        void Update(Travelogue travelogue);
        void Delete(int id);
        Travelogue Get(int id);
        List<Travelogue> GetAll();
        public Travelogue GetById(int id);
        List<Travelogue> Find(Expression<Func<Travelogue, bool>> filter);
        List<Travelogue> CheckIfExists(List<int> id);
    }
}
