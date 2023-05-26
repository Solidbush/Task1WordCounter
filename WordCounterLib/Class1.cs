using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WordCounterLib
{
    public class WordCounter
    {
        private static readonly char[] delimiterChars = { ' ', ',', '.', ':', '!', '?', '-', '\t' };
        private static readonly char newPart = '\n';
        private static readonly string nullString = "";
        private const int CommonValue = 1;
        private const int Incriment = 1;
        private static Dictionary<string, int> wordCountDictionary = new Dictionary<string, int>();

        private Dictionary<string, int> CountWords(string text)
        {
            string[] lines = text.Split(newPart);
            foreach (var line in lines)
            {
                string[] words = line.Split(delimiterChars);
                foreach (string word in words)
                {
                    var wordToLower = word.ToLower().Trim();
                    if (wordToLower == nullString)
                        continue;
                    if (wordCountDictionary.ContainsKey(wordToLower))
                        wordCountDictionary[wordToLower] += Incriment;
                    else
                        wordCountDictionary.Add(wordToLower, CommonValue);
                }
            }

            return wordCountDictionary.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

    }
}

