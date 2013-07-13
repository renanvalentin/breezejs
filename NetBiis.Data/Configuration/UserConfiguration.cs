using NetBiis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace NetBiis.Data.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(q => q.Id);

            HasRequired(q => q.Document)
                .WithMany(q => q.Users)
                .HasForeignKey(fk => fk.PIN);

            Property(q => q.Email).IsRequired().HasMaxLength(30);
            Property(q => q.Password).IsRequired().HasMaxLength(30);
            Property(q => q.CompanyName).IsRequired().HasMaxLength(30);

            Property(q => q.Adress.StreetName).HasColumnName("StreetName").IsRequired().HasMaxLength(30);
            Property(q => q.Adress.Number).HasColumnName("Number").IsRequired();
            Property(q => q.Adress.StreetName).HasColumnName("StreetName").HasMaxLength(10);
            Property(q => q.Adress.City).HasColumnName("City").IsRequired().HasMaxLength(30);
            Property(q => q.Adress.State).HasColumnName("State").IsRequired().HasMaxLength(30);
            Property(q => q.Adress.Suite).HasColumnName("Suite").IsRequired().HasMaxLength(30);
            Property(q => q.Adress.Zipcode).HasColumnName("Zipcode").IsRequired().HasMaxLength(30);

            Property(q => q.Contact.MainContact).HasColumnName("MainContact").IsRequired().HasMaxLength(30);
            Property(q => q.Contact.Position).HasColumnName("Position").IsRequired().HasMaxLength(30);
            Property(q => q.Contact.Phone).HasColumnName("Phone").IsRequired().HasMaxLength(30);
        }
    }
}
