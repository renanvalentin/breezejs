using NetBiis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBiis.Data.Contracts
{
    public interface IDocumentRepository
    {
        Document GetById(string pin);
    }
}
