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

        public void Add(Player player)
        {
            QuizDbContext.Players.Add(player);
            QuizDbContext.SaveChanges();
        }

        public void Delete(Player player)
        {
            QuizDbContext.Remove(player);
            QuizDbContext.SaveChanges();
        }

        public IEnumerable<Player> GetAll()
        {
            return QuizDbContext.Players;
        }

        public Player GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Player player)
        {
            QuizDbContext.Players.Update(player);
            QuizDbContext.SaveChanges();
        }
    }
}
