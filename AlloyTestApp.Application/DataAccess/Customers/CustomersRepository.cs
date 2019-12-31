using AlloyTestApp.Application.DataAccess.DataProviders;
using AlloyTestApp.Application.Exceptions;
using AlloyTestApp.Application.Utils;
using AlloyTestApp.Core.Entities;
using AlloyTestApp.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlloyTestApp.Application.DataAccess.Customers
{
    public class CustomersRepository : ICustomersRepository
    {
        private const string TableName = "customers";

        private readonly ICustomDataProvider _dataProvider;

        public CustomersRepository(ICustomDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        private async Task<IList<Customer>> GetCustomersAsync()
            => ByteConvert.GetFromByteArray<IList<Customer>>(await _dataProvider.ReadBytesAsync(TableName)) ?? new List<Customer>();

        private async Task SaveAsync(IEnumerable<Customer> customers)
        {
            byte[] bytes = ByteConvert.GetByteArray(customers);
            await _dataProvider.WriteBytesAsync(bytes, TableName);
        }

        public async Task AddAsync(Customer entity)
        {
            var customers = await GetCustomersAsync();

            if (customers.Any(x => x.Name == entity.Name))
                throw new UniqueNameException(TableName);

            customers.Add(entity);
            await SaveAsync(customers);
        }

        public async Task DeleteAsync(Customer entity)
        {
            var customers = await GetCustomersAsync();

            if (customers.Remove(entity))
                await SaveAsync(customers);
        }

        public async Task DeleteByNameAsync(string name)
        {
            var customers = await GetCustomersAsync();
            var entity = customers.FirstOrDefault(x => x.Name == name);
            if (entity != null && customers.Remove(entity))
                await SaveAsync(customers);
        }
        
        public async Task<IEnumerable<Customer>> GetListAsync() =>
            (await GetCustomersAsync()).OrderBy(x => x.Name);

    }
}
