using AlloyTestApp.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlloyTestApp.Application.DataAccess.RepositoryFactory
{
    public interface IRepositoryFactory
    {
        ICustomersRepository CreateCustomersRepository();
    }
}
