using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Shared.DTOs.SigUp
{
    public class RegistrationToken
    {
        public Guid Id { get; set; }
        public string IdentityNumber { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
    }

}
