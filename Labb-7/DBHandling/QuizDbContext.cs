using Microsoft.EntityFrameworkCore;

namespace Labb_7.DBHandling
{
    // Inherits DbContext, which gets us access to entity framework related methods and variables
    internal class QuizDbContext : DbContext
    {
        // Declare properties of classes we want to save inside database and query.
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Player> Players { get; set; }

        // Set what database we are using, in this case we are using a sqlite database called quiz.db. Which is first created inside constructor of ConsoleUI
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=quiz.db");
        }
    }
}
