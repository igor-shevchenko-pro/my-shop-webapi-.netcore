using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyShop.Core.Entities;
using MyShop.Core.Entities.Auth;
using MyShop.Core.Entities.ManyToMany;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;

namespace MyShop.DL.PostgreSql
{
    public class MyShopPostgreSqlContext : DbContext, IDbContext
    {
        IDatabaseSettings _databaseSettings;

        public static ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
           builder.AddFilter((category, level) =>
                             category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                             .AddConsole();
        });

        public MyShopPostgreSqlContext()
        {
            _databaseSettings = new PostgreSqlSettings();
        }

        public MyShopPostgreSqlContext(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var cs = _databaseSettings.GetConnectionString();

            optionsBuilder.UseLazyLoadingProxies()
                          .UseLoggerFactory(MyLoggerFactory) // Warning: Do not create a new ILoggerFactory instance each time
                          .UseNpgsql(cs);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<VerificationToken> VerificationTokens { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<FileEntity> FileEntities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Currency> Currencies { get; set; } 
        public DbSet<Modification> Modifications { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<ImageGallery> ImageGalleries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // GENDER
            modelBuilder.Entity<Gender>()
                .HasKey(x => new { x.Id, x.LanguageId });

            // CATEGORY
            modelBuilder.Entity<Category>()
                .HasKey(x => new { x.Id, x.LanguageId });

            // PRODUCT
            modelBuilder.Entity<Product>()
                .HasKey(x => new { x.Id, x.LanguageId });

            // SUPPLIER
            modelBuilder.Entity<Supplier>()
                .HasKey(x => new { x.Id, x.LanguageId });

            // REFRESHTOKEN
            modelBuilder.Entity<RefreshToken>()
                .HasKey(x => new { x.Id, x.Token });


            // USER_ROLE
            modelBuilder.Entity<UserRole>()
                .HasKey(x => new { x.Id, x.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.Id);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(x => x.RoleId);


            // CATEGORY_PRODUCT
            modelBuilder.Entity<CategoryProduct>()
                .HasKey(x => new { x.Id, x.CategoryLanguageId, x.ProductId, x.ProductLanguageId });

            modelBuilder.Entity<CategoryProduct>()
                .HasOne(x => x.Category)
                .WithMany(x => x.CategoryProducts)
                .HasForeignKey(x => new { x.Id, x.CategoryLanguageId });

            modelBuilder.Entity<CategoryProduct>()
                .HasOne(x => x.Product)
                .WithMany(p => p.CategoryProducts)
                .HasForeignKey(x => new { x.ProductId, x.ProductLanguageId });


            // ORDER_PRODUCT
            modelBuilder.Entity<OrderProduct>()
                .HasKey(x => new { x.Id, x.ProductId, x.ProductLanguageId, });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderProducts)
                .HasForeignKey(x => x.Id);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(x => x.Product)
                .WithMany(x => x.OrderProducts)
                .HasForeignKey(x => new { x.ProductId, x.ProductLanguageId });

            // SUPPLIER_PRODUCT
            modelBuilder.Entity<SupplierProduct>()
                .HasKey(x => new { x.Id, x.SupplierLanguageId, x.ProductId, x.ProductLanguageId });

            modelBuilder.Entity<SupplierProduct>()
                .HasOne(x => x.Supplier)
                .WithMany(x => x.SupplierProducts)
                .HasForeignKey(x => new { x.Id, x.SupplierLanguageId });

            modelBuilder.Entity<SupplierProduct>()
                .HasOne(x => x.Product)
                .WithMany(x => x.SupplierProducts)
                .HasForeignKey(x => new { x.ProductId, x.ProductLanguageId });


            // cascade delete settings
            modelBuilder.Entity<FileEntity>()
                .HasOne(p => p.ImageGallery)
                .WithMany(t => t.GalleryFileEntities)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Category>()
                .HasOne(p => p.ImageGallery)
                .WithOne(t => t.Category)
                .OnDelete(DeleteBehavior.SetNull);
        }


        public ICollection<T> GetDataInstances<T>() where T : class, new()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges<T>() where T : class, new()
        {
            SaveChanges();
        }
    }
}