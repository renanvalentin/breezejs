using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetBiis.Models
{
    public class User
    {
        public long Id { get; set; }
        public string PIN { get; set; }

        public string CompanyName { get; set; }
        

        public string Email { get; set; }
        public string Password { get; set; }

        public bool ReceivePromotions { get; set; }
        public bool CallSpecialist { get; set; }

        public virtual Document Document { get; set; }
        public virtual Adress Adress { get; set; }
        public virtual Contact Contact { get; set; }
    }
}