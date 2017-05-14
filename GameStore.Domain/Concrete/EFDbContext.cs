using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base ("GamesDB") { }
        public DbSet<Game> Games { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}