using NetBiis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;


namespace NetBiis.Data.Configuration
{
    public class DocumentConfiguration : EntityTypeConfiguration<Document>
    {
        public DocumentConfiguration()
        {
            HasKey(q => q.PIN);
        }
    }
}
