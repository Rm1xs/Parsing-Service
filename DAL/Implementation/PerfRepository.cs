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
    public class PerfRepository : BaseRepository, IPerfRepository
    {
        public PerfRepository(Context context) : base(context)
        { }
        public void Delete(int id)
        {
            iPerf3 perf = this.GetById(id);
            db.Entry(perf).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public IEnumerable<iPerf3> GetAll()
        {
            return db.iPersfs.ToList();
        }

        public iPerf3 GetById(int id)
        {
            return db.iPersfs.FirstOrDefault(x => x.PerfId == id);
        }

        public void Insert(iPerf3 perf)
        {
            db.Entry(perf).State = EntityState.Added;
            db.SaveChanges();
        }

        public void Update(iPerf3 perf)
        {
            db.Entry(perf).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void CheckDataBase(iPerf3 perf, string name)
        {
            var check = db.iPersfs.Where(c => c.Server == name).FirstOrDefault();
            if(check != null)
            {
                db.Entry(check).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.Entry(perf).State = EntityState.Added;
                db.SaveChanges();
            }
        }
    }
}
