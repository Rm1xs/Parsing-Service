using BOL;
using BOL.Models;
using DAL.Abstarct;
using DAL.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Db
{
    public class AutoParsingDb
    {
        private readonly IAutoParsing db;
        public AutoParsingDb(Context context)
        {
            db = new AutoParsingRepository(context);
        }

        public void Insert(AutoParsing pars)
        {
            db.Insert(pars);
        }

        public AutoParsing GetById(int id)
        {
            return db.GetById(id);
        }

        public IEnumerable<AutoParsing> GetAll()
        {
            return db.GetAll();
        }

        public void Delete(int id)
        {
            db.Delete(id);
        }

        public void Update(AutoParsing pars)
        {
            db.Update(pars);
        }

        //public void Check(iPerf3 pars, int id)
        //{
        //    db.CheckForUpdates(pars, id);
        //}
    }
}
