using System.ComponentModel.DataAnnotations;

namespace GameOria.Shared.ViewModels
{
    public class CompleteRegistrationVM
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string MobileNumber { get; set; }


        [Required, Display(Name = "Full Name")]
        public string FullName { get; set; }


        [Required, EmailAddress, Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required, MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MinLength(6), DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }

}
