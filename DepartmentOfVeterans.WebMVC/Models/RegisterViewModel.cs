using System.ComponentModel.DataAnnotations;

namespace DepartmentOfVeterans.WebMVC.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The first name should be 50 characters or less")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "The last name should be 50 characters or less")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "The user name should be 50 characters or less")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "The password should have a minimum length of 6 characters")]
        public string Password { get; set; } = string.Empty;
    }
}
