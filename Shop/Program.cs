using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Seller seller = new Seller("dad", 4 );
            Player player = new Player("rbs", 50);
            seller.ShowInfo();
            Console.WriteLine();
            player.ShowInfo();

            seller.Sell(ref player);
            seller.ShowInfo();
            Console.ReadKey();

            
        }
    }

    class Product
    {
        public string Name;
        public int Price { get; private set; }

        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }

    class Character
    {
        protected List<Product> _products = new List<Product>();
        protected string _name;
        protected int _money;

        public Character( string name, int money)
        {
            _name = name;
            _money = money;
        }

        public void ShowInfo()
        {
            foreach (var item in _products)
            {
                Console.WriteLine(item.Name);
            }
        }

        public List<Product> GetProducts()
        {
            return _products;
        }
    }

    class Player : Character
    {
        public Player(string name, int money) : base( name, money)
        {

        }

        public void Add(Product product)
        {
            _products.Add(product);
        }
    }

    class Seller : Character
    {
        public Seller(string name, int money) : base(name, money)
        {
            _products = AddProducts();
        }

        private List<Product> AddProducts()
        {
            _products.Add(new Product("Хлеб", 30));
            _products.Add(new Product("Аптечка", 60));
            _products.Add(new Product("Водка", 40));
            _products.Add(new Product("Винтовка", 1200));
            _products.Add(new Product("Пистолет", 600));
            _products.Add(new Product("Дробовик", 800));

            return _products;
        }

        public void Sell(ref Player player, List<Product> list)
        {   
            
            _products.RemoveAt(0);
        }
    }
}
