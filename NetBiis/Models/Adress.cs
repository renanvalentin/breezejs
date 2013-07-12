using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetBiis.Models
{
    public class Adress
    {
        public long Number { get; set; }
        public string StreetName { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
    }
}