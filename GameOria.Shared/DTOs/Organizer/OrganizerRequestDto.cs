using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Shared.DTOs.Organizer
{
    public class OrganizerRequestDto
    {
        public string UserFullName { get; set; } 
        public string StoreName { get; set; }   
        public string? BusinessEmail { get; set; }
        public string IdentityNumber { get; set; } 
        public string? PhoneNumber { get; set; }                  
    }

}
