using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityLecture.Core;

namespace UniversityLecture.DAL.Configurations
{
    class LecturerConfig : IEntityTypeConfiguration<Lecturer>
    {
        public void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.HasOne(p => p.Subject).WithMany().HasForeignKey(p => p.SubjectID).
                OnDelete(DeleteBehavior.NoAction);
        }
    }
}
