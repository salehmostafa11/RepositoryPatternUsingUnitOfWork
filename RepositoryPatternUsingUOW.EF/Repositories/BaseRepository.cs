using Microsoft.EntityFrameworkCore;
using RepositoryPatternUsingUOW.Core.Consts;
using RepositoryPatternUsingUOW.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternUsingUOW.EF.Repositories
{
    public class BaseRepository<T>(ApplicationDbContext applicationDbContext) : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context = applicationDbContext;

        public T Add(T entity)
        {
             _context.Set<T>().Add(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return entities;
        }

        public IEnumerable<T> FindAllOrdereWithProperetyInclude(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy= null,
                                                           string orderByDirection = OrderBy.Ascending,
                                                            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            //if(skip.HasValue)
            //    query = query.Skip(skip.Value);
            //if(take.HasValue)
            //    query = query.Take(take.Value);

            if(orderBy != null)
            {
                if(orderByDirection == OrderBy.Descending)
                    query = query.OrderByDescending(orderBy);
                else
                    query = query.OrderBy(orderBy);
            }

            if(includeProperties.Count() != 0)
            {
                foreach(var prop in includeProperties)
                    query = query.Include(prop);
            }

            return query.ToList();
        }

        public T FindByProperety(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().SingleOrDefault(criteria)!;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id)!;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id)!;
        }
        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.RemoveRange(entities);
        }

    }
}
