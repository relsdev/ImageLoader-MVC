using ImageLoader.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLoader.Data
{
    public class ImageContext : DbContext, IImageContext
    {
        public ImageContext() : base("DefaultConnection") { }
        public DbSet<Image> Images { get; set; }

        static ImageContext()
        {
            Database.SetInitializer<ImageContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions
            .Remove<PluralizingTableNameConvention>();
        }
    }
}
