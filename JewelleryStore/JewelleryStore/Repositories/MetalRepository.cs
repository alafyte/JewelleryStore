using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class MetalRepository : IRepository<Metal>
    {
        StoreDb db = new StoreDb();
        public List<Metal> GetAll()
        {
            return db.Metals.ToList();
        }
        public Metal Get(int id)
        {
            return db.Metals.FirstOrDefault(m => m.MetalId == id);
        }
        public void Add(Metal metal)
        {
            db.Metals.Add(metal);
            db.SaveChanges();
        }
        public void Update(Metal metal)
        {
            db.Entry(metal).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Metals.Remove(db.Metals.Where(d => d.MetalId == id).Single());
            db.SaveChanges();
        }
    }
}
