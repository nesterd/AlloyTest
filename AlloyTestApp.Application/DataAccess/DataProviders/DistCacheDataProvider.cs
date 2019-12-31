using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlloyTestApp.Application.DataAccess.DataProviders
{
    public class DistCacheDataProvider : ICustomDataProvider
    {

        private readonly IDistributedCache _cache;

        public DistCacheDataProvider(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<byte[]> ReadBytesAsync(string tableName) => await _cache.GetAsync(tableName);

        public async Task WriteBytesAsync(byte[] bytes, string tableName) => await _cache.SetAsync(tableName, bytes);
    }
}
