using System;
using System.Collections.Generic;
using UniversityLecture.Core;
using UniversityLecture.DAL.Interfaces;
using UniversityLecture.DAL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace UniversityLecture.DAL
{

    public partial class ULDbContext : DbContext, IDbContext
    {
        string _ConnectionString;
        public ULDbContext(string connect)
        {
            _ConnectionString = connect;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_ConnectionString);
        }
        public DbSet<LectureHall> LectureHalls { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<User> Users { get; set; }
        DbSet<T> IDbContext.Set<T>()
        {
            return Set<T>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LectureHallConfig());
            modelBuilder.ApplyConfiguration(new LecturerConfig());
            modelBuilder.ApplyConfiguration(new ReservationConfig());
            modelBuilder.ApplyConfiguration(new SubjectConfig());

            modelBuilder.Entity<Subject>().HasData(
                new Subject
                {
                    ID = 1,
                    Title = "Subject 1",
                    Duration = new TimeSpan(1, 30, 0)
                },
                new Subject
                {
                    ID = 2,
                    Title = "Subject 2",
                    Duration = new TimeSpan(2, 0, 0)
                },
                new Subject
                {
                    ID = 3,
                    Title = "Subject 3",
                    Duration = new TimeSpan(2, 30, 0)
                },
                new Subject
                {
                    ID = 4,
                    Title = "Subject 4",
                    Duration = new TimeSpan(2, 30, 0)
                });

            modelBuilder.Entity<LectureHall>().HasData(
                 new LectureHall()
                 {
                     ID = 1,
                     Number = "A1",
                     OpenFrom = 8,
                     OpenTo = 18
                 },
                 new LectureHall()
                 {
                     ID = 2,
                     Number = "A2",
                     OpenFrom = 8,
                     OpenTo = 14
                 },
                 new LectureHall()
                 {
                     ID = 3,
                     Number = "B1",
                     OpenFrom = 14,
                     OpenTo = 18
                 });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    ID = 1,
                    FirstName = "Jim",
                    LastName = "Bolton",
                    Login = "demo",
                    Password = "demo"
                });

            modelBuilder.Entity<Lecturer>().HasData(
                 new Lecturer()
                 {
                     ID = 1,
                     FirstName = "James",
                     LastName = "Baker",
                     SubjectID = 1,
                 },
                 new Lecturer()
                 {
                     ID = 2,
                     FirstName = "Oliver",
                     LastName = "Ball",
                     SubjectID = 2,
                 },
                 new Lecturer()
                 {
                     ID = 3,
                     FirstName = "Harry ",
                     LastName = "Bailey",
                     SubjectID = 3,
                 },
                 new Lecturer()
                 {
                     ID = 4,
                     FirstName = "Jack",
                     LastName = "Bilder",
                     SubjectID = 4,
                 });
        }
    }
}
