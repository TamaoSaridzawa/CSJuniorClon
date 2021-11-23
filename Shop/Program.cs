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
            Player player = new Player("Job", 1400);
            Seller seller = new Seller();
            bool isDeal = true;

            Console.WriteLine("Нажмите :");
            Console.WriteLine($"1 - открытие своего инвентаря\n2 - обзор товара продавца\n3 - покупка товара\n4 - выход из магазина");

            while (isDeal)
            {
                Console.Write("Введите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        player.ShowItems();
                        break;
                    case "2":
                        seller.ShowItems();
                        break;
                    case "3":
                        seller.Sell(ref player);
                        break;
                    case "4":
                        isDeal = false;
                        break;
                    default:
                        Console.WriteLine("Введены некорректные данные");
                        break;
                }
            }
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

    class Player : Inform
    {
        private List<Product> _items = new List<Product>();

        public string Name { get; private set; }
        public int Money { get; private set; }

        public Player(string name, int money) 
        {
            Name = name;
            Money = money;
        }

        public void Pay(int price)
        {
            Money -= price;
        }

        public void PickItem(Product product)
        {
            _items.Add(product);
        }

        public void ShowItems()
        {
            Console.WriteLine($"Денег в кармане - {Money}");

            if (_items.Count <= 0)
            {
                Console.WriteLine("В рюкзаке пусто ^_^");
            }
            else
            {
                Console.WriteLine("Вещи в рюкзаке:");

                foreach (var item in _items)
                {
                    Console.WriteLine($"{item.Name}");
                }
            }   
        }
    }

    class Seller : Inform
    {
        private List<Product> _products = new List<Product>();

        public Seller()
        {
            ExposeProducts();
        }

        public void Sell(ref Player player)
        {
            Console.Write("Какой товар вам продать? ");
            string userInput = Console.ReadLine();

            if (FindProduct(userInput, out Product product))
            {
                if (player.Money >= product.Price)
                {
                    player.Pay(product.Price);

                    player.PickItem(product);

                    Console.WriteLine("Поздравляю с покупкой!");

                    _products.Remove(product);
                }
                else
                {
                    Console.WriteLine("У вас недостаточно средств");
                }
            }
            else
            {
                Console.WriteLine("Такого товара у меня нет!");
            }
        }

        public void ShowItems()
        {
            if (_products.Count > 0)
            {
                Console.WriteLine("Сейчас у продавца есть следуюшие товары.");

                foreach (var item in _products)
                {
                    Console.WriteLine($"{item.Name}, цена - {item.Price}");
                }
            }
            else
            {
                Console.WriteLine("Товара больше нет.");
            }
        }

        private bool FindProduct(string userInput, out Product product)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                if (userInput.ToLower() == _products[i].Name.ToLower())
                {
                    product = _products[i];
                    return true;
                } 
            }
            product = null;
            return false;
        }

        private void ExposeProducts()
        {
            _products.Add(new Product("Хлеб", 30));
            _products.Add(new Product("Аптечка", 60));
            _products.Add(new Product("Водка", 40));
            _products.Add(new Product("Винтовка", 1200));
            _products.Add(new Product("Пистолет", 600));
            _products.Add(new Product("Дробовик", 800));
        }
    }

    interface Inform
    {
       void ShowItems();
    }
}



