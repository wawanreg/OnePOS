using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace OnePOS.Helpers
{
    public static class JsonHelper
    {
        public static async Task<string> ToJsonAsync(this object entity, JsonConverter converter)
        {
            JsonConverter _converter = converter;
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                MaxDepth = 1,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            if (converter != null)
                setting.Converters = new List<JsonConverter>() { converter };

            return await Task<string>.Factory.StartNew(() => JsonConvert.SerializeObject(entity, Formatting.None, setting));
        }
        public static string ToJson(this object entity, JsonConverter converter)
        {
            JsonConverter _converter = converter;
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                MaxDepth = 1,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            if (converter != null)
                setting.Converters = new List<JsonConverter>() { converter };

            return JsonConvert.SerializeObject(entity, Formatting.None, setting);
        }
    }
}