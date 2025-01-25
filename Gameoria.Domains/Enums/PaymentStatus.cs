using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Enums
{
    public enum PaymentStatus
    {
        Pending = 0,
        Processing = 1,
        Completed = 2,
        Failed = 3,
        Refunded = 4,
        PartiallyRefunded = 5,
        Cancelled = 6,
        Expired = 7,
        Authorized = 8,
        Captured = 9,
        Disputed = 10
    }
}
