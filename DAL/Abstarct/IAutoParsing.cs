using BOL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Abstarct
{
    public interface IAutoParsing
    {
        void Insert(AutoParsing role);
        IEnumerable<AutoParsing> GetAll();
        AutoParsing GetById(int id);
        void Delete(int id);
        void Update(AutoParsing role);
        //void CheckForUpdates(iPerf3 perf3, int id);
    }
}
