﻿using FreeCourse.Services.Order.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Infrastructure
{
    public class OrderDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "ordering";

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.OrderAggregate.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.OrderAggregate.Order>().ToTable("Orders", DEFAULT_SCHEMA);
            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().ToTable("OrderItems", DEFAULT_SCHEMA);

            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");

            //Aşağıdaki ownsOne ve WithOwner kullanımı şu işe yarıyor: Biz Order entity'si içerisinde bulunan Address sınıfının
            //veritabanında bir tabloya denk gelmeyeceğini Address class'ının içindeki property'lerin Order tablosunun içinde
            //generate olması gerektiğini söylüyoruz.
            //EF Core, Address için ayrı bir tablo oluşturmaz, onun yerine Order tablosunda ilgili sütunları oluşturur.
            modelBuilder.Entity<Domain.OrderAggregate.Order>().OwnsOne(x => x.Address).WithOwner();

            base.OnModelCreating(modelBuilder);
        }
    }
}
