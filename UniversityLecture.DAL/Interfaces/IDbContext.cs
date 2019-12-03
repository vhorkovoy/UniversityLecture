using System;
using System.Data.Entity;

namespace UniversityLecture.DAL.Interfaces
{
    public interface IDbContext : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
    }
}
