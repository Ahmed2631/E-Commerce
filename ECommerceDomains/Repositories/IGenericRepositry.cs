using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomains.Repositories
{
    public interface IGenericRepositry<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T,bool>>? expression=null , string? word=null);

        T GetFristOrDefault(Expression<Func<T, bool>>? expression = null, string? word = null);

        void Add(T item);

        void Remove(T item);

        void RemoveRange(IEnumerable<T> values);
    }
}
