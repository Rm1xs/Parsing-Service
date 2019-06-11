using BOL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Db
{
    public class AllDb
    {
        public iPerfDb PerfDb { get; set; }
        public RoleDb RoleDb { get; set; }
        public UserDb UserDb { get; set; }
        public AutoParsingDb AutoParsingDb { get; set; }

        public AllDb(Context context)
        {
            PerfDb = new iPerfDb(context);
            RoleDb = new RoleDb(context);
            UserDb = new UserDb(context);
            AutoParsingDb = new AutoParsingDb(context);
        }
    }
}
