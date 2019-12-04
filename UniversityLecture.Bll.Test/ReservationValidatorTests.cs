using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using UniversityLecture.Bll.Validators;
using UniversityLecture.Core;
using UniversityLecture.Repo.Interfaces;

namespace UniversityLecture.Bll.Test
{
    public class ReservationValidatorTests
    {
        ReservationValidator _Validator;
        [SetUp]
        public void Setup()
        {
            var mockRepo = new Mock<IRepository>(MockBehavior.Strict);
            mockRepo.Setup(p => p.GetAll<Reservation>()).Returns(new List<Reservation>()
            {
                new Reservation()
                {
                    ID = 1,
                    LectureHallId = 1,
                    LecturerId = 1,
                    StartDate = DateTime.Today.AddHours(12),
                    EndDate = DateTime.Today.AddHours(15)
                }
            }.AsQueryable());
            mockRepo.Setup(p => p.GetAll<LectureHall>()).Returns(new List<LectureHall>()
            {
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
                        OpenFrom = 9,
                        OpenTo = 17
                    },
                new LectureHall()
                    {
                        ID = 3,
                        Number = "B1",
                        OpenFrom = 14,
                        OpenTo = 18
                    },
            }.AsQueryable());
            mockRepo.Setup(p => p.GetAll<Lecturer>()).Returns(new List<Lecturer>()
            {
                new Lecturer()
                     {
                         ID = 1,
                         FirstName ="James",
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
                     },
            }.AsQueryable());
            _Validator = new ReservationValidator(mockRepo.Object);
        }

        [Test]
        public void TestSameSatrtEndDay()
        {
            var reservation = new Reservation()
            {
                LectureHallId = 1,
                LecturerId = 1,
                StartDate = new DateTime(2019, 12, 3, 8, 0, 0),
                EndDate = new DateTime(2019, 12, 4, 11, 0, 0)
            };
            var results = _Validator.Validate(reservation);
            Assert.IsFalse(results.IsValid);
            CollectionAssert.Contains(results.Errors.Select(e => e.ErrorMessage),
                ReservationValidator.MsgWrongDay);
        }
        
        [Test]
        public void TestSatrtLessThenEnd()
        {
            var reservation = new Reservation()
            {
                LectureHallId = 1,
                LecturerId = 1,
                StartDate = new DateTime(2019, 12, 24, 10, 0, 1),
                EndDate = new DateTime(2019, 12, 24, 10, 0, 0)
            };
            var results = _Validator.Validate(reservation);
            Assert.IsFalse(results.IsValid);
            CollectionAssert.Contains(results.Errors.Select(e => e.ErrorMessage),
                ReservationValidator.MSgStartBigerThenEnd);
        }

        [TestCase(7, 12)]
        [TestCase(8, 18)]
        public void TestWorkingHoursLimit(int sartHours, int endHours)
        {
            var reservation = new Reservation()
            {
                LectureHallId = 1,
                LecturerId = 1,
                StartDate = new DateTime(2019, 11, 27, sartHours, 59, 59),
                EndDate = new DateTime(2019, 11, 27, endHours, 0, 1)
            };
            var results = _Validator.Validate(reservation);
            Assert.IsFalse(results.IsValid);
            CollectionAssert.Contains(results.Errors.Select(e => e.ErrorMessage),
                ReservationValidator.MsgWrongWorkingHours);
        }
        
        [Test]
        public void TestMinDurationLimit()
        {
            var reservation = new Reservation()
            {
                LectureHallId = 1,
                LecturerId = 1,
                StartDate = new DateTime(2019, 11, 27, 8, 0, 1),
                EndDate = new DateTime(2019, 11, 27, 11, 0, 0)
            };
            var results = _Validator.Validate(reservation);
            Assert.IsFalse(results.IsValid);
            CollectionAssert.Contains(results.Errors.Select(e => e.ErrorMessage),
                ReservationValidator.MsgWrongMinDuration);
        }
        
        [Test]
        public void TestNotExistsLecturer()
        {
            var reservation = new Reservation()
            {
                LectureHallId = 1,
                LecturerId = 0,
                StartDate = new DateTime(2019, 11, 27, 8, 0, 0),
                EndDate = new DateTime(2019, 11, 27, 11, 0, 0)
            };
            var results = _Validator.Validate(reservation);
            Assert.IsFalse(results.IsValid);
            CollectionAssert.Contains(results.Errors.Select(e => e.ErrorMessage),
                ReservationValidator.MsgWrongLecturer);
        }
        
        [Test]
        public void TestNotExistsHall()
        {
            var reservation = new Reservation()
            {
                LectureHallId = 0,
                LecturerId = 1,
                StartDate = new DateTime(2019, 11, 27, 8, 0, 0),
                EndDate = new DateTime(2019, 11, 27, 11, 0, 0)
            };
            var results = _Validator.Validate(reservation);
            Assert.IsFalse(results.IsValid);
            CollectionAssert.Contains(results.Errors.Select(e => e.ErrorMessage),
                ReservationValidator.MsgWrongHall);
        }
        
        [TestCase(2, 8, 11, false)]
        [TestCase(2, 9, 17, true)]
        [TestCase(2, 15, 18, false)]
        [TestCase(1, 8, 18, true)]
        public void TestClosedHall(int hallID, int sartHours, int endHours, bool expected)
        {
            var reservation = new Reservation()
            {
                LectureHallId = hallID,
                LecturerId = 1,
                StartDate = new DateTime(2019, 11, 26, sartHours, 0, 0),
                EndDate = new DateTime(2019, 11, 26, endHours, 0, 0)
            };
            var results = _Validator.Validate(reservation);
            Assert.AreEqual(expected, results.IsValid);
            if (!expected)
            {
                CollectionAssert.Contains(results.Errors.Select(e => e.ErrorMessage),
                    ReservationValidator.MsgWrongPeriodHallClossed);
            }
        }

        [TestCase(1, 8, 13, false)]
        [TestCase(1, 12, 15, false)]
        [TestCase(1, 14, 17, false)]
        [TestCase(1, 15, 18, true)]
        [TestCase(2, 9, 17, true)]
        public void TestPeriodOverLap(int hallID, int sartHours, int endHours, bool expected)
        {
            var reservation = new Reservation()
            {
                LectureHallId = hallID,
                LecturerId = 1,
                StartDate = DateTime.Today.AddHours(sartHours),
                EndDate = DateTime.Today.AddHours(endHours),
            };
            var results = _Validator.Validate(reservation);
            Assert.AreEqual(expected, results.IsValid);
            if (!expected)
            {
                CollectionAssert.Contains(results.Errors.Select(e => e.ErrorMessage),
                   ReservationValidator.MsgWrongPeriodHallBusy);
            }
        }
    }
}