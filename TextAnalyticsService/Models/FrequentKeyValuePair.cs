using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyticsService.Models
{
    public class FrequentKeyValuePair<TKey, TValue>
    {
        public TKey word { get; set; }
        public TValue frequency { get; set; }
    }
}
