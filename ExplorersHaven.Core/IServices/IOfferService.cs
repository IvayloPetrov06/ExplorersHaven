using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface IOfferService
    {
        public IQueryable<Offer> GetAll();

        Task AddOfferAsync(Offer entity);
        Task UpdateOfferAsync(Offer entity);
        Task DeleteOfferAsync(Offer entity);
        Task DeleteOfferByIdAsync(int id);
        IQueryable<Offer> CombinedInclude(params Expression<Func<Offer, object>>[] includes);
        Task<Offer> GetOfferByIdAsync(int id);
        Task<Offer> GetOfferAsync(Expression<Func<Offer, bool>> filter);
        Task<IEnumerable<Offer>> GetAllOfferAsync(Expression<Func<Offer, bool>> filter);
        Task<IEnumerable<Offer>> GetAllOfferAsync();
    }
}
