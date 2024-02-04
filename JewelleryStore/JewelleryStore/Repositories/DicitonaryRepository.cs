using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class DicitonaryRepository : IRepository<Dictionary>
    {
        StoreDb db = new StoreDb();
        public List<Dictionary> GetAll()
        {
            return db.Dictionary.ToList();
        }
        public Dictionary Get(int id)
        {
            return db.Dictionary.FirstOrDefault(d => d.WordId == id);
        }
        public void Add(Dictionary dictionary)
        {
            db.Dictionary.Add(dictionary);
            db.SaveChanges();
        }
        public void Update(Dictionary dictionary)
        {
            db.Entry(dictionary).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Dictionary.Remove(db.Dictionary.Where(d => d.WordId == id).Single());
            db.SaveChanges();
        }
    }
}
