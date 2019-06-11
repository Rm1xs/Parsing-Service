using BLL.Db;
using BOL;
using BOL.Models;
using OfficeOpenXml;
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

                var alldata = db.PerfDb.GetAll();
                if (port.Length > 5)
                {
                    var modify = port.Insert(4, ",");
                    var obj = new iPerf3 { Server = mode, Port = modify, IPVersion = push.ElementAt(4), Speed = covert, Hosting = push.ElementAt(5), DateTime = DateTime.Now, Site = site };
                    db.PerfDb.Check(obj, mode);
                }
                else
                {
                    var obj = new iPerf3 { Server = mode, Port = push.ElementAt(1), IPVersion = push.ElementAt(4), Speed = covert, Hosting = push.ElementAt(5), DateTime = DateTime.Now, Site = site };
                    db.PerfDb.Check(obj, mode);
                }
            }
        }

        internal void AutoParsTable(string url, string param, int paramserv, int paramip, int paramport, int paramhost)
        {
            var obj = new AutoParsing { CustomeURL = url, ParamServer = param, ParamServerId = paramserv,  ParamHosting = paramhost, ParamIp = paramip, ParamPort = paramport, DateTime = DateTime.Now };
            db.AutoParsingDb.Insert(obj);

            var getdata = db.AutoParsingDb.GetAll();
            foreach(var check in getdata)
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
                foreach(var go in table)
                {
                    var send = new iPerf3 { Server = go.ElementAt(paramserv), Port = go.ElementAt(paramport), IPVersion = go.ElementAt(paramip), Hosting = go.ElementAt(paramhost), DateTime = DateTime.Now, Site = url };
                    db.PerfDb.Check(send, go.ElementAt(0));
                }
            }           
        }
        //internal void AutoParsEdit(string url, string param, int paramserv, int paramip, int paramport, int paramhost)
        //{
        //    var obj = new AutoParsing { CustomeURL = url, ParamServer = param, ParamHosting = paramhost, ParamIp = paramip, ParamPort = paramport, DateTime = DateTime.Now };
        //    db.AutoParsingDb.Insert(obj);

        //    var getdata = db.AutoParsingDb.GetAll();
        //    foreach (var check in getdata)
        //    {
        //        WebClient webClient = new WebClient();
        //        string page = webClient.DownloadString(check.CustomeURL);

        //        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        //        doc.LoadHtml(page);
        //        List<List<string>> table = doc.DocumentNode.SelectSingleNode(check.ParamServer)
        //            .Descendants("tr")
        //                    .Skip(1)
        //                    .Where(tr => tr.Elements("td").Count() > 1)
        //                    .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
        //                    .ToList();
        //        foreach (var go in table)
        //        {
        //            var send = new iPerf3 { Server = go.ElementAt(check.ParamServerId), Port = go.ElementAt(check.ParamPort), IPVersion = go.ElementAt(check.ParamIp), Hosting = go.ElementAt(check.ParamHosting), DateTime = DateTime.Now, Site = url };
        //            db.AutoParsingDb.Check(send, check.AutoId);
        //        }
        //    }
        //}
    }
}
