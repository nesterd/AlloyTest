using AlloyTestApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlloyTestApp.Core.Interfaces.DataAccess
{
    public interface ICustomersRepository : IBaseRepository<Customer>
    {
        Task DeleteByNameAsync(string name);

    }
}
