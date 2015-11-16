using ImageLoader.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLoader.Data
{

    public class Repository<T> : IRepository<T> where T : class, IIdentifiable
    {
        private readonly ImageContext context;
        public Repository(IUnitOfWork uow)
        {
            context = uow.Context as ImageContext;
        }
        public IQueryable<T> All
        {
            get
            {
                return context.Set<T>();
            }
        }

        public void Insert(T item)
        {
            context.Entry(item).State = EntityState.Added;
        }
        public void Update(T item)
        {
            context.Set<T>().Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var item = context.Set<T>().Find(id);
            context.Set<T>().Remove(item);
        }
        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }
    }
}
