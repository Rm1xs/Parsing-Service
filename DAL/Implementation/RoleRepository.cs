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
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(Context context) : base(context)
        {}
        public void Insert(Role role)
        {
            db.Entry(role).State = EntityState.Added;
            db.SaveChanges();
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles.ToList();
        }

        public Role GetById(int id)
        {
            return db.Roles.FirstOrDefault(x => x.RoleId == id);
        }

        public void Delete(int id)
        {
            Role role = GetById(id);
            db.Entry(role).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public void Update(Role role)
        {
            db.Entry(role).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
