using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Models;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Core.Services
{
    public class TransportService:ITransportService
    {
        IRepository<Transport> _TransportService;
        public TransportService(IRepository<Transport> TransportService)
        {
            _TransportService = TransportService;
        }
        public async Task AddTransportAsync(Transport entity)
        {
            await _TransportService.AddAsync(entity);
        }

        public IQueryable<Transport> AllWithInclude(params Expression<Func<Transport, object>>[] includes)
        {
            IQueryable<Transport> query = _TransportService.GetAllQuery();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task DeleteTransportAsync(Transport entity)
        {
            await _TransportService.DeleteAsync(entity);
        }

        public async Task DeleteTransportByIdAsync(int id)
        {
            await _TransportService.DeleteByIdAsync(id);
        }

        public IQueryable<Transport> GetAll()
        {
            return _TransportService.GetAll();
        }


        public async Task<IEnumerable<Transport>> GetAllTransportsAsync(Expression<Func<Transport, bool>> filter)
        {
            return await _TransportService.GetAllAsync(filter);
        }

        public async Task<IEnumerable<Transport>> GetAllTransportsAsync()
        {
            return await _TransportService.GetAllAsync();
        }

        public async Task<Transport> GetTransportAsync(Expression<Func<Transport, bool>> filter)
        {
            return await _TransportService.GetAsync(filter);
        }

        public async Task<Transport> GetTransportByIdAsync(int id)
        {
            return await _TransportService.GetByIdAsync(id);
        }

        public async Task UpdateTransportAsync(Transport entity)
        {
            await _TransportService.UpdateAsync(entity);
        }
    }
}
