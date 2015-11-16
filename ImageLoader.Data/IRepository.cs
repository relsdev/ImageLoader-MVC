using ImageLoader.Domain;
using System;
using System.Linq;

namespace ImageLoader.Data
{
    public interface IRepository<T> : IDisposable where T : class, IIdentifiable
    {
        IQueryable<T> All { get; }
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
