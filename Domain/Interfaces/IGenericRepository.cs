using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepository
    {
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
        Task<T> GetByIdAsync<T>(int id) where T : class;
        Task AddAsync<T>(T entity) where T : class;
        Task<IEnumerable<T>> FindByConditionAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task SaveChangesAsync();
    }
}
