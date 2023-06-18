using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TextAnalyticsService.Models;
using Newtonsoft.Json;

namespace TextAnalyticsService.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class TextAnalyzerController : ControllerBase
    {
        [HttpPost]
        [Route("POST/analyze")]
        public IActionResult TextAnalysis([FromBody] Analysis request)
        {
            if (request.isValid())  //chceks whether the text is null or empty if not then perform operations
            {
                // Removing spaces to calculate number of characters
                string withoutSpace = request.text.Replace(" ", "");
                string withoutPunctuation = Regex.Replace(withoutSpace, @"[\p{P}-[.]]+", "");
                int numCharacters = withoutPunctuation.Length;


                // Splitting the text into words
                string[] words = request.text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                int numWords = words.Length;

                // Splitting the text into sentences
                string[] sentences = request.text.Split(new[] { '.', '!', '?' });
                int numSentences = sentences.Length - 1;

                // Calculating the most frequent word and its frequency
                Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
                foreach (string word in words)
                {
                    if (wordFrequency.ContainsKey(word.ToLower()))
                        wordFrequency[word.ToLower()]++;
                    else
                        wordFrequency[word.ToLower()] = 1;
                }
                KeyValuePair<string, int> mostFrequentWord = wordFrequency.OrderByDescending(x => x.Value).FirstOrDefault();
                FrequentKeyValuePair<string, int> mostFrequent = new FrequentKeyValuePair<string, int>
                {
                    word = mostFrequentWord.Key,
                    frequency = mostFrequentWord.Value
                };

                // Finding the longest word and its length
                string longestWord = words.OrderByDescending(x => x.Length).FirstOrDefault();
                int longestWordLength = longestWord.Length;
                LongestKeyValuePair<string, int> longest = new LongestKeyValuePair<string, int>
                {
                    word = longestWord,
                    length = longestWordLength
                };

                // Creating an analysis result object
                var analysisResult = new
                {
                    charCount = numCharacters,
                    wordCount = numWords,
                    sentenceCount = numSentences,
                    mostFrequentWord = mostFrequent,
                    longestWord = longest
                };

                return Ok(analysisResult);
            }
            else if (request.IsInteger())
            {
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Message = "Not Integer, Provide a String",
                    Errors = new List<string> { "The text field is required." }
                };
                
                return BadRequest(errorResponse);
            }
            else
            {
                var errorResponse = new ErrorResponse
                {
                    ErrorCode = 400,
                    Message = "Provide a valid String!",
                    Errors = new List<string> { "The text field is required." }
                };
                
                return BadRequest(errorResponse);

            }

        }

        [HttpPost]
        [Route("POST/similarities")]
        public IActionResult TextSimilarity([FromBody] Similarity request)
        {
            // Converting the input strings to lowercase and splitting into words
            var words1 = request.text1.ToLower().Split();
            var words2 = request.text2.ToLower().Split();

            // Calculating the number of unique words in each string
            int uniqueWords1 = words1.Distinct().Count();
            int uniqueWords2 = words2.Distinct().Count();

            // Calculating the number of unique words in common
            int uniqueWordsInCommon_1 = words1.Intersect(words2).Distinct().Count();
            double percentage_1 = ((double)uniqueWordsInCommon_1 / (double)uniqueWords1) * 100;


            int uniqueWordsInCommon_2 = words2.Intersect(words1).Distinct().Count();
            double percentage_2 = ((double)uniqueWordsInCommon_2 / (double)uniqueWords2) * 100;

            // Calculating the percentage of unique words in common
            double percentage = (percentage_1 + percentage_2) / 2;

            var analysisResult = new 
            { 
                similarity = percentage
            };
            return Ok(analysisResult);
        }

    }
}
