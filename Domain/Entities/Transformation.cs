using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transformation
    {
        public int id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Ki { get; set; } = string.Empty;
        public int? CharacterId { get; set; } = 0;
        public Character? Character { get; set; }
    }
}
