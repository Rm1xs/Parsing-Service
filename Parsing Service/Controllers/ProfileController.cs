using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOL;
using BOL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Parsing_Service.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        public ProfileController(Context context) : base(context)
        { }
        public IActionResult ViewProfile()
        {
            User user = db.UserDb.GetAll().FirstOrDefault(x => x.Email == User.Identity.Name);
            return View(user);
        }

        public IActionResult AdminPanel()
        {
            var parsedinfo = db.AutoParsingDb.GetAll();
            return View(parsedinfo);
        }
    }
}