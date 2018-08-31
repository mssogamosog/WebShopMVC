﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebShopMVC.Models
{
    public partial class WebShopDBContextInMemory : WebShopDBContext, IWebShopDBContext
	{
        public WebShopDBContextInMemory()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

				optionsBuilder.UseInMemoryDatabase("InMemDb");
			}
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CartProducts>(entity =>
			{
				entity.HasKey(e => new { e.ProductId, e.CartId });

				entity.HasOne(d => d.Cart)
					.WithMany(p => p.CartProducts)
					.HasForeignKey(d => d.CartId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_CartProducts_Cart");

				entity.HasOne(d => d.Product)
					.WithMany(p => p.CartProducts)
					.HasForeignKey(d => d.ProductId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_CartProducts_Product");
			});

			modelBuilder.Entity<Category>(entity =>
			{
				entity.Property(e => e.Description).HasMaxLength(50);
			});

			modelBuilder.Entity<Customer>(entity =>
			{
				entity.Property(e => e.ContactInfo).HasMaxLength(50);

				entity.Property(e => e.FirtsName).HasMaxLength(50);

				entity.Property(e => e.LastName).HasMaxLength(50);

				entity.HasOne(d => d.Cart)
					.WithMany(p => p.Customer)
					.HasForeignKey(d => d.CartId)
					.HasConstraintName("FK_Customer_Cart");
			});

			modelBuilder.Entity<Order>(entity =>
			{
				entity.HasOne(d => d.Customer)
					.WithMany(p => p.Order)
					.HasForeignKey(d => d.CustomerId)
					.HasConstraintName("FK_Order_Customer");
			});

			modelBuilder.Entity<Product>(entity =>
			{
				entity.Property(e => e.Name).HasMaxLength(50);

				entity.Property(e => e.Quantity).HasMaxLength(50);
			});

			modelBuilder.Entity<ProductCategory>(entity =>
			{
				entity.HasKey(e => new { e.ProductId, e.CategoryId });

				entity.HasOne(d => d.Category)
					.WithMany(p => p.ProductCategory)
					.HasForeignKey(d => d.CategoryId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_ProductCategory_Category");

				entity.HasOne(d => d.Product)
					.WithMany(p => p.ProductCategory)
					.HasForeignKey(d => d.ProductId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_ProductCategory_Product");
			});

			modelBuilder.Entity<ProductOrder>(entity =>
			{
				entity.HasKey(e => new { e.ProductId, e.OrderId });

				entity.HasOne(d => d.Order)
					.WithMany(p => p.ProductOrder)
					.HasForeignKey(d => d.OrderId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_ProductOrder_Order");

				entity.HasOne(d => d.Product)
					.WithMany(p => p.ProductOrder)
					.HasForeignKey(d => d.ProductId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_ProductOrder_Product");
			});
			modelBuilder.Entity<Customer>().HasData(
			new { CustomerId = 1 ,
				ContactInfo = "123456" ,
				FirstName = "Pedro",
				LasttName = "Sin" }
			);
		}
    }
}
