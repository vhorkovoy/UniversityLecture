using System.Data.Entity.ModelConfiguration;
using UniversityLecture.Core;

namespace UniversityLecture.DAL.Configurations
{
    class ReservationConfig : EntityTypeConfiguration<Reservation>
    {
        public ReservationConfig()
        {
            HasKey(p => p.ID);
            Property(p => p.LectureHallId).IsRequired();
            Property(p => p.LecturerId).IsRequired();
            Property(p => p.StartDate).IsRequired().HasColumnType("datetime");
            Property(p => p.EndDate).IsRequired().HasColumnType("datetime");
        }
    }
}
