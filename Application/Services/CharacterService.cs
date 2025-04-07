using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CharacterService(IGenericRepository repository) : ICharacterService
    {
        public async Task<Character> CreateCharacterAsync(Character character)
        {
            await repository.AddAsync(character);
            await repository.SaveChangesAsync();
            return character;
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await repository.GetAllAsync<Character>();
        }

        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            return await repository.GetByIdAsync<Character>(id);
        }
    }
}
