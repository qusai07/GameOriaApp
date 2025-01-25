using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Domains.Enums
{
    public enum ProductType
    {
        Game = 1,
        Card = 2,
        DLC = 3,        // Downloadable Content
        Subscription = 4,
        Bundle = 5,     // Game + DLC Bundle
        Currency = 6    // Virtual Currency
    }
}
