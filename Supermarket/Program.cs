using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket();
            supermarket.Serve();
            Console.ReadKey();
        }
    }

    class Supermarket
    {
       private Queue<Client> _clients = new Queue<Client>();

        public void Serve()
        {
            GetClients();

            int counte = 1;

            while (_clients.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Идет обслуживание {counte++} клиента...");

                _clients.Dequeue().Buy();
            }
        }

        private void GetClients()
        {
            _clients.Enqueue(new Client(3000, new List<Product> {new Product("Молоко", 50), new Product("Колбаса", 350), new Product("Виски", 2550), new Product("Чипсы", 160), new Product("Сок", 100), new Product("Креветки", 450) } ));
            _clients.Enqueue(new Client(5000, new List<Product> {new Product("Мясо", 480), new Product("Сосиски", 210), new Product("Водка", 600), new Product("Вино", 1600), new Product("Сок", 100), new Product("Макароны", 300) } ));
            _clients.Enqueue(new Client(2000, new List<Product> {new Product("Молоко", 50), new Product("Колбаса", 350), new Product("Виски", 2550), new Product("Чипсы", 160), new Product("Лимонад", 95), new Product("Торт", 520) } ));
            _clients.Enqueue(new Client(2900, new List<Product> {new Product("Хлеб", 30), new Product("Колбаса", 350), new Product("Говядина", 1900), new Product("Чипсы", 160), new Product("Шампанское", 1400), new Product("Креветки", 450) } ));
        }
    }

    class Client
    {
        private Random _random = new Random();
        private int _money;
        private List<Product> _products = new List<Product>();

        public Client(int money, List<Product> products)
        {
            _money = money;
            _products = products;
        }

        public void Buy()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            bool succes = false;

            while (succes == false)
            {
                 int purchaseAmount = 0;

                CalculateAmountGoods(ref purchaseAmount);

                if (_money >= purchaseAmount)
                {
                    Console.WriteLine("\tПокупка совершена");

                    _money -= purchaseAmount;

                    Console.WriteLine($"\tОстаток денег на счету - {_money}");

                    succes = true;
                }
                else
                {
                    Console.WriteLine("\tУ вас не хватает денег, идет удаление товаров...");

                    RemoveProduct();
                }
            }
        }

        private void CalculateAmountGoods(ref int purchaseAmount)
        {
            Console.WriteLine("\tТоваров в корзине :");
            foreach (var product in _products)
            {
                purchaseAmount += product.Price;

                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine($"\t\t{product.Name} , Цена - {product.Price}");
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\tСумма ваших покупок составила {purchaseAmount}, Денежных средсв на вашей карте {_money}");
            
        }

        private void RemoveProduct()
        {
            int product = _random.Next(0, _products.Count);

            Console.WriteLine($"{_products[product].Name} удален из корзины.");

            _products.RemoveAt(product);
        }
    }

    class Product
    {
       public string Name { get; private set; }
       public int Price { get; private set; }

        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }
}
