using System.ComponentModel.DataAnnotations;
using TestProject.Validators;

namespace TestProject.Models
{
    public class Candidate
    {

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Key]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
        public string PhoneNumber { get; set; }
        public string LinkedinUrl { get; set; }
        public string GitHubUrl { get; set; }

        public TimeSpan? CallTimeStart { get; set; }

        [CallTimeEndValidation("CallTimeStart")]
        public TimeSpan? CallTimeEnd { get; set; }
    }
}
