using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using MVVM.Model;
using MVVM.View;

namespace MVVM.ViewModel
{
    public class Logic
    {
        private List<string> _oneOneList = new List<string>();
        private List<string> _oneTwoList = new List<string>();
        private List<string> _twoOneList = new List<string>();
        private List<string> _twoTwoList = new List<string>();
        private List<string> _threeOneList = new List<string>();
        private List<string> _threeTwoList = new List<string>();
        private List<string> _fourOneList = new List<string>();
        private List<string> _fourTwoList = new List<string>();
        private List<string> _fileList = new List<string>();

        public Logic(string folderLoc)
        {
            PopulateFileList(folderLoc);
            ConvertHtmlToList();
        }

        public HTMLToolModel GetResultsBetween(int option)
        {
            var result = new HTMLToolModel();
            switch (option)
            {
                case 1:
                    result = GetResult(_oneOneList, _oneTwoList);
                    break;
                case 2:
                    result = GetResult(_twoOneList, _twoTwoList);
                    break;
                case 3:
                    result = GetResult(_threeOneList, _threeTwoList);
                    break;
                case 4:
                    result = GetResult(_fourOneList, _fourTwoList);
                    break;
            }
            return result;
        }

        private void PopulateFileList(string folderLocation)
        {
            string[] filePaths = Directory.GetFiles(@folderLocation, "*.html", SearchOption.TopDirectoryOnly);
            _fileList = filePaths.ToList();
        }


        private void ConvertHtmlToList()
        {
            var doc = new HtmlDocument();

            foreach (var file in _fileList)
            {
                try
                {
                    doc.Load(file);
                    _oneOneList.AddRange  (from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where node.InnerText.Contains(ConfigurationManager.AppSettings["Pair 1.1"]) select node.InnerText);
                    _oneTwoList.AddRange  (from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where node.InnerText.Contains(ConfigurationManager.AppSettings["Pair 1.2"]) && node.InnerText.Contains("ENQPBP") select node.InnerText);
                    _twoOneList.AddRange  (from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where node.InnerText.Contains(ConfigurationManager.AppSettings["Pair 2.1"]) && node.InnerText.Contains("ENQPBP") select node.InnerText);
                    _twoTwoList.AddRange  (from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where node.InnerText.Contains(ConfigurationManager.AppSettings["Pair 2.2"]) select node.InnerText);
                    _threeOneList.AddRange(from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where node.InnerText.Contains(ConfigurationManager.AppSettings["Pair 3.1"]) select node.InnerText);
                    _threeTwoList.AddRange(from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where node.InnerText.Contains(ConfigurationManager.AppSettings["Pair 3.2"]) && node.InnerText.Contains("VALPBP") select node.InnerText);
                    _fourOneList.AddRange (from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where node.InnerText.Contains(ConfigurationManager.AppSettings["Pair 4.1"]) && node.InnerText.Contains("VALPBP") select node.InnerText);
                    _fourTwoList.AddRange (from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where node.InnerText.Contains(ConfigurationManager.AppSettings["Pair 4.2"]) select node.InnerText);
                }
                catch
                {
                    return;
                }
            }
        }

        private HTMLToolModel GetResult(List<string> from, List<string> to)
        {
            var result = new HTMLToolModel
            {
                Max = 0,
                Min = 1000
            };

            foreach (var x in from)
            {
                var pbpStart = x.IndexOf("PBP", 0);
                var pbpString = x.Substring(pbpStart, 12);

                for (int i = 0; i < to.Count; i++)
               {
                    if (to[i].Contains(pbpString))
                    {
                        var diff = FindDifference(x.Substring(1, 19), to[i].Substring(1, 19));
                        result.Average = result.Average + diff;
                        result.Count++;

                        if (diff < result.Min)
                        {
                            result.Min = diff;
                        }
                        if (diff > result.Max)
                        {
                            result.Max = diff;
                        }
                        to.Remove(to[i]);
                        break;
                    }
                }
            }
            result.Average = Math.Round((result.Average / result.Count), 2);
            result.Direction = nameof(from) + "->" + nameof(to);

            return result;

        }

        private static double FindDifference(string from, string to)
        {
            double difference = 0;
            try
            {
                from = from.Replace("h", ":");
                to = to.Replace("h", ":");
                var parseFrom = DateTime.ParseExact(from, "MMM dd HH:mm:ss.FFF", CultureInfo.CurrentCulture);
                var parseTo = DateTime.ParseExact(to, "MMM dd HH:mm:ss.FFF", CultureInfo.CurrentCulture);
                difference = parseTo.Subtract(parseFrom).TotalMilliseconds;
            }
            catch
            {
                // ignored
            }
            return difference;
        }
    }
}