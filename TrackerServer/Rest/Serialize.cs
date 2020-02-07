using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TrackerServer.Rest
{
    public static class Serialize
    {

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
                {
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
                }
            };
        }
        public static string ToJson(object r) => JsonConvert.SerializeObject(r, Converter.Settings);
    }
}