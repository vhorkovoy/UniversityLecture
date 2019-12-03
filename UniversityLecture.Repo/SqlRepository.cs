using System.Collections.Generic;
using UniversityLecture.DAL.Interfaces;
using System.Linq;
using UniversityLecture.Repo.Interfaces;


namespace UniversityLecture.Repo
{
    public class SqlRepository : IRepository
    {
        public SqlRepository(IDbContext context)
        {
            _Context = context;
        }

        private readonly IDbContext _Context;

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _Context.Set<T>().AsQueryable();
        }
       
        public void Create<T>(T entity) where T : class
        {
            //if (entity is IValidated)
            //{
            //    if(!((IValidated)entity).Validate(this, out List<string> errors))
            //        throw new DbEntityValidationException($"[{string.Join(", ", errors)}]");
            //}
            _Context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _Context.Set<T>().Remove(entity);
        }

        public void SaveChanges()
        {
            _Context.SaveChanges();
        }
        public void Dispose()
        {
            if (_Context != null)
            {
                _Context.Dispose();
            }
        }
    }
}
