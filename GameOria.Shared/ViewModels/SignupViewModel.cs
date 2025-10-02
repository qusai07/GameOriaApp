

using GameOria.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace GameOria.Shared.ViewModels
{
    public class SignupViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
