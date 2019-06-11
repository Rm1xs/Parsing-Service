using BOL;
using BOL.Models;
using DAL.Abstarct;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Implementation
{
    public class AutoParsingRepository : BaseRepository, IAutoParsing
    {
        public AutoParsingRepository(Context context) : base(context)
        { }
        public void Insert(AutoParsing parsing)
        {
            db.Entry(parsing).State = EntityState.Added;
            db.SaveChanges();
        }

        public IEnumerable<AutoParsing> GetAll()
        {
            return db.AutoParsings.ToList();
        }

        public AutoParsing GetById(int id)
        {
            return db.AutoParsings.FirstOrDefault(x => x.AutoId == id);
        }

        public void Delete(int id)
        {
            AutoParsing pars = GetById(id);
            db.Entry(pars).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public void Update(AutoParsing parsing)
        {
            db.Entry(parsing).State = EntityState.Modified;
            db.SaveChanges();
        }

        //public void CheckForUpdates(iPerf3 perf3, int id)
        //{
        //    var check = db.iPersfs.Where(c => c.PerfId == id).FirstOrDefault();
        //    if (check.Server != perf3.Server && check.Hosting != perf3.Hosting && check.IPVersion != perf3.IPVersion && check.Port != perf3.Port)
        //    {
        //        db.Entry(check).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //}
    }
}
