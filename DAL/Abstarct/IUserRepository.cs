using BOL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Abstarct
{
    public interface IUserRepository
    {
        void Insert(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Delete(int id);
        void Update(User user);
    }
}
