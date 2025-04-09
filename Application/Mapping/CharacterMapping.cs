using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public static class CharacterMapping
    {
        public static Character ToEntity(this CharacterDto dto) => new Character
        {
            Name = dto.Name,
            Ki = dto.Ki,
            Race = dto.Race,
            Gender = dto.Gender,
            Description = dto.Description,
            Affiliation = dto.Affiliation
        };
    }
}
