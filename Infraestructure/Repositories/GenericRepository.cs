using Domain.Interfaces;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class GenericRepository(AppDbContext context) : IGenericRepository
    {
        public async Task AddAsync<T>(T entity) where T : class
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : class
        {
            return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "id") == id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
