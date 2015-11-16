using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLoader.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IImageContext context;
        public UnitOfWork()
        {
            context = new ImageContext();
        }
        public UnitOfWork(IImageContext context)
        {
            this.context = context;
        }
        public int Save()
        {
            return context.SaveChanges();
        }
        public IImageContext Context
        {

            get
            {
                return context;
            }
        }

        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }
    }
}
