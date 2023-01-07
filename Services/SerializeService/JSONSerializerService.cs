using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksApp.Services.SerializeService
{
    public class JSONSerializerService : ISerializer
    {
        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string Serialize<T>(T objectValue)
        {
            return JsonConvert.SerializeObject(objectValue);
        }
    }

}
