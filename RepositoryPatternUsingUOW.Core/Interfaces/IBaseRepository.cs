using RepositoryPatternUsingUOW.Core.Consts;
using RepositoryPatternUsingUOW.Core.DTOs;
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
        //CRUD
        Task<T?> GetByCriteriaIncluded(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllIncluded(params Expression<Func<T, object>>[] Includes);
        Task<IEnumerable<T>> SearchIncluded(Expression<Func<T,bool>> criteria, 
            params Expression<Func<T, object>>[] includeProperties);

        void Add(T entity);
        T Update(T entity);
        void Delete(T entity); 

        Task<bool> exists(Expression<Func<T,bool>> criteria);
    }
}
