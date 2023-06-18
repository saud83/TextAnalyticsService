using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyticsService.Models
{
    public class LongestKeyValuePair<TKey, TValue>
    {
        [JsonProperty("word")]
        public TKey word { get; set; }

        [JsonProperty("length")]
        public TValue length { get; set; }
    }
}
