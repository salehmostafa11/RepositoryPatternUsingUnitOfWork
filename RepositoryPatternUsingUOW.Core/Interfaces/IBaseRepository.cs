using RepositoryPatternUsingUOW.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternUsingUOW.Core.Interfaces
{
    public interface IBaseRepository<T> where T:class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        T FindByProperety(Expression<Func<T,bool>> criteria);
        IEnumerable<T> FindAllOrdereWithProperetyInclude(Expression<Func<T,bool>> criteria, Expression<Func<T,object>> orderBy,
            string orderByDirection = OrderBy.Ascending
             , params Expression<Func<T, object>>[] includeProperties);
        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        T Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
