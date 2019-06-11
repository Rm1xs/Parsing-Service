using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Db;
using BOL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Parsing_Service.Controllers
{
    public class BaseController : Controller
    {
        protected AllDb db;

        public BaseController(Context context)
        {
            db = new AllDb(context);
        }
    }
}