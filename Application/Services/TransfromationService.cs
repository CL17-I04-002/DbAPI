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
        public async Task<List<Transformation>> GetAllTransformationAsync()
        {
            var allTransformation = await repository.GetAllAsync<Transformation>();
            return allTransformation.ToList();
        }
    }
}
