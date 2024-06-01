using BlogAPI.Application.Repositories;
using BlogAPI.Domain.Entities.Common;
using BlogAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Persistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly BlogContext _blogContext;
        public WriteRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public DbSet<T> Table => 
            _blogContext.Set<T>();


        public async Task<bool> AddAsync(T entity)
        {
           EntityEntry<T> entityEntry = await Table.AddAsync(entity);
           return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }


        public async Task<bool> RemoveAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Task.Run(() => Table.Remove(entity));
            return entityEntry.State == EntityState.Deleted;
        }


        public async Task<bool> RemoveAsync(string id)
        {
           T entity = await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            return await RemoveAsync(entity);
        }

        public bool RemoveRangeAsync(List<T> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public async Task<int> SaveAsync()
            => await _blogContext.SaveChangesAsync();


        public bool UpdateAsync(T entity)
        {
            EntityEntry <T> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }


    }
}
