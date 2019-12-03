using System.Data.Entity.ModelConfiguration;
using UniversityLecture.Core;

namespace UniversityLecture.DAL.Configurations
{
    class LectureHallConfig : EntityTypeConfiguration<LectureHall>
    {
        public LectureHallConfig()
        {
            HasKey(p => p.ID);
            Property(p => p.Number).IsRequired().HasMaxLength(10);
            Property(p => p.OpenFrom).IsRequired();
            Property(p => p.OpenTo).IsRequired();
        }
    }
}
