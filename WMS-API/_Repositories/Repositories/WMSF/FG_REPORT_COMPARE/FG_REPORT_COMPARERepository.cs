using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WMS_API._Repositories.Interfaces;
using WMS_API.Data.WMSF.FG_REPORT_COMPARE;

namespace WMS_API._Repositories.Repository.WMSF.FG_REPORT_COMPARE
{
    public class FG_REPORT_COMPARERepository<T> : IRepository<T> where T : class
    {
        private readonly DBContext_WMS _context;

        public FG_REPORT_COMPARERepository(DBContext_WMS context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void AddMultiple(List<T> entities)
        {
            _context.AddRange(entities);
        }

        public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate);
        }

        public async Task<T> FindById(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return await FindAll(includeProperties).FirstOrDefaultAsync(predicate);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Remove(object id)
        {
            Remove(FindById(id));
        }

        public void RemoveMultiple(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateMultiple(List<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }
    }
}