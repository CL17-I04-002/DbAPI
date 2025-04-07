using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.ExternalServices
{
    public interface IDbApi
    {
        Task<IEnumerable<Character>> GetAllCharactersAsync(int page, int perPage, string search);
        Task<Character> GetCharacterByIdAsync(int id);
        Task<IEnumerable<Transformation>> GetAllTransformationAsync(int page, int perPage, string search);

    }
}
