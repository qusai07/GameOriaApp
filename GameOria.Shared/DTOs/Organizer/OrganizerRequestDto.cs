using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Shared.DTOs.Organizer
{
    public class OrganizerRequestDto
    {
        public string StoreName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    }

}
