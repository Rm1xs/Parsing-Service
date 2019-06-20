using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BLL.Db;
using BOL;
using Microsoft.AspNetCore.Mvc;
using ParsingService;

namespace Parsing_Service.Controllers
{
    public class EFUpdateController : BaseController
    {
        private ParseHelper helper;
        public EFUpdateController(Context context) : base(context)
        {
            helper = new ParseHelper(context);
            db = new AllDb(context);
            try
            {
                context.Database.EnsureDeleted();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult Update()
        {
            return Redirect("~/Home/Index");
        }
    }
}