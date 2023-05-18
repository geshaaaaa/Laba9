using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        
      
        string directoryPath = "D:\\C#\\Frequencytext";
        var files = Directory.EnumerateFiles(directoryPath, "*.txt");

        var tokenizer = new Func<string, IEnumerable<string>>(text =>
        {
            return text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        });

        var frequencyCounter = new Func<IEnumerable<string>, IDictionary<string, int>>(tokens =>
        {
            return tokens.GroupBy(token => token)
                         .ToDictionary(group => group.Key, group => group.Count());
        });

        var reportGenerator = new Action<IDictionary<string, int>>(frequencyDictionary =>
        {
            foreach (var pair in frequencyDictionary.OrderBy(pair => pair.Value))
            {
                Console.WriteLine("{0}\t{1}", pair.Key, pair.Value);
            }
        });

        foreach (var file in files)
        {
            string text = File.ReadAllText(file);
            IEnumerable<string> tokens = tokenizer(text);
            IDictionary<string, int> frequencyDictionary = frequencyCounter(tokens);

            Console.WriteLine("Повторювані слова в файлі {0}:", file);
            reportGenerator(frequencyDictionary);
        }
    }
}