using AngleSharp.Html.Parser;
using BLL.Db;
using BOL;
using BOL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ParsingService
{
    public class FakeRepository
    {
        private AllDb db;
        internal FakeRepository(Context context)
        {
            db = new AllDb(context);
        }

        internal void GetHtml(string site)
        {
            //Parsing
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString(site);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            List<List<string>> table = doc.DocumentNode.SelectSingleNode("//div[@class='segment-row']")
                        .Descendants("tr")
                        .Skip(1)
                        .Where(tr => tr.Elements("td").Count() > 1)
                        .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                        .ToList();


            foreach (var push in table)
            {
                //Delete trash from speed
                string input = push.ElementAt(3);
                int index = input.LastIndexOf("&");
                if (index > 0)
                {
                    input = input.Substring(0, index);
                }
                var covert = Convert.ToInt32(input);
                //Delete trash from server
                var trash = push.ElementAt(0);
                var mode = trash.Replace("Копировать", "");
                //Add "," to port
                string port = push.ElementAt(1);
                string result = "";
                var alldata = db.PerfDb.GetAll();
                var porttoinbsert = "";
                int count = 0;
                int updatelentharr = 1;
                string[] arr = new string[updatelentharr];
                int start = 0;
                if (port.Length > 5)
                {
                    if (port.Contains("...") == true)
                    {
                        port = port.Replace("...", ",");
                        var len = port.Length;
                        port = port.Insert(4, ",");
                        len = port.Length;
                        for (int i = 0; i < port.Length; i++)
                        {
                            porttoinbsert = port;
                            if (porttoinbsert[i].ToString() == ",")
                            {
                                string text = porttoinbsert.Substring(start, i);
                                result = text;
                                arr[count] = result;
                                updatelentharr += 1;
                                start = i + 1;
                            }
                        }
                        var obj = new iPerf3 { Server = mode, Port = arr, IPVersion = push.ElementAt(4), Speed = covert, Hosting = push.ElementAt(5), DateTime = DateTime.Now, Site = site };
                        db.PerfDb.Check(obj, mode);
                    }
                }
                else
                {
                    int count2 = 0;
                    result = port;
                    arr[count] = result;
                    var obj = new iPerf3 { Server = mode, Port = arr, IPVersion = push.ElementAt(4), Speed = covert, Hosting = push.ElementAt(5), DateTime = DateTime.Now, Site = site };
                    db.PerfDb.Check(obj, mode);
                    count2++;
                }
            }
        }

        internal void AutoParsTable(string url, string param, int paramserv, int paramip, int paramport, int paramhost)
        {
            var obj = new AutoParsing { CustomeURL = url, ParamServer = param, ParamServerId = paramserv, ParamHosting = paramhost, ParamIp = paramip, ParamPort = paramport, DateTime = DateTime.Now };
            db.AutoParsingDb.Insert(obj);
            string result = "";
            var getdata = db.AutoParsingDb.GetAll();
            int updatelentharr = 1;
            int count = 0;
            string[] arr = new string[updatelentharr];
            foreach (var check in getdata)
            {
                WebClient webClient = new WebClient();
                string page = webClient.DownloadString(check.CustomeURL);

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);
                List<List<string>> table = doc.DocumentNode.SelectSingleNode(check.ParamServer)
                    .Descendants("tr")
                            .Skip(1)
                            .Where(tr => tr.Elements("td").Count() > 1)
                            .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                            .ToList();
                foreach (var go in table)
                {
                    result = go.ElementAt(paramport);
                    arr[count] = result;
                    count++;
                    updatelentharr++;
                    var send = new iPerf3 { Server = go.ElementAt(paramserv), Port = arr, IPVersion = go.ElementAt(paramip), Hosting = go.ElementAt(paramhost), DateTime = DateTime.Now, Site = url };
                    db.PerfDb.Check(send, go.ElementAt(0));
                }
            }
        }
        internal void AutoParseLine(string url, string path)
        {
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString(url);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);
            var tdNodes = doc.DocumentNode.SelectNodes(path);
            var obj2 = new AutoParsing { CustomeURL = url, ParamServer = path, DateTime = DateTime.Now };
            db.AutoParsingDb.Insert(obj2);
            int speed = 0;
            int updatelentharr = 1;
            int count = 0;
            string[] arr = new string[updatelentharr];
            for (int i = 0; i < tdNodes.Count; i++)
            {
                string format2result = "";
                string format3result = "";
                string checkformat = "";          
                //server
                string format = tdNodes[i].InnerText;
                format = format.Replace("iperf3", "");
                format = format.Replace("-c", "");
                format = format.Replace("-R", "");
                format = format.Replace("-P", "");
                format = format.Replace("-p", "");
                format = format.Replace("-r", "");
                format = format.Replace("-V", "");
                format = format.Replace("(for IPv4)", "");
                format = format.Replace("(for IPv6)", "");
                format = format.Replace("IPv4", "");
                format = format.Replace("IPv6", "");
                format = format.Replace("4", "");
                format = format.Replace("6", "");
                format = format.Replace("-4", "");
                format = format.Replace("-6", "");
                format = format.Replace("5002", "");
                format = format.Replace("-10", "");
                format = format.Replace("10", "");
                format = format.Replace("wget", "");
                format = format.Replace("-O", "");
                format = format.Replace("(für IPv4)", "");
                format = format.Replace("$", "");
                //ip
                string format2 = tdNodes[i].InnerText;
                if (format2.Contains("IPv4") == true)
                {
                    format2result = "IPv4";
                    format.Replace("IPv4", "");
                }
                else
                {
                    format2result = "IPv6";
                    format.Replace("IPv6", "");
                }
                //port
                List<string> ports = new List<string>();
                string result = "";
                ports.Add("5002");
                ports.Add("4");
                ports.Add("6");
                ports.Add("10");
                ports.Add("5200");
                ports.Add("5209");
                ports.Add("5202");
                ports.Add("5201");
                ports.Add("5203");
                string format3 = tdNodes[i].InnerText;
                foreach (var checkport in ports)
                {
                    if (format3.Contains(checkport) == true)
                    {
                        result = checkport;
                        arr[count] = result;
                        updatelentharr++;
                        checkformat = format.Replace(checkport, "");
                        checkformat = format.Replace("-" + checkport, "");
                        format = format.Trim();
                    }
                }
                string format4result = "unknown";
                if(url == "http://iperf.volia.net/")
                {
                    format4result = "Volia";
                }
                    
                if (url == "http://iperf.volia.net/")
                {
                    speed = 1;
                }

                var obj = new iPerf3 { Server = checkformat, IPVersion = format2result, Port = arr, Site = url, Hosting = format4result, Speed = speed, DateTime = DateTime.Now };
                db.PerfDb.Insert(obj);
            }
        }
    }
}
