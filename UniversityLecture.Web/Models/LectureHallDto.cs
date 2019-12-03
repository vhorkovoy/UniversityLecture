using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace UniversityLecture.WEB.Models
{
    [DataContract(Name = "LectureHall")]
    public class LectureHallDto
    {
        [Required]
        [DataMember(Order = 1)]
        public int ID { get; set; }

        [Required]
        [DataMember(Order = 1)]
        public string Number { get; set; }
      
        [Required]
        [DataMember(Order = 1)]
        public int OpenFrom { get; set; }

        [Required]
        [DataMember(Order = 1)]
        public int OpenTo { get; set; }
    }
}