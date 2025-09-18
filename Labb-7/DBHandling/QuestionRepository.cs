using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7.DBHandling
{
    // LINQ queries for fetching questions
    internal class QuestionRepository : IRepository<Question>
    {
        public QuizDbContext QuizDbContext;
        public QuestionRepository(QuizDbContext quizDbContext)
        {
            QuizDbContext = quizDbContext;
        }
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
    }
}