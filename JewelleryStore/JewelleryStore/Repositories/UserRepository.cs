using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class UserRepository : IRepository<User>
    {
        StoreDb db = new StoreDb();
        public List<User> GetAll()
        {
            return db.Users.ToList();
        }
        public User Get(int id)
        {
            return db.Users.FirstOrDefault(m => m.UserId == id);
        }
        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Users.Remove(db.Users.Where(o => o.UserId == id).Single());
            db.SaveChanges();
        }
    }
}
