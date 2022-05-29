using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LectureSchedule.Data.Repository.Interface
{
    public interface IRepository<T> where T: class
    {
        Task<T> GetSingleByFilterAsync(Expression<Func<T, bool>> predicate);

        Task<T[]> GetAllAsync(params Expression<Func<T, object>>[] includes);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void DeleteRange(T[] entity);

        Task<bool> SaveChangesAsync();
    }
}
