using System;
using System.Linq;


namespace UniversityLecture.Repo.Interfaces
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> GetAll<T>() where T : class;
        void Create<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

       void SaveChanges();
    }
}
