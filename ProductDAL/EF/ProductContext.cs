using ProductDAL.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;

namespace ProductDAL
{
    
    public class ProductDBEntities : DbContext
    {
        public ProductDBEntities()
            : base("name=ProductDB2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            //modelBuilder.Entity<Log>().HasKey(b => b.Id);
            modelBuilder.Entity<Log>().ToTable("Log");
            base.OnModelCreating(modelBuilder);
        }
    
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Log> Log { get; set; }

    }
}
