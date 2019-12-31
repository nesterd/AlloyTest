using AlloyTestApp.Application.DataAccess.Customers;
using AlloyTestApp.Application.DataAccess.DataProviders;
using AlloyTestApp.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlloyTestApp.Application.DataAccess.RepositoryFactory
{
    /// <summary>
    /// Фабрика для репозиториев, использующих кастомные поставщики данных
    /// </summary>
    public class CustomRepositoryFactory : IRepositoryFactory
    {
        private readonly ICustomDataProvider _customDataProvider;

        public CustomRepositoryFactory(ICustomDataProvider customDataProvider)
        {
            _customDataProvider = customDataProvider;
        }

        public ICustomersRepository CreateCustomersRepository() => 
            new CustomersRepository(_customDataProvider);
        
    }
}
