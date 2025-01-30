using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface ITripService
    {
        Trip Add(Trip trip);
        void Update(Trip trip);
        void Delete(int id);
        Trip Get(int id);
        List<Trip> GetAll();
        public Trip GetById(int id);
        List<Trip> Find(Expression<Func<Trip, bool>> filter);
        List<Trip> CheckIfExists(List<int> id);
    }
}
