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

        /// <summary>
        /// Synchronizes character and transformation data from an external API 
        /// and saves it to the local database if no data currently exists.
        /// </summary>
        /// <remarks>
        /// This method first checks if any characters or transformations exist in the database.
        /// If both are empty, it fetches characters and transformations from an external service,
        /// filters them based on specific criteria, and persists them.
        /// </remarks>
        /// <returns>
        /// A status code indicating the result of the synchronization:
        /// 0 - Data already exists, nothing was synced.
        /// 1 - Characters and transformations were successfully synced.
        /// 2 - Only characters were synced (no valid transformations found).
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// Thrown when there is a problem fetching data from the external API.
        /// </exception>
        public async Task<int> SyncDataFromExternalService()
        {
            var characters = await repository.GetAllAsync<Character>();
            var transformations = await repository.GetAllAsync<Domain.Entities.Transformation>();

            if (characters.Any() || transformations.Any()) return 0;

            var apiCharacterResponse = await httpClient.GetFromJsonAsync<CharacterApiResponse>("characters");
            var allCharacters = apiCharacterResponse?.Items.Select(dto => dto.ToEntity()).ToList() ?? new List<Character>();

            foreach (var character in allCharacters.Where(x => x.Race == "Saiyan"))
            {
                await repository.AddAsync(character);
            }

            await repository.SaveChangesAsync();

            var savedCharacters = await repository.GetAllAsync<Character>();

            var apiTransformationResponse = await httpClient.GetFromJsonAsync<List<TransformationDto>>("transformations");

            if (apiTransformationResponse!.Any())
            {
                var zFighterCharacters = savedCharacters
                    .Where(c => c.Affiliation == "Z Fighter")
                    .ToList();

                var filteredTransformations = apiTransformationResponse
                    .Where(t => zFighterCharacters.Any(c => t.Name.Contains(c.Name ?? string.Empty)))
                    .ToList();

                foreach (var transformationDto in filteredTransformations)
                {
                    var transformationEntity = transformationDto.ToEntity();

                    var matchedCharacter = zFighterCharacters
                        .FirstOrDefault(c => transformationDto.Name.Contains(c.Name ?? string.Empty));

                    if (matchedCharacter is not null)
                    {
                        transformationEntity.CharacterId = matchedCharacter.id;
                        await repository.AddAsync(transformationEntity);
                        await repository.SaveChangesAsync();
                    }
                }
            }
            if (savedCharacters.Any() && apiTransformationResponse!.Any()) return 1;
            else return 2;
        }
    }
}
