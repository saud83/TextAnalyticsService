using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyticsService.Models
{
    public class Similarity
    {
        [Required]
        public string text1 { get; set; }

        [Required]
        public string text2 { get; set; }
    }
}
