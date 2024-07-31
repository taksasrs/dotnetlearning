using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext()
        {
        }

        public MovieContext (DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
        public DbSet<User> User {get;set;}
        public DbSet<Shop> Shop {get;set;}
        public DbSet<Product> Product {get;set;}

        // protected override void OModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<User>(entity =>
        //     {
        //         entity.HasNoKey();
        //         entity.ToTable("User");
        //         entity.Property(e => e.Id).HasColumnName("Id");
        //         entity.Property(e => e.Username).HasMaxLength(60).IsUnicode(false);
        //         entity.Property(e => e.Password).HasMaxLength(30).IsUnicode(false);
        //         entity.Property(e => e.ChatId).HasMaxLength(50).IsUnicode(false);
        //         entity.Property(e => e.CreateDate).IsUnicode(false);
        //     });

        //     modelBuilder.Entity<Shop>(entity =>
        //     {
        //         entity.ToTable("Shop");
        //         entity.Property(e => e.Id).HasColumnName("Id");
        //         entity.Property(e => e.Name).HasMaxLength(15).IsUnicode(false);
        //         entity.Property(e => e.Description).HasMaxLength(100).IsUnicode(false);
        //         entity.Property(e => e.ImagePath).HasMaxLength(256).IsUnicode(false);
        //         entity.Property(e => e.CreateDate).IsUnicode(false);
        //     });

        //     modelBuilder.Entity<Product>(entity =>
        //     {
        //         entity.ToTable("Product");
        //         entity.Property(e => e.Id).HasColumnName("Id");
        //         entity.Property(e => e.Name).HasMaxLength(15).IsUnicode(false);
        //         entity.Property(e => e.Description).HasMaxLength(100).IsUnicode(false);
        //         entity.Property(e => e.ImagePath).HasMaxLength(256).IsUnicode(false);
        //         entity.Property(e => e.Price).HasMaxLength(256).IsUnicode(false);
        //         entity.Property(e => e.Stock).HasMaxLength(256).IsUnicode(false);
        //         entity.Property(e => e.CreateDate).IsUnicode(false);
        //     });

        //     // OnModelCreatingPartial(modelBuilder);
        // }

        // // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
