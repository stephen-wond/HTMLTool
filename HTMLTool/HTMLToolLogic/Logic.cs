using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace HTMLToolLogic
{
    public class Logic
    {
        private List<string> _nodeList = new List<string>();
        private readonly string _one = ConfigurationManager.AppSettings["Pair 1.1"];
        private readonly string _request1 = ConfigurationManager.AppSettings["Pair 1.2"];
        private readonly string _response1 = ConfigurationManager.AppSettings["Pair 2.1"];
        private readonly string _oneTen = ConfigurationManager.AppSettings["Pair 2.2"];
        private readonly string _two = ConfigurationManager.AppSettings["Pair 3.1"];
        private readonly string _request2 = ConfigurationManager.AppSettings["Pair 3.2"];
        private readonly string _response2 = ConfigurationManager.AppSettings["Pair 4.1"];
        private readonly string _twoTen = ConfigurationManager.AppSettings["Pair 4.2"];

        private List<string> fileList = new List<string>();

        public List<Result> GetResults(string folderLocation)
        {
            //setup
            PopulateFileList(folderLocation);
            var resultsList = new List<Result>();

            //build node list
            _nodeList = ConvertHtmlToList(fileList);

            //find times
            resultsList.Add(GetResult(_one, _request1));
            resultsList.Add(GetResult(_response1, _oneTen));
            resultsList.Add(GetResult(_two, _request2));
            resultsList.Add(GetResult(_response2, _twoTen));

            return resultsList;
        }


        private void PopulateFileList(string folderLocation)
        {
            string[] filePaths = Directory.GetFiles(@folderLocation, "*.html", SearchOption.TopDirectoryOnly);
            fileList = filePaths.ToList();
        }


        private List<string> ConvertHtmlToList(List<string> fileList)
        {
            var htmlList = new List<string>();
            var doc = new HtmlDocument();

            foreach (var file in fileList)
            {
                try
                {
                    doc.Load(file);
                    htmlList.AddRange(from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where !node.InnerText.Contains("/api/serverstatus.aspx") && !node.InnerText.Contains("STATUS=OK") where node.InnerText.Contains(_one) || node.InnerText.Contains(_request1) || node.InnerText.Contains(_response1) || node.InnerText.Contains(_oneTen) || node.InnerText.Contains(_two) || node.InnerText.Contains(_request2) || node.InnerText.Contains(_response2) || node.InnerText.Contains(_twoTen) select node.InnerText.Substring(0, 70));
                }
                catch
                {
                    // ignored
                }
            }

            return htmlList;
        }

        private Result GetResult(string from, string to)
        {
            var result = new Result();
            var userNumber = int.Parse(ConfigurationManager.AppSettings["MessageForward"]);
            result.Max = 0;
            result.Min = 100;

            //check next few messages
            for (var i = 0; i < _nodeList.Count; i++)
            {
                if (_nodeList[i].Contains(from))
                {
                    var k = i + 1;
                    while (k < i + userNumber && k < _nodeList.Count)
                    {
                        if (!_nodeList[k].Contains(to))
                        {
                            k++;
                            continue;
                        }
                        result.Count++;
                        var difference = FindDifference(_nodeList[i].Substring(1, 19),
                            _nodeList[k].Substring(1, 19));

                        result.Average = result.Average + difference;

                        if (difference < result.Min)
                        {
                            result.Min = difference;
                        }
                        if (difference > result.Max)
                        {
                            result.Max = difference;
                        }
                        break;
                    }
                }
            }
            result.Average = Math.Round((result.Average / result.Count),2);
            result.Direction = from.Substring(4, 4) + "->" + to.Substring(4, 4);

            return result;
        }

        private static int FindDifference(string from, string to)
        {
            var difference = 0;
            try
            {
                from = from.Replace("h", ":");
                to = to.Replace("h", ":");
                var parseFrom = DateTime.ParseExact(from, "MMM dd HH:mm:ss.FFF", CultureInfo.CurrentCulture);
                var parseTo = DateTime.ParseExact(to, "MMM dd HH:mm:ss.FFF", CultureInfo.CurrentCulture);
                difference = parseTo.Subtract(parseFrom).Milliseconds;
            }
            catch
            {
                // ignored
            }
            return difference;
        }
    }
}