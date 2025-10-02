using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Shared.ViewModels
{
    public class VerifyOtpViewModel
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "OTP is required")]
        public string Otp { get; set; }
    }
}
