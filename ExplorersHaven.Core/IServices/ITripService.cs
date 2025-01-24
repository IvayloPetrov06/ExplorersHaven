using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ExplorersHaven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface ITripService
    {
        void Add(Trip travel);
        void Update(Trip travel);
        void Delete(int id);
        Trip Get(int id);
        List<Trip> GetAll();
        List<Trip> Find(Expression<Func<Trip, bool>> filter);
        List<Trip> CheckIfExists(List<int> id);
    }
}
