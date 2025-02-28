﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Explorers_Haven.Models;

namespace Explorers_Haven.Core.IServices
{
    public interface IUserService
    {
        Task AddUserAsync(User entity);
        Task UpdateUserAsync(User entity);
        Task DeleteUserAsync(User entity);
        IQueryable<User> GetAll();

        Task DeleteUserByIdAsync(int id);
        IQueryable<User> AllWithInclude(params Expression<Func<User, object>>[] includes);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserAsync(Expression<Func<User, bool>> filter);
        Task<IEnumerable<User>> GetAllUsersAsync(Expression<Func<User, bool>> filter);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<string>> GetAllUserNamesAsync();
    }
}
