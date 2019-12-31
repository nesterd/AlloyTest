using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlloyTestApp.Application.Utils
{
    public static class ByteConvert
    {
        public static T GetFromByteArray<T>(byte[] bytes)
        {
            var json = Encoding.UTF8.GetString(bytes ?? new byte[0]);
            T obj = JsonConvert.DeserializeObject<T>(json);

            return obj;
        }

        public static byte[] GetByteArray<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            return bytes;
        }
    }
}
