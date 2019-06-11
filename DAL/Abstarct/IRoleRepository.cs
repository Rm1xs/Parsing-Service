using BOL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Abstarct
{
    public interface IRoleRepository
    {
        void Insert(Role role);
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        void Delete(int id);
        void Update(Role role);
    }
}
