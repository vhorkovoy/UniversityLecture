
namespace UniversityLecture.Core
{
    public class Lecturer : BaseEntity<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int SubjectID { get; set; }

        public Subject Subject { get; set; }
    }
}
