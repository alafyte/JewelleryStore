using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class BillRepository : IRepository<Bill>
    {
        StoreDb db = new StoreDb();
        public List<Bill> GetAll()
        {
            db.OrderInfos.Include(x => x.ProductCodeNavigation).ToList();
            return db.Bills.Include(b => b.OrderInfos).ToList();
        }
        public Bill Get(int id)
        {
            return db.Bills.Include(b => b.OrderInfos).FirstOrDefault(b => b.BillId == id);
        }
        public void Add(Bill bill)
        {
            db.Bills.Add(bill);
            db.SaveChanges();
        }
        public void Update(Bill bill)
        {
            db.Entry(bill).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.Bills.Remove(db.Bills.Where(d => d.BillId == id).Single());
            db.SaveChanges();
        }
    }
}
