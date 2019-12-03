using System;

namespace UniversityLecture.Core
{
    public class Subject : BaseEntity<int>
    {
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
