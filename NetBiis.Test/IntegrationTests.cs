using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using NetBiis.Data;
using System.Linq;
using NetBiis.Models;

namespace NetBiis.Test
{
    [TestClass]
    public class IntegrationTests
    {
        private NetBiisDbContext _db { get; set; }
        private DocumentRepository _documentRepository { get; set; }
        private UserRepository _userRepository { get; set; }

        [TestInitialize]
        public void Initialize()
        {
           Database.SetInitializer(new NetBiisDatabaseInitializer());
            _db = new NetBiisDbContext();
            _documentRepository = new DocumentRepository();
            _userRepository = new UserRepository();
        }

        [TestMethod]
        public void Load_Users_From_DataBase()
        {
            var users = _db.User.ToList<User>();

            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public void Retrieve_Document_Using_Stored_Procedure()
        {
            var document = _documentRepository.GetById("01.137.077/0007-27");

            Assert.IsNotNull(document);
        }

        [TestMethod]
        public void Retrieve_Document_Using_Stored_Procedure_With_Invalid_PIN()
        {
            var document = _documentRepository.GetById("01123.137.077/0007-27");

            Assert.IsNotNull(document);
        }

        [TestMethod]
        public void Verify_Exinting_Email()
        {
            var email = _userRepository.VerifyExintingEmail("po@a.com");

            Assert.IsTrue(email);
        }

        [TestMethod]
        public void Verify_Invalid_Email()
        {
            var email = _userRepository.VerifyExintingEmail("asdf@a.com");

            Assert.IsFalse(email);
        }

        [TestMethod]
        public void Insert_A_New_User()
        {
            var user = new User
            {
                Email = "po@a.com",
                Password = "1234",
                PIN = "04.137.077/0007-27",
                ReceivePromotions = false,
                CallSpecialist = true,
                CompanyName = "Netbiis",
                Adress = new Adress
                {
                    City = "s. jose do rio preto",
                    Number = 1245,
                    State = "sp",
                    StreetName = "portal",
                    Suite = "abc",
                    Zipcode = "1234"
                },
                Contact = new Contact
                {
                    MainContact = "Talita",
                    Phone = "22 2222 2222",
                    Position = "s"
                }
            };

            var rows = _userRepository.Save(user);

            Assert.AreEqual(rows, 1);
        }

    }
}
