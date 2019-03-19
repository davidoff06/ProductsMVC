using ProductDAL.Models;
using System;
using System.Data.Entity;
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
            modelBuilder.Entity<Log>().ToTable("Log");
            base.OnModelCreating(modelBuilder);
        }
    
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        //public System.Data.Entity.DbSet<ProductsMVC.Models.RoleViewModel> RoleViewModels { get; set; }
    }
}
