using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Character
    {
        public int id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Ki { get; set; } = string.Empty;
        public string? Race { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Affiliation { get; set; } = string.Empty;
    }
}
