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
    public class TransfromationService(IGenericRepository repository) : ITransformationService
    {
        public async Task<IEnumerable<Transformation>> GetAllTransformationsAsync()
        {
            return await repository.GetAllAsync<Transformation>();
        }
    }
}
