
namespace UniversityLecture.Core
{
    public class LectureHall : BaseEntity<int>
    {
        public string Number { get; set; }
        public int OpenFrom { get; set; }
        public int OpenTo { get; set; }
    }
}
