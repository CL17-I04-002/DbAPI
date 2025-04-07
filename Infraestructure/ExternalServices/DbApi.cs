using Domain.Entities;
using Domain.Interfaces.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.ExternalServices
{
    public class DbApi(HttpClient httpClient) : IDbApi
    {
        public async Task<IEnumerable<Character>> GetAllCharactersAsync(int page, int perPage, string search)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Character>>
                 ($"characters?page={page}&perPage={perPage}&search={search}");
        }

        public Task<IEnumerable<Transformation>> GetAllTransformationAsync(int page, int perPage, string search)
        {
            throw new NotImplementedException();
        }

        public Task<Character> GetCharacterByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
