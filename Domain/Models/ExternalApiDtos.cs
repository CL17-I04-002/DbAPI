using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class CharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Ki { get; set; } = string.Empty;
    public string MaxKi { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Affiliation { get; set; } = string.Empty;
    public string? DeletedAt { get; set; }
}
public class Meta
{
    public int TotalItems { get; set; }
    public int ItemCount { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}
public class Links
{
    public string First { get; set; } = string.Empty;
    public string? Previous { get; set; }
    public string Next { get; set; } = string.Empty;
    public string Last { get; set; } = string.Empty;
}
public class CharacterApiResponse
{
    public List<CharacterDto> Items { get; set; } = new();
    public Meta Meta { get; set; } = new();
    public Links Links { get; set; } = new();

    public CharacterApiResponse()
    {
        
    }
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

public class TransformationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Ki { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string? DeletedAt { get; set; }
}
