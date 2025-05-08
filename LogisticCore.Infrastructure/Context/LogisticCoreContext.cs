using LogisticCore.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCore.Infrastructure.Context
{
    public class LogisticCoreContext :DbContext
    {
        public LogisticCoreContext(DbContextOptions<LogisticCoreContext> options) : base(options)
        {
        }


        public DbSet<User>Users { get; set; }
        public DbSet<RefreshToken>RefreshTokens { get; set; }   
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LogisticCoreContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
