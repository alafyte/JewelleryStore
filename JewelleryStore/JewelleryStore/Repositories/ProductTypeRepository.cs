using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class ProductTypeRepository : IRepository<ProductType>
    {
        StoreDb db = new StoreDb();
        public List<ProductType> GetAll()
        {
            return db.ProductTypes.ToList();
        }
        public ProductType Get(int id)
        {
            return db.ProductTypes.FirstOrDefault(m => m.ProductTypeId == id);
        }
        public void Add(ProductType productType)
        {
            db.ProductTypes.Add(productType);
            db.SaveChanges();
        }
        public void Update(ProductType productType)
        {
            db.Entry(productType).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.ProductTypes.Remove(db.ProductTypes.Where(o => o.ProductTypeId == id).Single());
            db.SaveChanges();
        }
    }
}
