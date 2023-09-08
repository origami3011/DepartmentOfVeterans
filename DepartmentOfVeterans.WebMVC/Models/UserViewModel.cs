using DepartmentOfVeterans.WebMVC.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DepartmentOfVeterans.WebMVC.Models
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The user name should be 50 characters or less")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        public string UserPassword { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "The first name should be 50 characters or less")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The last name should be 50 characters or less")]
        public string? LastName { get; set; }
        public string? Picture { get; set; }
        public string? Gender { get; set; }
    }
}
