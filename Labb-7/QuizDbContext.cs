using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7
{
    internal class QuizDbContext : DbContext
    {
        DbSet<Question> questions;
        DbSet<Option> options;
        DbSet<Player> players;
    }
}
