using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParsingService;

namespace Parsing_Service.Controllers
{
    [Authorize]
    public class ParseController : BaseController
    {
        private ParseHelper helper;
        public ParseController(Context context) : base(context)
        {
            helper = new ParseHelper(context);
        }
        public IActionResult Index(string searchString)
        {
            var data = from m in db.PerfDb.GetAll()
                       select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                data = data.Where(s => s.Server.Contains(searchString));
                return View(data);
            }
            else
            {
                helper.StartParsingHtml();
                var parsedinfo = db.PerfDb.GetAll();
                return View(parsedinfo);
            }
        }
        [HttpPost]
        public string GetAtributes(string url, string param, int paramserv, int paramip, int paramport, int paramhost)
        {
            helper.AutoPars(url, param, paramserv, paramip, paramport, paramhost);
            return "Sucsess";
        }
        [HttpPost]
        public IActionResult GetAutoAributes(string url, string path)
        {
            helper.AutoParsingLine(url, path);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            db.PerfDb.Delete(id);
            return RedirectToAction("Index");
        }
    }
}