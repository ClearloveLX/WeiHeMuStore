using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WeiHeMuStore.Model.BaseModel;

namespace WeiHeMuStore.Model
{
    public class WeiHeMuStoreDBContext : DbContext
    {
        public WeiHeMuStoreDBContext(DbContextOptions<WeiHeMuStoreDBContext> options) : base(options) { }

        public virtual DbSet<ProductCate> ProductCates { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCate>(entity =>
            {
                entity.HasKey(e => e.ProductCateId);
                entity.Property(e => e.ProductCateName)
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.ProductName)
                    .HasMaxLength(100);
                entity.Property(e => e.ProductImage)
                    .HasMaxLength(150);
                entity.Property(e => e.ProductIntroduce)
                    .HasMaxLength(150);
                entity.Property(e => e.ProductPrice)
                    .HasColumnType("decimal(5,2)")
                    .HasDefaultValueSql("'0'");
                entity.Property(e => e.ProductPrice)
                    .HasDefaultValueSql("'0'");
            });
        }
    }
}
