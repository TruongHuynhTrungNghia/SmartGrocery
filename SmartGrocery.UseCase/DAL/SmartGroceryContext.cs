using SmartGrocery.Model.Customer;
using SmartGrocery.Model.Product;
using SmartGrocery.Model.Role;
using SmartGrocery.Model.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartGrocery.UseCase.DAL
{
    public class SmartGroceryContext : DbContext
    {
        public SmartGroceryContext() : base("Name=SmartGroceryDatabase")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<BaseProduct>().ToTable("Product");

            entity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            entity.Property(x => x.Name).IsRequired();

            MapBaseProduct(modelBuilder);
            MapCustomer(modelBuilder);
            MapPermissions(modelBuilder);
            MapRole(modelBuilder);
            MapTransaction(modelBuilder);
        }

        private void MapTransaction(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Transaction>();

            entity.ToTable("transaction");
            entity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            entity.Property(x => x.Amount).IsRequired();
            entity.Property(x => x.CreatedAt).IsRequired();
            entity.Property(x => x.CreatedBy).IsRequired();
            entity.Property(x => x.CustomerId).IsRequired();
            entity.Property(x => x.Id).IsRequired();
            entity.Property(x => x.LastUpdatedAt).IsRequired();
            entity.Property(x => x.LastUpdatedBy).IsRequired();

            entity
                .HasRequired(x => x.Customer)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete();
        }

        private void MapRole(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Role>();

            entity.ToTable("role");

            entity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            entity.Property(x => x.Name).IsRequired().HasMaxLength(128);

            entity.HasMany(x => x.Permissions)
                .WithMany(x => x.Roles)
                .Map(config => config.ToTable("permissions-in-roles"));
        }

        private void MapPermissions(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Permission>();

            entity.ToTable("permission");

            entity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            entity.Property(x => x.Name).IsRequired().HasMaxLength(128);

            entity.Property(x => x.Description).IsOptional().HasMaxLength(350);

            entity.HasMany(x => x.Roles).WithMany(x => x.Permissions);
        }

        private void MapCustomer(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Model.Customer.Customer>();

            entity.ToTable("customer");

            entity.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            entity.Property(x => x.FirstName).IsRequired();
            entity.Property(x => x.LastName).IsRequired();
            entity.Property(x => x.CustomerId).IsRequired();
            entity.Property(x => x.Age).IsRequired();
            entity.Property(x => x.DateOfBirth).IsRequired();
            entity.Property(x => x.Points).IsOptional();
            entity
                .HasMany(x => x.Transactions)
                .WithRequired(t => t.Customer)
                .HasForeignKey(k => k.CustomerId);
        }

        private void MapBaseProduct(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<BaseProduct>();

            entity.ToTable("base_product");

            entity
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            entity.Property(x => x.Name).IsRequired()
                .HasMaxLength(256);

            entity.Property(x => x.ProductNumber).IsRequired().HasMaxLength(128);

            entity.Property(x => x.Price).IsRequired();

            entity.Property(x => x.Quantity).IsRequired();

            entity.Property(x => x.ManufacturingDate).IsRequired();

            entity.Property(x => x.ExpiryDate).IsRequired();
        }
    }
}