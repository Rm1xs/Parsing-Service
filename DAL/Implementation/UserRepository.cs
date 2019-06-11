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
    public class UserRepository: BaseRepository, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        { }
        public void Insert(User user)
        {
            db.Entry(user).State = EntityState.Added;
            db.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.Include(role => role.Role).ToList();
        }

        public User GetById(int id)
        {
            return db.Users.FirstOrDefault(x => x.UserId == id);
        }

        public void Delete(int id)
        {
            User user = GetById(id);
            db.Entry(user).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
