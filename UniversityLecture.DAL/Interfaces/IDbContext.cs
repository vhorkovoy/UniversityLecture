using Microsoft.EntityFrameworkCore;
using System;

namespace UniversityLecture.DAL.Interfaces
{
    public interface IDbContext : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
    }
}
