using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7.DBHandling
{
    // Helper class so that Questionrepository and Playerrepository does not have to implement their own CRUD interface
    internal class Repository<T> : IRepository<T> where T : class
    {
        protected QuizDbContext quizDbContext {  get; }

        public Repository(QuizDbContext context)
        {
            quizDbContext = context;
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
