using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.BL.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        T Find(Expression<Func<T, bool>> Filter);
        T Find(string[] includes = null);
        IEnumerable<T> FindALL(Expression<Func<T, bool>> Filter,string[] includes = null);
        IEnumerable<T> FindALL(Expression<Func<T, bool>> Filter, int?take, int? skip,
                               Expression<Func<T, object>> order = null, string orderBy = Consts.Consts.Descending);
    
        T update(T entity);
        void delete(T entity);
        void Attach(T entity);
        int Count();
        int Count(Expression<Func<T, bool>> Filter);
    
    }
}
