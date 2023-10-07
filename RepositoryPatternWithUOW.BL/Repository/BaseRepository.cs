using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.BL.Interfaces;
using RepositoryPatternWithUOW.core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.BL.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

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

        public T Find( string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
             
                foreach (var include in includes)
                 
                  query=  query.Include(include);
               
            return query.FirstOrDefault() ;
        
        }

        public T Find(Expression<Func<T, bool>> Filter)
        { 
                return _context.Set<T>().SingleOrDefault(Filter); 
        }

        public   IEnumerable<T> FindALL(Expression<Func<T, bool>> Filter, string[] includes = null)
        {
            IQueryable<T> Query = _context.Set<T>();
            if(includes != null)
            {
                foreach (var include in includes)
                    Query = Query.Include(include);
            }
            return Query.Where(Filter);
        }

        public IEnumerable<T> FindALL(Expression<Func<T, bool>> Filter, int? take, int? skip, 
            Expression<Func<T, object>> order = null, string orderBy = Consts.Consts.Descending)
        {
            IQueryable<T> Query = _context.Set<T>().Where(Filter);
            if(take.HasValue)
                Query= Query.Take(take.Value);
            if (skip.HasValue)
                Query = Query.Skip(skip.Value);
            if (order  != null)
            {
                if(orderBy== Consts.Consts.Descending)
                 
                    Query = Query.OrderBy(order);
                else
                    Query = Query.OrderByDescending(order );
            }

            return Query.ToList();
        }

        public IEnumerable<T> GetAll()
        {
             return  _context.Set<T>().ToList();
        }

        public T GetById(int id)
        { 
            return  _context.Set<T>().Find(id);
           
        }
         
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

         public T update(T entity)
          {
            _context.Update(entity);
            return entity;
          }
        public void delete(T entity) 
        { 
          _context.Set<T>().Remove(entity);
        }
        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }
        public int Count()
        {
          return  _context.Set<T>().Count();
        }
        public int Count(Expression<Func<T, bool>> Filter)
        {
            return _context.Set<T>().Count(Filter);
        }

       

    }
}
