using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WordCounterLib;

namespace FileReader
{
    class Program
    {
        private static FileInfo fileForRead;
        private static FileInfo fileForWrite;

        public static string ReadText(string pathFileForRead, string pathFileForWrite)
        {
            string text = null;
            fileForRead = new FileInfo(pathFileForRead);
            fileForWrite = new FileInfo(pathFileForWrite);
            if (!fileForRead.Exists)
                throw new Exception($"File with path: {fileForRead.FullName} doesn't exists!");
            if (!fileForWrite.Exists)
            {
                var forRead = File.Create(fileForRead.FullName);
                forRead.Close();
            }

            text = File.ReadAllText(fileForRead.FullName, System.Text.Encoding.Default);

            Console.WriteLine($"File from {fileForRead.FullName} read successfully!");

            return text;
        }

        public static void WriteToFile(Dictionary<string, int> wordCountDictionary)
        {
            StreamWriter fileWriter = new StreamWriter(fileForWrite.FullName, false, System.Text.Encoding.Default);
            foreach (var wordCountPair in wordCountDictionary)
            {
                fileWriter.WriteLine($"{wordCountPair.Key} {wordCountPair.Value}");
            }

            fileWriter.Close();
            Console.WriteLine($"Words have been successfully written to file: {fileForWrite.FullName}");
            Console.WriteLine($"Total: {wordCountDictionary.Count()}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Input file's path for read: ");
            string pathForRead = Console.ReadLine();
            Console.WriteLine("Input file's path for write: ");

            string pathForWrite = Console.ReadLine();
            object readedText = ReadText(pathForRead, pathForWrite);
            MethodInfo countWords = typeof(WordCounter).GetMethod("CountWords", BindingFlags.Instance | BindingFlags.NonPublic);
            Dictionary<string, int> commonWordsCount = countWords.Invoke(new WordCounter(), new object[] { readedText }) as Dictionary<string, int>;
            WriteToFile(commonWordsCount);

            Console.ReadKey();
        }
    }
}
