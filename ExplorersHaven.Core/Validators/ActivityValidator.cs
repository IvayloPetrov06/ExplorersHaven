using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.DataAccess.Repository;
using ExplorersHaven.Models;

namespace Explorers_Haven.Core.Validators
{
    public static class ActivityValidator
    {

        private static IRepository<Activity> _repo;
        public static bool ValidateInput(string name)
        {
            if (name.Length == 0 || name.Length > 30)
            {
                return false;
            }
            return true;
        }
        public static bool ActivityExists(int id)
        {
            if (_repo.Get(id) == null)
            {
                return false;
            }
            return true;
        }

    }
}
