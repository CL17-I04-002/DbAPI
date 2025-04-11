using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICharacterService
    {
        Task<IEnumerable<Character?>> GetAllCharactersAsync();
        Task<Character?> GetCharacterByIdAsync(int id);
        Task<IEnumerable<Character?>> GetByNameAsync(string param);
        Task<IEnumerable<Character?>> GetByAfiliationAsync(string param);
    }
}