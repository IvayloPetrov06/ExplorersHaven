using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Validators;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Models;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Core.Services
{
    public class BookingService:IBookingService
    {
        IRepository<Booking> _bookingService;
        public BookingService(IRepository<Booking> bookingService)
        {
            _bookingService = bookingService;
        }
        public async Task AddBookingAsync(Booking entity)
        {
            await _bookingService.AddAsync(entity);
        }

        public IQueryable<Booking> AllWithInclude(params Expression<Func<Booking, object>>[] includes)
        {
            IQueryable<Booking> query = _bookingService.GetAllQuery();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task DeleteBookingAsync(Booking entity)
        {
            await _bookingService.DeleteAsync(entity);
        }

        public async Task DeleteBookingByIdAsync(int id)
        {
            await _bookingService.DeleteByIdAsync(id);
        }
        

        public async Task DeleteAllBookingsByOffers(int Id)
        {
            var likes = await _bookingService.GetAllAsync(x => x.OfferId == Id);
            foreach (var like in likes)
            {
                await _bookingService.DeleteAsync(like);
            }
        }

        public IQueryable<Booking> GetAll()
        {
            return _bookingService.GetAll();
        }
        

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync(Expression<Func<Booking, bool>> filter)
        {
            return await _bookingService.GetAllAsync(filter);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _bookingService.GetAllAsync();
        }

        public async Task<Booking> GetBookingAsync(Expression<Func<Booking, bool>> filter)
        {
            return await _bookingService.GetAsync(filter);
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _bookingService.GetByIdAsync(id);
        }

        public async Task UpdateBookingAsync(Booking entity)
        {
            await _bookingService.UpdateAsync(entity);
        }
    }
}
