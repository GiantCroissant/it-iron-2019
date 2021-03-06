// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using ItIron2019.CustomAssetGraph.CodeGen;
//
//    var monster = Monster.FromJson(jsonString);

namespace ItIron2019.CustomAssetGraph.CodeGen
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Monster
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Hp")]
        public long Hp { get; set; }

        [JsonProperty("Mp")]
        public long Mp { get; set; }

        [JsonProperty("Attack")]
        public long Attack { get; set; }
    }

    public partial class Monster
    {
        public static Monster FromJson(string json) => JsonConvert.DeserializeObject<Monster>(json, ItIron2019.CustomAssetGraph.CodeGen.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Monster self) => JsonConvert.SerializeObject(self, ItIron2019.CustomAssetGraph.CodeGen.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
