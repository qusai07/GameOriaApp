using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Application.Common.Models
{
    public class LookupDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public LookupDto(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
