using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TextAnalyticsService.Models
{
    public class Analysis
    {
        [Required]
        public string text { get; set; }

        public bool isValid()
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            return true;
        }
        public bool IsInteger()
        {
            bool isInteger = int.TryParse(text, out _);
            return isInteger;
        }

    }
}
