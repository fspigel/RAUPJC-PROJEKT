using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kravate4.Models
{
    public class Comment
    {
        public Guid ID { get; set; }

        public Guid PoliticianID { get; set; }

        public Politician Politician { get; set; }

        public Guid UserID { get; set; }

        public string Body { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public Comment()
        {
            ID = Guid.NewGuid();

            DateCreated = DateTime.Now;

            DateUpdated = DateCreated;
        }
    }
}
