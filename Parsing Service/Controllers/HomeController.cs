using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BLL.Db;
using BOL;
using BOL.Models;
using Microsoft.AspNetCore.Mvc;
using Parsing_Service.Models;
using ParsingService;

namespace Parsing_Service.Controllers
{
    public class HomeController : BaseController
    {
        private ParseHelper helper;
        public HomeController(Context context) : base(context)
        {
            helper = new ParseHelper(context);
            db = new AllDb(context);

            if (!db.RoleDb.GetAll().Any())
            {
                db.RoleDb.FillRoles();
                db.UserDb.InsertAdmin();
            }
        }

        public static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("https://iperf.cc/ru/"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public IActionResult Index()
        {
            if (CheckInternetConnection() == true)
            {
                ViewData["Message"] = "Connected";
                return View();
            }
            else
            {
                ViewData["Message"] = "Internet connection error. Can't parse data.";
                return View();
            }
        }
        public IActionResult Edit(int id)
        {
            if (id != null)
            {
                var data = db.AutoParsingDb.GetById(id);
                if (data != null)
                    return View(data);             
            }
            return NotFound();
        }
        public IActionResult Api()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(AutoParsing autoParsing, int id)
        {
            var data = db.AutoParsingDb.GetById(id);
            data.CustomeURL = autoParsing.CustomeURL;
            data.ParamHosting = autoParsing.ParamHosting;
            data.ParamIp = autoParsing.ParamIp;
            data.ParamPort = autoParsing.ParamPort;
            data.ParamServer = autoParsing.ParamServer;
            data.ParamSpeed = autoParsing.ParamSpeed;
            data.ParamServerId = autoParsing.ParamServerId;
            db.AutoParsingDb.Update(data);
            //helper.Edit(autoParsing.CustomeURL, autoParsing.ParamServer, autoParsing.ParamServerId, autoParsing.ParamIp, autoParsing.ParamPort, autoParsing.ParamHosting);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            if (id != null)
            {
                AutoParsing parsing = db.AutoParsingDb.GetById(id);
                if (parsing != null)
                    return View(parsing);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = db.PerfDb.GetAll();
            AutoParsing parsing = db.AutoParsingDb.GetById(id);    
            foreach(var a in data)
            {
                if(a.Site == parsing.CustomeURL)
                {
                    db.PerfDb.Delete(a.PerfId);
                }
            }
            db.AutoParsingDb.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
