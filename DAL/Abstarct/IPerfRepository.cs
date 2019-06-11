using BOL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Abstarct
{
    public interface IPerfRepository
    {
        void Insert(iPerf3 perf);
        void CheckDataBase(iPerf3 perf, string name);
        IEnumerable<iPerf3> GetAll();
        iPerf3 GetById(int id);
        void Delete(int id);
        void Update(iPerf3 perf);
        
    }
}
