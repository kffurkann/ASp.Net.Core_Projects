﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        //Product sınıfına karşılık gelen bir veritabanı tablosunu temsil eder.
        //EF Core, bu DbSet üzerinden Product varlıklarını sorgulayabilir, ekleyebilir, güncelleyebilir ve silebilir.
        //DbSet, tablolarla çalışmanızı sağlayan EF Core'un sağladığı bir sınıftır.

        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
            .HasMany(e => e.Categories)
            .WithMany(e => e.Products)
            .UsingEntity<ProductCategory>();

            modelBuilder.Entity<Category>()
                .HasIndex(u => u.Url)//sql de ef ile bu property'i unique yapmış oluruz
                .IsUnique();

            modelBuilder.Entity<Product>().HasData(
                new List<Product>() {
                new() { Id=1, Name="Samsung S24", Price=50000, Description="güzel telefon"},//new Product(){
                new() { Id=2, Name="Samsung S25", Price=60000, Description="güzel telefon"},
                new() { Id=3, Name="Samsung S26", Price=70000, Description="güzel telefon"},
                new() { Id=4, Name="Samsung S27", Price=80000, Description="güzel telefon"},
                new() { Id=5, Name="Samsung S28", Price=90000, Description="güzel telefon"},
                new() { Id=6, Name="Samsung S29", Price=100000, Description="güzel telefon"}
                }
            );

            modelBuilder.Entity<Category>().HasData(
            new List<Category>() {
                new () { Id = 1,  Name = "Telefon", Url = "telefon"},
                new () { Id = 2,  Name = "Elektronik", Url = "elektronik"},
                new () { Id = 3,  Name = "Beyaz Eşya", Url = "beyaz-esya"}  // extension method, slug dotnet 
            }
        );

            modelBuilder.Entity<ProductCategory>().HasData(
                new List<ProductCategory>() {
                new ProductCategory() { ProductId=1, CategoryId=1},
                new ProductCategory() { ProductId=1, CategoryId=2},
                new ProductCategory() { ProductId=2, CategoryId=1},
                new ProductCategory() { ProductId=3, CategoryId=1},
                new ProductCategory() { ProductId=4, CategoryId=1},
                new ProductCategory() { ProductId=5, CategoryId=2},
                new ProductCategory() { ProductId=6, CategoryId=2},
                }
            );
        }
    }
}