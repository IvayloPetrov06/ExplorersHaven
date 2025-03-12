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
    public class CommentService:ICommentService
    {
        IRepository<Comment> _commentService;
        public CommentService(IRepository<Comment> CommentService)
        {
            _commentService = CommentService;
        }
        public async Task AddCommentAsync(Comment entity)
        {
            await _commentService.AddAsync(entity);
        }

        public IQueryable<Comment> AllWithInclude(params Expression<Func<Comment, object>>[] includes)
        {
            IQueryable<Comment> query = _commentService.GetAllQuery();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task DeleteCommentAsync(Comment entity)
        {
            await _commentService.DeleteAsync(entity);
        }

        public async Task DeleteCommentByIdAsync(int id)
        {
            await _commentService.DeleteByIdAsync(id);
        }

        public IQueryable<Comment> GetAll()
        {
            return _commentService.GetAll();
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync(Expression<Func<Comment, bool>> filter)
        {
            return await _commentService.GetAllAsync(filter);
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _commentService.GetAllAsync();
        }

        public async Task<Comment> GetCommentAsync(Expression<Func<Comment, bool>> filter)
        {
            return await _commentService.GetAsync(filter);
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _commentService.GetByIdAsync(id);
        }

        public async Task UpdateCommentAsync(Comment entity)
        {
            await _commentService.UpdateAsync(entity);
        }
    }
}
