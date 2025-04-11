using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CharacterService(IGenericRepository repository) : ICharacterService
    {

        public async Task<IEnumerable<Character?>> GetAllCharactersAsync()
        {
            var allCharacters = await repository.GetAllAsync<Character>();
            return allCharacters.ToList();
        }

        public async Task<IEnumerable<Character?>> GetByAfiliationAsync(string param)
        {
            return await repository.FindByConditionAsync<Character>(c => c.Affiliation == param);
        }

        public async Task<IEnumerable<Character?>> GetByNameAsync(string param)
        {
            return await repository.FindByConditionAsync<Character>(c => c.Name == param);
        }

        public async Task<Character?> GetCharacterByIdAsync(int id)
        {
            var character = await repository.GetByIdAsync<Character>(id);
            if (character != null) return character;
            else return null;
        }
    }
}
