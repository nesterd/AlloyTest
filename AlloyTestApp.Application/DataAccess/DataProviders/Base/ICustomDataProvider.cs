using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlloyTestApp.Application.DataAccess.DataProviders
{
    public interface ICustomDataProvider
    {
        Task<byte[]> ReadBytesAsync(string tableName);

        Task WriteBytesAsync(byte[] bytes, string tableName);
    }
}
