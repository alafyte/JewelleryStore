using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore 
{
    public class DatabaseUnit
    {
        private StoreDb db = new StoreDb();
        private BasketRepository basketRepository;
        private FavoriteRepository favoriteRepository;
        private BillRepository billRepository;
        private DicitonaryRepository dicitonaryRepository;
        private MetalRepository metalRepository;
        private OrderInfoRepository orderInfoRepository;
        private ProductsRepository productsRepository;
        private ProductTypeRepository productTypeRepository;
        private StoneRepository stoneRepository;
        private UserRepository userRepository;

        public BillRepository Bills
        {
            get
            {
                if (billRepository == null) 
                { 
                    billRepository = new BillRepository();
                }
                return billRepository;
            }
        }
        public DicitonaryRepository Dictionary
        {
            get
            {
                if (dicitonaryRepository == null)
                {
                    dicitonaryRepository = new DicitonaryRepository();
                }
                return dicitonaryRepository;
            }
        }
        public MetalRepository Metals
        {
            get
            {
                if (metalRepository == null)
                {
                    metalRepository = new MetalRepository();
                }
                return metalRepository;
            }
        }
        public OrderInfoRepository OrderInfos
        {
            get
            {
                if (orderInfoRepository == null)
                {
                    orderInfoRepository = new OrderInfoRepository();
                }
                return orderInfoRepository;
            }
        }
        public ProductsRepository Products
        {
            get
            {
                if (productsRepository == null)
                {
                    productsRepository = new ProductsRepository();
                }
                return productsRepository;
            }
        }
        public ProductTypeRepository ProductTypes
        {
            get
            {
                if (productTypeRepository == null)
                {
                    productTypeRepository = new ProductTypeRepository();
                }
                return productTypeRepository;
            }
        }
        public StoneRepository Stones
        {
            get
            {
                if (stoneRepository == null)
                {
                    stoneRepository = new StoneRepository();
                }
                return stoneRepository;
            }
        }
        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository();
                }
                return userRepository;
            }
        }
        public BasketRepository Baskets
        {
            get
            {
                if (basketRepository == null)
                {
                    basketRepository = new BasketRepository();
                }
                return basketRepository;
            }
        }
        public FavoriteRepository Favorites
        {
            get
            {
                if (favoriteRepository == null)
                {
                    favoriteRepository = new FavoriteRepository();
                }
                return favoriteRepository;
            }
        }
        public void Save()
        { 
            db.SaveChanges(); 
        }

    }
}
