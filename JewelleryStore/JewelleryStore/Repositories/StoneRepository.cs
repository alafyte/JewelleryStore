using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class StoneRepository : IRepository<Stone>
    {
        StoreDb db = new StoreDb();
        public List<Stone> GetAll()
        {
            return db.Stones.ToList();
        }
        public Stone Get(int id)
        {
            return db.Stones.FirstOrDefault(m => m.StoneId == id);
        }
        public void Add(Stone stone)
        {
            db.Stones.Add(stone);
            db.SaveChanges();
        }
        public void Update(Stone stone)
        {
            db.Entry(stone).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Stones.Remove(db.Stones.Where(o => o.StoneId == id).Single());
            db.SaveChanges();
        }
    }
}
