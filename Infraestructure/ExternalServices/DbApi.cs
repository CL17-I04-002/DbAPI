using Domain.Entities;
using Domain.Interfaces.ExternalServices;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Mapping;
using Domain.Interfaces;


namespace Infraestructure.ExternalServices
{
    public class DbApi(HttpClient httpClient, IGenericRepository repository) : IDbApi
    {
        public async Task<List<Character>> GetAllCharactersAsync()
        {
            var allCharacters = await repository.GetAllAsync<Character>();
            return allCharacters.ToList();
        }

        public Task<IEnumerable<Domain.Entities.Transformation>> GetAllTransformationAsync(int page, int perPage, string search)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Entities.Character?> GetCharacterByIdAsync(int id)
        {
            var character = await repository.GetByIdAsync<Character>(id);
            if (character != null) return character;
            else return null;
        }

        public async Task<List<Character>?> SyncDataFromExternalService()
        {
            var characters = await repository.GetAllAsync<Character>();
            var transformation = await repository.GetAllAsync<Domain.Entities.Transformation>();
            if (characters.Any() || transformation.Any()) return null;
            else
            {
                var apiResponse = await httpClient.GetFromJsonAsync<CharacterApiResponse>("characters");
                var allCharacters = apiResponse?.Items.Select(dto => dto.ToEntity()).ToList() ?? new List<Character>();
                foreach (var character in allCharacters.Where(x => x.Race == "Saiyan"))
                {
                    await repository.AddAsync(character);
                    await repository.SaveChangesAsync();
                }
                return apiResponse?.Items.Select(dto => dto.ToEntity()).ToList() ?? new List<Character>();
            }
        }
    }
}
