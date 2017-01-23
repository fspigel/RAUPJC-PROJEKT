using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kravate4.Models;

namespace Kravate4.Data
{
    public class KravateDbContext : DbContext
    {
        public KravateDbContext (DbContextOptions<KravateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Politician> Politician { get; set; }

        public DbSet<Rating> Rating { get; set; }

        public DbSet<Comment> Comment { get; set; }
    }
}
