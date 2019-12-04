using System;
using System.Linq;
using FluentValidation;
using UniversityLecture.Core;
using UniversityLecture.Repo.Interfaces;

namespace UniversityLecture.Bll.Validators
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public const string MsgWrongDay = "Reservation start and end dates should be on the same day.";
        public const string MSgStartBigerThenEnd = "Reservation start time should be less then end time.";
        public const string MsgWrongWorkingHours = "All reservations should be done inside working hours 8-18 (it can't start before 8 and must finish at 18 at the very latest).";
        public const string MsgWrongMinDuration = "Reservation must last 3 hours at most.";
        public const string MsgWrongLecturer = "Lecturer is not valid.";
        public const string MsgWrongHall = "Lecture hall is not valid.";
        public const string MsgWrongPeriodHallClossed = "Reservation period is not allowed. (Hall is closed)";
        public const string MsgWrongPeriodHallBusy = "Reservation period is not allowed. (Hall is busy)";
        
        private IRepository _Repo;
        public ReservationValidator(IRepository repo)
        {
            _Repo = repo;

            RuleFor(r => r.StartDate.Date).Equal(r => r.EndDate.Date).WithMessage(MsgWrongDay);
            
            RuleFor(r => r.StartDate).LessThan(r => r.EndDate).WithMessage(MSgStartBigerThenEnd);

            RuleFor(r => r).Must(HasValidWorkingHours).WithMessage(MsgWrongWorkingHours);
           
            RuleFor(r => r.EndDate).GreaterThanOrEqualTo(r => r.StartDate.AddHours(3)).
                WithMessage(MsgWrongMinDuration);

            RuleFor(r => r.LecturerId).Must(HasValidLecturer).WithMessage(MsgWrongLecturer);
            
            RuleFor(r => r.LectureHallId).Must(HasValidHall).WithMessage(MsgWrongHall).
                DependentRules(() => RuleFor(r => r).Must(HasOpenHall).
                WithMessage(MsgWrongPeriodHallClossed));
           
            RuleFor(r => r).Must(HasNoConflictPeriod).WithMessage(MsgWrongPeriodHallBusy);
        }
       
        private bool HasValidWorkingHours(Reservation resrevation)
        {
            return resrevation.StartDate >= resrevation.StartDate.Date.AddHours(8) &&
                 resrevation.StartDate <= resrevation.StartDate.Date.AddHours(18) &&
                 resrevation.EndDate >= resrevation.EndDate.Date.AddHours(8) &&
                 resrevation.EndDate <= resrevation.EndDate.Date.AddHours(18);
        }

        private bool HasValidLecturer(int lecturerId)
        {
            return _Repo.GetAll<Lecturer>().Any(l => l.ID == lecturerId);
        }
        private bool HasValidHall(int hallId)
        {
            return _Repo.GetAll<LectureHall>().Any(h => h.ID == hallId);
        }
        private bool HasOpenHall(Reservation resrevation)
        {
            var hall = _Repo.GetAll<LectureHall>().SingleOrDefault(h => h.ID == resrevation.LectureHallId);
            if (hall == null || 
                resrevation.StartDate.Date.AddHours(hall.OpenFrom) > resrevation.StartDate ||
                resrevation.StartDate.Date.AddHours(hall.OpenTo) < resrevation.EndDate)
                return false;

           return true;
        }
        private bool HasNoConflictPeriod(Reservation resrevation)
        {
            return !_Repo.GetAll<Reservation>().Any(r => resrevation.LectureHallId == r.LectureHallId &&
             resrevation.StartDate < r.EndDate && r.StartDate < resrevation.EndDate);
        }
    }
}
