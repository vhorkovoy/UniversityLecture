using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityLecture.Core;

namespace UniversityLecture.DAL.Configurations
{
    class LectureHallConfig : IEntityTypeConfiguration<LectureHall>
    {
        public void Configure(EntityTypeBuilder<LectureHall> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.Number).IsRequired().HasMaxLength(10);
            builder.Property(p => p.OpenFrom).IsRequired();
            builder.Property(p => p.OpenTo).IsRequired();
        }
    }
}
