using BOL;
using BOL.Models;
using DAL.Abstarct;
using DAL.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Db
{
    public class iPerfDb
    {
        private readonly IPerfRepository db;
        public iPerfDb(Context context)
        {
            db = new PerfRepository(context);
        }

        public void Insert(iPerf3 perf)
        {
            db.Insert(perf);
        }

        public iPerf3 GetById(int id)
        {
            return db.GetById(id);
        }

        public IEnumerable<iPerf3> GetAll()
        {
            return db.GetAll();
        }

        public void Delete(int id)
        {
            db.Delete(id);
        }

        public void Update(iPerf3 perf)
        {
            db.Update(perf);
        }

        public void Check(iPerf3 perf, string name)
        {
            db.CheckDataBase(perf, name);
        }
    }
}
