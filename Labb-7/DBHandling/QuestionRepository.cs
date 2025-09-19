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

        public void Delete(Question entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetAll()
        {
            throw new NotImplementedException();
        }

        public Question GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Question entity)
        {
            throw new NotImplementedException();
        }
        // Gets random questions and returns a list
        public List<Question> getRandomQuestions(int amount)
        {
            Random random = new Random();
            using (var context = new QuizDbContext())
            {
                if (amount <= context.Questions.Count())
                {
                    // Orders by random, then takes amount, include options for question and return as list
                    var list = context.Questions.OrderBy(question => EF.Functions.Random()).Take(amount).Include(question => question.Options).ToList();
                    return list;

                }
            }
            return new List<Question>();
        }
    }
}