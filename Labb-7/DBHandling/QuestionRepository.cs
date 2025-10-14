using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7.DBHandling
{
    internal class QuestionRepository(QuizDbContext quizDbContext) : IRepository<Question>
    {
        public QuizDbContext QuizDbContext = quizDbContext;
        // Adds a question to database and saves changes
        public void Add(Question question)
        {
            QuizDbContext.Questions.Add(question);
            QuizDbContext.SaveChanges();
        }
        // Removes a question from the database and saves changes
        public void Remove(Question question)
        {
            QuizDbContext.Questions.Remove(question);
            QuizDbContext.SaveChanges();
        }
        // Returns all questions with options as an IEnumerable so they are easier to sort
        public IEnumerable<Question> GetAll()
        {
            return QuizDbContext.Questions.Include(question => question.Options);
        }
        // Updates database player with a player object
        public void Update(Question question)
        {
            QuizDbContext.Update(question);
            QuizDbContext.SaveChanges();
        }
        // Gets random questions using orderby to sort list randomly and then by taking the amount specified, returns a list
        public List<Question> GetRandomQuestions(int amount)
        {
            Random random = new Random();
            if (amount <= QuizDbContext.Questions.Count())
            {
                // Orders by random, then takes amount, include options for question and return as list
                var list = QuizDbContext.Questions.OrderBy(question => EF.Functions.Random()).Take(amount).Include(question => question.Options).ToList();
                return list;

            }
            return new List<Question>();
        }
    }
}