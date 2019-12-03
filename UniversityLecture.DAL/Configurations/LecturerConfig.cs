using System.Data.Entity.ModelConfiguration;
using UniversityLecture.Core;

namespace UniversityLecture.DAL.Configurations
{
    class LecturerConfig : EntityTypeConfiguration<Lecturer>
    {
        public LecturerConfig()
        {
            HasKey(p => p.ID);
            Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            Property(p => p.LastName).IsRequired().HasMaxLength(50);
            HasRequired(p => p.Subject).WithMany().
                HasForeignKey(p => p.SubjectID).
                WillCascadeOnDelete(false);
        }
    }
}
