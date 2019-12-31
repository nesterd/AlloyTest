using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AlloyTestApp.Application.DataAccess.DataProviders
{
    public class FileDataProvider : ICustomDataProvider
    {

        private readonly string _dataDirectory;

        public FileDataProvider(string dataDirectory)
        {
            _dataDirectory = dataDirectory;
        }


        public async Task<byte[]> ReadBytesAsync(string tableName)
        {
            byte[] bytes;
            using (Stream stream = File.Open(Path.Combine(_dataDirectory, $"{tableName}.txt"), FileMode.OpenOrCreate))
            {
                bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
            }

            return bytes;
        }

        public async Task WriteBytesAsync(byte[] bytes, string tableName)
        {
            using (Stream stream = File.Open(Path.Combine(_dataDirectory, $"{tableName}.txt"), FileMode.Truncate))
            {
                await stream.WriteAsync(bytes);
            }
        }
    }
}
