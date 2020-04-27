using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auction.Persistence
{
    public class DbEventStorage :  DbContext
    {
        public DbEventStorage(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EventStoreDao> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
