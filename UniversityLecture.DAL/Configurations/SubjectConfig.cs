using System.Data.Entity.ModelConfiguration;
using UniversityLecture.Core;

namespace UniversityLecture.DAL.Configurations
{
    class SubjectConfig : EntityTypeConfiguration<Subject>
    {
        public SubjectConfig()
        {
            HasKey(p => p.ID);
            Property(p => p.Title).IsRequired().HasMaxLength(255);
            Property(p => p.Duration).IsRequired().HasColumnType("time");
        }
    }
}
