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

        [HttpGet("GetAll")]
        public IEnumerable<iPerf3> GetAll()
        {
            return db.PerfDb.GetAll();
        }
        [HttpGet("GetById/{servername}")]
        public IActionResult GetById(string servername)
        {
            var check = db.PerfDb.GetAll();
            foreach (var a in check)
            {
                if (a.Server == servername)
                {
                    return Ok(a);
                }
            }
            return Ok();
        }
    }
}