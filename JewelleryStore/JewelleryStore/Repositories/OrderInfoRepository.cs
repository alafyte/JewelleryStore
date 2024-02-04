using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class OrderInfoRepository : IRepository<OrderInfo>
    {
        StoreDb db = new StoreDb();
        public List<OrderInfo> GetAll()
        {
            return db.OrderInfos.ToList();
        }
        public OrderInfo Get(int id)
        {
            return db.OrderInfos.FirstOrDefault(m => m.BillId == id);
        }
        public void Add(OrderInfo orderInfo)
        {
            db.OrderInfos.Add(orderInfo);
            db.SaveChanges();
        }
        public void Update(OrderInfo orderInfo)
        {
            db.Entry(orderInfo).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.OrderInfos.Remove(db.OrderInfos.Where(o => o.BillId == id).Single());
            db.SaveChanges();
        }
    }
}
