using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOL;
using BOL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParsingService;

namespace Parsing_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : BaseController
    {
        private ParseHelper helper;
        public DataController(Context context) : base(context)
        {
            helper = new ParseHelper(context);
        }

        [HttpGet]
        public IEnumerable<iPerf3> Get()
        {
            return db.PerfDb.GetAll();
        }
    }
}