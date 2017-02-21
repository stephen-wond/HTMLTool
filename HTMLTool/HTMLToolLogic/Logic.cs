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
        private List<string> _oneOneList = new List<string>();
        private List<string> _oneTwoList = new List<string>();
        private List<string> _twoOneList = new List<string>();
        private List<string> _twoTwoList = new List<string>();
        private List<string> _threeOneList = new List<string>();
        private List<string> _threeTwoList = new List<string>();
        private List<string> _fourOneList = new List<string>();
        private List<string> _fourTwoList = new List<string>();
        private List<string> _fileList = new List<string>();

        private readonly string _oneOne = ConfigurationManager.AppSettings["Pair 1.1"];
        private readonly string _oneTwo= ConfigurationManager.AppSettings["Pair 1.2"];
        private readonly string _twoOne = ConfigurationManager.AppSettings["Pair 2.1"];
        private readonly string _twoTwo = ConfigurationManager.AppSettings["Pair 2.2"];
        private readonly string _threeOne = ConfigurationManager.AppSettings["Pair 3.1"];
        private readonly string _threeTwo = ConfigurationManager.AppSettings["Pair 3.2"];
        private readonly string _fourOne= ConfigurationManager.AppSettings["Pair 4.1"];
        private readonly string _fourTwo = ConfigurationManager.AppSettings["Pair 4.2"];


        public List<Result> GetResults(string folderLocation)
        {
            //setup
            var resultsList = new List<Result>();
            PopulateFileList(folderLocation);
            
            //build node list
            ConvertHtmlToList(_fileList);

            resultsList.Add(GetResult(_oneOneList, _oneTwoList));
            resultsList.Add(GetResult(_twoOneList, _twoTwoList));
            //resultsList.Add(GetResult(_threeOneList, _threeTwoList));
           // resultsList.Add(GetResult(_fourOneList, _fourTwoList));

            //find times
            //resultsList.Add(GetResult(_one, _request1));
            //resultsList.Add(GetResult(_response1, _oneTen));
            //resultsList.Add(GetResult(_two, _request2));
            //resultsList.Add(GetResult(_response2, _twoTen));

            return resultsList;
        }


        private void PopulateFileList(string folderLocation)
        {
            string[] filePaths = Directory.GetFiles(@folderLocation, "*.html", SearchOption.TopDirectoryOnly);
            _fileList = filePaths.ToList();
        }


        private void ConvertHtmlToList(List<string> fileList)
        {
            var doc = new HtmlDocument();

            foreach (var file in fileList)
            {
                try
                {
                    doc.Load(file);
                    _oneOneList.AddRange  (from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where !node.InnerText.Contains("/api/serverstatus.aspx") && !node.InnerText.Contains("STATUS=OK") where node.InnerText.Contains(_oneOne) select node.InnerText);
                    _oneTwoList.AddRange  (from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where !node.InnerText.Contains("/api/serverstatus.aspx") && !node.InnerText.Contains("STATUS=OK") where node.InnerText.Contains(_oneTwo) && node.InnerText.Contains("ENQPBP") select node.InnerText);
                    _twoOneList.AddRange  (from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where !node.InnerText.Contains("/api/serverstatus.aspx") && !node.InnerText.Contains("STATUS=OK") where node.InnerText.Contains(_twoOne) && node.InnerText.Contains("ENQPBP") select node.InnerText);
                    _twoTwoList.AddRange  (from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where !node.InnerText.Contains("/api/serverstatus.aspx") && !node.InnerText.Contains("STATUS=OK") where node.InnerText.Contains(_twoTwo) select node.InnerText);
                    _threeOneList.AddRange(from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where !node.InnerText.Contains("/api/serverstatus.aspx") && !node.InnerText.Contains("STATUS=OK") where node.InnerText.Contains(_threeOne) select node.InnerText);
                    _threeTwoList.AddRange(from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where !node.InnerText.Contains("/api/serverstatus.aspx") && !node.InnerText.Contains("STATUS=OK") where node.InnerText.Contains(_threeTwo) && node.InnerText.Contains("VALPBP") select node.InnerText);
                    _fourOneList.AddRange (from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where !node.InnerText.Contains("/api/serverstatus.aspx") && !node.InnerText.Contains("STATUS=OK") where node.InnerText.Contains(_fourOne) && node.InnerText.Contains("VALPBP") select node.InnerText);
                    _fourTwoList.AddRange (from node in doc.DocumentNode.SelectNodes("//div[@id='postA']") where !node.InnerText.Contains("/api/serverstatus.aspx") && !node.InnerText.Contains("STATUS=OK") where node.InnerText.Contains(_fourTwo) select node.InnerText);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private Result GetResult(List<string> from, List<string> to)
        {
            var result = new Result
            {
                Max = 0,
                Min = 1000
            };

            foreach (var x in from)
            {
                var pbpStart = x.IndexOf("PBP", 0);
                var pbpString = x.Substring(pbpStart, 12);

                foreach (var y in to)
                {
                    if (y.Contains(pbpString))
                    {
                        var diff = FindDifference(x.Substring(1, 19),y.Substring(1, 19));
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
                        break;
                    }
                }
            }
            result.Average = Math.Round((result.Average / result.Count), 2);
            result.Direction = from.ToString().Substring(4, 4) + "->" + to.ToString().Substring(4, 4);

            return result;

        }

        //private Result GetResult(string from, string to)
        //{
        //    var result = new Result();
        //    var userNumber = int.Parse(ConfigurationManager.AppSettings["MessageForward"]);
        //    result.Max = 0;
        //    result.Min = 100;

        //    //check next few messages
        //    for (var i = 0; i < _nodeList.Count; i++)
        //    {
        //        if (_nodeList[i].Contains(from))
        //        {
        //            var k = i + 1;
        //            while (k < i + 1 + userNumber && k < _nodeList.Count)
        //            {
        //                if (!_nodeList[k].Contains(to))
        //                {
        //                    k++;
        //                    continue;
        //                }
        //                result.Count++;
        //                var difference = FindDifference(_nodeList[i].Substring(1, 19),
        //                    _nodeList[k].Substring(1, 19));

        //                result.Average = result.Average + difference;

        //                if (difference < result.Min)
        //                {
        //                    result.Min = difference;
        //                }
        //                if (difference > result.Max)
        //                {
        //                    result.Max = difference;
        //                }
        //                break;
        //            }
        //        }
        //    }
        //    result.Average = Math.Round((result.Average / result.Count),2);
        //    result.Direction = from.Substring(4, 4) + "->" + to.Substring(4, 4);

        //    return result;
        //}

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