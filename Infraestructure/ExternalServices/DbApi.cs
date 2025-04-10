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
        /// Synchronizes character and transformation data from an external API into the local database.
        /// </summary>
        /// <returns>
        /// Returns an integer result indicating the outcome:
        /// 0 - Data already exists in the local database; no synchronization was performed.
        /// 1 - Both characters and transformations were successfully synchronized.
        /// 2 - Only partial data was synchronized (either characters or transformations).
        /// </returns>
        /// <remarks>
        /// The method performs the following operations:
        /// - Checks if any characters or transformations already exist in the database.
        /// - If data exists, it returns 0 and does nothing else.
        /// - If the database is empty:
        ///     1. It fetches all characters from the external API and filters only "Saiyan" race characters.
        ///     2. It adds those characters to the database.
        ///     3. Then it fetches all transformations from the external API.
        ///     4. It filters transformations whose name contains any character name that belongs to the "Z Fighter" affiliation.
        ///     5. It saves those transformations to the database.
        /// - Returns 1 if both character and transformation data were inserted, or 2 if only partial data was stored.
        /// </remarks>
        public async Task<int> SyncDataFromExternalService()
        {
            var characters = await repository.GetAllAsync<Character>();
            var transformation = await repository.GetAllAsync<Domain.Entities.Transformation>();
            if (characters.Any() || transformation.Any()) return 0;
            else
            {
                var apiCharacterResponse = await httpClient.GetFromJsonAsync<CharacterApiResponse>("characters");
                var allCharacters = apiCharacterResponse?.Items.Select(dto => dto.ToEntity()).ToList() ?? new List<Character>();
                foreach (var character in allCharacters.Where(x => x.Race == "Saiyan"))
                {
                    await repository.AddAsync(character);
                }
                await repository.SaveChangesAsync();
                var lstCharacter = apiCharacterResponse?.Items.Select(dto => dto.ToEntity()).ToList() ?? new List<Character>();

                var apiTransformationResponse = await httpClient.GetFromJsonAsync<List<TransformationDto>>("transformations");
                if (apiTransformationResponse!.Any())
                {
                    var zFighterNames = allCharacters
                        .Where(c => c.Affiliation == "Z Fighter")
                        .Select(c => c.Name)
                        .ToList();

                    var filteredTransformations = apiTransformationResponse
                        .Where(t => zFighterNames.Any(name => t.Name.Contains(name)))
                        .ToList();

                    foreach (var transformationDto in filteredTransformations)
                    {
                        var transformationEntity = transformationDto.ToEntity();
                        await repository.AddAsync(transformationEntity);
                    }

                    await repository.SaveChangesAsync();
                }
                if (lstCharacter.Any() && apiTransformationResponse!.Any()) return 1;
                else return 2;
            }
        }
    }
}
