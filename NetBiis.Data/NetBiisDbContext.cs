using NetBiis.Data.Configuration;
using NetBiis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NetBiis.Data
{
    public class NetBiisDbContext : DbContext
    {
        public NetBiisDbContext()
            : base(nameOrConnectionString: "NetBiis") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new DocumentConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
        }

        public DbSet<Document> Document { get; set; }
        public DbSet<User> User { get; set; }
    }
}