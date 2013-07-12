using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace NetBiis.Models
{
    public class NetBiisDatabaseInitializer :
           DropCreateDatabaseIfModelChanges<NetBiisDbContext>
    {
        protected override void Seed(NetBiisDbContext context)
        {
            var documents = new[] {
               new Document { PIN = "01.137.077/0007-27" },
               new Document { PIN = "02.137.077/0007-27" },
               new Document { PIN = "03.137.077/0007-27" },
               new Document { PIN = "04.137.077/0007-27" },
               new Document { PIN = "05.137.077/0007-27" },
               new Document { PIN = "06.137.077/0007-27" }
           };

            Array.ForEach(documents, document =>
            {
                context.Document.Add(document);
            });
            
            context.SaveChanges();
        }
    }
}
