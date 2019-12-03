using System;
using System.Collections.Generic;
using System.Data.Entity;
using UniversityLecture.Core;
using UniversityLecture.DAL.Interfaces;
using UniversityLecture.DAL.Configurations;

namespace UniversityLecture.DAL
{

    public partial class ULDbContext : DbContext, IDbContext
    {
        static ULDbContext()
        {
            Database.SetInitializer(new DummyInitializer());
            
        }
        public ULDbContext(string connect)
            : base(connect)
        {
        }
        public DbSet<LectureHall> LectureHalls { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<User> Users { get; set; }
        IDbSet<T> IDbContext.Set<T>()
        {
            return Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new LectureHallConfig());
            modelBuilder.Configurations.Add(new LecturerConfig());
            modelBuilder.Configurations.Add(new ReservationConfig());
            modelBuilder.Configurations.Add(new SubjectConfig());
        }
    }
}
