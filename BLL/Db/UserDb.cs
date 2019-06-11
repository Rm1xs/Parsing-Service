using BOL;
using BOL.Models;
using DAL.Abstarct;
using DAL.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Db
{
    public class UserDb
    {
        private readonly IUserRepository db;

        public UserDb(Context context)
        {
            db = new UserRepository(context);
        }

        public void Insert(User user)
        {
            db.Insert(user);
        }

        public User GetById(int id)
        {
            return db.GetById(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.GetAll();
        }

        public void Delete(int id)
        {
            db.Delete(id);
        }

        public void Update(User user)
        {
            db.Update(user);
        }

        public void InsertAdmin()
        {
            db.Insert(new User { Name = "Nikita", Surname ="Kartashov", Email = "admin@admin.com", Password = "admin", RoleId = 1 });
        }
    }
}
