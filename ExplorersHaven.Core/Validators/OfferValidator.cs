using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.Validators
{
    internal static class OfferValidator
    {
        //private IRepository<Offer> _repo;
        //public OfferValidator(IRepository<Offer> repo)
        //{
        //    this._repo = repo;
        //}
        public static bool ValidateInput(string name)
        {
            if (name.Length == 0 || name.Length > 30)
            {
                return false;
            }
            return true;
        }
        public static  bool OfferExists(int id, IRepository<Offer> repo)
        {
            if (repo.GetByIdAsync(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
