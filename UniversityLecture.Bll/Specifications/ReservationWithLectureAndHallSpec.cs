using System;
using System.Collections.Generic;
using System.Text;
using UniversityLecture.Core;

namespace UniversityLecture.Bll.Specifications
{
    public class ReservationWithLectureAndHallSpec : BaseSpec<Reservation>
    {
        public ReservationWithLectureAndHallSpec()
        {
            AddInclude(r => r.Lecturer);
            AddInclude(r => r.LectureHall);
        }
    }
}
