using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class FavoriteRepository : IRepository<Favorite>
    {
        StoreDb db = new StoreDb();
        public List<Favorite> GetAll()
        {
            return db.Favorites.Include(b => b.Products).ToList();
        }
        public Favorite Get(int id)
        {
            return db.Favorites.Include(b => b.Products).FirstOrDefault(b => b.UserId == id);
        }
        public void Add(Favorite favorite)
        {
            db.Favorites.Add(favorite);
            db.SaveChanges();
        }
        public void Update(Favorite favorite)
        {
            db.Entry(favorite).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Favorites.Remove(db.Favorites.Where(d => d.UserId == id).Single());
            db.SaveChanges();
        }
    }
}
