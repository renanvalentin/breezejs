using NetBiis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetBiis.Data.Contracts
{
    public interface IUserRepository
    {
        bool VerifyExintingEmail(string email);
        int Save(User user);
    }
}
