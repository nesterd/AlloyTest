using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlloyTestApp.Core.Interfaces.DataAccess
{ 
    public interface IBaseRepository<T> where T: Entities.BaseEntity
    {
        Task<IEnumerable<T>> GetListAsync();
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
