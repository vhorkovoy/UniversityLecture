using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace UniversityLecture.WEB.Models
{
    [DataContract(Name = "Lecturer")]
    public class LecturerDto
    {
        [Required]
        [DataMember(Order = 1)]
        public int ID { get; set; }

        [Required]
        [DataMember(Order = 2)]
        public string FirstName { get; set; }

        [Required]
        [DataMember(Order = 3)]
        public string LastName { get; set; }

        [Required]
        [DataMember(Order = 4)]
        public int SubjectID { get; set; }

        [Required]
        [DataMember(Order = 5)]
        public SubjectDto Subject { get; set; }

    }
}