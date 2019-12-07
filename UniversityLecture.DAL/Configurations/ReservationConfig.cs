using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityLecture.Core;

namespace UniversityLecture.DAL.Configurations
{
    class ReservationConfig : IEntityTypeConfiguration<Reservation>
    { 
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.LectureHallId).IsRequired();
            builder.Property(p => p.LecturerId).IsRequired();
            builder.Property(p => p.StartDate).IsRequired().HasColumnType("datetime");
            builder.Property(p => p.EndDate).IsRequired().HasColumnType("datetime");
        }
    }
}
