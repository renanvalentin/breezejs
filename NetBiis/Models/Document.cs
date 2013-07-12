using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetBiis.Models
{
    public class Document
    {
        public string PIN { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}