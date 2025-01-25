using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameoria.Application.Common.Models
{
    public class SelectListDto
    {
        public string Value { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public bool Selected { get; set; }
        public bool Disabled { get; set; }
        public string? Group { get; set; }

        public SelectListDto(string value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}
