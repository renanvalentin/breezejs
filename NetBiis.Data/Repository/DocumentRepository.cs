using NetBiis.Data.Contracts;
using NetBiis.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace NetBiis.Data
{
    public class DocumentRepository : IDocumentRepository
    {
        private DbContext _ctx { get; set; }

        public DocumentRepository()
        {
            _ctx = new NetBiisDbContext();
        }

        public Document GetById(string pin)
        {
            var parameter = new SqlParameter("@PIN", SqlDbType.Text);
            parameter.Value = pin;

            return _ctx.Database.SqlQuery<Document>("exec DocumentById @PIN", parameter).FirstOrDefault();
        }
    }
}
