using Explorers_Haven.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Core.IServices
{
    public interface IBookingService
    {
        public IQueryable<Booking> GetAll();

        Task AddBookingAsync(Booking entity);
        Task UpdateBookingAsync(Booking entity);
        Task DeleteBookingAsync(Booking entity);
        Task DeleteBookingByIdAsync(int id);
        IQueryable<Booking> AllWithInclude(params Expression<Func<Booking, object>>[] includes);
        Task<Booking> GetBookingByIdAsync(int id);
        Task<Booking> GetBookingAsync(Expression<Func<Booking, bool>> filter);
        Task<IEnumerable<Booking>> GetAllBookingsAsync(Expression<Func<Booking, bool>> filter);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
    }
}
