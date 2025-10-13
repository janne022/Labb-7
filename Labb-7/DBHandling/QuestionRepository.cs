using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7.DBHandling
{
    // LINQ queries for fetching questions
    internal class QuestionRepository(QuizDbContext quizDbContext) : IRepository<Question>
    {
        public QuizDbContext QuizDbContext = quizDbContext;

        public void Add(Question question)
        {
            QuizDbContext.Questions.Add(question);
            QuizDbContext.SaveChanges();
        }

        public void Delete(Question question)
        {
            QuizDbContext.Questions.Remove(question);
            QuizDbContext.SaveChanges();
        }
        // Returns all questions with options as an IEnumerable
        public IEnumerable<Question> GetAll()
        {
            return QuizDbContext.Questions.Include(question => question.Options);
        }

        public Question GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Question question)
        {
            QuizDbContext.Update(question);
            QuizDbContext.SaveChanges();
        }
        // Gets random questions and returns a list
        public List<Question> getRandomQuestions(int amount)
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