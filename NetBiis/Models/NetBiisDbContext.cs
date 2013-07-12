using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NetBiis.Models
{
    public class NetBiisDbContext : DbContext
    {
              static NetBiisDbContext()
        {
            Database.SetInitializer(new NetBiisDatabaseInitializer());
        }

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

    public class DocumentConfiguration : EntityTypeConfiguration<Document>
    {
        public DocumentConfiguration()
        {
            HasKey(q => q.PIN);

            HasMany(q => q.Users).WithMany();
        }
    }

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(q => q.Id);

            HasRequired(q => q.Document);

            Property(q => q.Email).IsRequired().HasMaxLength(30);
            Property(q => q.Password).IsRequired().HasMaxLength(30);
            Property(q => q.CompanyName).IsRequired().HasMaxLength(30);

            Property(q => q.Adress.StreetName).IsRequired().HasMaxLength(30);
            Property(q => q.Adress.Number).IsRequired();
            Property(q => q.Adress.StreetName).HasMaxLength(10);
            Property(q => q.Adress.City).IsRequired().HasMaxLength(30);
            Property(q => q.Adress.State).IsRequired().HasMaxLength(30);
            Property(q => q.Adress.Zipcode).IsRequired().HasMaxLength(30);

            Property(q => q.Contact.MainContact).IsRequired().HasMaxLength(30);
            Property(q => q.Contact.Position).IsRequired().HasMaxLength(30);
            Property(q => q.Contact.Phone).IsRequired().HasMaxLength(30);
        }
    }
}