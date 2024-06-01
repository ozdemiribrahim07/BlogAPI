using BlogAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T :BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        bool UpdateAsync(T entity);
        Task<bool> RemoveAsync(T entity);
        Task<bool> RemoveAsync(string id);
        bool RemoveRangeAsync(List<T> entities);
        Task<int> SaveAsync();

    }
   

}
