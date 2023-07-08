using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Repositories;

namespace Shop.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ShopDbContext _context;

        public Repository(ShopDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);

        }

        public T Get(Expression<Func<T, bool>> expression, params string[] includes)
        {
           var query=_context.Set<T>().AsQueryable();
            foreach (var item in includes)
            {
               query=query.Include(item);
            }
            return query.FirstOrDefault(expression);
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return query.Where(expression).ToList();
        }

        public IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> expression, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(Expression<Func<T, bool>> expression, params string[] includes)
        {

            var query = _context.Set<T>().AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return query.Any(expression);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public int Commit()
        {
            return _context.SaveChanges();

        }
    }
}
