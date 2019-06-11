using BOL;
using BOL.Models;
using DAL.Abstarct;
using DAL.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Db
{
    public class RoleDb
    {
        private readonly IRoleRepository db;

        public RoleDb(Context context)
        {
            db = new RoleRepository(context);
        }

        public void Insert(Role role)
        {
            db.Insert(role);
        }

        public Role GetById(int id)
        {
            return db.GetById(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return db.GetAll();
        }

        public void Delete(int id)
        {
            db.Delete(id);
        }

        public void Update(Role role)
        {
            db.Update(role);
        }

        public void FillRoles()
        {
            db.Insert(new Role { RoleName = "Administrator" });
            db.Insert(new Role { RoleName = "User" });
        }
    }
}
