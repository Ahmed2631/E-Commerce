using ECommerceDomains.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RepositryImplementation
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepositry(ApplicationDbContext context)
        {
            _context = context;
            _dbSet   = _context.Set<T>();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? expression = null, string? word = null)
        {
            IQueryable<T> query = _dbSet;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if(word != null)
            {
                foreach(var item in word.Split(new char[] {','} , StringSplitOptions.RemoveEmptyEntries ))
                {
                    query = query.Include(item.Trim());
                }
            }
            return query.ToList();
        }

        public T GetFristOrDefault(System.Linq.Expressions.Expression<Func<T, bool>>? expression = null, string? word = null)
        {
            IQueryable<T> query = _dbSet;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (word != null)
            {
                foreach (var item in word.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item.Trim());
                }
            }
            return query.SingleOrDefault();
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> values)
        {
            _dbSet.RemoveRange(values);
        }
    }
}
