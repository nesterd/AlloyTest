using System;
using System.Collections.Generic;
using System.Text;

namespace AlloyTestApp.Application.Extensions.DI
{
    public class RepositoryFactoryOptions
    {
        public string DataStorageType { get; set; }
        public string FileStorageRootPath { get; set; }
    }
}
