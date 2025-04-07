using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public static class CharacterMapping
    {
        public static Character ToCharacter(this Character character) => new Character
        {
            id = character.id,
            Name = character.Name,
            Ki = character.Ki,
            Race = character.Race,
            Gender = character.Gender,
            Description = character.Description,
            Affiliation = character.Affiliation
        };
    }
}
