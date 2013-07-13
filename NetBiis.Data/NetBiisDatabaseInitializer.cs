using NetBiis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace NetBiis.Data
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

            var defaultAdress = new Adress {
                                    City = "s. jose do rio preto",
                                    Number = 1245,
                                    State = "sp",
                                    StreetName = "portal",
                                    Suite = "abc",
                                    Zipcode = "1234"
                                };

            var defaultContact = new Contact {
                                    MainContact = "Talita",
                                    Phone = "22 2222 2222",
                                    Position = "s"
                                };

            var users = new[] {
                new User {
                    Email = "a@a.com",
                    Password = "1234",
                    ReceivePromotions = false,
                    CallSpecialist = true,
                    CompanyName = "Netbiis",
                    Document = documents[0],
                    Adress = defaultAdress,
                    Contact = defaultContact
                },
                new User {
                    Email = "b@a.com",
                    Password = "1234",
                    ReceivePromotions = false,
                    CallSpecialist = true,
                    CompanyName = "Netbiis",
                    Document = documents[1],
                    Adress = defaultAdress,
                    Contact = defaultContact
                },
                new User {
                    Email = "c@a.com",
                    Password = "1234",
                    ReceivePromotions = false,
                    CallSpecialist = true,
                    CompanyName = "Netbiis",
                    Document = documents[2],
                    Adress = defaultAdress,
                    Contact = defaultContact
                }
            };

            Array.ForEach(users, user =>
            {
                context.User.Add(user);
            });
            
            context.SaveChanges();
        }
    }
}
