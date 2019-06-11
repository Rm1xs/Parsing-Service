using BOL;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Implementation
{
    public class BaseRepository
    {
        protected Context db;

        public BaseRepository(Context context)
        {
            db = context;
        }
    }
}
