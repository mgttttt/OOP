using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UI
{

    internal class GameDbContext : DbContext
    {
        public DbSet<Game> Game { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {

        }

    }
}
