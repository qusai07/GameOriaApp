using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Enums
{
    public enum OrderStatus
    {
        Created = 0,
        Pending = 1,
        PaymentReceived = 2,
        Processing = 3,
        CodeAssigned = 4,
        Completed = 5,
        Cancelled = 6,
        Failed = 7,
        Refunded = 8,
        Disputed = 9
    }
}
