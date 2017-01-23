using Kravate4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kravate4.Models
{
    public class Politician
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        private DateTime DateOfBirth { get; }

        public Guid PartyID { get; set; }

        public virtual List<Rating> Ratings { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public float Score {
            get
            {
                if (Ratings.Count == 0) return 0;
                float s = 0;
                foreach (Rating r in Ratings) s += r.Value;
                return s / Ratings.Count;
            }
        }

        //public float Score
        //{
        //    get
        //    {
        //        KravateDbContext context = new KravateDbContext();
        //        var ratings = context.Rating.Where(r => r.PoliticianID == ID).ToList();
        //        float value = 0;
        //        foreach (Rating r in ratings)
        //        {
        //            value += r.Value;
        //        }
        //        return value / ratings.Count();
        //    }
        //}

        public int Age
        {
            get
            {
                return (DateTime.Now.Year - DateOfBirth.Year - 1) +
                    (((DateTime.Now.Month > DateOfBirth.Month) ||
                    ((DateTime.Now.Month == DateOfBirth.Month) && (DateTime.Now.Day >= DateOfBirth.Day))) ? 1 : 0);
            }
        }

        public int VoteCount {
            get
            {
                return Ratings.Count();
            }
        }

        public float Popularity { get; set; }

        public Politician()
        {
            ID = Guid.NewGuid();

            Name = "NAME";

            Surname = "SURNAME";

            Ratings = new List<Rating>();

            Comments = new List<Comment>();
        }

        public Politician(DateTime Birthday, string name, string surname, Guid partyID)
        {
            ID = new Guid();

            Name = name;

            Surname = surname;

            DateOfBirth = Birthday;

            PartyID = partyID;

            Ratings = new List<Rating>();
        }
    }
}
