using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace UniversityLecture.WEB.Models
{
    [DataContract(Name = "Subject")]
    public class SubjectDto
    {
        [Required]
        [DataMember(Order = 1)]
        public int ID { get; set; }

        [Required]
        [DataMember(Order = 2)]
        public string Title { get; set; }

        [Required]
        [DataMember(Order = 3)]
        public string Duration { get; set; }
    }
}