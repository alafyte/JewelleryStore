using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class BasketRepository : IRepository<Basket>
    {
        StoreDb db = new StoreDb();
        public List<Basket> GetAll()
        {
            return db.Baskets.Include(p => p.Products).ToList();
        }
        public Basket Get(int id)
        {
            return db.Baskets.Include(p => p.Products).FirstOrDefault(b => b.UserId == id);
        }
        public void Add(Basket basket)
        {
            db.Baskets.Add(basket);
            db.SaveChanges();
        }
        public void Update(Basket basket)
        {
            db.Entry(basket).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Baskets.Remove(db.Baskets.Where(d => d.UserId == id).Single());
            db.SaveChanges();
        }
    }
}
