using DataLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly Context _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(Context context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.Where(e => !EF.Property<bool>(e, "IsDeleted")).ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null && EF.Property<bool>(entity, "IsDeleted"))
                return null;
            return entity;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;

            // Soft delete: set IsDeleted to true if the property exists
            var property = entity.GetType().GetProperty("IsDeleted");
            if (property != null && property.PropertyType == typeof(bool))
            {
                property.SetValue(entity, true);
                _dbSet.Update(entity);
            }
            else
            {
                // Physical delete if IsDeleted not present
                _dbSet.Remove(entity);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> RestoreAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;

            var property = entity.GetType().GetProperty("IsDeleted");
            if (property != null && property.PropertyType == typeof(bool))
            {
                property.SetValue(entity, false);
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }

            // Cannot restore if IsDeleted not present
            return false;
        }
    }
}
