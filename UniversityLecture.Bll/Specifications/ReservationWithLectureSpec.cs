using System;
using System.Collections.Generic;
using System.Text;
using UniversityLecture.Core;

namespace UniversityLecture.Bll.Specifications
{
    public class ReservationWithLectureSpec : BaseSpec<Reservation>
    {
        public ReservationWithLectureSpec(int lecturerId):
            base(r => r.LecturerId == lecturerId)
        {
            AddInclude(r => r.Lecturer);
        }
    }
}
