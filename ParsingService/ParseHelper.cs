using BOL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ParsingService
{
    public class ParseHelper
    {
        private readonly FakeRepository rep;

        public ParseHelper(Context context)
        {
            rep = new FakeRepository(context);
        }

        public void StartParsingHtml()
        {
            rep.GetHtml("https://iperf.cc/ru/");
        }
        public void AutoParsingLine(string url, string path)
        {
            rep.AutoParseLine(url, path);
        }
        public void AutoPars(string url, string param, int paramserv, int paramip, int paramport, int paramhost)
        {
            rep.AutoParsTable(url, param, paramserv, paramip, paramport, paramhost);
        }
    }
}
