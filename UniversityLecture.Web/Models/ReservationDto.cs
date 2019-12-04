using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace UniversityLecture.WEB.Models
{
    [DataContract(Name = "Reservation")]
    public class ReservationDto
    {
        [Required]
        [DataMember(Order = 1)]
        public int ID { get; set; }

        [Required]
        [DataMember(Order = 2)]
        [DisplayFormat(DataFormatString = "dd.MM.yyyy")]
        public string Date { get; set; }

        [Required]
        [DataMember(Order = 2)]
        public string StartAt { get; set; }

        [Required]
        [DataMember(Order = 3)]
        public string EndAt { get; set; }

        [Required]
        [DataMember(Order = 4)]
        public int LectureHallId { get; set; }

        //[DataMember(Order = 5)]
        //public LectureHallDto LectureHall { get; set; }

        [Required]
        [DataMember(Order = 6)]
        public int LecturerId { get; set; }

        //[DataMember(Order = 7)]
        //public LecturerDto Lecturer { get; set; }
    }
}