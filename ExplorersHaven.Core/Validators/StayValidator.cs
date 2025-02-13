using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.Validators
{
    internal class StayValidator
    {
        private IRepository<Stay> _repo;
        public StayValidator(IRepository<Stay> repo)
        {
            this._repo = repo;
        }
        public bool ValidateInput(string name)
        {
            if (name.Length == 0 || name.Length > 30)
            {
                return false;
            }
            return true;
        }
        public bool StayExists(int id)
        {
            if (_repo.GetByIdAsync(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
