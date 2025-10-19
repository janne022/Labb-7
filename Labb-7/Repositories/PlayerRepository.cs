using Labb_7.Data;
using Labb_7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7.DBHandling
{
    internal class PlayerRepository(QuizDbContext quizDbContext) : IRepository<Player>
    {
        public QuizDbContext QuizDbContext = quizDbContext;
        // Adds a player to database and saves changes
        public void Add(Player player)
        {
            QuizDbContext.Players.Add(player);
            QuizDbContext.SaveChanges();
        }
        // Removes a player from the database and saves changes
        public void Remove(Player player)
        {
            QuizDbContext.Remove(player);
            QuizDbContext.SaveChanges();
        }
        // Returns all players from database as a IEnumerable so they are easier to sort
        public IEnumerable<Player> GetAll()
        {
            return QuizDbContext.Players;
        }
        // Updates database player with a player object
        public void Update(Player player)
        {
            QuizDbContext.Players.Update(player);
            QuizDbContext.SaveChanges();
        }
    }
}
