using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

namespace OfJSONsAndXMLs
{
    public class Program
    {
        static Dictionary<string, int> CountWords(string text)
        {
            string[] words = text.Split(' ', '.', ',', '-', '!', '?', '«', '»', ':', ';');

            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            foreach (string word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    if (wordCount.ContainsKey(word))
                    {
                        wordCount[word]++;
                    }
                    else
                    {
                        wordCount[word] = 1;
                    }
                }
            }

            return wordCount;
        }

        static string ConvertToJson(Dictionary<string, int> wordCount)
        {
            var json = new
            {
                WordCount = wordCount
            };

            return JsonSerializer.Serialize(json, new JsonSerializerOptions { WriteIndented = true });
        }

        static string ConvertToXml(Dictionary<string, int> wordCount)
        {
            XElement xml = new XElement("WordCount");

            foreach (var pair in wordCount)
            {
                xml.Add(new XElement(pair.Key, pair.Value));
            }

            return xml.ToString();
        }

        static void SaveToFile(string content, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(content);
            }
        }
        static void Main(string[] args)
        {
            string text = "Вот дом, Который построил Джек. А это пшеница, Которая в темном чулане хранится В доме, Который построил Джек. А это веселая птица-синица, Которая часто ворует пшеницу, Которая в тёмном чулане хранится В доме, Который построил Джек.";

            Dictionary<string, int> wordCount = CountWords(text);

            string json = ConvertToJson(wordCount);
            SaveToFile(json, "statistics.json");

            string xml = ConvertToXml(wordCount);
            SaveToFile(xml, "statistics.xml");

            Console.WriteLine("Статистика количества слов сохранена.");
        }
    }
}