
using System;
using System.Linq;
using System.Collections.Generic;
using eZeroOne.Domain;

namespace eZeroOne.Service.Repository
{
    public class Repository : IRepository
    {
        public eResortsEntities Context { get; set; }

        public Repository(eResortsEntities context)
        {
            Context = context;
        }

        public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            var query = All<T>().Where(expression);
            foreach (var item in query)
            {
                Delete(item);
            }
        }

        public void Delete<T>(T item) where T : class, new()
        {
            Context.Set<T>().Remove(item);
        }

        public void DeleteAll<T>() where T : class, new()
        {
            var query = All<T>();
            foreach (var item in query)
            {
                Delete(item);
            }
        }

        public T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            //return All<T>().FirstOrDefault(expression);
            return All<T>().SingleOrDefault(expression);
        }

        public IQueryable<T> All<T>() where T : class, new()
        {
            return Context.Set<T>().AsQueryable();
        }

        public void Add<T>(T item) where T : class, new()
        {
            Context.Set<T>().Add(item);
        }

        public void Add<T>(IEnumerable<T> items) where T : class, new()
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public void Update<T>(T item) where T : class, new()
        {
            //nothing needed here
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}