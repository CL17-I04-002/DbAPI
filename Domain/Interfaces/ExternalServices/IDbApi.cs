using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ExternalServices
{
    public interface IDbApi
    {
        Task<List<Character>> GetAllCharactersAsync();
        Task<Entities.Character?> GetCharacterByIdAsync(int id);
        Task<IEnumerable<Entities.Transformation>> GetAllTransformationAsync(int page, int perPage, string search);
        Task<List<Character>?> SyncDataFromExternalService();

    }
}
