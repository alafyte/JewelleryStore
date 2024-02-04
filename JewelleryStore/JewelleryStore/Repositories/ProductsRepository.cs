using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class ProductsRepository : IRepository<Product>
    {
        StoreDb db = new StoreDb();
        public List<Product> GetAll()
        { 
            return db.Products.ToList(); 
        }
        public Product Get(int id)
        {
            return db.Products.FirstOrDefault(p => p.ProductCode == id);
        }
        public void Add(Product Product)
        {
            db.Products.Add(Product);
            db.SaveChanges();
        }
        public void Update(Product Product)
        {
            db.Entry(Product).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var product = Get(id);
            if (!product.IsActive)
                throw new InvalidOperationException("ProductAlreadyInactive");
            product.IsActive = false;
            db.SaveChanges();
        }
        public Product? Find(int id)
        {
            return db.Products.Find(id);
        }
    }
}
