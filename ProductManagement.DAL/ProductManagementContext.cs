using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductManagement.DAL
{
    public class ProductManagementContext: IdentityDbContext<IdentityUser>
    {
        public DbSet<Product> Products { get; set; }
        public ProductManagementContext(DbContextOptions<ProductManagementContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>(entity=>
                entity.HasKey(p=>p.id)
            );
            //builder.Entity<Product>(entity =>
            //    entity.Property(p => p.title)
            //    .HasMaxLength(200)
            //);
            //builder.Entity<Product>(entity =>
            //    entity.Property(p => p.category)
            //    .HasMaxLength(200)
            //);
            builder.Entity<Product>(entity =>
               entity.Property(p => p.description)
               .IsRequired(false)
            );
            builder.Entity<Product>(entity =>
                entity.Property(p => p.price)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0.0m)
            );
            builder.Entity<Product>(entity =>
                entity.Property(p => p.quantity)
                .HasDefaultValue(0)
            );
            builder.Entity<Product>(entity =>
                entity.Property(p=>p.image)
                .IsRequired(false)
            );
        }
    }
}
