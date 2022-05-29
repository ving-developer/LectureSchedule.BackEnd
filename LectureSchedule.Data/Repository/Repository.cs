using LectureSchedule.Data.Context;
using LectureSchedule.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LectureSchedule.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly LectureScheduleContext _context;

        public Repository(LectureScheduleContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public void DeleteRange(T[] entities)
        {
            _context.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public async Task<T[]> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes is not null)
            {
                //includes in the object query each of the lambdas parameters passed to this function
                query = includes.Aggregate(query,
                  (current, include) => current.Include(include));
            }
            
            return await query.ToArrayAsync();
        }

        public async Task<T> GetSingleByFilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
