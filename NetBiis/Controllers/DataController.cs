using Breeze.WebApi;
using NetBiis.Data;
using NetBiis.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetBiis.Controllers
{
    [BreezeController]
    public class DataController : ApiController
    {
        readonly EFContextProvider<NetBiisDbContext> _contextProvider =
    new EFContextProvider<NetBiisDbContext>();

        readonly DocumentRepository _documentRepository = new DocumentRepository();
        readonly UserRepository _userRepository = new UserRepository();

        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        [HttpGet]
        public Document Document(string pin)
        {
            return _documentRepository.GetById(pin);
        }

        [HttpGet]
        public bool VerifyExintingEmail(string email)
        {
            return _userRepository.VerifyExintingEmail(email);
        }
    }
}
