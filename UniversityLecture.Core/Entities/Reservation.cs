using System;

namespace UniversityLecture.Core
{
    public class Reservation : BaseEntity<int>
    {
        public const string MsgWrongDay = "'Reservation start and end dates should be on the same day.'";
        public const string MSgStartBigerThenEnd = "'Reservation start time should be less then end time.'";
        public const string MsgWrongWorkingHours = "'All reservations should be done inside working hours 8-18 (it can't start before 8 and must finish at 18 at the very latest).'";
        public const string MsgWrongMinDuration = "'Reservation must last 3 hours at most.'";
        public const string MsgWrongLecturer = "'Lecturer is not valid.'";
        public const string MsgWrongHall = "'Lecture hall is not valid.'";
        public const string MsgWrongPeriodHallClossed = "'Reservation period is not allowed. (Hall is closed)'";
        public const string MsgWrongPeriodHallBusy = "'Reservation period is not allowed. (Hall is busy)'";
        public int LectureHallId { get; set; }
        public int LecturerId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public LectureHall LectureHall { get; set; }

        public Lecturer Lecturer { get; set; }

        //public bool Validate(IRepository repo, out List<string> errors)
        //{
        //    errors = new List<string>();
        //    if (StartDate.Date != EndDate.Date)
        //        errors.Add(MsgWrongDay);

        //    if (StartDate >= EndDate)
        //        errors.Add(MSgStartBigerThenEnd);

        //    if (StartDate.Hour < 8 || EndDate.Hour > 18)
        //        errors.Add(MsgWrongWorkingHours);

        //    if (EndDate - StartDate < new TimeSpan(hours: 3, 0, 0))
        //        errors.Add(MsgWrongMinDuration);

        //    if (!repo.GetAll<Lecturer>().Any(l => l.ID == LecturerId))
        //        errors.Add(MsgWrongLecturer);

        //    var hall = repo.GetById<LectureHall>(LectureHallId);
        //    if (hall == null)
        //    {
        //        errors.Add(MsgWrongHall);
        //    }
        //    else if (StartDate.Date.AddHours(hall.OpenFrom) > StartDate ||
        //             StartDate.Date.AddHours(hall.OpenTo) < EndDate)
        //    {
        //        errors.Add(MsgWrongPeriodHallClossed);
        //    }
        //    if (repo.GetAll<Reservation>().Any(r => LectureHallId == r.LectureHallId &&
        //        StartDate < r.EndDate && r.StartDate < EndDate))
        //        errors.Add(MsgWrongPeriodHallBusy);
        //    return !errors.Any();
        //}
    }
}
