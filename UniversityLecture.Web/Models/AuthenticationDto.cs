using System.ComponentModel.DataAnnotations;

namespace UniversityLecture.WEB.Models
{
    public class AuthenticationDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
