using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;
    public class Character
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Ki { get; set; } = string.Empty;
        public string? MaxKi { get; set; } = string.Empty;
        public string? Race { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public string? Affiliation { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; } = null;

    public OriginPlanet? OriginPlanet { get; set; }

    public List<Transformation> Transformations { get; set; } = new();
}
public class OriginPlanet
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsDestroyed { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public class Transformation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Ki { get; set; } = string.Empty;
}