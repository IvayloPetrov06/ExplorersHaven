using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.DataAccess.Repository;
using ExplorersHaven.Models;

namespace Explorers_Haven.Core.Validators
{
    internal class StayValidator
    {
        private static IRepository<Stay> _repo;
        public static bool ValidateInput(string name)
        {
            if (name.Length == 0 || name.Length > 30)
            {
                return false;
            }
            return true;
        }
        public static bool StayExists(int id)
        {
            if (_repo.Get(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
