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
            //rep.GetHtml2("https://iperf.fr/iperf-servers.php");
        }
        public void AutoPars(string url, string param, int paramserv, int paramip, int paramport, int paramhost)
        {
            rep.AutoParsTable(url, param, paramserv, paramip, paramport, paramhost);
        }
        //public void Edit(string url, string param, int paramserv, int paramip, int paramport, int paramhost)
        //{
        //    rep.AutoParsEdit(url, param, paramserv, paramip, paramport, paramhost);
        //}
    }
}
